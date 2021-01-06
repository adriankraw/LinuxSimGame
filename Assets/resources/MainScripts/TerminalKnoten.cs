using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalKnoten : MonoBehaviour
{
    [SerializeField] private string string2 = "Terminals/Prefaps/TerminalEmpty_Prefap";

    private KeyCode SuperKey = KeyCode.LeftAlt;

    void Update()
    {
        if ( this.transform.childCount == 0 && (Input.GetKey(SuperKey) && Input.GetKeyDown(KeyCode.Return)))
        {
            Erzeugen();
        }
    }

    public void Erzeugen()
    {
        GameObject newTerminal = Instantiate(Resources.Load<GameObject>(string2), this.transform);
        newTerminal.tag = "Fokussed";
    }
}
