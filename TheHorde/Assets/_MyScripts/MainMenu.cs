using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button continueButton;
    public Text continueText;
    public AudioSource continueSound;
    bool LoadingInitiated = false;
    public GameObject btnPressPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        //telling Soundtrack to play menuTrack
        Soundtrack st = GameObject.FindObjectOfType<Soundtrack>();
        st.PlayLevelTrack(false);

        if (!SaveManager.IsSaveFile()){
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

        SaveManager.NewGame(); //overwrites the save file and sets exp back to zero and leveel to 1
        Continue();
    }



    public void Continue()
    {
        if (!LoadingInitiated)
        {
            StartCoroutine(DelayedContinue());
            LoadingInitiated = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator DelayedContinue()
    {
        continueSound.Play();
        Instantiate(btnPressPrefab);
        //Wait until clip finish playing
        yield return new WaitForSeconds(continueSound.clip.length/3);

        //Load scene here
        SaveManager.LoadGame();
        SceneManager.LoadScene("LevelSelector", LoadSceneMode.Single);

    }
}
