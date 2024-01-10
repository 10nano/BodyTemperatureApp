using BodyTemperatureApp;

const ConsoleColor mySubHeader = ConsoleColor.DarkBlue;
const ConsoleColor myOption = ConsoleColor.DarkYellow;
const ConsoleColor myInput = ConsoleColor.DarkGreen;
const ConsoleColor myHeaderBgground = ConsoleColor.Yellow;
const ConsoleColor myHeaderFrground = ConsoleColor.Black;

const String progName = "\"Statystyki z pomiarów temperatury ciała\"";

const String appHeader = $"Witamy w programie {progName}\n";

const String CorrTempAdult =
    "╔═══════════════════════════════════════╗\n" +
    "║ Temperatura ciała dorosłego człowieka ║\n" +
    "╚═══════════════════════════════════════╝\n";

var screen = new Screen();
String inputName = null;
PatientInMemory patientMemory = null;
PatientInFile patientFile = null;

var showMainMenu = true;

while (showMainMenu)
{
    Console.Clear();
    screen.ClsAppHeader(myHeaderFrground, myHeaderBgground, appHeader);
    screen.NewLine();
    screen.ColorWrite(mySubHeader, $"Uruchom program, zapisując dane: \n");
    screen.ColorWrite(myOption, "1) W pamięci ulotnej komputera\n");
    screen.ColorWrite(myOption, "2) W pliku na dysku komputera\n");
    screen.ColorWrite(myOption, "E) Wyjdź z programu\n");
    screen.NewLine();
    screen.ColorWrite(mySubHeader, "\r\nWybierz opcję: ");

    var inputKey = Console.ReadLine().ToUpper();

    if (inputKey == "1" || inputKey == "2")
    {
        inputName = PatientName(screen);
    }

    switch (inputKey)
    {
        case "1": // Uruchom program w pamięci ulotnej komputera
            patientMemory = new PatientInMemory(inputName);
            MenuInMemory(screen, patientMemory);
            break;
        case "2": // Uruchom program w pliku na dysku komputera
            patientFile = new PatientInFile(inputName);
            MenuInFile(screen, patientFile);
            break;
        case "E": // Wyjdź z programu
            return;
    }
}

static void MenuInMemory(Screen screen, PatientInMemory patient)
{
    bool showMenu = true;

    while (showMenu)
    {
        screen.ClsAppHeader(myHeaderFrground, myHeaderBgground, $"Program {progName} - dane zapisywane w pamięci\n");
        screen.NewLine();
        screen.ColorWrite(myOption, "1) Podaj kolejne wartości temperatury ciała,\n");
        screen.ColorWrite(myOption, "2) Wyświetl wszystkie wprowadzone wartości temperatury\n");
        screen.ColorWrite(myOption, "3) Wyświetl statystyki z wprowadzonych wartości\n");
        screen.ColorWrite(myOption, "E) Wróć do poprzedniego menu\n");
        screen.NewLine();
        screen.ColorWrite(myInput,$"Pacjent {patient.Name}");
        screen.NewLine();
        screen.ColorWrite(mySubHeader, "\r\nWybierz opcję: ");

        switch (Console.ReadLine().ToUpper())
        {
            case "1": // Podaj kolejne wartości temperatury ciała
                String inputString = null;
                screen.NewLine();
                while (true)
                {
                    screen.ColorWrite(myInput, "Dodaj poprawnie zmierzoną temperaturę ciała: ");
                    inputString = Console.ReadLine();
                    if (inputString == "")
                    {
                        break;
                    }
                    try
                    {
                        patient.AddBodyTemp(inputString);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine($" Niepoprawne dane: {exc.Message}");
                    }
                }
                break;
            case "2": // Wyświetl wszystkie wprowadzone wartości temperatury
                patient.PrintAllBodyTemps(screen);
                Console.ReadKey();
                break;
            case "3": // Wyświetl statystyki z wprowadzonych wartości
                var statistics = patient.GetStatistics();
                Console.WriteLine();
                Console.WriteLine($"Minimalna: {statistics.Min}");
                Console.WriteLine($"Maksymalna: {statistics.Max}");
                if (statistics.Count > 1)
                {
                    if (statistics.Rises && statistics.NotRises) // both true
                    {
                        Console.WriteLine(" To się nie powinno zdarzyć ");
                    }
                    else if (statistics.Rises) // ! NotRises
                    {
                        Console.WriteLine(" Temperatura WZRASTA !!!");
                    }
                    else if (statistics.NotRises) // ! Rises
                    {
                        Console.WriteLine(" Temperatura nie wzrasta ");
                    }
                    else // ! Rises && ! NotRises
                    {
                        Console.WriteLine(" Temperatura \"skacze\"");
                    }
                }
                else
                {
                    Console.WriteLine("Zbyt mało pomiarów, aby określić tendencję");
                }

                Console.ReadKey();
                break;
            case "E": // Wróć do poprzedniego menu
                showMenu = false;
                break;
            default:
                showMenu = true;
                break;
        }

    }
}

static void MenuInFile(Screen screen, PatientInFile patient)
{
    bool showMenu = true;

    while (showMenu)
    {
        screen.ClsAppHeader(myHeaderFrground, myHeaderBgground, $"Program {progName} - dane zapisywane na dysku\n");
        screen.NewLine();
        screen.ColorWrite(myOption, "1) Dopisz kolejne wartości temperatury ciała,\n");
        screen.ColorWrite(myOption, "2) Wyświetl wszystkie wartości temperatury z pliku\n");
        screen.ColorWrite(myOption, "3) Wyświetl statystyki z wprowadzonych wartości\n");
        screen.ColorWrite(myOption, "E) Wróć do poprzedniego menu\n");
        screen.NewLine();
        screen.ColorWrite(myInput, $"Pacjent {patient.Name}, nazwa pliku: {patient.fileName}");
        screen.NewLine();
        screen.ColorWrite(mySubHeader, "\r\nWybierz opcję: ");

        switch (Console.ReadLine().ToUpper())
        {
            case "1": // Dopisz kolejne wartości temperatury ciała
                String inputString = null;
                screen.NewLine();
                while (true)
                {
                    screen.ColorWrite(myInput, "Dopisz poprawnie zmierzoną temperaturę ciała: ");
                    inputString = Console.ReadLine();
                    if (inputString == "")
                    {
                        break;
                    }
                    try
                    {
                        patient.AddBodyTemp(inputString);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine($" Niepoprawne dane: {exc.Message}");
                    }
                }
                break;
            case "2": // Wyświetl wszystkie wartości temperatury z pliku
                patient.PrintAllBodyTemps(screen);
                Console.ReadKey();
                break;
            case "3": // Wyświetl statystyki z wprowadzonych wartości
                var statistics = patient.GetStatistics();
                Console.WriteLine();
                Console.WriteLine($"Minimalna: {statistics.Min}");
                Console.WriteLine($"Maksymalna: {statistics.Max}");
                if (statistics.Rises && statistics.NotRises)
                {
                    Console.WriteLine(" Temperatura skacze ");
                }
                else if (statistics.Rises)
                {
                    Console.WriteLine(" Temperatura ROŚNIE !!!");
                }
                else if (statistics.NotRises)
                {
                    Console.WriteLine(" Temperatura Nie rośnie ");
                }
                else
                {
                    Console.WriteLine(" Temperatura zarówno rosła jak i malała");
                }
                Console.ReadKey();
                break;
            case "E": // Wróć do poprzedniego menu
                showMenu = false;
                break;
            default:
                showMenu = true;
                break;
        }
    }
}

static string PatientName(Screen screen)
{
    screen.NewLine();
    screen.ColorWrite(myInput, "Podaj imię lub nazwisko pacjenta: ");
    var name = Console.ReadLine();
    // Walidacja ?
    return name;
}
