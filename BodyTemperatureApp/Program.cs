using BodyTemperatureApp;

const ConsoleColor mySubHeader = ConsoleColor.DarkBlue;
const ConsoleColor myOption = ConsoleColor.DarkYellow;
const ConsoleColor myInput = ConsoleColor.DarkGreen;
const ConsoleColor myStats = ConsoleColor.Magenta;
const ConsoleColor myExcept = ConsoleColor.Red;
const ConsoleColor myEvent = ConsoleColor.DarkRed;
const ConsoleColor myHdBkground = ConsoleColor.Yellow;
const ConsoleColor myHdFrground = ConsoleColor.Black;

const string progName = "\"Statystyki z pomiarów temperatury ciała\"";
const string appHeader = $"Witamy w programie {progName}\n";

const string CorrTempAdult =
    "╔═══════════════════════════════════════╗\n" +
    "║ Temperatura ciała dorosłego człowieka ║\n" +
    "╚═══════════════════════════════════════╝\n";

var screen = new Screen();

PatientInMemory patientMemory;
PatientInFile patientFile;

{
    screen.ColorWrite(myEvent, "\nProszę natychmiast zgłosić się do lekarza\n" +
                                "Podana temperatura jest niebezpieczna dla życia Pacjenta");
}

string inputName = "";

var showMainMenu = true;

while (showMainMenu)
{
    Console.Clear();
    screen.ClsAppHeader(myHdFrground, myHdBkground, appHeader);
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
        screen.ClsAppHeader(myHdFrground, myHdBkground, $"Program {progName} - dane zapisywane w pamięci\n");
        screen.NewLine();
        screen.ColorWrite(myOption, "1) Podaj kolejne wartości temperatury ciała,\n");
        screen.ColorWrite(myOption, "2) Wyświetl wszystkie wprowadzone wartości temperatury\n");
        screen.ColorWrite(myOption, "3) Wyświetl statystyki z wprowadzonych wartości\n");
        screen.ColorWrite(myOption, "E) Wróć do poprzedniego menu\n");
        screen.NewLine();
        screen.ColorWrite(myInput, $"Pacjent {patient.Name} \n");

        screen.ColorWrite(mySubHeader, "\r\nWybierz opcję: ");

        switch (Console.ReadLine().ToUpper())
        {
            case "1": // Podaj kolejne wartości temperatury ciała
                string inputString;
                screen.NewLine();
                screen.ColorWrite(myOption, "Podawanie kolejnych wartości kończy ENTER\n\n");
                while (true)
                {
                    screen.ColorWrite(myInput, "Podaj poprawnie zmierzoną temperaturę ciała: ");
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
                        screen.ColorWrite(myExcept, $"Niepoprawne dane: {exc.Message} \n");
                    }
                }
                break;
            case "2": // Wyświetl wszystkie wprowadzone wartości temperatury
                screen.NewLine();
                patient.PrintAllBodyTemps(screen);
                screen.ColorWrite(myOption, "\n\nNaciśnij dowolny klawisz");
                Console.ReadKey();
                break;
            case "3": // Wyświetl statystyki z wprowadzonych wartości
                var statistics = patient.GetStatistics();
                if (statistics.Count == 0)
                {
                    screen.ColorWrite(myEvent,"Brak pomiarów temperatury");
                    Console.ReadKey();
                    break;
                }

                    screen.NewLine();
                    screen.ColorWrite(myStats, $"Temperatura minimalna: {statistics.Min}\n");
                    screen.ColorWrite(myStats, $"Temperatura maksymalna: {statistics.Max}\n");
                    if (statistics.Count > 1)
                    {
                        if (statistics.Rises && statistics.NotRises) // both true
                        {
                            screen.ColorWrite(myStats, " To się nie powinno zdarzyć \n");
                        }
                        else if (statistics.Rises) // ! NotRises
                        {
                            screen.ColorWrite(myStats, " Temperatura WZRASTA !!!\n");
                        }
                        else if (statistics.NotRises) // ! Rises
                        {
                            screen.ColorWrite(myStats, " Temperatura nie wzrasta \n");
                        }
                        else // ! Rises && ! NotRises
                        {
                            screen.ColorWrite(myStats, " Temperatura \"skacze\"\n");
                        }
                    }
                    else
                    {
                        screen.ColorWrite(myStats, "Zbyt mało pomiarów, aby określić tendencję\n");
                    }
                
                screen.ColorWrite(myOption, "\n\nNaciśnij dowolny klawisz");
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
        screen.ClsAppHeader(myHdFrground, myHdBkground, $"Program {progName} - dane zapisywane na dysku\n");
        screen.NewLine();
        screen.ColorWrite(myOption, "1) Dopisz kolejne wartości temperatury ciała,\n");
        screen.ColorWrite(myOption, "2) Wyświetl wszystkie wartości temperatury z pliku\n");
        screen.ColorWrite(myOption, "3) Wyświetl statystyki z wprowadzonych wartości\n");
        screen.ColorWrite(myOption, "E) Wróć do poprzedniego menu\n");
        screen.NewLine();
        screen.ColorWrite(myInput, $"Pacjent {patient.Name}, nazwa pliku: {patient.fileName}");

        screen.ColorWrite(mySubHeader, "\r\nWybierz opcję: ");

        switch (Console.ReadLine().ToUpper())
        {
            case "1": // Dopisz kolejne wartości temperatury ciała
                string inputString;
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
                        screen.ColorWrite(myExcept, $" Niepoprawne dane: {exc.Message}\n");
                    }
                }
                break;
            case "2": // Wyświetl wszystkie wartości temperatury z pliku
                screen.NewLine();
                patient.PrintAllBodyTemps(screen);
                screen.ColorWrite(myOption, "\n\nNaciśnij dowolny klawisz");
                Console.ReadKey();
                break;
            case "3": // Wyświetl statystyki z wprowadzonych wartości
                var statistics = patient.GetStatistics();
                screen.NewLine();
                screen.ColorWrite(myStats, $"Minimalna: {statistics.Min} \n");
                screen.ColorWrite(myStats, $"Maksymalna: {statistics.Max}\n");
                if (statistics.Rises && statistics.NotRises)
                {
                    screen.ColorWrite(myStats, "# Temperatura skacze \n");
                }
                else if (statistics.Rises)
                {
                    screen.ColorWrite(myStats, "# Temperatura ROŚNIE !!!\n");
                }
                else if (statistics.NotRises)
                {
                    screen.ColorWrite(myStats, "# Temperatura Nie rośnie \n");
                }
                else
                {
                    screen.ColorWrite(myStats, "# Temperatura zarówno rosła jak i malała \n");
                }
                screen.ColorWrite(myOption, "\n\nNaciśnij dowolny klawisz");
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