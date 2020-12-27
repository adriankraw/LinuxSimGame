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
        content.GetChild(content.childCount-1).gameObject.GetComponentInChildren<InputField>().Select();
    }
}
