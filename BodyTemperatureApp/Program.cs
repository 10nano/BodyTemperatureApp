using BodyTemperatureApp;


const ConsoleColor myInput2 = ConsoleColor.DarkBlue;
const ConsoleColor myDefBgground = ConsoleColor.Black;
const ConsoleColor myText = ConsoleColor.White;
const ConsoleColor myInput = ConsoleColor.DarkGreen;
const ConsoleColor myHeaderBgground = ConsoleColor.Yellow;
const ConsoleColor myHeaderFrground = ConsoleColor.DarkMagenta;


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


//  "//wzarsta// =utrzymuje się== \\spada\\"

// histereza 0.3

var showMainMenu = true;

while (showMainMenu)
{
    Console.Clear();
    screen.ClsAppHeader(myHeaderFrground, myHeaderBgground, appHeader);
    screen.NewLine();
    screen.ColorWrite(myInput2, $"Uruchom program {progName}, zapisując dane: \n");
    screen.ColorWrite(myText, "1) W pamięci ulotnej komputera\n");
    screen.ColorWrite(myText, "2) W pliku na dysku komputera\n");
    screen.ColorWrite(myText, "E) Wyjdź z programu\n");
    screen.NewLine();
    screen.ColorWrite(myInput2, "\r\nWybierz opcję: ");

    var inputKey = Console.ReadLine().ToUpper();

    if (inputKey == "1" || inputKey == "2")
    {
        inputName = PatientName(screen);
    }

    switch (inputKey)
    {
        case "1":
            patientMemory = new PatientInMemory(inputName);
            MenuInMemory(screen, patientMemory);
            break;
        case "2":
            patientFile = new PatientInFile(inputName);
            MenuInFile(screen, patientFile);
            break;
        case "E":
            return;
    }
}

static void MenuInMemory(Screen screen, PatientInMemory patient)
{
    bool showMenu = true;

    while (showMenu)
    {
        screen.ClsAppHeader(myHeaderFrground, myHeaderBgground, $"Program {progName} - Dane zapisywane w pamięci\n");
        screen.NewLine();
        screen.ColorWrite(myText, "1) Podaj kolejne wartości temperatury ciała,\n");
        screen.ColorWrite(myText, "2) Wyświetl wszystkie wprowadzone wartości temperatury\n");
        screen.ColorWrite(myText, "3) Wyświetl statystyki z wprowadzonych wartości\n");
        screen.ColorWrite(myText, "E) Wróć do poprzedniego menu\n");
        screen.NewLine();
        screen.ColorWrite(myInput,$"Pacjent {patient.Name}");

        screen.ColorWrite(myInput2, "\r\nWybierz opcję: ");

        switch (Console.ReadLine().ToUpper())
        {
            case "1": // Dodaj kolejne wartości temperatury ciała
                      // wyświetl statystyki z wprowadzonych wartości
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
            case "3": // STATISTICS
                var statistics = patient.GetStatistics();
                Console.WriteLine();
                Console.WriteLine($"Średnia ocena: {statistics.Average:N2}");
                Console.WriteLine($"Minimalna: {statistics.Min}");
                Console.WriteLine($"Maksymalna: {statistics.Max}");
                Console.ReadKey();
                break;
            case "E": // EXIT
                showMenu = false;
                // Environment.Exit(0);
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
        screen.ClsAppHeader(myHeaderFrground, myHeaderBgground, $"Program {progName} - Dane zapisywane na dysku\n");
        screen.NewLine();
        screen.ColorWrite(myText, "1) Dopisz kolejne wartości temperatury ciała,\n");
        screen.ColorWrite(myText, "2) Wyświetl wszystkie wartości temperatury z pliku\n");
        screen.ColorWrite(myText, "3) Wyświetl statystyki z wprowadzonych wartości\n");
        screen.ColorWrite(myText, "E) Wróć do poprzedniego menu\n");
        screen.NewLine();
        screen.ColorWrite(myInput, $"Pacjent {patient.Name}");

        screen.ColorWrite(myInput2, "\r\nWybierz opcję: ");

        switch (Console.ReadLine().ToUpper())
        {
            case "1":
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
            case "2":
                patient.PrintAllBodyTemps(screen);
                Console.ReadKey();
                break;
            case "3":
                var statistics = patient.GetStatistics();
                Console.WriteLine();
                Console.WriteLine($"Średnia ocena: {statistics.Average:N2}");
                Console.WriteLine($"Minimalna: {statistics.Min}");
                Console.WriteLine($"Maksymalna: {statistics.Max}");
                Console.ReadKey();
                break;
            case "E":
                showMenu = false;
                // Environment.Exit(0);
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
    // WALIDACJA
    return name;
}




