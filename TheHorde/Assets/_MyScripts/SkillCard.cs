using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    public SkillManager.Skill skill;
    public EXPMilestone content;
    public Image img;
    public Button buyBtn;
    public Text title;
    public Text desc;
    public Text price;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(content != null)
        {
            if(skill.locked == false && skill.equipped == false && PlayerStats.EXP < content.milestoneEXP)
            {
                buyBtn.interactable = false;
            }

            if (skill.equipped)
            {
                buyBtn.interactable = false;
                Text buttonText = buyBtn.GetComponentInChildren<Text>();
                buttonText.text = "ACQUIRED";
            }
        }
    }

    public void SetUpCard(SkillManager.Skill skill)
    {
        this.skill = skill;
        this.content = skill.content;
        title.text = content.title;
        desc.text = content.description;
        price.text = content.milestoneEXP + " EXP";
        img.sprite = content.icon;

        //modify button & add listener to button
        buyBtn.onClick.AddListener(() => SkillManager.BuySkill(skill));

        if (skill.locked)
        {
            buyBtn.interactable = false;
            Text buttonText = buyBtn.GetComponentInChildren<Text>();
            buttonText.text = "LOCKED";
        }

    }
}
