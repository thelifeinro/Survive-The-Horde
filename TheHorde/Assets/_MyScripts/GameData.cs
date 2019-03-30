using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string name = "player";
    public int EXP;
    public int level;

    public GameData(int EXP, int level)
    {
        this.EXP = EXP;
        this.level = level;
    }
}
