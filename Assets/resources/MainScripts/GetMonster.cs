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
    public void Angriff() => _monster.Angreifen();

}
