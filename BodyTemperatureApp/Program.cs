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

static string PatientName()
{
    Screen.NewLine();
    Screen.ColorWrite(myInput, "Podaj imię lub nazwisko pacjenta: ");
    var name = Console.ReadLine();
    if(name == "")
    {
        name = "NN";
    }
    return name;
}

PatientInMemory patientMemory;
PatientInFile patientFile;

string patientName = "";
string patientTemp = "";

void PatientDangerTemp(float temp, object sender, EventArgs args)
{
    Screen.ColorWrite(myEvent, "Proszę natychmiast zgłosić się do lekarza\n" +
    $"Temperatura {temp:N1} jest niebezpieczna dla życia Pacjenta\n\n");
}

void PatientFileExist(string fileName, object sender, EventArgs args)
{
    Screen.ColorWrite(myEvent2, $"UWAGA: plik {fileName} już istnieje\n" +
        "Pomiar został dodany do istniejącego pliku\n\n");
}

var showMainMenu = true;

while (showMainMenu)
{
    Screen.ClsAppHeader(myHdFrground, myHdBkground, appHeader);
    Screen.NewLine();
    Screen.ColorWrite(mySubHeader, $"Uruchom program, zapisując dane: \n");
    Screen.ColorWrite(myOption, "1) W pamięci ulotnej komputera\n");
    Screen.ColorWrite(myOption, "2) W pliku na dysku komputera\n");
    Screen.ColorWrite(myOption, "E) Wyjdź z programu\n");
    Screen.NewLine();
    Screen.ColorWrite(mySubHeader, chooseOption);

    var inputOption = Console.ReadLine().ToUpper();

    if (inputOption == "1" || inputOption == "2")
    {
        patientName = PatientName();
    }

    switch (inputOption)
    {
        case "1": // Uruchom program w pamięci ulotnej komputera
            patientMemory = new PatientInMemory(patientName);
            patientMemory.DangerTemp += PatientDangerTemp;
            patientTemp = MenuInMemory(patientMemory);
            break;
        case "2": // Uruchom program w pliku na dysku komputera
            patientFile = new PatientInFile(patientName);
            patientFile.DangerTemp += PatientDangerTemp;
            patientFile.FileExist += PatientFileExist;
            patientTemp = MenuInFile(patientFile);
            break;
        case "E": // Wyjdź z programu
            return;
    }
}

static string MenuInMemory(PatientInMemory patient)
{
    bool showMenu = true;
    string inputTemp = "";

    while (showMenu)
    {
        Screen.ClsAppHeader(myHdFrground, myHdBkground, $"{progName} - dane zapisywane w pamięci\n");
        Screen.NewLine();
        Screen.ColorWrite(myOption, "1) Podaj kolejne wartości temperatury ciała,\n");
        Screen.ColorWrite(myOption, "2) Wyświetl wszystkie wprowadzone wartości temperatury\n");
        Screen.ColorWrite(myOption, "3) Wyświetl statystyki z wprowadzonych wartości\n");
        Screen.ColorWrite(myOption, "E) Wróć do poprzedniego menu\n");
        Screen.NewLine();
        Screen.ColorWrite(myInput, $"Pacjent {patient.Name} \n");
        Screen.ColorWrite(mySubHeader, chooseOption);

        switch (Console.ReadLine().ToUpper())
        {
            case "1": // Podaj kolejne wartości temperatury ciała
                Screen.NewLine();
                Screen.ColorWrite(myOption, "ENTER kończy wprowadzanie kolejnych pomiarów\n\n");
                while (true)
                {
                    Screen.ColorWrite(myInput, "Podaj poprawnie zmierzoną temperaturę ciała: ");
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
                        Screen.ColorWrite(myExcept, $"Niepoprawne dane: {exc.Message} \n");
                    }
                }
                break;
            case "2": // Wyświetl wszystkie wprowadzone wartości temperatury
                Screen.NewLine();
                patient.PrintAllBodyTemps();
                Screen.ColorWrite(myOption, pressAnyKey);
                Console.ReadKey();
                break;
            case "3": // Wyświetl statystyki z wprowadzonych wartości
                var statistics = patient.GetStatistics();
                if (statistics.Count == 0)
                {
                    Screen.ColorWrite(myEvent, "Brak pomiarów temperatury");
                    Console.ReadKey();
                    break;
                }

                Screen.NewLine();
                Screen.ColorWrite(myStats, $"Temperatura minimalna: {statistics.Min:N1}\n");
                Screen.ColorWrite(myStats, $"Temperatura maksymalna: {statistics.Max:N1}\n");
                if (statistics.Count > 1)
                {
                    if (statistics.Rises && statistics.NotRises) // both true
                    {
                        Screen.ColorWrite(myStats, " To się nie powinno zdarzyć \n");
                    }
                    else if (statistics.Rises) // ! NotRises
                    {
                        Screen.ColorWrite(myStats, " Temperatura WZRASTA !!!\n");
                    }
                    else if (statistics.NotRises) // ! Rises
                    {
                        Screen.ColorWrite(myStats, " Temperatura nie wzrasta \n");
                    }
                    else // ! Rises && ! NotRises
                    {
                        Screen.ColorWrite(myStats, " Temperatura \"skacze\"\n");
                    }
                }
                else
                {
                    Screen.ColorWrite(myStats, "Zbyt mało pomiarów, aby określić tendencję\n");
                }

                Screen.ColorWrite(myOption, pressAnyKey);
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

static string MenuInFile(PatientInFile patient)
{
    bool showMenu = true;
    string inputTemp = "";

    while (showMenu)
    {
        Screen.ClsAppHeader(myHdFrground, myHdBkground, $"{progName} - dane zapisywane na dysku\n");
        Screen.NewLine();
        Screen.ColorWrite(myOption, "1) Dopisz kolejne wartości temperatury ciała,\n");
        Screen.ColorWrite(myOption, "2) Wyświetl wszystkie wartości temperatury z pliku\n");
        Screen.ColorWrite(myOption, "3) Wyświetl statystyki z wprowadzonych wartości\n");
        Screen.ColorWrite(myOption, "E) Wróć do poprzedniego menu\n");
        Screen.NewLine();
        Screen.ColorWrite(myInput, $"Pacjent {patient.Name}, nazwa pliku: {patient.fileName}");
        Screen.ColorWrite(mySubHeader, chooseOption);

        switch (Console.ReadLine().ToUpper())
        {
            case "1": // Dopisz kolejne wartości temperatury ciała
                //string inputTemp;
                Screen.NewLine();
                while (true)
                {
                    Screen.ColorWrite(myInput, "Dopisz poprawnie zmierzoną temperaturę ciała: ");
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
                        Screen.ColorWrite(myExcept, $" Niepoprawne dane: {exc.Message}\n");
                    }
                }
                break;
            case "2": // Wyświetl wszystkie wartości temperatury z pliku
                Screen.NewLine();
                patient.PrintAllBodyTemps();
                Screen.ColorWrite(myOption, pressAnyKey);
                Console.ReadKey();
                break;
            case "3": // Wyświetl statystyki z wprowadzonych wartości
                var statistics = patient.GetStatistics();
                Screen.NewLine();
                Screen.ColorWrite(myStats, $"Minimalna: {statistics.Min} \n");
                Screen.ColorWrite(myStats, $"Maksymalna: {statistics.Max}\n");
                if (statistics.Rises && statistics.NotRises)
                {
                    Screen.ColorWrite(myStats, "# Temperatura skacze \n");
                }
                else if (statistics.Rises)
                {
                    Screen.ColorWrite(myStats, "# Temperatura ROŚNIE !!!\n");
                }
                else if (statistics.NotRises)
                {
                    Screen.ColorWrite(myStats, "# Temperatura Nie rośnie \n");
                }
                else
                {
                    Screen.ColorWrite(myStats, "# Temperatura zarówno rosła jak i malała \n");
                }
                Screen.ColorWrite(myOption, pressAnyKey);
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
