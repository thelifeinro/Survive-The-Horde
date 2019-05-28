using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Napalm : SpecialAttack {

    public GameObject projectilePrefab;
    public GameObject AimEffect;
    public float HeightToDrop = 70;
    public bool activeAim = false;
    public GameObject NapalmOverlay;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (activeAim && Input.GetKeyDown(KeyCode.E))
        {
                activeAim = false;
                Vector3 position = new Vector3(AimEffect.transform.position.x, HeightToDrop, AimEffect.transform.position.z);
                Instantiate(projectilePrefab, position, Quaternion.identity);
                StopAttack();
        }
	}

    public override void StartAttack()
    {
        //tell BookOfEnemies it's special attack time so it doesn't show up info about enemies; do this for all special attacks
        GameObject.FindGameObjectWithTag("EnemyBook").GetComponent<EnemyBook>().specialAttack = true;
        hq.PauseMission();
        NapalmOverlay.SetActive(true);
        UIhiddener.HideUI();
        GlobalVariables.objectsAreInteractible = false;
        activeAim = true;
        AimEffect.SetActive(true);
    } 

    public void StopAttack()
    {
        NapalmOverlay.SetActive(false);
        UIhiddener.ShowUI();
        GlobalVariables.objectsAreInteractible = true;
        activeAim = false;
        AimEffect.SetActive(false);
        hq.ResumeMission();
        GameObject.FindGameObjectWithTag("EnemyBook").GetComponent<EnemyBook>().specialAttack = false;

    }

}
