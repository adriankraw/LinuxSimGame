using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Menue : MonoBehaviour
{
    [SerializeField] private Button toggleButton;
    [SerializeField] Dropdown auflösung;

    [SerializeField] Toggle fullscreen;
    private bool _isfullscreen;
    List<string> options= new List<string>();
    
    private void Awake()
    {
        toggleButton.onClick.AddListener(toggleActive);
        this.gameObject.GetComponent<Canvas>().enabled = false;
        StartAufloesung();
        auflösung.onValueChanged.AddListener(updateAufloesung);
        _isfullscreen = Screen.fullScreen;
        fullscreen.isOn = fullscreen;
        fullscreen.onValueChanged.AddListener(updatefullscreen);
    }

    private void updateAufloesung(int data)
    {
        string[] _data = options[data].Split(':');
        Screen.SetResolution(int.Parse(_data[0]),int.Parse(_data[1]),false,Screen.currentResolution.refreshRate);
    }

    private void updatefullscreen(bool _full)
    {
        Screen.fullScreen = _full;
    }

    private void StartAufloesung()
    {
        options.Clear();
        foreach (Resolution res in Screen.resolutions)
        {
            options.Add(res.width + ":" + res.height);
        }
        auflösung.AddOptions(options);
    }

    void toggleActive()
    {
        this.gameObject.GetComponent<Canvas>().enabled = !this.gameObject.GetComponent<Canvas>().enabled;
    }
}
