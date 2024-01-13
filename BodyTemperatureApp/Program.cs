using BodyTemperatureApp;

const ConsoleColor mySubHeader = ConsoleColor.DarkBlue;
const ConsoleColor myOption = ConsoleColor.DarkYellow;
const ConsoleColor myInput = ConsoleColor.DarkGreen;
const ConsoleColor myStats = ConsoleColor.Magenta;
const ConsoleColor myExcept = ConsoleColor.Red;
const ConsoleColor myEvent = ConsoleColor.DarkRed;
const ConsoleColor myEvent2 = ConsoleColor.Yellow;
const ConsoleColor myHdBkground = ConsoleColor.Yellow;
const ConsoleColor myHdFrground = ConsoleColor.Black;

const string progName = "\"Statystyki z pomiarów temperatury ciała\"";
const string appHeader = $"Witamy w programie {progName}\n";

const string pressAnyKey = "\n\nNaciśnij dowolny klawisz";
const string chooseOption = "Wybierz opcję: ";

static string PatientName(Screen screen)
{
    screen.NewLine();
    screen.ColorWrite(myInput, "Podaj imię lub nazwisko pacjenta: ");
    var name = Console.ReadLine();
    if(name == "")
    {
        name = "NN";
    }
    return name;
}

Screen screen = new Screen();

PatientInMemory patientMemory;
PatientInFile patientFile;

string patientName = "";
string patientTemp = "";

void PatientDangerTemp(object sender, EventArgs args)
{
    screen.ColorWrite(myEvent, "Proszę natychmiast zgłosić się do lekarza\n" +
    $"Podana temperatura jest niebezpieczna dla życia Pacjenta\n\n");
    //$"Temperatura: {bodyTemp} jest niebezpieczna dla życia Pacjenta\n\n");
}

void PatientFileExist(object sender, EventArgs args)
{
    //screen.ColorWrite(myEvent, $"UWAGA plik: {fileName} istnieje\n" +
    screen.ColorWrite(myEvent2, $"UWAGA powyższy plik już istnieje\n" +
        "Pomiar został dodany do istniejącego pliku\n");
}

var showMainMenu = true;

while (showMainMenu)
{
    screen.ClsAppHeader(myHdFrground, myHdBkground, appHeader);
    screen.NewLine();
    screen.ColorWrite(mySubHeader, $"Uruchom program, zapisując dane: \n");
    screen.ColorWrite(myOption, "1) W pamięci ulotnej komputera\n");
    screen.ColorWrite(myOption, "2) W pliku na dysku komputera\n");
    screen.ColorWrite(myOption, "E) Wyjdź z programu\n");
    screen.NewLine();
    screen.ColorWrite(mySubHeader, chooseOption);

    var inputOption = Console.ReadLine().ToUpper();

    if (inputOption == "1" || inputOption == "2")
    {
        patientName = PatientName(screen);
    }
 
    switch (inputOption)
    {
        case "1": // Uruchom program w pamięci ulotnej komputera
            patientMemory = new PatientInMemory(patientName);
            patientMemory.DangerTemp += PatientDangerTemp;
            patientTemp = MenuInMemory(screen, patientMemory);
            break;
        case "2": // Uruchom program w pliku na dysku komputera
            patientFile = new PatientInFile(patientName);
            patientFile.DangerTemp += PatientDangerTemp;
            patientFile.FileExist += PatientFileExist;
            patientTemp = MenuInFile(screen, patientFile);
            break;
        case "E": // Wyjdź z programu
            return;
    }
}

static string MenuInMemory(Screen screen, PatientInMemory patient)
{
    bool showMenu = true;
    string inputTemp = "";

    while (showMenu)
    {
        screen.ClsAppHeader(myHdFrground, myHdBkground, $"{progName} - dane zapisywane w pamięci\n");
        screen.NewLine();
        screen.ColorWrite(myOption, "1) Podaj kolejne wartości temperatury ciała,\n");
        screen.ColorWrite(myOption, "2) Wyświetl wszystkie wprowadzone wartości temperatury\n");
        screen.ColorWrite(myOption, "3) Wyświetl statystyki z wprowadzonych wartości\n");
        screen.ColorWrite(myOption, "E) Wróć do poprzedniego menu\n");
        screen.NewLine();
        screen.ColorWrite(myInput, $"Pacjent {patient.Name} \n");
        screen.ColorWrite(mySubHeader, chooseOption);

        switch (Console.ReadLine().ToUpper())
        {
            case "1": // Podaj kolejne wartości temperatury ciała
                screen.NewLine();
                screen.ColorWrite(myOption, "ENTER kończy wprowadzanie kolejnych pomiarów\n\n");
                while (true)
                {
                    screen.ColorWrite(myInput, "Podaj poprawnie zmierzoną temperaturę ciała: ");
                    inputTemp = Console.ReadLine();
                    if (inputTemp == "")
                    {
                        break;
                    }
                    try
                    {
                        patient.AddBodyTemp(inputTemp);
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
                screen.ColorWrite(myOption, pressAnyKey);
                Console.ReadKey();
                break;
            case "3": // Wyświetl statystyki z wprowadzonych wartości
                var statistics = patient.GetStatistics();
                if (statistics.Count == 0)
                {
                    screen.ColorWrite(myEvent, "Brak pomiarów temperatury");
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

                screen.ColorWrite(myOption, pressAnyKey);
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
    return inputTemp;
}

static string MenuInFile(Screen screen, PatientInFile patient)
{
    bool showMenu = true;
    string inputTemp = "";

    while (showMenu)
    {
        screen.ClsAppHeader(myHdFrground, myHdBkground, $"{progName} - dane zapisywane na dysku\n");
        screen.NewLine();
        screen.ColorWrite(myOption, "1) Dopisz kolejne wartości temperatury ciała,\n");
        screen.ColorWrite(myOption, "2) Wyświetl wszystkie wartości temperatury z pliku\n");
        screen.ColorWrite(myOption, "3) Wyświetl statystyki z wprowadzonych wartości\n");
        screen.ColorWrite(myOption, "E) Wróć do poprzedniego menu\n");
        screen.NewLine();
        screen.ColorWrite(myInput, $"Pacjent {patient.Name}, nazwa pliku: {patient.fileName}");
        screen.ColorWrite(mySubHeader, chooseOption);

        switch (Console.ReadLine().ToUpper())
        {
            case "1": // Dopisz kolejne wartości temperatury ciała
                //string inputTemp;
                screen.NewLine();
                while (true)
                {
                    screen.ColorWrite(myInput, "Dopisz poprawnie zmierzoną temperaturę ciała: ");
                    inputTemp = Console.ReadLine();
                    if (inputTemp == "")
                    {
                        break;
                    }
                    try
                    {
                        patient.AddBodyTemp(inputTemp);
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
                screen.ColorWrite(myOption, pressAnyKey);
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
                screen.ColorWrite(myOption, pressAnyKey);
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
    return inputTemp;
}