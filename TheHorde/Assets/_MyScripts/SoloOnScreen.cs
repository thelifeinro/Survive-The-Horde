using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloOnScreen : MonoBehaviour
{
    GameObject mainUI;
    // Start is called before the first frame update
    private void Start()
    {
        mainUI = GameObject.FindGameObjectWithTag("MainUI");
    }
    public void HideUI()
    {
        //Debug.Log("Hiding UI");
        mainUI.SetActive(false);
    }

    public void ShowUI()
    {
       // Debug.Log("Showing UI");
        mainUI.SetActive(true);
    }
}
