using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureSpawner : MonoBehaviour {
    public GameObject curePrefab;
    public float minTime;
    public float maxTime;
    public float spawnerRadius;
    private bool counting = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!counting)
        {
            counting = true;
            float time = Random.Range(minTime, maxTime);
            StartCoroutine(Countdown(time));
        }
        else return;
	}

    private IEnumerator Countdown(float amount)
    {
        float normalizedTime = 0;
        while (normalizedTime <= amount)
        {
            //countdownImage.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        //spawn cure
        Instantiate(curePrefab, transform.position + new Vector3(Random.Range(0, spawnerRadius),0, Random.Range(0, spawnerRadius)), Quaternion.identity);
        counting = false;
    }
}
