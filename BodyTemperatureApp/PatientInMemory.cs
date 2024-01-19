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
                if (bodyTemp <= Hipotherm || bodyTemp >= Hipertherm)
                {
                    SnapEventDangerTemp(bodyTemp);
                }

                bodyTempMeasures.Add(bodyTemp);
            }
            else
            {
                ExceptionOutOfScale(bodyTemp);
            }
        }

        public override void PrintAllBodyTemps()
        {
            foreach (var bodyTemp in bodyTempMeasures)
            {
                Screen.ColorWrite(ConsoleColor.Magenta, $"{bodyTemp:N1} ");
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
