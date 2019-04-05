using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPButton : MonoBehaviour
{
    public Text value;
    // Start is called before the first frame update
    void Start()
    {
        value.text = PlayerStats.EXP.ToString() + "EXP";
    }

    void OnDisable()
    {
        PlayerStats.OnEXPChange -= EXPChanged;
    }

    void OnEnable()
    {
        PlayerStats.OnEXPChange += EXPChanged;
    }

    public void EXPChanged(int count)
    {
        Debug.Log("EXP BUTTON CHANGED");
        value.text = PlayerStats.EXP.ToString()+"EXP";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
