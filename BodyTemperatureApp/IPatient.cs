using static BodyTemperatureApp.PatientBase;

namespace BodyTemperatureApp
{
    public interface IPatient
    {
        string Name { get; }

        void AddBodyTemp(float bodyTemp);
        void AddBodyTemp(int bodyTemp);
        void AddBodyTemp(string bodyTemp);
        void PrintAllBodyTemps(Screen screen);

        Statistics GetStatistics();
    }
}
