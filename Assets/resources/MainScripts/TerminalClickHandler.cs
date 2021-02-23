using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TerminalClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public Transform content;

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("I was clicked");
        ClickInputField();
    }
    public void ClickInputField()
    {

        Transform top = this.transform;
        while (top.parent.GetComponent<TerminalKnoten>())
        {
            top = top.parent;
        }
        UnMarkAll(top);

        content.GetChild(content.childCount - 1).gameObject.GetComponentInChildren<InputField>().Select();
        StartCoroutine(WaitForMe());
    }
    public void UnMarkAll(Transform top)
    {
        for(int i = 0; i < top.childCount;i++)
        {
            if(top.GetChild(i).GetComponent<TerminalKnoten>())
            {
                UnMarkAll(top.GetChild(i));
            }
            if(top.GetChild(i).tag == "Fokussed")
            {
                top.GetChild(i).tag = "Untagged";
            }
        }
    }
    public void SelectInputField()
    {
        content.GetChild(content.childCount - 1).gameObject.GetComponentInChildren<InputField>().Select();
        StartCoroutine(WaitForMe());
    }
    IEnumerator WaitForMe()
    {
        yield return new WaitForEndOfFrame();
        this.transform.tag = "Fokussed";
    }
    public IEnumerator WaitForUnFokussed()
    {
        yield return new WaitForEndOfFrame();
        this.transform.tag = "Untagged";
    }
}
