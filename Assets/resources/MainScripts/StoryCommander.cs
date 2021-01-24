using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StoryCommander
{
    private static Dictionary<string,bool> storyDictionary = new Dictionary<string, bool>();
    public static void InitializeStory() {
        storyDictionary.Add("home", false);
    }
    public static string StoryTelling(string keywords)
    {
        string _eingabe = "";
        string a = PlayerChar._name;
        if(keywords == PlayerChar._name && !storyDictionary.ContainsKey(PlayerChar._name)){
            _eingabe = "Hmmm, du scheinst nicht alleine zu sein:";
            _eingabe+= @"\n";
            _eingabe+= "Was wirst du tun ?";
            storyDictionary[PlayerChar._name] = true;
        }else
        if(keywords == "home"){
            _eingabe = "Dies ist ein Ordner indem nutzerspezifische Daten gespeichert werden.";
            _eingabe+= @"\n";
            _eingabe+= "Diese Daten können unteranderem Programme, Konfigurationsdateien und von dir erstellte Ordner sein.";
        }
        else{
            return "";
        }        
        return _eingabe;
    }
}
