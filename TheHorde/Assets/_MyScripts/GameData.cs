using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string name = "player";
    public int EXP;
    public int level;
    public int aLev;
    public int rLev;
    public int wLev;
    public int tLev;

    public GameData(int EXP, int level, int aLev, int rLev, int wLev, int tLev)
    {
        this.EXP = EXP;
        this.level = level;
        this.aLev = aLev;
        this.rLev = rLev;
        this.wLev = wLev;
        this.tLev = tLev;
    }
}
