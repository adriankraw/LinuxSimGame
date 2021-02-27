using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Points
{

    /*
    Diese Stats sollen hochgezählt werden, sodas der Spieler eine Herrausforderung bekommen,
    Er soll die die Befehle dadruch genauer anschauen und herrausfinden, wie effektiv man das Spiel spielen kann.
    Das ganze wird in einer Datei gespeichert, welcher der SPieler jederzeit einsehen kann, wenn er möchte.
    */
    public static int mistakesMade { get; set; }
    public static int usedCommands { get; set; }
    public static int monstersKilled { get; set; }
}

public static class StoryCommander
{
    private static Dictionary<string, bool> storyDictionary = new Dictionary<string, bool>();
    public static void InitializeStory()
    {
        storyDictionary.Add("user1", false);
        storyDictionary.Add("home", false);
        storyDictionary.Add("bin", false);
        storyDictionary.Add("guest", false);
        storyDictionary.Add("forest", false);
    }
    public static string StoryTelling(string keywords)
    {
        string _eingabe = "";
        string a = PlayerChar._name;
        if (keywords == a && storyDictionary["user1"] == false)
        {
            _eingabe = "Lass uns mal darüber reden, wie du Dateien oder Applicationen ausführen kannst:\n";
            _eingabe += "Wenn du eine Datei findest, handelt es sich nicht um einen Ordner, somit kannst du ihn schonmal nicht betreten.\n";
            _eingabe += @"\n";
            _eingabe += "Falls du eine Datei ausführen möchtest, machst du das in der Regel so: './Name_Der_Datei'\n";
            _eingabe += "\n";
            storyDictionary["user1"] = true;
        }
        else
        if (keywords == "home" && storyDictionary["home"] == false)
        {
            _eingabe = "Wilkommen in dei.... jagut nicht ganz deinem Zuhause. Dies ist der Ordner indem alle Häuser zufinden sind. \n";
            _eingabe += "Von hier aus kannst du den Ordner eines Nutzers betreten, sofern du die Rechte dafür hast.\n";
            storyDictionary["home"] = true;
        }
        else
        if (keywords == "bin" && storyDictionary["bin"] == false)
        {
            _eingabe = "Dies ist ein Ordner indem du die meisten Applikationen findest.";
            _eingabe += @"\n";
            storyDictionary["bin"] = true;
        }
        else
        if (keywords == "guest" && storyDictionary["guest"] == false)
        {
            _eingabe = "Hierbei handelt es sich ein Gäste-Account. Da du aber als "+PlayerChar._name+" angemeldet bist. Betrifft dich dieser Ordner nicht.";
            _eingabe += @"\n";
            storyDictionary["guest"] = true;
        }
        else
        if (keywords == "forest" && storyDictionary["forest"] == false)
        {
            _eingabe = "Du scheinst die kleinste Dungeon gefunden zu haben.";
        }
        else
        {
            return "";
        }
        return _eingabe;
    }
}
