namespace BodyTemperatureApp
{
    public abstract class PatientBase : IPatient
    {
        public readonly float MinScaleTemp = 35f;
        public readonly float MaxScaleTemp = 42.5f;
        public readonly float Hipotherm = 35f;
        public readonly float Hipertherm = 40f;

        public delegate void DangerTempDelegate(float temp, object sender, EventArgs args);
        public event DangerTempDelegate DangerTemp;

        public void SnapEventDangerTemp(float temp)
        {
            if (DangerTemp != null)
            {
                DangerTemp(temp, this, new EventArgs());
            }
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

        public PatientBase(string name)
        {
            Name = name;
        }

        public PatientBase(string name, string fileName)
        {
            Name = name;
            FileName = fileName;
        }

        public static void ExceptionOutOfScale(float temp)
        {
            throw new Exception($"Podana wartość temperatury \"{temp:N1}\" jest poza zakresem termometru.");
        }

        public string Name { get; private set; } = string.Empty;

        public string FileName { get; private set; } = string.Empty;

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

        public abstract void PrintAllBodyTemps();

        public abstract Statistics GetStatistics();
    }
}
