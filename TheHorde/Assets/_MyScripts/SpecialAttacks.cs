using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttacks : MonoBehaviour {

    public SpecialAttack MainAtt;
    public SpecialAttack Other1;
    public SpecialAttack Other2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator AttCountdown(SpecialAttack att)
    {
        float normalizedTime = 0;
        while (normalizedTime <= att.timeBetweenEnables)
        {
            //countdownImage.fillAmount = normalizedTime;
            att.Button.image.fillAmount = normalizedTime / att.timeBetweenEnables;
            normalizedTime += Time.deltaTime;
            float remaining = att.timeBetweenEnables - normalizedTime;
            //asign to text
            yield return null;
        }
        att.Button.interactable = true;
    }

    public void MainAttack()
    {
        if (PlayerStats.Money >= MainAtt.price)
        {
            PlayerStats.Money -= MainAtt.price;
            MainAtt.StartAttack();
            StartCoroutine(AttCountdown(MainAtt));
            MainAtt.Button.interactable = false;
        }
    }
    public void OtherAttack1()
    {
        if (PlayerStats.Money >= Other1.price)
        {
            PlayerStats.Money -= Other1.price;
            Other1.StartAttack();
            StartCoroutine(AttCountdown(Other1));
            Other1.Button.interactable = false;
        }
    }
    public void OtherAttack2()
    {
        if (PlayerStats.Money >= Other2.price)
        {
            PlayerStats.Money -= Other2.price;
            Other2.StartAttack();
            StartCoroutine(AttCountdown(Other2));
            Other2.Button.interactable = false;
        }
    }
}
