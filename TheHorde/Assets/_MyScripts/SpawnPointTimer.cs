using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPointTimer : MonoBehaviour {
    WaveSpawner waveSpawner;
    public Image timeFill;
    public CanvasGroup canvas;
	// Use this for initialization
	void Start () {
        waveSpawner = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<WaveSpawner>();
	}
	
	// Update is called once per frame
	void Update () {
        float amount = waveSpawner.waveCountdown / waveSpawner.timeBetweenWaves;
        if(amount <= 0.01f)
        {
            canvas.alpha = 0;
            //Destroy(gameObject);
        }
        else
        {
            canvas.alpha = 1;
            timeFill.fillAmount = amount;
        }

    }
}
