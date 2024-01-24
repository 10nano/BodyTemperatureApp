using BodyTemperatureApp;

const string progName = "\"Statystyki z pomiarów temperatury ciała\"";
const string appHeader = $"Witamy w programie {progName}\n";
const string pressAnyKey = "\n\nNaciśnij dowolny klawisz";
const string chooseOption = "Wybierz opcję: ";

string patientName = "";
string patientTemp = "";

PatientInMemory patientMemory;
PatientInFile patientFile;

var showMainMenu = true;

while (showMainMenu)
{
    Screen.ClsAppHeader(Screen.myHdFrground, Screen.myHdBkground, appHeader);
    Screen.NewLine();
    Screen.ColorWrite(Screen.mySubHeader, $"Uruchom program, zapisując dane: \n");
    Screen.ColorWrite(Screen.myOption, "1) W pamięci ulotnej komputera\n");
    Screen.ColorWrite(Screen.myOption, "2) W pliku na dysku komputera\n");
    Screen.ColorWrite(Screen.myOption, "E) Wyjdź z programu\n");
    Screen.NewLine();
    Screen.ColorWrite(Screen.mySubHeader, chooseOption);

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

static string PatientName()
{
    Screen.NewLine();
    Screen.ColorWrite(Screen.myInput, "Podaj imię lub nazwisko pacjenta: ");
    var name = Console.ReadLine();
    if (name == "")
    {
        name = "NN";
    }
    return name;
}

static string SubMenu(IPatient patient, bool isInMemory)
{
    string where, option1, option2, option3, option4, patientStr;

    where = isInMemory ? "w pamięci" : "na dysku";
    option1 = "1) Podaj kolejne wartości temperatury ciała,\n";
    option2 = isInMemory ?
        "2) Wyświetl wszystkie wprowadzone wartości temperatury\n" :
        "2) Wyświetl wszystkie wartości temperatury z pliku\n";
    option3 = "3) Wyświetl statystyki z wprowadzonych wartości\n";
    option4 = "E) Wróć do poprzedniego menu\n";
    patientStr = $"Pacjent {patient.Name} \n";

    patient.NoData += PatientNoData;

    string inputTemp = "";
    bool showMenu = true;

    while (showMenu)
    {
        Screen.ClsAppHeader(Screen.myHdFrground, Screen.myHdBkground, 
            $"{progName} - dane zapisywane są {where}\n");
        Screen.NewLine();
        Screen.ColorWrite(Screen.myOption, option1);
        Screen.ColorWrite(Screen.myOption, option2);
        Screen.ColorWrite(Screen.myOption, option3);
        Screen.ColorWrite(Screen.myOption, option4);
        Screen.NewLine();
        Screen.ColorWrite(Screen.myInput, patientStr);
        Screen.ColorWrite(Screen.mySubHeader, chooseOption);

        switch (Console.ReadLine().ToUpper())
        {
            case "1": // Dopisz kolejne wartości temperatury ciała
                Screen.ColorWrite(Screen.myInput, "\n\tZakończ [ENTER]\n");
                while (true)
                {
                    Screen.ColorWrite(Screen.myInput, "Podaj poprawnie zmierzoną temperaturę ciała: ");
                    inputTemp = Console.ReadLine().ToUpper();
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
                        Screen.ColorWrite(Screen.myExcept, $"(EXC) Niepoprawne dane: {exc.Message}\n");
                    }
                }
                break;
            case "2": // Wyświetl wszystkie wartości temperatury
                patient.PrintAllBodyTemps();
                Screen.ColorWrite(Screen.myOption, pressAnyKey);
                Console.ReadKey();
                break;
            case "3": // Pobierz i wyświetl statystyki z wprowadzonych wartości
                var statistics = patient.GetStatistics();
                if (!patient.HasNoData())
                {
                    Screen.NewLine();
                    Screen.ColorWrite(Screen.myStats, $"Minimalna: {statistics.Min} \n");
                    Screen.ColorWrite(Screen.myStats, $"Maksymalna: {statistics.Max}\n");

                    var test = (statistics.Rises, statistics.NotRises);
                    var tempResult = test switch
                    {
                        (true, true) => "# Temeratura stała\n",
                        (true, false) => "# Temperatura ROŚNIE !!!\n",
                        (false, true) => "# Temperatura Nie rośnie\n",
                        (false, false) => "# Temperatura zmienna\n",
                    };
                    Screen.ColorWrite(Screen.myStats, tempResult);
                }
                Screen.ColorWrite(Screen.myOption, pressAnyKey);
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

static void PatientDangerTemp(float temp, object sender, EventArgs args)
{
    Screen.ColorWrite(Screen.myEvent, "\tProszę natychmiast zgłosić się do lekarza\n" +
    $"\tTemperatura {temp:N1} jest niebezpieczna dla życia Pacjenta\n\n");
}

static void PatientFileExist(string fileName, object sender, EventArgs args)
{
    Screen.ColorWrite(Screen.myEvent2, $"\tPomiar dodano do istniejącego pliku: {fileName}\n");
}

static void PatientNoData(object sender, EventArgs args)
{
    Screen.ColorWrite(Screen.myEvent2, $"\n\tBRAK DANYCH: Podaj kolejne wartości temperatur\n");
}
