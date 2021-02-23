using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalKnoten : MonoBehaviour
{
    [SerializeField] private string string2 = "Terminals/Prefaps/TerminalEmpty_Prefap";
    [SerializeField] private string stringKnoten = "Terminals/Prefaps/Terminal_Tree";

    private KeyCode SuperKey = KeyCode.LeftAlt;

    void Update()
    {
        if ( this.transform.childCount == 0 && (Input.GetKey(SuperKey) && Input.GetKeyDown(KeyCode.Return)))
        {
            Erzeugen();
        }
    }

    public GameObject Erzeugen()
    {
        GameObject newTerminal = Instantiate(Resources.Load<GameObject>(string2), this.transform);
        newTerminal.tag = "Fokussed";
        return newTerminal;
    }
    public GameObject ErzeugeKnoten()
    {
        GameObject newTerminal = Instantiate(Resources.Load<GameObject>(stringKnoten), this.transform);
        newTerminal.tag = "Fokussed";
        return newTerminal;
    }
    private void FixedUpdate() {
        if(this.transform.parent.name == "Main_Canvas") return;
        if(this.transform.childCount == 0)
        {
            Destroy(this.gameObject);
        }
        if(this.transform.childCount == 1)
        {
            Transform onlychild = this.transform.GetChild(0);
            onlychild.SetParent(this.transform.parent);
            onlychild.GetComponent<TerminalManager>().knoten = this.transform.parent.gameObject;
        }
    }
}
