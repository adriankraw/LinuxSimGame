using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChar: MonoBehaviour
{
    public static PlayerChar instance;
    public string _name { get; set; }
    public int _hp { get; set; }
    public int _maxhp { get; set; }
    public int _atk { get; set; }
    public int _exp { get; set; }
    public int _lvl { get; set; }
    public int _lvlPoints { get; set; }
    public bool _moveable { get; set; }
    public GameObject inventar { get; set; }

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }else{
            Destroy(this);
        }
        _moveable = true;
    }

    public void Verteidigen(int schaden)
    {
        if (_hp - schaden <= 0)
        {
            _hp = 0;
        }
        else
        {
            _hp -= schaden;
        }
    }
    public int Angreifen()
    {
        return _atk;
    }
    public string LevelUp()
    {
        if (_exp == 100)
        {
            _exp = 0;
            _lvl++;
            playerEvents.PlayerLevelUp();
            _lvlPoints = _lvlPoints + 5;
            _atk+=5;
            _maxhp+=25;
            _hp = _maxhp;
            return _name + ": hat die Stufe: " + _lvl + " erreicht";
        }
        else
        {
            return "Du scheinst noch nicht bereit zu sein.";
        }
    }
    public int Heal(int a)
    {
        if(_hp + a > _maxhp) _hp = _maxhp;
        return (_hp += a);
    }
}
