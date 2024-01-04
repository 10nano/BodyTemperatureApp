namespace BodyTemperatureApp
{
    public class PatientInFile : PatientBase
    {
        private string fileName;
        private string fileNameSufix = "_BodyTemp.txt";
        public PatientInFile(string name)
            : base(name)
        {
            fileName = name + fileNameSufix;
        }
       
        public override void AddBodyTemp(float bodyTemp)
        {
            if (bodyTemp >= MinScaleTemp && bodyTemp <= MaxScaleTemp)
            {
                using (var writer = File.AppendText(fileName))
                {
                    writer.WriteLine(bodyTemp);
                }

            }
            else
            {
                throw new Exception($"Score value: {bodyTemp} is out of range");
            }
        }

        public override Statistics GetStatistics()
        {
            var bodyTempsFromFile = ReadTempsFromFile();
            var result = CountStatistics(bodyTempsFromFile);
            return result;
        }
        private List<float> ReadTempsFromFile()
        {
            var bodyTemps = new List<float>();
            if (File.Exists($"{fileName}"))
            {
                using (var reader = File.OpenText($"{fileName}"))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var number = float.Parse(line);
                        bodyTemps.Add(number);
                        line = reader.ReadLine();
                    }
                }
            }
            return bodyTemps;
        }
        private Statistics CountStatistics(List<float> bodyTemps)
        {
            Statistics statistics = new Statistics();

            foreach (var bodyTemp in bodyTemps)
            {
                statistics.AddBodyTemp(bodyTemp);
            }

            return statistics;
        }

    }
}

