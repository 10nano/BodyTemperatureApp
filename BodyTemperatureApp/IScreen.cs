namespace BodyTemperatureApp
{
    public interface IScreen
    {

        void ColorWrite(ConsoleColor fgColor, ConsoleColor bgColor, string text);
        void ColorWrite(ConsoleColor fgColor, string text);
        void ClsAppHeader(ConsoleColor fgColor, ConsoleColor bgColor, string text);
    }
}
