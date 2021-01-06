using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TerminalClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform content;

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("I was clicked");
        SelectInputField();
    }

    public void SelectInputField()
    {
        content.GetChild(content.childCount-1).gameObject.GetComponentInChildren<InputField>().Select();
        foreach(Transform bro in transform.parent.GetComponentsInChildren<Transform>())
        {
            bro.tag ="Untagged";
        }
        this.tag = "Fokussed";
    }
}
