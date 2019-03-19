﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sniper : SpecialAttack {

    public GameObject SniperOverlay;
    public GameObject SniperPlayer;
    public Camera MainCam;
    public Text eta;
    public float duration;
    public bool running = false;
    public Camera sniperCam;
    public GameObject UI;
    public Transform EffectDropzone;
    public GameObject effectPrefab;

    private int enemiesHit;
    private int expAwarded;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (running)
        {
            //be able to shoot
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("pressed");
                Shoot();
            }
        }
	}

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(sniperCam.transform.position, sniperCam.transform.forward, out hit))
        {
            Debug.Log("Hit " + hit.transform.name);
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<Enemy>().Die();
                enemiesHit++;
                //Debug.Log("HitEnemy");
                if(enemiesHit == 7)
                {
                    DoubleEXP();
                    return;
                }
                AwardEXP();
            }
        }
    }

    void AwardEXP()
    {
        expAwarded++;
        SpawnEffect("+1 EXP");
    }

    void DoubleEXP()
    {
        Debug.Log("Doubling EXP");
        expAwarded *= 2;
        SpawnEffect("X 2");
    }

    void SpawnEffect(string content)
    {
        GameObject effect  = Instantiate(effectPrefab, EffectDropzone);
        effect.GetComponent<Text>().text = content;
        Destroy(effect, 2);
    }

    public override void StartAttack()
    {
        enemiesHit = 0;
        expAwarded = 0;
        hq.PauseMission();
        UIhiddener.HideUI();
        GlobalVariables.objectsAreInteractible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UI.GetComponent<UIManager>().Hide();
        running = true;
        SniperOverlay.SetActive(true);

        SniperPlayer.SetActive(true);
        MainCam.enabled = false;
        StartCoroutine(Countdown());
        //start timer or sth

    }

    private IEnumerator Countdown()
    {
        float normalizedTime = 0;
        while (normalizedTime <= duration)
        {
            //countdownImage.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime;
            float remaining = duration - normalizedTime;
            eta.text = System.String.Format("{0:0.00}", remaining);
            //asign to text
            yield return null;
        }
        StopAttack();
    }

    void StopAttack()
    {

        UIhiddener.ShowUI();
        running = false;
        SniperOverlay.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MainCam.enabled = true;
        SniperPlayer.SetActive(false);
        UI.GetComponent<UIManager>().Show();
        GlobalVariables.objectsAreInteractible = true;
        hq.ResumeMission();
        PlayerStats.instance.AddEXP(expAwarded);
    }
}
