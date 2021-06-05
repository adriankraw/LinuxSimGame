using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] PotionType _mytype;
    public void Use()
    {
        switch (_mytype)
        {
            case PotionType.SmallHeal:
                PlayerChar.instance.Heal(15);
                break;
            case PotionType.Heal:
                PlayerChar.instance.Heal(25);
                break;
            case PotionType.BigHeal:
                PlayerChar.instance.Heal(50);
                break;
        }
    }

    public PotionType GetPotionType()
    {
        return _mytype;
    }
}
