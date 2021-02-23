using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour
{
    [SerializeField] public GameObject knoten;
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

    private CommandsManager manager;
    public FitType fitType;

    // Start is called before the first frame update
    void Start()
    {
        // erstelle die erste Zeile
        manager = new CommandsManager();
        manager.ReadFileSystem(GameObject.FindWithTag("RootDictory"));
        newLine(manager.GetDirectoryPath(), false);
        newLine("", true);
        if (this.transform.parent.GetComponent<FlexibleGridLayout>())
        {
            knoten = this.transform.parent.gameObject;
        }
        historyPointer = 0;

        _rawEingabenHistory = new Stack<string>(10);
        _rawEingabenHistory.Push(" ");
        _tmphistory = new Stack<string>(10);

        MonsterAttackEvent.MonsterAttack += MonsterAttacks;
        fitType = knoten.GetComponent<FlexibleGridLayout>().fitType;
    }

    private void MonsterAttacks(int obj)
    {
        if (this.tag != "Fokussed") return;
        PlayerChar.Verteidigen(obj);
        newLine("Enemy is attacking", false);
        newLine(PlayerChar._name + "'s HP: " + PlayerChar._hp + "/" + PlayerChar._maxhp, false);
        newLine("", true); // true cause we need that " < " icon. User can type in these lines
        StartCoroutine(GoToEnd());
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
                }
                if (terminalBody.transform.GetChild(terminalBody.transform.childCount - 1).GetComponentInChildren<InputField>().text == "" && Input.GetKeyDown(KeyCode.Backspace))
                {
                    while (_tmphistory.Count > 0)
                    {
                        _rawEingabenHistory.Push(_tmphistory.Pop());
                    }
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    try
                    {
                        if (_rawEingabenHistory.Count == 0) return;
                        terminalBody.transform.GetChild(terminalBody.transform.childCount - 1).GetComponentInChildren<InputField>().text = _rawEingabenHistory.Peek();
                        _tmphistory.Push(_rawEingabenHistory.Pop());
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    try
                    {
                        if (_tmphistory.Count == 0) return;
                        terminalBody.transform.GetChild(terminalBody.transform.childCount - 1).GetComponentInChildren<InputField>().text = _tmphistory.Peek();
                        _rawEingabenHistory.Push(_tmphistory.Pop());
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e);
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (knoten.GetComponent<FlexibleGridLayout>().fitType != fitType)
                    {
                        GameObject newKnoten = transform.parent.GetComponent<TerminalKnoten>().ErzeugeKnoten();
                        newKnoten.tag = "Knoten";
                        GameObject newTerminal = Erzeugen();
                        this.transform.SetParent(newKnoten.transform);
                        newTerminal.transform.SetParent(newKnoten.transform);
                        this.knoten = newKnoten;
                        StartCoroutine(RecalcKnoten(newKnoten, fitType));
                    }
                    else
                    {
                        Erzeugen();
                    }
                }
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Q))
                {
                    Delete();
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    GoUp(FitType.Width);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    GoUp(FitType.Heigth);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    GoDown(FitType.Width);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    GoDown(FitType.Heigth);
                }
                //Restliche Eingabe muss abgefragt werden, damit Befehle funktionieren
                if (Input.GetKeyDown(KeyCode.B))
                {
                    fitType = FitType.Heigth;
                    //knoten.GetComponent<FlexibleGridLayout>().fitType = FitType.Heigth;
                }
                if (Input.GetKeyDown(KeyCode.V))
                {
                    fitType = FitType.Width;
                    //knoten.GetComponent<FlexibleGridLayout>().fitType = FitType.Width;
                }
                if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
                {
                    if (transform.GetSiblingIndex() - 1 <= -1) return;

                    int index = transform.GetSiblingIndex();
                    this.transform.parent.GetChild(index - 1).SetSiblingIndex(index);
                    this.GetComponent<TerminalClickHandler>().ClickInputField();
                    StartCoroutine(this.transform.parent.GetChild(index).GetComponent<TerminalClickHandler>().WaitForUnFokussed());
                }
                if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
                {
                    if (this.transform.GetSiblingIndex() + 1 > transform.parent.childCount) return;

                    int index = this.transform.GetSiblingIndex();
                    this.transform.parent.GetChild(index + 1).SetSiblingIndex(index);
                    this.GetComponent<TerminalClickHandler>().ClickInputField();
                    StartCoroutine(this.transform.parent.GetChild(index).GetComponent<TerminalClickHandler>().WaitForUnFokussed());
                }
            }
        }
    }
    IEnumerator RecalcKnoten(GameObject _knoten, FitType _fittype)
    {
        yield return new WaitForEndOfFrame();
        _knoten.GetComponent<FlexibleGridLayout>().fitType = _fittype;
        for (int i = 0; i < _knoten.transform.childCount; i++)
        {
            _knoten.transform.GetChild(i).GetComponent<TerminalManager>().fitType = _fittype;
        }
    }
    private void GoUp(FitType fit)
    {
        int a = 0;
        a = this.transform.GetSiblingIndex();
        if (a == 0 && this.transform.parent.parent.GetComponent<TerminalKnoten>())
        {
            a = this.transform.parent.GetSiblingIndex();
        }
        if (a == 0) return;//Wenn immernoch a = 0 ist dann bist du das oberste Objekt
        if (this.transform.GetSiblingIndex() == 0)
        {
            if (this.transform.parent.parent.GetChild(a - 1).GetComponent<TerminalKnoten>())
            {
                this.transform.parent.parent.GetChild(a - 1).GetChild(0).GetComponent<TerminalClickHandler>().SelectInputField();
            }
            else
            {
                this.transform.parent.parent.GetChild(a - 1).GetComponent<TerminalClickHandler>().SelectInputField();
            }
        }
        else
        {
            if (this.transform.parent.GetChild(a - 1).GetComponent<TerminalKnoten>())
            {
                this.transform.parent.GetChild(a - 1).GetChild(0).GetComponent<TerminalClickHandler>().SelectInputField();
            }
            else
            {
                this.transform.parent.GetChild(a - 1).GetComponent<TerminalClickHandler>().SelectInputField();
            }
        }
        this.transform.tag = "Untagged";
    }
    private void GoDown(FitType fit)
    {
        int a = 0;
        a = this.transform.GetSiblingIndex();
        if (a == this.transform.parent.childCount && this.transform.parent.parent.GetComponent<TerminalKnoten>())
        {
            a = this.transform.parent.GetSiblingIndex();
        }
        if (a == this.transform.parent.childCount) return;//Wenn immernoch a = ende der listen ist dann bist du das oberste Objekt
        if (this.transform.GetSiblingIndex() == this.transform.parent.childCount - 1)
        {
            try
            {
                if (this.transform.parent.parent.GetChild(a + 1).GetComponent<TerminalKnoten>())
                {
                    this.transform.parent.parent.GetChild(a + 1).GetChild(0).GetComponent<TerminalClickHandler>().SelectInputField();
                }
                else
                {
                    this.transform.parent.parent.GetChild(a + 1).GetComponent<TerminalClickHandler>().SelectInputField();
                }
            }catch{
                //Ein fehler im algoritmus sorgt für ein Transform child out of bounds fehler
            }
        }
        else
        {
            if (this.transform.parent.GetChild(a + 1).GetComponent<TerminalKnoten>())
            {
                this.transform.parent.GetChild(a + 1).GetChild(0).GetComponent<TerminalClickHandler>().SelectInputField();
            }
            else
            {
                this.transform.parent.GetChild(a + 1).GetComponent<TerminalClickHandler>().SelectInputField();
            }
        }
        this.transform.tag = "Untagged";
    }
    private void readInput()
    {
        try
        {
            _rawEingabe = terminalBody.transform.GetChild(terminalBody.transform.childCount - 1).GetComponentInChildren<InputField>().text;
            _rawEingabe.ToString();

            while (_tmphistory.Count > 0)
            {
                _rawEingabenHistory.Push(_tmphistory.Pop());
            }
            _rawEingabenHistory.Push(_rawEingabe);

            if (_rawEingabenHistory.Count > 10)
            {
                while (_rawEingabenHistory.Count > 1)
                {
                    _tmphistory.Push(_rawEingabenHistory.Pop());
                }
                _rawEingabenHistory.Pop();
                while (_tmphistory.Count > 0)
                {
                    _rawEingabenHistory.Push(_tmphistory.Pop());
                }
            }

            if (_rawEingabe != "")//hat der User überhaubt was geschrieben ?
            {
                _eingabe = _rawEingabe.Split(' ');
                string[] option = new string[] { "", "" };
                if (_eingabe.Length > 1)
                {
                    option[0] = _eingabe.GetValue(1).ToString();
                    if (_eingabe.Length > 2) option[1] = _eingabe.GetValue(2).ToString();
                }

                foreach (string text in manager.BefehleErkennen(_eingabe.GetValue(0).ToString(), option)) //für jeden string, der als Ergebnis von befehleErkennen erzeugt wird
                {
                    newLine(text, false); // false because we dont need that " < " - Icon. 
                }
            }
        }
        catch (Exception e)
        {
            newLine("Error: " + e, false);
        }
        newLine(manager.GetDirectoryPath(), false);
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
        if (_eingabe != null)
        {
            _eingabe = _eingabe.Replace(@"\t", "\t");
            _eingabe = _eingabe.Replace(@"\n", "\n");
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

    public GameObject Erzeugen()
    {
        if (knoten == this.transform.parent.gameObject)
        {
            GameObject newTerminal = Instantiate(Resources.Load<GameObject>(string2), knoten.transform);
            this.transform.tag = "Untagged";
            newTerminal.tag = "Fokussed";
            newTerminal.name = "" + transform.parent.childCount;
            return newTerminal;
        }
        return null;
    }

    public void Delete()
    {
        if (transform.parent.childCount > 1)
        {
            this.transform.parent.GetChild(this.transform.GetSiblingIndex() - (this.transform.GetSiblingIndex() == 0 ? 0 : 1)).gameObject.tag = "Fokussed";
        }
        Destroy(this.gameObject);
    }
}