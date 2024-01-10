namespace BodyTemperatureApp
{
    public class Screen : IScreen
    {

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
        public void NewLine()
        {
            Console.WriteLine();
        }

        public void ClsAppHeader(ConsoleColor fgColor, ConsoleColor bgColor, string text)
        {
            Console.Clear();
            Console.WriteLine();
            ColorWrite(fgColor, bgColor, text);
        }

    }
}
