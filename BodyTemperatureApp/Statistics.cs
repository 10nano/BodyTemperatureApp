namespace BodyTemperatureApp
{
    public class Statistics
    {
        public float Min { get; set; }

        public float Max { get; set; }

        public float Sum { get; set; }

        public int Count { get; set; }

        public float Average
        {
            get
            {
                return Sum / Count;
            }
        }

        //  "//wzarsta// =utrzymuje się== \\spada\\"

        // histereza 0.3


        public Statistics()
        {
            Count = 0;
            Sum = 0;
            Min = float.MaxValue;
            Max = float.MinValue;
        }

        public void AddBodyTemp(float bodyTemp)
        {
            Count++;
            Sum += bodyTemp;
            Min = Math.Min(this.Min, bodyTemp);
            Max = Math.Max(this.Max, bodyTemp);
        }

    }
}
