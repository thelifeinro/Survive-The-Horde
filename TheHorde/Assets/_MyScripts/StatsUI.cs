using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour {


    public Text moneyText;
    public Text totalPopText;
    public Text healthyPopText;
    public Text infectedPopText;
    public Text cureText;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moneyText.text = PlayerStats.Money.ToString();
        totalPopText.text = (PlayerStats.HealthyPopulation + PlayerStats.InfectedPopulation).ToString();
        healthyPopText.text = PlayerStats.HealthyPopulation.ToString();
        infectedPopText.text = PlayerStats.InfectedPopulation.ToString();
        cureText.text = PlayerStats.Cure.ToString();
	}
}
