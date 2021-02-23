using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CommandsManager
{

    private string[] directry = { "/", "    - bin" };
    private string[] manual = { "Das Spiel functioniert so:" };
    private string[] update = { "System Updating... ", "Nah..just joking. This System is already perfekt." };
    private string[] error = { "", "not found. Try Again. Or check the Manuel" };

    public GameObject _currentDirectory;

    public string[] BefehleErkennen(string _befehl, string[] _option)
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
                if (!PlayerChar._moveable)
                {
                    _eingabe[0] = "Du kannst scheinst an diesen Ort gebunden zu sein";
                    return _eingabe;
                }
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
            case "shutdown":
                if (_option[0] == "")
                {
                    Application.Quit();
                }
                break;
            case "exit":
                _eingabe[0] = "Eigentlich sollte ich jetzt dein Terminal schließen.\n";
                _eingabe[0] += "Aber ich mach das an dieser Stelle nicht.\n\n";
                _eingabe[0] += "Zum schließen kannst du aber: STRG+ALT + Q drücken.";
                break;
            case string dummy when _befehl.StartsWith("./"):
                _eingabe = ausführen(dummy, _eingabe, _option);
                break;
            default:
                error[0] = _befehl;
                _eingabe = error;
                Points.mistakesMade++;
                break;
        }
        return _eingabe;
    }
    private string[] ausführen(string dummy, string[] _eingabe, string[] _option)
    {
        string[] file = dummy.Split('/');
        dummy = file[file.Length - 1];
        PlaceFile _placefile;
        for (int i = 0; i < _currentDirectory.transform.childCount; i++)
        {
            Transform _file = _currentDirectory.transform.GetChild(i);
            try
            {
                _placefile = _file?.GetComponent<PlaceFile>();
            }
            catch
            {
                _eingabe[0] = "Es handel sich dabei nicht um eine Datei";
                return _eingabe;
            }

            if (_file.name == dummy) // existiert die datei ?
            {
                File_Obj file_Obj = _file.GetComponent<File_Obj>();
                if (file_Obj.gameType == GamePlayObject.monster)
                {
                    _eingabe[0] = "Du scheinst " + _placefile?.GetPlace() + "x Monster beschworen zu haben.";
                    PlayerChar._moveable = false;
                }
                if (file_Obj.gameType == GamePlayObject.Dungeon)
                {
                    _eingabe = BefehleErkennen("cd", new string[] { _file.name });
                    foreach (Transform game in _file)
                    {
                        if (game.name == "enemy" && game.gameObject.activeSelf)
                        {
                            _eingabe[0] += "\nDu scheinst " + game?.GetComponent<PlaceFile>().GetPlace() + "x Monster beschworen zu haben.";
                            PlayerChar._moveable = false;
                        }
                    }
                }
                else
                {

                    switch (_option[0])
                    {
                        case "":
                            if (_file.GetComponent<GetMonster>()) goto case "--attack";
                            if (_file.GetComponent<Briefe>()) goto case "--read";
                            if (_file.GetComponent<inventar>()) goto case "--show";
                            if (_file.GetComponent<Potion>()) goto case "addItem";
                            break;
                        case "--attack":
                            try
                            {
                                GetMonster monster = _file?.GetComponent<GetMonster>();
                                monster.Verteidigen(PlayerChar.Angreifen());
                                if (monster.dead)
                                {
                                    Points.monstersKilled++;
                                    if (PlayerChar._lvl == 1 && Points.monstersKilled == 1)
                                    {
                                        _eingabe[0] = "Glückwunsch DAS war dein erster Gegner!\n";
                                        _eingabe[0] += "Du hast für deinen ersten Kampf direkt 100Exp bekommen, weswegen du direkt ein Update/LevelUp machen kannst.\n";
                                        _eingabe[0] += "'apt-get update' kannst du dafür nutzen.\n";
                                        _eingabe[0] += "Anscheind hat der Gegner sogar etwas fallen gelassen\n";
                                    }
                                    else
                                    {
                                        _eingabe[0] = "Gegner wurde besiegt! Glückwunsch";
                                    }
                                    PlayerChar._exp += monster.Die();
                                    monster.Drop(_currentDirectory.transform);
                                    _file = _file.parent;
                                    monster.DeleteMonster();
                                    PlayerChar._moveable = true;
                                    for (int j = 0; j < _file.childCount; j++)
                                    {
                                        if (_file.GetChild(j).GetComponent<GetMonster>())
                                        {
                                            PlayerChar._moveable = false;
                                        }
                                    }
                                }
                                else
                                {
                                    //PlayerChar.Verteidigen(monster.Angriff());
                                    _eingabe[0] = _file.name + " HP: " + _file?.GetComponent<GetMonster>().hp + " ";
                                }
                            }
                            catch (Exception e)
                            {
                                _eingabe[0] = "Diese Datei anzugreifen macht nicht wirklich Sinn oder ?";
                                Debug.Log(e);
                            }
                            break;
                        case "--read":
                            try
                            {
                                _eingabe[0] = "\n";
                                _eingabe[1] = "\n";
                                try
                                {
                                    string[][] asd1 = _file.GetComponent<Briefe>().GetBrief1();
                                    foreach (string text in asd1[PlayerChar._lvl - 1])
                                    {
                                        _eingabe[0] += text + @"\n";
                                    }
                                }
                                catch
                                {
                                    _eingabe[0] = _file + " kannst du nicht lesen, versuch was anderes";
                                }
                            }
                            catch
                            {
                                _eingabe[0] = "Versuchst du gerade " + _file.name + " zu LESEN?";
                            }
                            break;
                        case "--show":
                            _eingabe = _file.GetComponent<inventar>().GetItemNames();
                            break;
                        case "--use":
                            _eingabe[0] = _file.GetComponent<inventar>().Use(_option[1]);
                            break;
                        case "addItem":
                            if(_currentDirectory.GetComponent<inventar>())
                            {
                                _eingabe[0] = _file.GetComponent<inventar>().Use(_option[1]);
                            }
                            else{
                                try{
                                _file.transform.SetParent(PlayerChar.inventar.transform);
                                }catch{
                                    _eingabe[0] = "Du scheinst noch kein inventar zu besitzen";
                                }
                            }
                                break;
                        case "--remove":
                            _eingabe[0] = "Das darfst du leider nicht.";
                            break;
                        case "--help":
                            _eingabe[0] = "Die ist eine Liste mit dir zur verfügung stehenden Parameter\n\n";
                            _eingabe[0] += "./Datei --attack              damit kannst du einen Angriff ausführen\n";
                            _eingabe[0] += "./Datei --read                gibt den Text aus\n";
                            _eingabe[0] += "./Datei --show                zeigt den inhalt an\n";
                            _eingabe[0] += "./Datei --use Itemname        nutzt das Item mit dem Itemnamen\n";
                            _eingabe[0] += "\n";
                            _eingabe[0] += "Achtung: nicht jede Datei reagiert auf jeden Parameter";
                            break;
                    }
                }
            }
        }
        return _eingabe;
    }
    private string[] playerMethode(string[] _option, string[] _eingabe)
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
                _eingabe[0] = "Player Exp: " + PlayerChar._exp + "/100";
                break;
            case "":
            case "--stats":
                _eingabe[0] = "Your stats are: " + @"\n" + @"\n" +
                              "Name: " + PlayerChar._name + @"\n" +
                              "Hp: " + PlayerChar._hp + "/" + PlayerChar._maxhp + @"\n" +
                              "Atk: " + PlayerChar._atk + @"\n" +
                              "Lvl: " + PlayerChar._lvl + @"\n" +
                              "SkillPoints: " + PlayerChar._lvlPoints + @"\n" +
                              "Exp: " + PlayerChar._exp + "/100";
                break;
            case "--inventar":
                _eingabe[0] = "Du kannst dein Inventar hier finden: '/home/" + PlayerChar._name + "'";
                break;
            default:
                _eingabe[0] = "player doesn't support " + _option[0] + " in this way. Consider checking the manuel for more information.";
                break;
        }
        return _eingabe;
    }

    private string[] LsMethode(string _option, string[] _eingabe)
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

    private string[] ManMehode(string _option, string[] _eingabe)
    {
        switch (_option)
        {
            case "game":
                _eingabe[0] = "Hast du schon verstanden wie der 'man'-Befehl funktioniert?\n";
                _eingabe[0] += "\nTipps:\n";
                _eingabe[0] += "'man player' zeigt dir alle Funktionalitätten des 'player'-Befehles\n";
                _eingabe[0] += "'man cd' und 'man ls' könnten ebenfalls interessant für dich sein\n";
                _eingabe[0] += "\nNun, ich will dich nicht länger aufhalten. Geh erst einmal nach Hause und ruhe dich aus.\n";
                _eingabe[0] += "\n";
                _eingabe[0] += "\nPS: Falls du etwas vergessen hast, kannst du 'man game' jederzeit erneut ausführen";
                break;
            case "asdf":
                _eingabe[0] = "ist ..... eine 'besondere' Leistung";
                break;
            case "man":
                _eingabe[0] = "Der 'man'-Befehl dient dazu, Informationen zu liefern" + @"\n";
                _eingabe[0] += "Es funktioniert so: 'man'+Leerzeichen+'Name eines Befehles'" + @"\n";
                _eingabe[0] += "Wobei 'Name eines Befehles', der Name einer beliebigen Applikation sein kann.";
                break;
            case "player":
                _eingabe[0] = "Mit hilfe von 'player' bist du im Stande. deinen Spielcharakter zu steuern und zu beeinflussen" + @"\n" + @"\n";
                _eingabe[0] += "player -n oder --name         anzeigen des characternamens" + @"\n";
                _eingabe[0] += "player --stats                zeigt die statuswerte an" + @"\n";
                _eingabe[0] += "player --inventory            zeigt dir den Ort deines Inventars an" + @"\n";
                break;
            case "cd":
                _eingabe[0] = "Der 'cd'-Befehl ähnelt, von der Funktionweise, einer Landkarte.\n";
                _eingabe[0] += "Wenn du dir gerade ein Land vorstellst, dann ist dieses Land '/'.\n\n";
                _eingabe[0] += "'/' ist nichts spezifisches. Schaust du dir jedoch '/' genauer an, so siehst du alle Ordner in '/'.\n";
                _eingabe[0] += "Diese Ordner könnten Bundesländer sein. Welche du mit 'cd Name_des_Bundeslandes' betreten kannst.\n";
                _eingabe[0] += "So wie zuvor mit dem Land, kannst du dir die Bundesländer ebenfalls genauer anschauen und betreten.\n";
                _eingabe[0] += "Dies kannst du so lange tun, bis es keine genauere Darstellung mehr gibt.";
                _eingabe[0] += "'cd Name_des_Ordners'      bewegt dich in einen Ordner hinein.";
                _eingabe[0] += "\n\n";
                _eingabe[0] += "'cd ..'                    bewegt dich wieder aus einen Ornder hinaus";
                _eingabe[0] += "\n";
                _eingabe[0] += "Weitere Möglichkeiten sind:\n";
                _eingabe[0] += "'cd ../Name_eines_Ordners' bewegt dich erst raus und dann in einen Ordner wieder hinein\n";
                _eingabe[0] += "'cd ../../..'              bewegt dich drei schritte zurück\n";
                break;
            case "ls":
                _eingabe[0] += "Der 'ls'-Befehl funktionert sehr einfach.\n";
                _eingabe[0] += "Wenn du dich gerade in einem Ordner befindest, kann du dir alle Unterordner anzeigen lassen, welche sich in deinem Ordner befinden.\n";
                _eingabe[0] += "Befindest du dich im Ordner '/' so siehst du, beim ausführen von 'ls', Ordner wie z.B. home, usr, sys, ...\n\n";
                _eingabe[0] += "Möchtest du noch mehr Informationen über eine Gruppe erfahren, so kannst du den Parameter '-la' anhängen.\n";
                _eingabe[0] += "'ls -la' gibt eine Liste aller Unterordner aus, zusammen mit Informationen wie: Deine Rechte, Der Besitzer, Anzahl an Unterordner in jenem Ordner, ...\n";
                break;
            default:
                _eingabe[0] = @" _      _                   _____ _           " + @"\n" +
                                @"| |    (_)                 / ____(_)          " + @"\n" +
                                @"| |     _ _ __  _   ___  _| (___  _ _ __ ___  " + @"\n" +
                                @"| |    | | '_ \| | | \ \/ /\___ \| | '_ ` _ \ " + @"\n" +
                                @"| |____| | | | | |_| |>  < ____) | | | | | | |" + @"\n" +
                                @"|______|_|_| |_|\__,_/_/\_\_____/|_|_| |_| |_|" + @"\n";
                _eingabe[0] += @"\n";
                _eingabe[0] += "In diesem Spiel nutzt du verschiedene Befehle um dich in einem virtuellen Raum zu bewegen. Dabei steht jeder Befehl für eine ganz bestimmte Funktion! So funktioniert der 'man'-Befehl wie eine Bedienungsanleitung mit unglaublich vielen Kapiteln.";
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

    private string[] aptGetMethode(string _option, string[] _eingabe)
    {
        switch (_option)
        {
            case "update":
                BefehleErkennen("cd", new string[] { ".." });
                BefehleErkennen("cd", new string[] { ".." });
                _eingabe[0] = PlayerChar.LevelUp() + "\n";
                _eingabe[0] += _currentDirectory.GetComponent<LevelUpSzene>().LevelUp();
                break;
            default:
                _eingabe[0] = "";
                break;
        }

        return _eingabe;
    }

    private void cdMethode(string _option)
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

    public void ReadFileSystem(GameObject filesystem)
    {
        _currentDirectory = filesystem;

        directry = ReturnReadFileSystem(false);
    }

    public string[] ReturnReadFileSystem(bool quality)
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

    public string GetDirectoryPath()
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
