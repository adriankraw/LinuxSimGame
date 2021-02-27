using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dungeon : MonoBehaviour
{
    [Serializable]
    public struct DungeonEbene
    {
        public int ebene;
        public Monster[] monsters;
    }

    [SerializeField] DungeonEbene[] ebenen;
    [SerializeField] GameObject monster;
    [SerializeField] int fortschritt = 1;

    public void ausführen()
    {
        for(int i = 0; i < ebenen[fortschritt-1].monsters.Length; i++)
        {
            monster.GetComponent<GetMonster>()._monster = GameObject.Instantiate(ebenen[fortschritt-1].monsters[i]);
            GameObject a = Instantiate(monster,this.transform);
            a.name = monster.GetComponent<GetMonster>().monsterName+i;
        }
    }
}
