using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilestoneManager : MonoBehaviour
{
    public EXPMilestone[] milestones;
    List<GameObject> UImilestoneElements = new List<GameObject>();
    public GameObject msUIPrefab;
    private Slider milestonesBar;
    public Text amountText;
    public GameObject UIElement;
    public GameObject unlockAnimationPrefab;

    public delegate void UnlockEventHandler(EXPMilestone ms);
    public static event UnlockEventHandler OnUpgradeUnlock;

    public int nextMs = 0;

    // Start is called before the first frame update
    void Start()
    {
        milestonesBar = GameObject.FindGameObjectWithTag("MilestoneUI").GetComponent<Slider>();
        //populam meniul cu butoane milestone la locatia corecta de pe scrollbar
        PopulateMilestoneUI();
        // da-ti seama ce Milestones sunt deja unlocked de la inceput si seteaza PlayerStats milestones accordingly
        UnlockMilestones();

        SetAmount(PlayerStats.EXP);

        //abonare la exp change
        PlayerStats.OnEXPChange += EXPChanged;
        CloseUI();
    }

    void OnDisable()
    {
        PlayerStats.OnEXPChange -= EXPChanged;
    }




    /// <summary>
    ///Manager is subscribed to EXP changes;
    ///On each exp change it checks if nextMilestone to be reached has been reached
    ///When milestone is reached it triggers OnUpgradeUnlock()
    /// </summary>
    public void PopulateMilestoneUI()
    {
        GameObject milestonesParent = GameObject.FindGameObjectWithTag("MilestoneUI");
        if (milestonesParent == null)
            return;
        foreach(EXPMilestone ms in milestones)
        {
            GameObject msUI = Instantiate(msUIPrefab, milestonesParent.transform);
            UIMilestonePoint msLogic = msUI.GetComponent<UIMilestonePoint>();
            msLogic.SetMilestone(ms);
            UImilestoneElements.Add(msUI);

        }
    }
    public void UnlockMilestones()
    {
        foreach(EXPMilestone mst in milestones)
        {
            if(PlayerStats.EXP >= mst.milestoneEXP)
            {
                UnlockMilestone(mst, true);
            }
        }
    }

    public void UnlockMilestone(EXPMilestone mst, bool initial = false)
    {
        if (PlayerStats.EXP >= mst.milestoneEXP)
        {
            PlayerStats.instance.unlockedLevels[mst.type] = mst.level;
            if (OnUpgradeUnlock != null)
            {
                //letting subscribers know a milestone has been reached
                OnUpgradeUnlock(mst);
            }
            //if this is a new upgrade
            if(!initial)
                {
                //we can instantiate an animated message prefab 
                GameObject anim = Instantiate(unlockAnimationPrefab, GameObject.FindGameObjectWithTag("MainUI").transform);
                Destroy(anim, 3);
                }
            nextMs++;
        }
    }

    public void EXPChanged(int count)
    {
        Debug.Log(gameObject.name + "EXPchange event triggered");
        //trying to unlock next milestone
        while(nextMs < milestones.Length && milestones[nextMs].milestoneEXP <= PlayerStats.EXP)
            UnlockMilestone(milestones[nextMs]);
        // upgrade scrollbar
        SetAmount(PlayerStats.EXP);

    }

    public void SetAmount(int amount)
    {
        milestonesBar.value = Mathf.Min(PlayerStats.EXP, milestonesBar.maxValue);
        amountText.text = amount.ToString()+" EXP";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUI()
    {
        UIElement.SetActive(true);
    }

    public void CloseUI()
    {
        UIElement.SetActive(false);
    }
}
