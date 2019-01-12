using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Headquarter : MonoBehaviour
{
    public int MaxMissionTimeSeconds = 120;
    public int TimeReducedByOnePerson = 15;
    public int MaxPeople = 6;
    public int MinPeople = 2;
    public GameObject HQMenu;
    Slider menuSlider;
    public Text NeededTimeText;
    public Text RemainingPeopleText;
    int remainingP = 0;
    int neededT = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        //HQMenu = GameObject.FindGameObjectWithTag("HQMenu");
        menuSlider = HQMenu.GetComponentInChildren<Slider>();
        menuSlider.maxValue = MaxPeople;
        menuSlider.minValue = MinPeople;
        menuSlider.onValueChanged.AddListener(delegate { UpdateValues(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateValues()
    {
        Debug.Log("Value is" + menuSlider.value);
        CalculateValues();
        RemainingPeopleText.text = remainingP.ToString();
        NeededTimeText.text = neededT.ToString() + "s";
    }

    void CalculateValues()
    {
        remainingP = PlayerStats.HealthyPopulation - (int)menuSlider.value;
        neededT = MaxMissionTimeSeconds - TimeReducedByOnePerson * ((int)menuSlider.value - MinPeople);

    }

    public void Exit()
    {
        HQMenu.SetActive(false);
    }

    public void StartMission()
    {
        //Check validity of input and show error message if incorrect

        //otherwise, just start mission and reduce population accoringly
        Debug.Log("Started Mission");
    }
}
