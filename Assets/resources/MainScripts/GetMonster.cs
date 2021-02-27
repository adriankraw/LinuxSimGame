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
    public void Verteidigen(int x)
    {
        _monster.Verteidigen(x);
        StartCoroutine(Defend(0.2f));
    }
    public void Drop(Transform tran)
    {
        if (_monster.Drop())
        {
            GameObject dropped = Instantiate(_monster.Drop(), tran);
            dropped.name = "itemDrop";
        }
    }
    public int Die()
    {
        return _monster.Die();
    }
    public void DeleteMonster()
    {
        DestroyImmediate(this.gameObject);
    }
    IEnumerator Defend(float time)
    {
        yield return new WaitForSeconds(time);
        MonsterAttackEvent.monsterAttack(Angriff());
    }
}
