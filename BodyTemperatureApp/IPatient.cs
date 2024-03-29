﻿using static BodyTemperatureApp.PatientBase;

namespace BodyTemperatureApp
{
    public interface IPatient
    {
        string Name { get; }

        void AddBodyTemp(float bodyTemp);
        void AddBodyTemp(int bodyTemp);
        void AddBodyTemp(string bodyTemp);
        bool HasNoData();
        void PrintAllBodyTemps();
        Statistics GetStatistics();

        event DangerTempDelegate? DangerTemp;
        event NoDataDelegate? NoData;

    }
}
