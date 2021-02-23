using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSzene : MonoBehaviour
{
    int level => PlayerChar._lvl;
    private string returnString;
    [Header("level 1")]
    [SerializeField] GameObject Dungeon;

    public string LevelUp()
    {
        switch (level)
        {
            case 2:
                returnString = "Bei eines LevelUp/SystemUpdate passieren manchmal viele Dinge. Es lohnt sich verschiedene Orte im Auge zu behalten.\n\n";
                returnString += "Hier ein kleines Rätsel, welches dir den nächsten Ordner/Ort nennt:\n";
                returnString += "Ich bin ein englischer Nutzer, welchem ein Buchstabe gestohlen worden ist. wer BIN ich ?";
                break;
            case 3:
                returnString = "jawohl ich bin jetzt level 3";
                break;
            case 4:
                returnString = "jawohl ich bin jetzt level 4";
                break;
            case 5:
                returnString = "jawohl ich bin jetzt level 5";
                break;
            default:
                returnString = "Dat war nix";
                break;
        }
        return returnString;
    }
}
