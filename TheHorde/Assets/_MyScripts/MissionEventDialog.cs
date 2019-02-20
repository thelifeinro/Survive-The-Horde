﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionEventDialog : DialogBox
{
    // Start is called before the first frame update
    public MissionEvent me;
    public MissionEvent.MissionOutcome mo;
    public Text content;
    public GameObject yes;
    public GameObject no;
    public GameObject ok;

    private void Start()
    {
        content.text = me.EventQuestion;
        ok.SetActive(false);
    }
    // Update is called once per frame
    public void Yes()
    {
        //hide yes and no buttons; show OK button; show outcome text
        yes.SetActive(false);
        no.SetActive(false);
        ok.SetActive(true);
        content.text = mo.Text;
        //Exit();
    }
}
