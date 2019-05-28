using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quarantine : MonoBehaviour
{
    public int baseTimeToTurn;
    public int timeToTurnSeconds;
    public int count;
    private int killPrice = 0;
    public GameObject containerList;
    public GameObject elementPrefab;
    public GameObject zombiePrefab;
    Transform zombieSpawnPoint;
    public Text countText;
    public GameObject Warning;
    private bool isWarningUp = false;
    // Start is called before the first frame update
    void Start()
    {
        timeToTurnSeconds = baseTimeToTurn;
        count = 0;
        countText.text = count.ToString();
        zombieSpawnPoint = GameObject.FindGameObjectWithTag("Quarant").transform;
        UnlockLevels(PlayerStats.instance.unlockedLevels[MilestoneType.turntime]); // adding unlocked upgrads to base turn time
        PlayerStats.OnUpgradeUnlock += UpgradeUnlocked; // subscribing quarantine to milstone manager so it stays uptodate with the upgrades
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnEnable()
    {
        PlayerStats.OnUpgradeUnlock += UpgradeUnlocked;
    }
    public void OnDisable()
    {
        PlayerStats.OnUpgradeUnlock -= UpgradeUnlocked;
    }

    public void UpgradeUnlocked(EXPMilestone mst)
    {
        //Debug.Log(gameObject.name + " Quarantine upgrade event triggered");
        if (mst.type == MilestoneType.turntime)
            AddTurnTimeSeconds(mst.level);
    }

    public void UnlockLevels(int i)
    {
        AddTurnTimeSeconds(i);
    }

    public void AddTurnTimeSeconds(int k)
    {
        timeToTurnSeconds = baseTimeToTurn + k;
    }

    public void CheckIn()
    {
        if (!isWarningUp)
            ShowWarning();
        StartCoroutine(InfectedPersonTurning());
        count++;
        countText.text = count.ToString();
    }

    public void CheckOut()
    {
        count--;
        if (count == 0)
            HideWarning();
        countText.text = count.ToString();
    }
    
    public void ShowWarning()
    {
        Warning.SetActive(true);
        isWarningUp = true;
    }

    public void HideWarning()
    {
        Warning.SetActive(false);
        isWarningUp = false;
    }
    public void SpawnZombie()
    {
        PlayerStats.instance.KillOneInfected(killPrice);
        CheckOut();
        Debug.Log("QUARANTINE: Should be spawning a zombie");
        //instantiate at zombie spawn point
        Instantiate(zombiePrefab, zombieSpawnPoint.transform.position, zombieSpawnPoint.transform.rotation);
        //TO DO: maybe show a notification on screen
    }

    IEnumerator InfectedPersonTurning()
    {
        GameObject element = Instantiate(elementPrefab, containerList.transform);
        QuarantineItem qi = element.GetComponent<QuarantineItem>();
        float normalizedTime = 0;
        while (normalizedTime <= timeToTurnSeconds)
        {
            qi.progressBar.fillAmount = normalizedTime / timeToTurnSeconds;
            normalizedTime += Time.deltaTime;
            if (qi.heal == true || qi.kill == true)
            {
                //destroying element from list
                Destroy(element);
                yield break;
            }
            yield return null;
        }
        //if it gets here, it has to turn to a zombie
        SpawnZombie();
        Destroy(element);
    }

    public int GetKillPrice()
    {
        return killPrice;
    }
}