using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Points{

    /*
    Diese Stats sollen hochgezählt werden, sodas der Spieler eine Herrausforderung bekommen,
    Er soll die die Befehle dadruch genauer anschauen und herrausfinden, wie effektiv man das Spiel spielen kann.
    Das ganze wird in einer Datei gespeichert, welcher der SPieler jederzeit einsehen kann, wenn er möchte.
    */
    public static int mistakesMade {get; set;}
    public static int usedCommands {get; set;}
}

public static class StoryCommander
{
    private static Dictionary<string,bool> storyDictionary = new Dictionary<string, bool>();
    public static void InitializeStory() {
        storyDictionary.Add("user1", false);
        storyDictionary.Add("home", false);
    }
    public static string StoryTelling(string keywords)
    {
        string _eingabe = "";
        string a = PlayerChar._name;
        if(keywords == a && storyDictionary["user1"] == false){
            _eingabe = "Hmmm, du scheinst nicht alleine zu sein:";
            _eingabe+= @"\n";
            _eingabe+= "wenn du eine .Monsters Datei findest. Überlege ob du sie ausführen möchtest( Ausführen: './Monsters' )";
            storyDictionary["user1"] = true;
        }else
        if(keywords == "home" && storyDictionary["home"] == false){
            _eingabe = "Dies ist ein Ordner indem nutzerspezifische Daten gespeichert werden.";
            _eingabe+= @"\n";
            _eingabe+= "Diese Daten können unteranderem Programme, Konfigurationsdateien und von dir erstellte Ordner sein.";
            storyDictionary["home"] = true;
        }
        else{
            return "";
        }        
        return _eingabe;
    }
}
