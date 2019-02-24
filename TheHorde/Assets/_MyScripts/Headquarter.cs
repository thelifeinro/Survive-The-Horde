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
    //int scrapsToBeGained;
    int selectedPeople;

    int peopleOnMission;
    float scrapsOnMission = 0.0f;

    public int scrapsPerPerson;
    public Image timebar;
    public GameObject EventDialoguePrefab;
    public MissionEvent[] missionEvents;

    MissionEvent me;
    MissionEvent.MissionOutcome mo;



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
        selectedPeople = (int)menuSlider.value;
        //scrapsToBeGained = scrapsPerPerson * selectedPeople;
    }

    


    public void Exit()
    {
        HQMenu.SetActive(false);
        Time.timeScale = 1; // unpausing game
    }

    public void StartMission()
    {
        //Check validity of input and show error message if incorrect

        //Check if enough available people
        if (PlayerStats.HealthyPopulation - selectedPeople < 1)
        {
            Debug.Log("Not enough people");
            //activate red text on ui element
        }
        else
        {
            //otherwise, just start mission and reduce population accoringly
            //"Killing" leaving people
            peopleOnMission = selectedPeople;
            scrapsOnMission = 0.0f;
            PlayerStats.instance.Kill(selectedPeople);

            StartCoroutine(Mission());
            Debug.Log("Started Mission");
            Exit(); //TO DO!!! disabble option to open menu while mission under progress
        }
    }

    public void EndMission()
    {
        //adding gains to PlayerStats
        PlayerStats.instance.AddPeople(peopleOnMission);
        PlayerStats.instance.AddMoney((int)scrapsOnMission);
    }

    public void EndEvent()
    {
        //adding event changes to mission stats
        peopleOnMission -= mo.peopleLost;
        peopleOnMission += mo.addedPeople;
        scrapsOnMission = mo.addedScraps;
    }


    private IEnumerator Mission()
    {
        float normalizedTime = 0;
        float randomEventTime = Random.Range(0.0f, neededT);

        //get MissionEvent randomly
        me = missionEvents[Random.Range(0, missionEvents.Length)];
        //get MissionOutcome randomly
        mo = me.Outcomes[Random.Range(0, me.Outcomes.Length)];


        Debug.Log("event time: "+ randomEventTime +"  neededTime:"+neededT);
        while (normalizedTime < randomEventTime)
        {
            //timebar.fillAmount = normalizedTime / neededT;
            normalizedTime += Time.deltaTime;
            yield return null;
        }

        //Random Event HAPPENS HERE
        normalizedTime += Time.deltaTime;
        Debug.Log("reached event");
        //Instantiate dialogue box for event
        GameObject dialogbox = Instantiate(EventDialoguePrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
        MissionEventDialog dialogComponent = dialogbox.GetComponent<MissionEventDialog>();
        dialogComponent.me = me;
        dialogComponent.mo = mo;
        yield return null;
        EndEvent();

        // REST OF THE COUNTDOWN HAPPENS HERE
        while (normalizedTime <= neededT)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        // END OF MISSION IS HERE
        //at the end show the Mission Outcome
        EndMission();
        Debug.Log("end mission");
        // Instantiate(fxPrefab, transform.position, Quaternion.identity);
        // Destroy(parent);
    }
}
