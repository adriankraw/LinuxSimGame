using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalManager : MonoBehaviour
{
    [SerializeField] private GameObject knoten;
    [SerializeField] private GameObject terminal;
    [SerializeField] private GameObject zeile;

    // Start is called before the first frame update
    void Start()
    {
        // erstelle die erste Zeile
        Instantiate(zeile, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.tag != "Fokussed") return;
        else{
            if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Erzeugen");
                Erzeugen();
            }
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Delete");
                Delete();
            }
            //Restliche Eingabe muss abgefragt werden, damit Befehle funktionieren
        }
    }

    public void Erzeugen()
    {
        if(this.transform.parent.tag == "Knoten")
        {
            GameObject newTerminal =  Instantiate(terminal, this.transform.parent);
            this.transform.tag = "Untagged";
            newTerminal.tag = "Fokussed";
        }
    }

    public void Delete()
    {
        this.transform.parent.GetChild(this.transform.GetSiblingIndex() -1 ).gameObject.tag = "Fokussed";
        Destroy(this.gameObject);
    }
}