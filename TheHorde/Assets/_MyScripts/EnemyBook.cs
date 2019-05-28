using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBook : MonoBehaviour
{
    public EnemyBookPage[] pages;
    private GameObject windowInstance;
    public GameObject windowPrefab;
    private GameObject mainUI;
    EnemyPageUI pageUI;

    public delegate void ExpEventHandler(int count);
    public static event ExpEventHandler OnEXPChange;
    public bool specialAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        mainUI = GameObject.FindGameObjectWithTag("MainUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateWindow() {
        if (windowInstance == null)
        {
            windowInstance = Instantiate(windowPrefab, mainUI.transform);
            pageUI = windowInstance.GetComponent<EnemyPageUI>();
        }
    }

    public EnemyBookPage GetPage(string name) {
        foreach(EnemyBookPage p in pages)
        {
            if (p.Name == name) {
                return p;
            }
        }
        return null;
    }

    public void ShowEnemyStats(string name) {
        if (!specialAttack)
        {
            CreateWindow();
            pageUI.SetUp(GetPage(name));
        }
    }
}
