using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharakter : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        PlayerChar._name = "user1";
        PlayerChar._hp = 100;
        PlayerChar._atk = 10;
    }

    private IEnumerator ChangeName(string name)
    {
        PlayerChar._name = name;
        yield return new WaitForEndOfFrame();
    }
}
