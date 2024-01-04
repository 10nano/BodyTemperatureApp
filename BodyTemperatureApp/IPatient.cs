namespace BodyTemperatureApp
{
    public interface IPatient
    {
        string Name { get; }

        void AddBodyTemp(float bodyTemp);
        void AddBodyTemp(int bodyTemp);
        void AddBodyTemp(string bodyTemp);

        Statistics GetStatistics();

    }
}
