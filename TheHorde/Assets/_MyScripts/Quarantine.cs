using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quarantine : MonoBehaviour
{
    public int timeToTurnSeconds;
    public int count;
    public int killPrice;
    public GameObject containerList;
    public GameObject elementPrefab;
    public GameObject zombiePrefab;
    Transform zombieSpawnPoint;
    public Text countText;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        countText.text = count.ToString();
        zombieSpawnPoint = GameObject.FindGameObjectWithTag("Quarant").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckIn()
    {
       StartCoroutine(InfectedPersonTurning());
        count++;
        countText.text = count.ToString();
    }

    public void CheckOut()
    {
        count--;
        countText.text = count.ToString();
    }

    public void SpawnZombie() {
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
            if (qi.heal == true || qi.kill == true) {
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
    
}
