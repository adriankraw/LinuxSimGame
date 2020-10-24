using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsManager : MonoBehaviour
{

    private static string[] directry = {"/","    - bin"};
    private static string[] manual = {"Das Spiel functioniert so: ","du schreibst hier deine Befehle rein","und der Computer sorgt dafür, dass dieser ausgeführt wird."};
    private static string[] update = {"System Updating... ","0% - 100%"};

    public static GameObject _currentDirectory;

    public static string[] BefehleErkennen(string _befehl, string _option)
    {
        string[] _eingabe = new string[] {};

        switch(_befehl)
        {
            case "":
                break;
            case "cd":
                //look for the Objects
                if (_option == "..")
                { 
                    //still habe to check if is insode of my filemanager GameObj
                    if( _currentDirectory.transform.tag != "RootDictory" ){
                        _currentDirectory = _currentDirectory.transform.parent.gameObject;
                    }
                }else{
                    GameObject tmpGame = _currentDirectory;
                    int i = 0;
                    while( tmpGame.name != _option )
                    {
                        tmpGame = _currentDirectory.transform.GetChild(i).gameObject;
                        i++;
                    }
                    if(tmpGame != _currentDirectory)
                    {
                        _currentDirectory = tmpGame;
                    }
                }
                    ReadFileSystem(_currentDirectory);
                break;
            case "apt-get":
                if(_option == "update")
                {
                    _eingabe = update;
                }
                break;
            case "man":
                if(_option == "" || _option == "all")
                {
                    _eingabe = manual;
                }
                break;
            case "exa":
            case "dir":
            case "ls":{
                if(_option == "")
                {
                    _eingabe = directry;
                }
                break;
            }
        }
        return _eingabe;
    }

    public static void ReadFileSystem(GameObject filesystem)
    {
        _currentDirectory = filesystem;

        string[] _eingabe = new string[filesystem.transform.childCount];

        for(int i  = 0; i< filesystem.transform.childCount; i++)
        {
            _eingabe[i] = filesystem.transform.GetChild(i).name;
        }

        directry = _eingabe;
    }
    public static string GetDirectoryPath()
    {
        string path = "~";
        Stack<string> stack = new Stack<string>();
        stack.Push(_currentDirectory.name);
        GameObject tmp_currentDirectory = _currentDirectory;

        while ( tmp_currentDirectory.tag != "RootDictory" )
        {
            tmp_currentDirectory = tmp_currentDirectory.transform.parent.gameObject;
            stack.Push(tmp_currentDirectory.name);
        }

        while (stack.Count != 0)
        {
            path += stack.Pop();
        }
        //stack backwords
        return path;
    }
}
