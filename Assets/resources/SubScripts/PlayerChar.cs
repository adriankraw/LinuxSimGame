using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerChar
{
    public static string _name { get; set; }
    public static int _hp { get; set; }
    public static int _maxhp { get; set; }
    public static int _atk { get; set; }
    public static int _exp { get; set; }
    public static int _lvl { get; set; }
    public static int _lvlPoints { get; set; }
    public static bool _moveable { get; set; }
    public static void Verteidigen(int schaden)
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
    public static int Angreifen()
    {
        return _atk;
    }
}
