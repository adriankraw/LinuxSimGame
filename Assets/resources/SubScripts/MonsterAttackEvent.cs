using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterAttackEvent : MonoBehaviour
{
    public static event Action<int> MonsterAttack;
    public static void monsterAttack(int s)
    {
        MonsterAttack?.Invoke(s);
    }
}
