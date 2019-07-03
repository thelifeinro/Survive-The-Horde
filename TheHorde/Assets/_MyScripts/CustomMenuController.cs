using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMenuController : MonoBehaviour
{

    public Headquarter HQ;
    public Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        //if component is not in action, open it 
        if (HQ.IsInProgress()) {
            //Debug.Log("Mission in progress!");
            animation.Play("MissionInProgress");
            // play deny animation
        }
        else
        {
            HQ.UpdateValues();
            gameObject.SetActive(true);
            //freeze time; make sure to unfreeze it when exitting the menu!!!
            Time.timeScale = 0;
        }
        
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
