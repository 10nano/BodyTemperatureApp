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

        public override bool HasNoData()
        {
            return bodyTempMeasures.Count > 0 ? false : true;
        }

        public override void PrintAllBodyTemps()
        {
            if (HasNoData())
            {
                SnapEventNoData();
                return;
            }
            foreach (var bodyTemp in bodyTempMeasures)
            {
                Screen.ColorWrite(ConsoleColor.Magenta, $"{bodyTemp:N1} ");
            }
        }

        public override Statistics GetStatistics()
        {
            if (HasNoData())
            {
                SnapEventNoData();
            }

            Statistics statistics = new Statistics();
            foreach (var bodyTemp in bodyTempMeasures)
            {
                statistics.AddBodyTemp(bodyTemp);
            }
            return statistics;
        }
    }
}
