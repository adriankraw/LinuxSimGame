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
        for(int i = 0; i< transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).tag = "Untagged";
        }
        content.GetChild(content.childCount-1).gameObject.GetComponentInChildren<InputField>().Select();
        StartCoroutine(WaitForMe());
    }
    public void SelectInputField()
    {
        content.GetChild(content.childCount-1).gameObject.GetComponentInChildren<InputField>().Select();
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
