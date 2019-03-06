using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SpecialAttack : MonoBehaviour
{
    public Headquarter hq;
    public SoloOnScreen UIhiddener;
    public float timeBetweenEnables;
    public int price;
    public Button Button;
    public abstract void StartAttack();
}

