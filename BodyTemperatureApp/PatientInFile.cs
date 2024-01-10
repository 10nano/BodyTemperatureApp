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
                if (File.Exists($"{fileName}"))
                {
                    throw new Exception($"UWAGA plik: {fileName} istnieje\n" +
                        "Pomiary zostaną dodane do istniejącego pliku");
                    ; // Dopisywanie do istniejącego pliku
                }

                    using (var writer = File.AppendText(fileName))
                    {
                        writer.WriteLine(bodyTemp);
                    }
            }
            else
            {
                throw new Exception($"Podana wartość temperatury: \"{bodyTemp}\" jest poza zakresem termometru");
            }
        }

        public override void PrintAllBodyTemps(Screen screen)
        {
            var bodyTempsFromFile = ReadTempsFromFile();
            foreach (var bodyTemp in bodyTempsFromFile)
            {
                screen.ColorWrite(ConsoleColor.Green, $"{bodyTemp} ");
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

