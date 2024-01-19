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

PatientInMemory patientMemory;
PatientInFile patientFile;

string patientName = "";
string patientTemp = "";

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

    bool isInMemory;
    switch (inputOption)
    {
        case "1": // Uruchom program w pamięci ulotnej komputera
            patientMemory = new PatientInMemory(patientName);
            patientMemory.DangerTemp += PatientDangerTemp;
            isInMemory = true;
            patientTemp = SubMenu(patientMemory, isInMemory);
            break;
        case "2": // Uruchom program w pliku na dysku komputera
            patientFile = new PatientInFile(patientName);
            patientFile.DangerTemp += PatientDangerTemp;
            patientFile.FileExist += PatientFileExist;
            isInMemory = false;
            patientTemp = SubMenu(patientFile, isInMemory);
            break;
        case "E": // Wyjdź z programu
            return;
    }
}

static string SubMenu(IPatient patient, bool isInMemory)
{
    string where, option1, option2, option3, option4, patientStr;

    if (isInMemory)
    {
        where = "w pamięci";
        option1 = "1) Podaj kolejne wartości temperatury ciała,\n";
        option2 = "2) Wyświetl wszystkie wprowadzone wartości temperatury\n";
        option3 = "3) Wyświetl statystyki z wprowadzonych wartości\n";
        option4 = "E) Wróć do poprzedniego menu\n";
        patientStr = $"Pacjent {patient.Name} \n";
    }
    else // isInFile
    {
        where = "na dysku";
        option1 = "1) Dopisz kolejne wartości temperatury ciała,\n";
        option2 = "2) Wyświetl wszystkie wartości temperatury z pliku\n";
        option3 = "3) Wyświetl statystyki z wprowadzonych wartości\n";
        option4 = "E) Wróć do poprzedniego menu\n";
        patientStr = $"Pacjent {patient.Name}, nazwa pliku: {patient.FileName}";
    }
    

    bool showMenu = true;
    string inputTemp = "";

    while (showMenu)
    {
        Screen.ClsAppHeader(myHdFrground, myHdBkground, $"{progName} - dane zapisywane są {where}\n");
        Screen.NewLine();
        Screen.ColorWrite(myOption, option1);
        Screen.ColorWrite(myOption, option2);
        Screen.ColorWrite(myOption, option3);
        Screen.ColorWrite(myOption, option4);
        Screen.NewLine();
        Screen.ColorWrite(myInput, patientStr);
        Screen.ColorWrite(mySubHeader, chooseOption);
        switch (Console.ReadLine().ToUpper())
        {
            case "1": // Dopisz kolejne wartości temperatury ciała
                Screen.NewLine();
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

static string PatientName()
{
    Screen.NewLine();
    Screen.ColorWrite(myInput, "Podaj imię lub nazwisko pacjenta: ");
    var name = Console.ReadLine();
    if (name == "")
    {
        name = "NN";
    }
    return name;
}

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
