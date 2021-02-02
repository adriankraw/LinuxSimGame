using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharakter : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        PlayerChar._name = "user1";
        PlayerChar._maxhp = 100;
        PlayerChar._hp = PlayerChar._maxhp;
        PlayerChar._atk = 10;
        PlayerChar._lvl = 1;
        PlayerChar._lvlPoints = 0;
    }

    private IEnumerator ChangeName(string name)
    {
        PlayerChar._name = name;
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator LevelUp()
    {
        PlayerChar._lvl = PlayerChar._lvl +1;
        PlayerChar._lvlPoints = PlayerChar._lvlPoints + 5;
        yield return new WaitForEndOfFrame();
    }
}
