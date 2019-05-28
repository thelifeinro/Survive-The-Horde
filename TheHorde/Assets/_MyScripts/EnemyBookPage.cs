using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Enemy Page", menuName = "Enemy Book Page")]
public class EnemyBookPage : ScriptableObject
{
    public string Name;
    public Sprite icon;
    public string desc;
    public string weakness;
    public string resistance;
}
