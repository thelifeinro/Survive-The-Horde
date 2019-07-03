using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIMilestonePoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    public Text tooltipTitle;
    public Text tooltipDesc;
    public EXPMilestone milestoneData;

    // Start is called before the first frame update
    void Start()
    {
        if(milestoneData.milestoneEXP > PlayerStats.EXP)
            gameObject.GetComponent<Button>().interactable = false;
        //subscribing to UpgradeUnlocked event
        MilestoneManager.OnUpgradeUnlock += UpgradeUnlocked;
    }

    void OnDisable()
    {
        MilestoneManager.OnUpgradeUnlock -= UpgradeUnlocked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpgradeUnlocked(EXPMilestone ms)
    {
        //Debug.Log(gameObject.name + "Milestone point received upgradeunlocked");
        //if you are the unlocked upgrade, set button as interactible so it doesn't look greyed out
        if(ms.milestoneEXP == milestoneData.milestoneEXP)
            gameObject.GetComponent<Button>().interactable = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }

    public void SetTooltip(string title, int exp, string description)
    {
        tooltipTitle.text = title + " - " + exp + "EXP";
        tooltipDesc.text = description;
    }

    public void SetPosition(float exp)
    {
        RectTransform parentTransform = gameObject.transform.parent.GetComponent<RectTransform>();
        float positionProc = exp / 3500;
        RectTransform myTransform = gameObject.GetComponent<RectTransform>();
        Vector2 pos = myTransform.anchoredPosition;
        myTransform.anchoredPosition = new Vector2 (-parentTransform.rect.width/2 + positionProc * parentTransform.rect.width, pos.y);

    }

    public void SetMilestone(EXPMilestone data)
    {
        milestoneData = data;
        SetTooltip(data.title, data.milestoneEXP, data.description);
        SetPosition(data.milestoneEXP);
    }
}
