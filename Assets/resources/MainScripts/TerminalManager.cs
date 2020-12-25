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

    //Input Vielleicht mach ich das noch änderbar aber ich weiß nicht so recht
    [SerializeField] KeyCode SuperKey = KeyCode.LeftAlt;

    //Befehle
    private string _rawEingabe;
    private string[] _eingabe;

    private Stack<String> _rawEingabenHistory;
    private Stack<String> _tmphistory;
    private int historyPointer;

    private GameObject tmpObj = null;

    // Start is called before the first frame update
    void Start()
    {
        // erstelle die erste Zeile
        newLine(CommandsManager.GetDirectoryPath(), false);
        newLine("", true);
        if (this.transform.parent.GetComponent<FlexibleGridLayout>())
        {
            knoten = this.transform.parent.gameObject;
        }
        historyPointer = 0;

        _rawEingabenHistory = new Stack<string>(10);
        _rawEingabenHistory.Push(" ");
        _tmphistory = new Stack<string>(10);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag == "Fokussed")
        {
            if (!Input.GetKey(SuperKey))
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //Die Hauptmethode die alle eingaben lesen kann
                    readInput();
                    while (_tmphistory.Count > 0)
                    {
                        _rawEingabenHistory.Push(_tmphistory.Pop());
                    }
                }
                if(terminalBody.transform.GetChild(terminalBody.transform.childCount - 1).GetComponentInChildren<InputField>().text == "" && Input.GetKeyDown(KeyCode.Backspace))
                {
                    while (_tmphistory.Count > 0)
                    {
                        _rawEingabenHistory.Push(_tmphistory.Pop());
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                try{
                    if(_rawEingabenHistory.Count == 0)return;
                    terminalBody.transform.GetChild(terminalBody.transform.childCount - 1).GetComponentInChildren<InputField>().text = _rawEingabenHistory.Peek();
                    _tmphistory.Push(_rawEingabenHistory.Pop());
                }catch(Exception e){
                    Debug.Log(e);
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                try{
                    if(_tmphistory.Count == 0)return;
                    terminalBody.transform.GetChild(terminalBody.transform.childCount - 1).GetComponentInChildren<InputField>().text = _tmphistory.Peek();
                    _rawEingabenHistory.Push(_tmphistory.Pop());
                }catch(Exception e){
                    Debug.Log(e);
                }
            }
            if (Input.GetKey(SuperKey) && Input.GetKeyDown(KeyCode.Return))
            {
                Erzeugen();
            }
            if (Input.GetKey(SuperKey) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Q))
            {
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
        try
        {
            _rawEingabe = terminalBody.transform.GetChild(terminalBody.transform.childCount - 1).GetComponentInChildren<InputField>().text;
            _rawEingabe.ToString();

            _rawEingabenHistory.Push(_rawEingabe);

            while(_rawEingabenHistory.Count >= 10)
            {
                _rawEingabenHistory.Pop();
            }


            if (_rawEingabe != "")//hat der User überhaubt was geschrieben ?
            {
                _eingabe = _rawEingabe.Split(' ');
                string option = "";
                if (_eingabe.Length > 1)
                {
                    option = _eingabe.GetValue(1).ToString();
                }

                foreach (string text in CommandsManager.BefehleErkennen(_eingabe.GetValue(0).ToString(), option))
                {
                    newLine(text, false); // false because we dont need that " < " - Icon. 
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e);
        }
        newLine(CommandsManager.GetDirectoryPath(), false);
        newLine("", true); // true cause we need that " < " icon. User can type in these lines

        StartCoroutine(GoToEnd());
    }

    private IEnumerator GoToEnd()
    {
        yield return new WaitForEndOfFrame();
        GetComponent<ScrollToBottom>().scrollToEnd();
    }

    private void newLine(string _eingabe, bool eingabe)
    {
        tmpObj = Instantiate(zeile, terminalBody.transform);
        TerminalRow row = tmpObj.GetComponent<TerminalRow>();
        if(_eingabe != null)
        {
            _eingabe = _eingabe.Replace(@"\t","\t");
            _eingabe = _eingabe.Replace(@"\n","\n");
        }
        tmpObj.GetComponentInChildren<InputField>().text = _eingabe;

        if (eingabe)
        {
            tmpObj.GetComponentInChildren<Text>().text = ">";
            tmpObj.GetComponentInChildren<InputField>().ActivateInputField();
        }
        else
        {
            tmpObj.GetComponentInChildren<Text>().text = " ";
            tmpObj.GetComponentInChildren<InputField>().readOnly = true;
            tmpObj.GetComponentInChildren<Text>().GetComponent<LayoutElement>().preferredWidth = 10;
            tmpObj.GetComponentInChildren<Text>().GetComponent<LayoutElement>().preferredHeight = 30;
        }

        GetComponent<ScrollToBottom>().scrollToEnd();
        
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
        if (transform.parent.childCount > 1)
        {
            this.transform.parent.GetChild(this.transform.GetSiblingIndex() - 1).gameObject.tag = "Fokussed";
        }
        Destroy(this.gameObject);
    }
}