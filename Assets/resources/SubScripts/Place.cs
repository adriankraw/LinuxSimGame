using UnityEngine;

[CreateAssetMenu(fileName ="Place", menuName = "ScriptableObjects/CreatePlacePreset")]
public class Place : ScriptableObject
{
    public string PlaceName;

    public float wahrscheinlichkeit;

    public Monster[] Monsters;

    public Monster GetMonster()
    {
        if (Random.Range(0,100) < wahrscheinlichkeit*100 )
        {
            return Monsters[Random.Range(0, Monsters.Length)];
        }
        return null;
    }

    public Monster[] GetMonsters()
    {
        return Monsters;
    }
}