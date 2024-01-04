using BodyTemperatureApp;

const string appHeader =
    "Witamy w programie \"Statystyki z pomiarów temperatury ciała\"\n";

const string CorrTempAdult =
    "╔═══════════════════════════════════════╗\n" +
    "║ Temperatura ciała dorosłego człowieka ║\n" +
    "╚═══════════════════════════════════════╝\n";

var screen = new Screen();
screen.ClsAppHeader(screen.myBlue, screen.myYellow, appHeader);
screen.ColorWrite(screen.myMagenta, CorrTempAdult);

var patientMemory = new PatientInMemory("Anna Maria Jopek");
var patientFile = new PatientInFile("Jan Krzysztof Bielecki");

while (true)
{
    screen.ColorWrite(screen.myWhite, "Podaj poprawnie zmierzoną temperaturę ciała: ");
    var input = Console.ReadLine();
    if (input == "")
    {
        break;
    }
    try
    {
        patientMemory.AddBodyTemp(input);
    }
    catch (Exception exc)
    {
        Console.WriteLine($" Niepoprawne dane: {exc.Message}");
    }
}

patientFile.AddBodyTemp(37f);
patientFile.AddBodyTemp(38f);
patientFile.AddBodyTemp(39f);
patientFile.AddBodyTemp(39f);
patientFile.AddBodyTemp(39f);

var statisticsMemory = patientMemory.GetStatistics(); 
var statisticsFile = patientFile.GetStatistics();

Console.WriteLine();
Console.WriteLine($"Pacjent mem {patientMemory.Name}");
Console.WriteLine($"Średnia ocena: {statisticsMemory.Average:N2}");
Console.WriteLine($"Minimalna: {statisticsMemory.Min}");
Console.WriteLine($"Maksymalna: {statisticsMemory.Max}");

Console.WriteLine();
Console.WriteLine($"Pacjent file {patientFile.Name}");
Console.WriteLine($"Średnia ocena: {statisticsFile.Average:N2}");
Console.WriteLine($"Minimalna: {statisticsFile.Min}");
Console.WriteLine($"Maksymalna: {statisticsFile.Max}");

//  "//rośnie// ==stoi== \\spada\\"

// histereza 0.3
