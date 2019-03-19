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
        PlayerStats.OnEXPChange += EXPChanged;
    }

    // Update is called once per frame
    public void EXPChanged(int count)
    {
        value.text = PlayerStats.EXP.ToString()+"EXP";
    }

    void Update()
    {
        
    }
}
