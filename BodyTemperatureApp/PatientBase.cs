namespace BodyTemperatureApp
{
    public abstract class PatientBase : IPatient
    {
        public readonly float MinScaleTemp = 35f;
        public readonly float MaxScaleTemp = 42.5f;
        public readonly float Hipotherm = 35f;
        public readonly float Hipertherm = 40f;

        public delegate void DangerTempDelegate(object sender, EventArgs args);
        public event DangerTempDelegate DangerTemp;

        public void SnapEventDangerTemp()
        {
            if (DangerTemp != null)
            {
                DangerTemp(this, new EventArgs());
            }
        }

        public delegate void FileExistDelegate(object sender, EventArgs args);
        public event FileExistDelegate FileExist;

        public void SnapEventFileExist()
        {
            if (FileExist != null)
            {
                FileExist(this, new EventArgs());
            }
        }

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
                throw new Exception($"Podana wartość: \"{bodyTemp}\" nie jest wartością temperatury");
            }
        }

        public abstract void PrintAllBodyTemps(Screen screen);

        public abstract Statistics GetStatistics();
    }
}
