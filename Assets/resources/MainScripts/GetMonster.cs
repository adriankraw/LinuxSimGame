using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetMonster : MonoBehaviour
{
    public Monster _monster;
    public string monsterName => _monster.MonsterName;
    public int lvl => _monster.lvl;
    public int exp => _monster.exp;
    public int anzahl => _monster.GetAnzahl();
    public int hp => _monster.hp;
    public int atk => _monster.atk;
    public bool dead => _monster.dead;
    public int Angriff() => _monster.Angreifen();
    public void Verteidigen(int x) => _monster.Verteidigen(x);
    public void Drop(Transform tran) { 
        GameObject dropped = Instantiate(_monster.Drop(),tran); 
        dropped.name = "itemDrop";
    }
    public int Die()
    {
        return _monster.Die();
    }
}
