using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FileType{
    file,
    directry
}

public enum GamePlayObject{
    monster,
    Dungeon,
    item,
    etc
}
public class File_Obj: MonoBehaviour
{
    [SerializeField] public string permission;
    [SerializeField] public int links;
    [SerializeField] public string owner;
    [SerializeField] public string groupname;
    [SerializeField] public int size => this.transform.childCount;
    [SerializeField] public string date;
    [SerializeField] public string time;
    [SerializeField] public string filename => this.name;
    [SerializeField] public FileType fileType;
    [SerializeField] public GamePlayObject gameType = GamePlayObject.etc;
}
