using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterMainMenu : MonoBehaviour
{
    public Animator camAnimator;
    public GameObject uiHelpText;
    public GameObject mainmenu;
    // Start is called before the first frame update
    void Start()
    {
        mainmenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            camAnimator.SetBool("Entered", true);
            this.enabled = false;
            uiHelpText.SetActive(false);
            mainmenu.SetActive(true);
        }
    }

    public void Debugg()
    {
        Debug.Log("pressed");
    }

    //PROVIZORIU
    public void LoadALevel()
    {
        SceneManager.LoadScene("TheWoods_Terrain", LoadSceneMode.Single);
    }
}
