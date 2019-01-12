using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour {

    public TurretBlueprint CrossBow;
    public TurretBlueprint WaterTank;
    public TurretBlueprint Rocket;

    public Text CrossBowPriceUI;
    public Text WaterTankPriceUI;
    public Text RocketPriceUI;

    private Animator shopAnimator;
    public BuildManager buildmanager;
    public GameObject selectedNode;
	// Use this for initialization
	void Start () {
        shopAnimator = gameObject.GetComponent<Animator>();
        buildmanager = BuildManager.instance;
        SetPricesInUI();

    }
	
    void SetPricesInUI()
    {
        CrossBowPriceUI.text = CrossBow.cost.ToString();
        WaterTankPriceUI.text = WaterTank.cost.ToString();
        RocketPriceUI.text = Rocket.cost.ToString();
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void SetNode(GameObject _node)
    {
        selectedNode = _node;
    }

    public void PurchaseCrossbow()
    {
            buildmanager.SelectTurretAndBuild(CrossBow);
            CloseShop();
    }

    public void PurchaseWaterTank()
    {
        buildmanager.SelectTurretAndBuild(WaterTank);
        CloseShop();
    }

    public void PurchaseRocket()
    {
        buildmanager.SelectTurretAndBuild(Rocket);
        CloseShop();
    }

    public void CloseShop()
    {
        shopAnimator.SetBool("SlideUp", false);
    }
}
