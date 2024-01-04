namespace BodyTemperatureApp
{
    public class Screen : IScreen
    {
        public readonly ConsoleColor myBlue = ConsoleColor.DarkBlue;
        public readonly ConsoleColor myBlack = ConsoleColor.Black;
        public readonly ConsoleColor myWhite = ConsoleColor.White;
        public readonly ConsoleColor myGreen = ConsoleColor.DarkGreen;
        public readonly ConsoleColor myYellow = ConsoleColor.DarkYellow;
        public readonly ConsoleColor myMagenta = ConsoleColor.DarkMagenta;


        public void ColorWrite(ConsoleColor fgColor, ConsoleColor bgColor, string text)
        {
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;
            Console.Write(text);
            Console.ResetColor();
        }
        public void ColorWrite(ConsoleColor fgColor, string text)
        {
            Console.ForegroundColor = fgColor;
            Console.Write(text);
            Console.ResetColor();

        }
        public void ClsAppHeader(ConsoleColor fgColor, ConsoleColor bgColor, string text)
        {
            Console.Clear();
            Console.WriteLine();
            ColorWrite(fgColor, bgColor, text);
            Console.WriteLine();
        }

    }
}
