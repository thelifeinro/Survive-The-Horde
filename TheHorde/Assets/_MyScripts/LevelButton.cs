using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public LevelInfo lvl;
    public Text titleUI;
    public Text noUI;
    LevelController levelController;
    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetButton(LevelInfo linfo)
    {
        lvl = linfo;
        titleUI.text = "Level " + linfo.index.ToString() + " - " + linfo.title;
        noUI.text = linfo.index.ToString();
    }

    public void LoadLevel()
    {
        //make loading screen
        Debug.Log("Should be laoding scene " + lvl.scene.handle);

        levelController.LoadLevel(lvl.scene.handle);
    }

}
