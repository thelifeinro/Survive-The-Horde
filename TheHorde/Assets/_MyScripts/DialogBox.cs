using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0; //stopping time
    }

    // Update is called once per frame
    public void Exit()
    {
        Destroy(gameObject);
        //restoring time
        Time.timeScale = 1;
    }
}
