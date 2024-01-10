namespace BodyTemperatureApp
{
    public class PatientInMemory : PatientBase
    {
        private List<float> bodyTempMeasures = new List<float>();

        public PatientInMemory(string name)
            : base(name)
        {  
        }
        public override void AddBodyTemp(float bodyTemp)
        {
            if (bodyTemp >= MinScaleTemp && bodyTemp <= MaxScaleTemp)
            {
                bodyTempMeasures.Add(bodyTemp);
            }
            else
            {
                throw new Exception($" \"{bodyTemp}\" jest poza zakresem termometru.");
            }
        }

        public override void PrintAllBodyTemps(Screen screen)
        {
            foreach (var bodyTemp in bodyTempMeasures)
            {
                screen.ColorWrite(ConsoleColor.Green,$"{bodyTemp} ");
            }  
        }

        public override Statistics GetStatistics()
        {
            Statistics statistics = new Statistics();

            foreach (var bodyTemp in bodyTempMeasures)
            {
                statistics.AddBodyTemp(bodyTemp);
            }
            return statistics;
        }
    }
}
