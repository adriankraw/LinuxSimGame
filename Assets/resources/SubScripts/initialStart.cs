using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialStart : MonoBehaviour
{
    [SerializeField] private GameObject rootFileDirectory;

    // Start is called before the first frame update
    void Awake()
    {
        //Setting StringArray to current RootFileDirectory
        CommandsManager.ReadFileSystem(rootFileDirectory);
        PlayerChar._moveable = true;
    }
}
