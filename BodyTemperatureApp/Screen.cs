namespace BodyTemperatureApp
{
    public static class Screen
    {
        public static ConsoleColor mySubHeader = ConsoleColor.DarkBlue;
        public static ConsoleColor myOption = ConsoleColor.DarkYellow;
        public static ConsoleColor myInput = ConsoleColor.DarkGreen;
        public static ConsoleColor myStats = ConsoleColor.Magenta;
        public static ConsoleColor myExcept = ConsoleColor.Red;
        public static ConsoleColor myEvent = ConsoleColor.DarkRed;
        public static ConsoleColor myEvent2 = ConsoleColor.Yellow;
        public static ConsoleColor myHdBkground = ConsoleColor.Yellow;
        public static ConsoleColor myHdFrground = ConsoleColor.Black;

        public static void ColorWrite(ConsoleColor fgColor, ConsoleColor bgColor, string text)
        {
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void ColorWrite(ConsoleColor fgColor, string text)
        {
            Console.ForegroundColor = fgColor;
            Console.Write(text);
            Console.ResetColor();

        }
        public static void NewLine()
        {
            Console.WriteLine();
        }

        public static void ClsAppHeader(ConsoleColor fgColor, ConsoleColor bgColor, string text)
        {
            Console.Clear();
            Console.WriteLine();
            ColorWrite(fgColor, bgColor, text);
        }

    }
}
