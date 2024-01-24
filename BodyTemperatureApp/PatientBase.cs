namespace BodyTemperatureApp
{
    public abstract class PatientBase : IPatient
    {
        public const float MinScaleTemp = 35f;
        public const float MaxScaleTemp = 42.5f;
        public const float Hipotherm = 35f;
        public const float Hipertherm = 40f;

        public string Name { get; private set; } = string.Empty;

        public PatientBase(string name)
        {
            Name = name;
        }

        public abstract void AddBodyTemp(float bodyTemp);

        public void AddBodyTemp(int bodyTemp)
        {
            this.AddBodyTemp((float)bodyTemp);
        }

        public void AddBodyTemp(string bodyTemp)
        {
            if (float.TryParse(bodyTemp, out float result))
            {
                AddBodyTemp(result);
            }
            else
            {
                throw new Exception($"Podana wartość: \"{bodyTemp}\" nie jest wartością temperatury");
            }
        }

        public abstract bool HasNoData();
        public abstract void PrintAllBodyTemps();
        public abstract Statistics GetStatistics();

        public delegate void DangerTempDelegate(float temp, object sender, EventArgs args);
        public event DangerTempDelegate? DangerTemp;
        public void SnapEventDangerTemp(float temp)
        {
            if (DangerTemp != null)
            {
                DangerTemp(temp, this, new EventArgs());
            }
        }

        public delegate void NoDataDelegate(object sender, EventArgs args);
        public event NoDataDelegate? NoData;
        public void SnapEventNoData()
        {
            if (NoData != null)
            {
                NoData(this, new EventArgs());
            }
        }

        public static void ExceptionOutOfScale(float temp)
        {
            throw new Exception($"Podana wartość temperatury \"{temp:N1}\" jest poza zakresem termometru.");
        }

    }
}
