using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingBlade : SpecialAttack
{
    Camera MainCam;
    public Text eta;
    public GameObject BladeOverlay;
    public float duration;
    public GameObject bladePrefab;
    private GameObject instanceBlade;
    public int awardedExp = 0;

    public void Start()
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public override void StartAttack()
    {
        hq.PauseMission();
        UIhiddener.HideUI();
        GlobalVariables.objectsAreInteractible = false;
        BladeOverlay.SetActive(true);
        instanceBlade = Instantiate(bladePrefab, bladePrefab.transform.position, Quaternion.identity);
        StartCoroutine(Countdown());
    }


   public IEnumerator Countdown()
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

    public void StopAttack()
    {
        UIhiddener.ShowUI();
        PlayerStats.instance.AddEXP(awardedExp);
       

        BladeOverlay.SetActive(false);
        Destroy(instanceBlade);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        MainCam.enabled = true;
        StopCoroutine(Countdown());
        GlobalVariables.objectsAreInteractible = true;
        hq.ResumeMission();

    }
    // Update is called once per frame
    void Update () {
		
	}
}
