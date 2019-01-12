using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SpecialAttack : MonoBehaviour
{
        public float timeBetweenEnables;
        public int price;
        public Button Button;
        public abstract void StartAttack();
}

