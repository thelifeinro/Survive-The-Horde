﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenuByTag : MonoBehaviour
{
    public string Tag;
    GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        //!!! panel must be active in the editor to be found by its tag
        panel = GameObject.FindGameObjectWithTag(Tag);
        panel.SetActive(false);
    }

    void OnMouseDown()
    {
        panel.SetActive(true);

        //freeze time; make sure to unfreeze it when exitting the menu!!!
        Time.timeScale = 0;
    }

}
