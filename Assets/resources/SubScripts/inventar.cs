using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class inventar : MonoBehaviour
{
    public List<GameObject> items;
    private void Start()
    {
        inventarEvents.onInventarUpdate += UpdateList;
    }
    private void FixedUpdate() {
        UpdateList();
    }
    public void UpdateList()
    {
        items.Clear();
        for(int i = 0;i<this.transform.childCount;i++)
        {
            items.Add(this.transform.GetChild(i).gameObject);
        }
    }

    public string[] GetItemNames()
    {
        List<string> _list = new List<string>();
        foreach (GameObject a in items)
        {
            _list.Add(a.name);
        }
        return _list.ToArray();
    }

    public string Use(string name){
        foreach(GameObject item in items){
            if(item.name == name)
            {
                item.GetComponent<Potion>().Use();
                return ("item: "+name+" got used.");
            }
        }
        return "item was not found";
    }
}
