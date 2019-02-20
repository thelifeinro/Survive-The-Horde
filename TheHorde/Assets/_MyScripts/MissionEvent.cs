using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Mission Event", menuName = "Mission Event")]
public class MissionEvent : ScriptableObject
{
    public string EventQuestion;
    [System.Serializable]
    public class MissionOutcome
    {
        public string Text;
        [Header("Maxim 2; Daca moare un om de-al tau sau nu")]
        public int peopleLost; //maxim 2
        public int addedPeople; //number of new people found
        public int addedScraps; // number of scraps won
    }
    [Header("Outcomes")]
    public MissionOutcome[] Outcomes;
    

    //intrebari gen: go inside building?
    // raspuns: ok, sau no
    //2 outcome-uri pentru ok, unul pozitiv si unul negativ;

    
}
