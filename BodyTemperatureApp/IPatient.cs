using static BodyTemperatureApp.PatientBase;

namespace BodyTemperatureApp
{
    public interface IPatient
    {
        string Name { get; }
        string FileName { get; }

        void AddBodyTemp(float bodyTemp);
        void AddBodyTemp(int bodyTemp);
        void AddBodyTemp(string bodyTemp);
        void PrintAllBodyTemps();

        event DangerTempDelegate DangerTemp;
        event FileExistDelegate FileExist;

        Statistics GetStatistics();
    }
}
