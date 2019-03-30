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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Populate()
    {
        foreach(LevelInfo l in linfos)
        {
            GameObject ui = Instantiate(levelBtnPrefab, panel.transform);
            LevelButton lbtnUI = ui.GetComponent<LevelButton>();
            lbtnUI.SetButton(l);
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
}
