using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingBlade : SpecialAttack
{
    public Camera MainCam;
    public Text eta;
    public GameObject BladeOverlay;
    public float duration;
    public GameObject bladePrefab;
    private GameObject instanceBlade;
    public override void StartAttack()
    {
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
        
        BladeOverlay.SetActive(false);
        Destroy(instanceBlade);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        MainCam.enabled = true;
        StopCoroutine(Countdown());

}
    // Update is called once per frame
    void Update () {
		
	}
}
