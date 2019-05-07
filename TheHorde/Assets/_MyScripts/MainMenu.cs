using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button continueButton;
    public Text continueText;
    // Start is called before the first frame update
    void Awake()
    {
        if(!SaveManager.IsSaveFile()){
            continueButton.interactable = false;
            Debug.Log("Disabling continue button");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!SaveManager.IsSaveFile())
        {
            continueButton.interactable = false;
            continueText.color = new Color(1f,1f,1f,0.4f);
        }
    }

    public void NewGame()
    {

        SaveManager.SaveGame(0, 1, false, 0, 0,0 ,0); //overwrites the save file and sets exp back to zero and leveel to 1
    }



    public void Continue()
    {
        SaveManager.LoadGame();
        //provizoriu: aici o sa incarce level selector
        SceneManager.LoadScene("LevelSelector", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
