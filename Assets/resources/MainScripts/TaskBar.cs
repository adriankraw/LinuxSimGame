using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBar : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject _exitButton;

    public void ExitApplication()
    {
        //Ask if you are shure should be added here
        //Followed by cool animation -> Matrix Terminal
        Application.Quit();
    }

}
