using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollToBottom : MonoBehaviour
{
    public void scrollToEnd()
    {
        this.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
    }
}
