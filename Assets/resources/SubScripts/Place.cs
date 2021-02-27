using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Place", menuName = "ScriptableObjects/CreatePlacePreset")]
public class Place : ScriptableObject
{
    public string PlaceName;

    public float wahrscheinlichkeit;

    public Monster[] Monsters;

    public GameObject Itemdrop;

    public Monster GetMonster()
    {
        if (Random.Range(0, 100) < wahrscheinlichkeit * 100)
        {
            return Monsters[Random.Range(0, Monsters.Length)];
        }
        return null;
    }

    public Monster[] GetMonsters()
    {
        List<Monster> _monster = new List<Monster>();

        for (int i = 0; i < Monsters.Length; i++)
        {
            int monsterZahl = Monsters[i].GetAnzahl();
            for (int j = 0; j < monsterZahl; j++)
            {
                _monster.Add(Monsters[i]);
            }
        }
        return _monster.ToArray();;
    }
}