using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Headquarter : MonoBehaviour
{
    public int MaxMissionTimeSeconds = 120;
    public int TimeReducedByOnePerson = 15;
    public int MaxPeople = 7;
    public int MinPeople = 3;
    public GameObject HQMenu;
    Slider menuSlider;
    public Text NeededTimeText;
    public Text RemainingPeopleText;
    int remainingP = 0;
    int neededT = 0; //misssion time
    public Image timebar;
    public GameObject EventDialoguePrefab;
    public MissionEvent[] missionEvents;
   

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
        Time.timeScale = 1; // unpausing game
    }

    public void StartMission()
    {
        //Check validity of input and show error message if incorrect

        //otherwise, just start mission and reduce population accoringly
        StartCoroutine(Mission());
        Debug.Log("Started Mission");
        Exit(); //TO DO!!! disabble option to open menu while mission under progress
    }


    private IEnumerator Mission()
    {
        float normalizedTime = 0;
        float randomEventTime = Random.Range(0.0f, neededT);

        //get MissionEvent randomly
        MissionEvent me = missionEvents[Random.Range(0, missionEvents.Length)];
        //get MissionOutcome randomly
        MissionEvent.MissionOutcome mo = me.Outcomes[Random.Range(0, me.Outcomes.Length)];


        Debug.Log("event time: "+ randomEventTime +"  neededTime:"+neededT);
        while (normalizedTime < randomEventTime)
        {
            //timebar.fillAmount = normalizedTime / neededT;
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        normalizedTime += Time.deltaTime;
        Debug.Log("reached event");
        //Instantiate dialogue box for event
        GameObject dialogbox = Instantiate(EventDialoguePrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
        MissionEventDialog dialogComponent = dialogbox.GetComponent<MissionEventDialog>();
        dialogComponent.me = me;
        dialogComponent.mo = mo;

        yield return null;

        while (normalizedTime <= neededT)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        //at the end show the Mission Outcome
        Debug.Log("end mission");
        // Instantiate(fxPrefab, transform.position, Quaternion.identity);
        // Destroy(parent);
    }
}
