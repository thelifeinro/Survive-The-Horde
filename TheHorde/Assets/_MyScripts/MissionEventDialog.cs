using System.Collections;
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
        //WTF HAPPENS IF U IN THE MIDDLE OF A SPECIAL ATTACK?

        Time.timeScale = 0; //stopping time
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
        content.text = mo.Text+"\nLost People: "+mo.peopleLost+"\nNew People: "+ mo.addedPeople+"\nScraps Brought: "+mo.addedScraps;
        //Exit();
    }
    public void OK()
    {
        //ending event
        GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Headquarter>().EndEvent();
        Exit();
    }
}
