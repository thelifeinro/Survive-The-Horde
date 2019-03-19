using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilestoneManager : MonoBehaviour
{
    public EXPMilestone[] milestones;

    public delegate void UnlockEventHandler(EXPMilestone ms);
    public static event UnlockEventHandler OnUpgradeUnlock;
    public int nextMs = 0;

    // Start is called before the first frame update
    void Start()
    {
        //abonare la exp change

        // da-ti seama ce Milestones sunt deja unlocked de la inceput si seteaza PlayerStats milestones accordingly
        UnlockMilestones();
        PlayerStats.OnEXPChange += EXPChanged;
    }

    /// <summary>
    ///Manager is subscribed to EXP changes;
    ///On each exp change it checks if nextMilestone to be reached has been reached
    ///When milestone is reached it triggers OnUpgradeUnlock()
    /// </summary>

    public void UnlockMilestones()
    {
        foreach(EXPMilestone mst in milestones)
        {
            if(PlayerStats.EXP >= mst.milestoneEXP)
            {
                UnlockMilestone(mst);
            }
        }
    }

    public void UnlockMilestone(EXPMilestone mst)
    {
        if (PlayerStats.EXP >= mst.milestoneEXP)
        {
            PlayerStats.instance.unlockedLevels[mst.type] = mst.level;
            if (OnUpgradeUnlock != null)
            {
                //letting subscribers know a milestone has been reached
                OnUpgradeUnlock(mst);
            }
            nextMs++;
        }
    }

    public void EXPChanged(int count)
    {
        Debug.Log(gameObject.name + "EXPchange event triggered");
        //trying to unlock next milestone
        UnlockMilestone(milestones[nextMs]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
