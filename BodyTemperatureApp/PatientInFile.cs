namespace BodyTemperatureApp
{
    public class PatientInFile : PatientBase
    {
        protected internal string fileName;

        public PatientInFile(string name)
            : base(name)
        {
            fileName = $"{name}_BodyTemp.txt";
        }

        public override void AddBodyTemp(float bodyTemp)
        {
            if (bodyTemp >= MinScaleTemp && bodyTemp <= MaxScaleTemp)
            {
                if (bodyTemp <= Hipotherm || bodyTemp >= Hipertherm)
                {
                    SnapEventDangerTemp(bodyTemp);
                }

                if (File.Exists($"{fileName}"))
                {
                    SnapEventFileExist(fileName);
                }

                using (var writer = File.AppendText(fileName))
                {
                    writer.WriteLine(bodyTemp);
                }
            }
            else
            {
                ExceptionOutOfScale(bodyTemp);
            }
        }

        public override void PrintAllBodyTemps()
        {
            var bodyTempsFromFile = ReadTempsFromFile();
            foreach (var bodyTemp in bodyTempsFromFile)
            {
                Screen.ColorWrite(ConsoleColor.Magenta, $"{bodyTemp:N1} ");
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

