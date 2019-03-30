using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {
    public int level;
    public static int Money;
    public static int HealthyPopulation;
    public static int InfectedPopulation;
    public static int Cure;
    public static int EXP = SaveManager.EXP;
    public int startCure = 1;
    public int startMoney = 500;
    public int startPopulation = 500;
    public int baseLevelEXP;
    public int bonusThreshold;
    public GameObject gameOverPrefab;

    public delegate void ExpEventHandler(int count);
    public static event ExpEventHandler OnEXPChange;

    public Dictionary<MilestoneType, int> unlockedLevels = new Dictionary<MilestoneType, int>()
{
      {MilestoneType.arrow, 0},
      {MilestoneType.rocket, 0},
      {MilestoneType.water, 0},
      {MilestoneType.turntime, 0}
};

    public static PlayerStats instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one PlayerStats in this scene!");
        }
        instance = this;
    }

    void Update()
    {
    }

    public void LevelComplete()
    {
        int awardedEXP = baseLevelEXP;
        if(HealthyPopulation > bonusThreshold)
        {
            awardedEXP += 5 * (HealthyPopulation - bonusThreshold);
        }
        AddEXP(awardedEXP);

        //unlock next level
        Save();
        
    }

    public void IsGameOver()
    {
        if (HealthyPopulation + InfectedPopulation == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Instantiate(gameOverPrefab, Vector3.zero, Quaternion.identity);
        Time.timeScale = 0;
        //instantiate animation;
        //saving won exp during battle
        Save();
    }

    public void Save()
    {
        // MARE ATENTIE! suprascrie highestLevel la cel al nivelului asta, daca faci replay la un nivel anterior
        SaveManager.SaveGame(EXP,level);
        Debug.Log("Saved game. EXP:" + SaveManager.LoadGame().EXP);
    }

    private void Start()
    {
        Money = startMoney;
        HealthyPopulation = startPopulation;
        InfectedPopulation = 0;
        Cure = startCure;
    }


    public void AddEXP(int count)
    {
        EXP += count;
        OnEXPChange(count);
    }

    public void Kill(int count)
    {
        HealthyPopulation -= count;
        IsGameOver();
    }

    public void KillOneInfected(int price)
    {
        InfectedPopulation--;
        Money -= price;
        IsGameOver();
    }

    public int CountHealthyPpl()
    {
        return HealthyPopulation;
    }

    public void AddPeople(int count)
    {
        HealthyPopulation += count;
    }

    public void Infect(int count)
    {
        HealthyPopulation -= count;
        InfectedPopulation += count;
    }

    public void AddMoney(int amount)
    {
        Money += amount;
    }

    public void AddCure(int amount)
    {
        Cure += amount;
    }

    public void CureOne()
    {
        if (Cure > 0)
        {
            if (InfectedPopulation >= 1)
            {
                InfectedPopulation--;
                HealthyPopulation++;
                Cure--;
            }
        }
    }
}
