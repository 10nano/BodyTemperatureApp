namespace BodyTemperatureApp
{
    public class PatientInFile : PatientBase
    {

        public string FileName { get; private set; }

        public PatientInFile(string name)
            : base(name)
        {
            FileName = $"{name}_BodyTemp.txt";
        }

        public delegate void FileExistDelegate(string fileName, object sender, EventArgs args);
        public event FileExistDelegate FileExist;

        public void SnapEventFileExist(string fileName)
        {
            if (FileExist != null)
            {
                FileExist(fileName, this, new EventArgs());
            }
        }


        public override void AddBodyTemp(float bodyTemp)
        {
            if (bodyTemp >= MinScaleTemp && bodyTemp <= MaxScaleTemp)
            {
                if (bodyTemp <= Hipotherm || bodyTemp >= Hipertherm)
                {
                    SnapEventDangerTemp(bodyTemp);
                }
                if (File.Exists($"{FileName}"))
                {
                    SnapEventFileExist(FileName);
                }

                using (var writer = File.AppendText(FileName))
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
            if (File.Exists($"{FileName}"))
            {
                using (var reader = File.OpenText($"{FileName}"))
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

