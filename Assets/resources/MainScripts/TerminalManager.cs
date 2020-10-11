using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour
{
    [SerializeField] private GameObject knoten;
    [SerializeField] private GameObject terminal;
    [SerializeField] private GameObject terminalBody;
    [SerializeField] private GameObject zeile;

    [SerializeField] private string string2 = "Terminals/Prefaps/TerminalEmpty_Prefap";

    //Input
    KeyCode SuperKey = KeyCode.LeftAlt;

    //Befehle
    private string _rawEingabe;
    private string[] _eingabe;

    // Start is called before the first frame update
    void Start()
    {
        // erstelle die erste Zeile
        newLine();
        if (this.transform.parent.GetComponent<FlexibleGridLayout>())
        {
            knoten = this.transform.parent.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag != "Fokussed") return;
        else
        {
            if (!Input.GetKey(SuperKey))
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //Die Hauptmethode die alle eingaben lesen kann
                    readInput();
                }
            }
            if (Input.GetKey(SuperKey) && Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Erzeugen");
                Erzeugen();
            }
            if (Input.GetKey(SuperKey) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Delete");
                Delete();
            }
            //Restliche Eingabe muss abgefragt werden, damit Befehle funktionieren
            if (Input.GetKey(SuperKey) && Input.GetKey(KeyCode.H))
            {
                knoten.GetComponent<FlexibleGridLayout>().SetLayoutHorizontal();
            }
            if (Input.GetKey(SuperKey) && Input.GetKey(KeyCode.V))
            {
                knoten.GetComponent<FlexibleGridLayout>().SetLayoutVertical();
            }
        }
    }

    private void readInput()
    {
        _rawEingabe = terminalBody.transform.GetChild(terminalBody.transform.childCount - 1).GetComponentInChildren<Text>().text;
        _rawEingabe = _rawEingabe.Substring(2);
        _eingabe = _rawEingabe.Split(' ');

        foreach (string eingabe in _eingabe)
        {
            Debug.Log(eingabe);
        }

        newLine();
    }

    private void newLine()
    {
        GameObject tmpObj = Instantiate(zeile, terminalBody.transform);
        tmpObj.GetComponentInChildren<Text>().text += " " + terminalBody.transform.childCount;
    }

    public void Erzeugen()
    {
        if (knoten == this.transform.parent.gameObject)
        {
            GameObject newTerminal = Instantiate(Resources.Load<GameObject>(string2), knoten.transform);
            this.transform.tag = "Untagged";
            newTerminal.tag = "Fokussed";
        }
    }

    public void Delete()
    {
        this.transform.parent.GetChild(this.transform.GetSiblingIndex() - 1).gameObject.tag = "Fokussed";
        Destroy(this.gameObject);
    }
}