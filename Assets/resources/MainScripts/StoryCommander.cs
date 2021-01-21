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
            _eingabe = "Hmm seams like you are not the only one in here:";
            _eingabe+= @"\n";
            _eingabe+= "What are you going to do ?";
            storyDictionary[PlayerChar._name] = true;
        }else
        if(keywords == "/*"){
            
        }
        else{
            return "";
        }        
        return _eingabe;
    }
}
