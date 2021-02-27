using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFile : MonoBehaviour
{
    [SerializeField] private Place place;
    private Monster[] _monsters;

    public GameObject MonsterObj;

    public int GetPlace()
    {
        _monsters = place.GetMonsters();
        for (int i = 0; i < _monsters.Length; i++)
        {
            GameObject gObjekt = Object.Instantiate(MonsterObj,this.transform.parent);
            gObjekt.GetComponent<GetMonster>()._monster = Instantiate(_monsters[i]);
            gObjekt.name = gObjekt.GetComponent<GetMonster>()._monster.MonsterName+i;
            gObjekt.GetComponent<File_Obj>().fileType = FileType.file;
            gObjekt.GetComponent<GetMonster>()._monster.itemdrop = place.Itemdrop;
            Destroy(this.gameObject);
        }
        return _monsters.Length;
    }
}
