using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    bool menuHidden = true;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuHidden)
            {
                ShowMenu();
            }
            else
            {
                HideMenu();
            }
        }
    }

    public void ShowMenu()
    {
        pauseMenu.SetActive(true);
        menuHidden = false;
        Time.timeScale = 0;
    }

    public void HideMenu()
    {
        pauseMenu.SetActive(false);
        menuHidden = true;
        Time.timeScale = 1;
    }

    public void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        HideMenu();
    }

}
