using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public LevelInfo[] linfos;
    public GameObject levelBtnPrefab;
    public GameObject panel;
    public GameObject LoadingScreen;
    public Slider progBar;
    // Start is called before the first frame update
    void Start()
    {
        Populate();

        Cursor.lockState = CursorLockMode.None;
        // Hide cursor when locking
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Populate()
    {
        Debug.Log("highestLevel: " + SaveManager.highestLevel);
        foreach(LevelInfo l in linfos)
        {
            GameObject ui = Instantiate(levelBtnPrefab, panel.transform);
            LevelButton lbtnUI = ui.GetComponent<LevelButton>();
            lbtnUI.SetButton(l);
            //checking if it should be locked
            if(l.scene.handle > SaveManager.highestLevel)
            {
                lbtnUI.btn.interactable = false;
            }
        }
    }

    public void LoadLevel(int index)
    {
        //make loading screen
        Debug.Log("Should be laoding scene " + index);
        StartCoroutine(LoadAsync(index));
    }

    IEnumerator LoadAsync(int index)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(index);
        LoadingScreen.SetActive(true);
        //enable loading screen
        while (!op.isDone)
        {

            float progress = Mathf.Clamp01(op.progress / 0.9f);
            progBar.value = progress;
            yield return null;
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
}
