using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFile : MonoBehaviour
{
    [SerializeField] private Place place;
    private Monster[] _monsters;

    public GameObject MonsterObj;

    public void GetPlace()
    {
        _monsters = place.GetMonsters();
        for (int i = 0; i < _monsters.Length; i++)
        {
            GameObject gObjekt = Object.Instantiate(MonsterObj,this.transform.parent);
            gObjekt.GetComponent<GetMonster>()._monster = _monsters[i];
            gObjekt.name = gObjekt.GetComponent<GetMonster>()._monster.MonsterName;
            gObjekt.GetComponent<File_Obj>().fileType = FileType.file;
            Destroy(this.gameObject);
        }

    }
}
