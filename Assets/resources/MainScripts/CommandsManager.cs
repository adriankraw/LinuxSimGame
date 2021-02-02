using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CommandsManager : MonoBehaviour
{

    private static string[] directry = { "/", "    - bin" };
    private static string[] manual = { "Das Spiel functioniert so:" };
    private static string[] update = { "System Updating... ", "Nah..just joking. This System is already perfekt." };
    private static string[] error = { "", "not found. Try Again. Or check the Manuel" };

    public static GameObject _currentDirectory;

    public static string[] BefehleErkennen(string _befehl, string[] _option)
    {
        Points.usedCommands++;
        string[] _eingabe = new string[] { "", "" };
        switch (_befehl)
        {
            case "":
                break;
            case "sudo":
                _eingabe[0] = "Ist ein zu mächtiges Tool. Dieser Befehl gibt dir die Macht, alles zu ändern.";
                break;
            case "top":
                _eingabe[0] = "So viele Eingaben hast du bislang gebraucht: " + Points.usedCommands;
                _eingabe[0] += @"\n";
                _eingabe[0] += "So viele Fehler hast du bislang gemacht: " + Points.mistakesMade;
                break;
            case "cd":
                cdMethode(_option[0]);
                _eingabe[1] = StoryCommander.StoryTelling(_currentDirectory.name);
                break;
            case "apt-get":
                _eingabe = aptGetMethode(_option[0], _eingabe);
                break;
            case "man":
                _eingabe = ManMehode(_option[0], _eingabe);
                break;
            case "dir":
            case "ls":
                _eingabe = LsMethode(_option[0], _eingabe);
                break;
            case "player":
                _eingabe = playerMethode(_option, _eingabe);
                break;
            case "whoami":
                _eingabe[0] = "student: " + PlayerChar._name;
                break;
            case string dummy when _befehl.StartsWith("./"):
                string[] file = dummy.Split('/');
                dummy = file[file.Length - 1];
                PlaceFile _placefile;
                for (int i = 0; i < _currentDirectory.transform.childCount; i++)
                {
                    Transform _file = _currentDirectory.transform.GetChild(i);
                    _placefile = _file?.GetComponent<PlaceFile>();
                    if (_file.name == dummy)
                    {
                        if (dummy == "enemy")
                        {
                            _eingabe[0] = "Du scheinst " + _placefile?.GetPlace() + "x Monster beschworen zu haben.";
                        }
                        else
                        {

                            switch (_option[0])
                            {
                                case "--attack":
                                    try
                                    {
                                        GetMonster monster = _file?.GetComponent<GetMonster>();
                                        monster.Verteidigen(PlayerChar.Angreifen());
                                        if (monster.dead)
                                        {
                                            PlayerChar._exp += monster.Die();
                                            monster.Drop(_currentDirectory.transform);
                                            Destroy(monster.gameObject);
                                            _eingabe[0] = "Gegner wurde besiegt! Glückwunsch";
                                        }
                                        else
                                        {
                                            PlayerChar.Verteidigen(monster.Angriff());
                                            _eingabe[0] = _file.name + ": " + _file?.GetComponent<GetMonster>().hp + " hp";
                                            _eingabe[0] += PlayerChar._name + "'s hp: " + PlayerChar._hp + "/" + PlayerChar._maxhp;
                                        }
                                    }
                                    catch
                                    {
                                        _eingabe[0] = "Das Item anzugreifen macht nicht wirklich Sinn oder ?";
                                    }
                                    break;
                                case "--read":
                                    try
                                    {
                                        _eingabe[0] = _file?.GetComponent<Briefe>().brief1;
                                        _eingabe[1] = _file?.GetComponent<Briefe>().brief2;
                                    }
                                    catch
                                    {
                                        _eingabe[0] = "Versuchst du gerade " + _file.name + " zu LESEN?";
                                    }
                                    break;
                            }
                        }
                    }
                }
                break;
            default:
                error[0] = _befehl;
                _eingabe = error;
                Points.mistakesMade++;
                break;
        }
        return _eingabe;
    }

    private static string[] playerMethode(string[] _option, string[] _eingabe)
    {
        switch (_option[0])
        {
            case "--name":
            case "-n":
                if (_option.Length == 2 && _option[1] != "")
                {
                    PlayerChar._name = _option[1];
                }
                _eingabe[0] = "Player name: " + PlayerChar._name;
                break;
            case "--hp":
                _eingabe[0] = "Player hp: " + PlayerChar._hp;
                break;
            case "--atk":
                _eingabe[0] = "Player Atk: " + PlayerChar._atk;
                break;
            case "--lvl":
                _eingabe[0] = "Player Lvl: " + PlayerChar._lvl;
                break;
            case "--exp":
                _eingabe[0] = "Player Exp: " + PlayerChar._exp;
                break;
            case "":
            case "--stats":
                _eingabe[0] = "Your stats are: " + @"\n" + @"\n" +
                              "Name: " + PlayerChar._name + @"\n" +
                              "Hp: " + PlayerChar._hp + "/" + PlayerChar._maxhp + @"\n" +
                              "Atk: " + PlayerChar._atk + @"\n" +
                              "Lvl: " + PlayerChar._lvl + @"\n" +
                              "Exp: " + PlayerChar._exp + "/100";

                break;
            default:
                _eingabe[0] = "player doesn't support " + _option[0] + " in this way. Consider checking the manuel for more information.";
                break;
        }
        return _eingabe;
    }

    private static string[] LsMethode(string _option, string[] _eingabe)
    {
        if (_option == "")
        {
            ReadFileSystem(_currentDirectory);

            foreach (string x in directry)
            {
                _eingabe[0] = _eingabe[0] + x + "  ";
            }
        }
        if (_option == "-la")
        {
            _eingabe = ReturnReadFileSystem(true);
        }

        return _eingabe;
    }

    private static string[] ManMehode(string _option, string[] _eingabe)
    {
        switch (_option)
        {
            case "game":
                _eingabe[0] = "Das Spiel Beginnt: \nKannst du dir den Namen deines Spielers anzeigen lassen ?\n";
                _eingabe[0] += "\nTipps:\n";
                _eingabe[0] += "'man player' zeigt dir alle Funktionalitätten des 'player'-Befehles\n";
                _eingabe[0] += "benutze 'cd' und 'ls' um dich durch deine Ordnerstruktur zu bewegen\n";
                _eingabe[0] += @"schreibe '.\'+'Name einer Datei', um diese Datei auszuführen \n";
                _eingabe[0] += "\nnun reicht es auch mit den Tipps. Geh erst einmal nach Hause und ruhe dich aus.";
                break;
            case "asdf":
                _eingabe[0] = "ist ..... eine 'besondere' Leistung";
                break;
            case "man":
                _eingabe[0] = "Der 'man'-Befehl dient dazu, Informationen zu liefern" + @"\n";
                _eingabe[0] += "Es funktioniert so: 'man'+Leerzeichen+'Name eines Befehles'" + @"\n";
                _eingabe[0] += "Wobei NameX der Name einer beliebigen Applikation sein kann.";
                break;
            case "player":
                _eingabe[0] = "Mit hilfe von 'player' bist du im Stande. deinen Spielcharakter zu steuern und zu beeinflussen" + @"\n" + @"\n";
                _eingabe[0] += "player -n oder --name          anzeigen des characternamens" + @"\n";
                _eingabe[0] += "player --stats                zeigt die statuswerte an" + @"\n";
                _eingabe[0] += "player --dungeons             gibt die Namen aller bekannten Dungeons wieder" + @"\n";
                _eingabe[0] += "player --inventory /notImplem zeigt dir alle Gegenstände, welcher der Spieler bei sich trägt" + @"\n";
                break;
            case "cd":
                _eingabe[0] = "du kannst den 'cd'-Befehl um dich innerhalb der Ordnerstruktur zu bewegen.";
                _eingabe[0] += @"\n";
                _eingabe[0] += "Befindest du dich im Ordner '/' und möchtest in den 'home' Ordner, So nutzt du: ";
                _eingabe[0] += @"\n";
                _eingabe[0] += "'cd home'";
                _eingabe[0] += @"\n";
                _eingabe[0] += "möchtest du vom 'home' Ordner wieder zurück in den '/' Ordner so nutzt du:";
                _eingabe[0] += @"\n";
                _eingabe[0] += "'cd ..'";
                break;
            default:
                _eingabe[0] = @" _      _                   _____ _           " + @"\n" +
                                @"| |    (_)                 / ____(_)          " + @"\n" +
                                @"| |     _ _ __  _   ___  _| (___  _ _ __ ___  " + @"\n" +
                                @"| |    | | '_ \| | | \ \/ /\___ \| | '_ ` _ \ " + @"\n" +
                                @"| |____| | | | | |_| |>  < ____) | | | | | | |" + @"\n" +
                                @"|______|_|_| |_|\__,_/_/\_\_____/|_|_| |_| |_|" + @"\n";
                _eingabe[0] += @"\n";
                _eingabe[0] += "In diesem Spiel nutzt du verschiedene Befehle um dich in einem virtuellen Raum zu bewergen. Dabei steht jeder Befehl für eine ganz bestimmte Funktion! So funktioniert der 'man'-Befehl wie eine Bedienungsanleitung mit unglaublich vielen Kapiteln.";
                _eingabe[0] += @"\n";
                _eingabe[0] += @"\n";
                _eingabe[0] += "Weitere Möglichkeiten des 'man'-Befehles sind 'man cd' oder 'man ls' wo du mehr zur Funktionweise von 'cd' oder 'ls' erfahren kannst.";
                _eingabe[0] += @"\n";
                _eingabe[0] += "Falls du noch mehr Informationen zum Spiel erhalten möchtest so nutze den Befehl 'man game'.";
                _eingabe[0] += @"\n";
                _eingabe[0] += "Ansonsten wünsche ich dir !!!!viel Spaß!!!!";
                break;
        }

        return _eingabe;
    }

    private static string[] aptGetMethode(string _option, string[] _eingabe)
    {
        if (_option == "update")
        {
            //Coole animation laufen lassen
            _eingabe = update;
        }

        return _eingabe;
    }

    private static void cdMethode(string _option)
    {
        string[] split = _option.Split('/');
        foreach (string _split in split)
        {
            switch (_split)
            {
                case ".":
                    //you are staying were ever you are right now
                    break;
                case "..":
                    //still habe to check if is insode of my filemanager GameObj
                    if (_currentDirectory.transform.tag != "RootDictory")
                    {
                        _currentDirectory = _currentDirectory.transform.parent.gameObject;
                    }
                    break;
                default:
                    GameObject tmpGame = _currentDirectory;
                    int i = 0;
                    while (tmpGame.name != _split)
                    {
                        tmpGame = _currentDirectory.transform.GetChild(i).gameObject;
                        i++;
                    }
                    if (tmpGame != _currentDirectory && tmpGame.GetComponent<File_Obj>().fileType != FileType.file)
                    {
                        _currentDirectory = tmpGame;
                    }
                    break;
            }
            ReadFileSystem(_currentDirectory);
        }
    }

    public static void ReadFileSystem(GameObject filesystem)
    {
        _currentDirectory = filesystem;

        directry = ReturnReadFileSystem(false);
    }

    public static string[] ReturnReadFileSystem(bool quality)
    {
        string[] _eingabe;

        if (quality)
        {
            _eingabe = new string[1];
            for (int i = 0; i < _currentDirectory.transform.childCount; i++)
            {
                _eingabe[0] += string.Concat(
                     _currentDirectory.transform.GetChild(i).GetComponent<File_Obj>().permission,
                @"\t", _currentDirectory.transform.GetChild(i).GetComponent<File_Obj>().links,
                @"\t", _currentDirectory.transform.GetChild(i).GetComponent<File_Obj>().owner,
                @"\t", _currentDirectory.transform.GetChild(i).GetComponent<File_Obj>().groupname,
                @"\t", _currentDirectory.transform.GetChild(i).GetComponent<File_Obj>().size,
                @"\t", _currentDirectory.transform.GetChild(i).GetComponent<File_Obj>().date,
                @"\t", _currentDirectory.transform.GetChild(i).GetComponent<File_Obj>().time,
                @"\t", _currentDirectory.transform.GetChild(i).GetComponent<File_Obj>().filename, @"\n"
                );
            }

        }
        else
        {
            _eingabe = new string[_currentDirectory.transform.childCount];
            for (int i = 0; i < _currentDirectory.transform.childCount; i++)
            {
                _eingabe[i] = _currentDirectory.transform.GetChild(i).GetComponent<File_Obj>().filename;
            }
        }

        return _eingabe;
    }

    public static string GetDirectoryPath()
    {
        string path = "~";
        Stack<string> stack = new Stack<string>();
        stack.Push(_currentDirectory.name);
        GameObject tmp_currentDirectory = _currentDirectory;
        int first = 0;

        while (tmp_currentDirectory.tag != "RootDictory")
        {
            tmp_currentDirectory = tmp_currentDirectory.transform.parent.gameObject;
            stack.Push(tmp_currentDirectory.name);
        }

        while (stack.Count > 0)
        {
            if (first < 2)
            {
                path += stack.Pop();
                first++;
            }
            else
            {
                path += "/" + stack.Pop();
            }
        }
        //stack backwords
        return path;
    }
}
