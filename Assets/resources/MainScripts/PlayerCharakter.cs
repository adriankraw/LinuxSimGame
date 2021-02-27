using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCharakter : MonoBehaviour
{
    [SerializeField] GameObject inventar;
    void Start()
    {
        PlayerChar._name = "user1";
        PlayerChar._maxhp = 100;
        PlayerChar._hp = PlayerChar._maxhp;
        PlayerChar._atk = 10;
        PlayerChar._lvl = 1;
        PlayerChar._lvlPoints = 0;
        playerEvents.onPlayerLevelUp += LevelUp;
    }
    private IEnumerator ChangeName(string name)
    {
        PlayerChar._name = name;
        yield return new WaitForEndOfFrame();
    }

    private void LevelUp()
    {
        switch (PlayerChar._lvl)
        {
            case 1:
                Debug.Log(1);
                break;
            case 2:
                GameObject _inventar = Instantiate(inventar,this.transform);
                _inventar.name = "inventar";
                PlayerChar.inventar = _inventar;
                break;
            case 3:
                Debug.Log(3);
                break;
            case 4:
                Debug.Log(4);
                break;
            case 5:
                Debug.Log(5);
                break;
        }
    }
}
public class playerEvents : MonoBehaviour
{
    public static event Action onPlayerLevelUp;
    public static void PlayerLevelUp()
    {
        onPlayerLevelUp?.Invoke();
    }
}
public class inventarEvents : MonoBehaviour
{
    public static event Action onInventarUpdate;
    public static void InventarUpdate()
    {
        onInventarUpdate?.Invoke();
    }
}
