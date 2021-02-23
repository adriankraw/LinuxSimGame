using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObjects/CreateMonsterPreset")]
public class Monster : ScriptableObject
{
    public string MonsterName;

    //stats
    public int lvl;
    public int exp;
    public Vector2 anzahl;
    public int hp;
    public int atk;
    public bool dead = false;
    public GameObject itemdrop;

    public int Angreifen()
    {
        return atk;
    }

    public int Verteidigen(int schaden)
    {
        if (hp - schaden >= 0)
        {
            hp -= schaden;
            return hp;
        }
        else
        {
            hp = 0;
            dead = true;
            return hp;
        }
    }
    public int GetAnzahl()
    {
        return (int)UnityEngine.Random.Range(anzahl.x, anzahl.y + 1);
    }
    public int Die()
    {
        return exp;
    }
    public GameObject Drop()
    {
        return itemdrop;
    }
}
