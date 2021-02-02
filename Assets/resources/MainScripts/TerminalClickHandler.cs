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
        SelectInputField();
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
}
