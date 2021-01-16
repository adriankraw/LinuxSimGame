using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CommandsManager : MonoBehaviour
{

    private static string[] directry = { "/", "    - bin" };
    private static string[] manual = { "Das Spiel functioniert so: ", "du schreibst hier deine Befehle rein", "und der Computer sorgt dafür, dass dieser ausgeführt wird.", "vielleicht versucht du es man mit 'man game'" };
    private static string[] update = { "System Updating... ", "0% - 100%" };
    private static string[] error = { "", "not found. Try Again. Or check the Manuel" };

    public static GameObject _currentDirectory;

    public static string[] BefehleErkennen(string _befehl, string _option)
    {
        string[] _eingabe = new string[] { "" };
        Debug.Log("Eingabe: "+_befehl+" "+_option);
        switch (_befehl)
        {
            case "":
                break;
            case "cd":
                cdMethode(_option);
                break;
            case "apt-get":
                _eingabe = aptGetMethode(_option, _eingabe);
                break;
            case "man":
                _eingabe = ManMehode(_option, _eingabe);
                break;
            case "dir":
            case "ls":
                _eingabe = LsMethode(_option, _eingabe);
                break;
            case "player":
                playerMethode(_option, _eingabe);
                break;
            case "whoami":
                _eingabe[0] = "student: 00102";
                break;
            default:
                error[0] = _befehl;
                _eingabe = error;
                break;
        }
        return _eingabe;
    }

    private static void playerMethode(string _option, string[] _eingabe)
    {
        switch (_option)
        {
            case "name":
            case "n":
                _eingabe[0] = "player Name: "+ PlayerChar._name;
                break;
            case "hp":
                _eingabe[0] = "";
            break;
            case "":
            case "stats":
                _eingabe[0] = "player stats are: " + @"\n" + @"\n" +
                 "Name: "+PlayerChar._name+ @"\n" +
                 "Hp: "+PlayerChar._hp+ @"\n" +
                 "Atk: "+PlayerChar._atk+ @"\n" +
                 "Lvl: "+PlayerChar._lvl+ @"\n" +
                 "Exp: "+PlayerChar._exp+"/100";

                break;
            default:
                _eingabe[0] = "player does support " + _option + ". Consider checking the manuel for more information.";
                break;
        }
    }

    private static string[] LsMethode(string _option, string[] _eingabe)
    {
        if (_option == "")
        {
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
                _eingabe[0] = @" _      _                   _____ _           " + @"\n" +
                              @"| |    (_)                 / ____(_)          " + @"\n" +
                              @"| |     _ _ __  _   ___  _| (___  _ _ __ ___  " + @"\n" +
                              @"| |    | | '_ \| | | \ \/ /\___ \| | '_ ` _ \ " + @"\n" +
                              @"| |____| | | | | |_| |>  < ____) | | | | | | |" + @"\n" +
                              @"|______|_|_| |_|\__,_/_/\_\_____/|_|_| |_| |_|" + @"\n";
                              
                _eingabe[0] += "\nDas Spiel Beginnt: \nKannst du dir den Namen deines Spielers anzeigen lassen ?: \t Tipp: 'man player' \n";
                _eingabe[0] += "\nbenutze 'cd' und 'ls' um dich durch deine Ordnerstruktur zu bewegen\n";
            break;
            case "asd": 
                _eingabe[0]="...lass das! Pfui";
            break;
            case "man":
                _eingabe[0]="man inception daaaam.dam.daaaaaaa";
            break;
            case "player":
                _eingabe[0] ="Mit hilfe von 'player' bist du im stande. deinen Spielcharacter zu steuern und zu beeinflussen"+@"\n"+@"\n";
                _eingabe[0]+="player n oder name          anzeigen des characternamens"+@"\n";
                _eingabe[0]+="player stats                zeigt die statuswerte an";
            break;
            default:
                _eingabe = manual;
            break;
        }

        return _eingabe;
    }

    private static string[] aptGetMethode(string _option, string[] _eingabe)
    {
        if (_option == "update")
        {
            _eingabe = update;
        }

        return _eingabe;
    }

    private static void cdMethode(string _option)
    {
        switch (_option)
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
                while (tmpGame.name != _option)
                {
                    tmpGame = _currentDirectory.transform.GetChild(i).gameObject;
                    i++;
                }
                if (tmpGame != _currentDirectory)
                {
                    _currentDirectory = tmpGame;
                }
            break;
        }
        ReadFileSystem(_currentDirectory);
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
            if(first<2){
                path += stack.Pop();
                first++;
            }else{
                path += "/"+stack.Pop();
            }
        }
        //stack backwords
        return path;
    }
}
