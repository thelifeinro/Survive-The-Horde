using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MilestoneType
{
    turntime, arrow, water, rocket
}

[CreateAssetMenu(fileName = "New EXPMilestone", menuName = "EXP Milestone")]
public class EXPMilestone : ScriptableObject
{
    public string title;
    public string description;
    public MilestoneType type;
    public int milestoneEXP; //price
    public int level;
    public Sprite icon;
}
