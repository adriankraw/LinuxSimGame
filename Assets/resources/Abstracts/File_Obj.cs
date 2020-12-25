using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FileType{
    file,
    directry
}

public class File_Obj: MonoBehaviour
{
    [SerializeField] public string permission;
    [SerializeField] public int links;
    [SerializeField] public string owner;
    [SerializeField] public string groupname;
    [SerializeField] public int size;
    [SerializeField] public string date;
    [SerializeField] public string time;
    [SerializeField] public string filename;
    [SerializeField] public FileType fileType;

    void Awake()
    {
        filename = this.name;
        size = this.transform.childCount;
    }
}
