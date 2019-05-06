using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuarantineItem : MonoBehaviour
{
    public Image progressBar;
    public bool heal = false;
    public bool kill = false;
    public Button healBtn;
    public Button killBtn;
    public Text killPriceText;
    int killPrice;
    Quarantine quarantine;

    // Start is called before the first frame update
    void Start()
    {
        quarantine = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Quarantine>();
        killPrice = quarantine.GetKillPrice();
        killPriceText.text = killPrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //check if you can afford the scraps to kill person; if not: button stays disabled
        if (PlayerStats.Money < killPrice)
        {
            killBtn.interactable = false;
        }
        else
        {
            killBtn.interactable = true;
        }

        if (PlayerStats.Cure < 1)
        {
            healBtn.interactable = false;
        }
        else
        {
            healBtn.interactable = true;
        }
    }

    public void KillInfected()
    {
        kill = true;
        PlayerStats.instance.KillOneInfected(killPrice);
        quarantine.CheckOut();
    }

    public void HealInfected()
    {
        heal = true;
        PlayerStats.instance.CureOne();
        quarantine.CheckOut();
    }
}