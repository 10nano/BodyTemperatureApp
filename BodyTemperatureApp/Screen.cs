namespace BodyTemperatureApp
{
    public static class Screen
    {
        public const ConsoleColor mySubHeader = ConsoleColor.DarkBlue;
        public const ConsoleColor myOption = ConsoleColor.DarkYellow;
        public const ConsoleColor myInput = ConsoleColor.DarkGreen;
        public const ConsoleColor myStats = ConsoleColor.Magenta;
        public const ConsoleColor myExcept = ConsoleColor.Red;
        public const ConsoleColor myEvent = ConsoleColor.DarkRed;
        public const ConsoleColor myEvent2 = ConsoleColor.Yellow;
        public const ConsoleColor myHdBkground = ConsoleColor.Yellow;
        public const ConsoleColor myHdFrground = ConsoleColor.Black;

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
