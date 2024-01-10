namespace BodyTemperatureApp
{
    public class Statistics
    {
        public float Min { get; set; }

        public float Max { get; set; }

        public float PrevTemp { get; set; }

        public bool Rises { get; set; }

        public bool NotRises { get; set; }

        public int Count { get; set; }

        public Statistics()
        {
            Count = 0;
            Rises = true;
            NotRises = true;
            PrevTemp = -1;
            Min = float.MaxValue;
            Max = float.MinValue;
        }

        public void AddBodyTemp(float currTemp)
        {
            Count++;
            Min = Math.Min(Min, currTemp);
            Max = Math.Max(Max, currTemp);

            if (PrevTemp > 0)
            {
                if (PrevTemp < currTemp)
                {
                    NotRises = false; // nie rośnie
                }
                else
                {
                    Rises = false; // rośnie
                }
            }
            PrevTemp = currTemp;
        }
    }
}
