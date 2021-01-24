using UnityEngine;

[CreateAssetMenu(fileName ="Monster", menuName = "ScriptableObjects/CreateMonsterPreset")]
public class Monster : ScriptableObject
{
    public string MonsterName;

    //stats
    public int lvl;
    public int exp;
    public Vector2 anzahl;
    public int hp;
    public int atk;

    public int Angreifen(){
        return atk;
    }

    public int Verteidigen(int schaden){
        return (hp-schaden >= 0) ? (hp-=schaden) : 0;
    }

    public int GetAnzahl()
    {
        return (int) Random.Range(anzahl.x,anzahl.y);
    }
}
