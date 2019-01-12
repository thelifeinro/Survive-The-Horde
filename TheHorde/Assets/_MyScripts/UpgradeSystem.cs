using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour {
    public Animator uiAnimator;
    public Turret turret;
    public Node selectedNode;

    public Text title;
    public Text info1;
    public Text info2;
    public Text info3;

    public Button UpgradeButton;
    public Button SellButton;

    public Text sellWorth;
    public Text lvlPrice;
    public Text nxtLvlText;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetNode(GameObject _node)
    {
        selectedNode = _node.GetComponent<Node>();
        turret = selectedNode.turret.GetComponent<Turret>();
        UpgradeButton.interactable = true;
        UpdateText();
    }

    public void UpdateText()
    {
        
        title.text = turret.name;
        sellWorth.text = "+"+turret.sellworth.ToString();
        info1.text = turret.menCount.ToString();
        info2.text = turret.range.ToString();
        info3.text = turret.rate.ToString();
        nxtLvlText.text = "L"+turret.nextLevel.ToString();
        if (turret.nextLevel >= turret.levels.Length)
        {
            lvlPrice.text = "FULL";
        }
        else
        {
            lvlPrice.text = turret.levels[turret.nextLevel].price.ToString();
            if (turret.levels[turret.nextLevel].locked)
                UpgradeButton.interactable = false;
        }

    }

    public void TriggerUpgrade()
    {
        turret.Upgrade();
        UpdateText();
        selectedNode.DrawRadius();
    }


    public void TriggerSell()
    {
        selectedNode.SellTurret();
        CloseUpgrade();
    }

    public void CloseUpgrade()
    {
        uiAnimator.SetBool("SlideUp", false);
        if(selectedNode!=null)
            selectedNode.EraseRadius();
    }
}
