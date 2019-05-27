﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeyLoadLevelSelector : MonoBehaviour
{
    public bool waitIsOver = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitUp());
    }

    // Update is called once per frame
    void Update()
    {
        if (waitIsOver)
        {
            if (Input.anyKey)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("LevelSelector");
            }
        }
    }

    private IEnumerator waitUp()
    {
        yield return new WaitForSeconds(2f);
        waitIsOver = true;
    }
}
