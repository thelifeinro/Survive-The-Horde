using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarantineUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    // Update is called once per frame
    public void Toggle()
    {
        if (anim.GetBool("Extended") == true)
        {
            anim.SetBool("Extended", false);
        }
        else
        {
            anim.SetBool("Extended", true);
        }
    }
}
