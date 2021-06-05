using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    void Start()
    {
        instance = this;
    }

    public event Action<string> OnPlayerNameChanged;
    public void PlayerNameChanged(string _, string newPlayerName)
    {
        OnPlayerNameChanged.Invoke(newPlayerName);
    }
}
