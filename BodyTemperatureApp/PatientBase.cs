namespace BodyTemperatureApp
{
    public abstract class PatientBase : IPatient
    {
        public readonly float MinScaleTemp = 35f;
        public readonly float MaxScaleTemp = 42.5f;

        public PatientBase(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

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
                throw new Exception($" \"{bodyTemp}\" nie jest wartością temperatury");
            }
        }

        public abstract void PrintAllBodyTemps(Screen screen);

        public abstract Statistics GetStatistics();
    }
}
