using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsManager : MonoBehaviour
{
    private static string[] directry = {"/","\t- usr","\t- etc"};

    public static string[] BefehleErkennen(string _befehl, string _option)
    {
        string[] _eingabe = new string[] {};

        switch(_befehl)
        {
            case "":
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
}
