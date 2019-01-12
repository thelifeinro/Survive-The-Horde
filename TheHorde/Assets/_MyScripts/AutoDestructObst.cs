using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoDestructObst : MonoBehaviour {

    public float Lifetime;
    public GameObject fxPrefab;
    public GameObject parent;
    public Image healthbar;
	// Use this for initialization
	void Start () {
        
	}

    private void OnEnable()
    {
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update () {
		
	}

    private IEnumerator Countdown()
    {
        float normalizedTime = 0;
        while (normalizedTime <= Lifetime)
        {
            //countdownImage.fillAmount = normalizedTime;
            healthbar.fillAmount = (Lifetime - normalizedTime) / Lifetime;
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        Instantiate(fxPrefab, transform.position, Quaternion.identity);
        Destroy(parent);
    }
}
