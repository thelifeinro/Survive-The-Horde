using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour {
    private Color startColor;
    public Color hoverColor;
    public GameObject turret;
    public TurretBlueprint turretBlueprint;

    public Image radiusImage;

    public GameObject buildEffect;

    private Animator shopAnimator;
    private Animator upgradeAnimator;
    private ShopScript shopManager;
    private UpgradeSystem upgradeManager;
    private GameObject ShopObject;
    BuildManager buildManager;

    private Renderer rend;
	// Use this for initialization

	void Start () {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        ShopObject = GameObject.FindGameObjectWithTag("Shop");
        shopAnimator = ShopObject.GetComponent<Animator>();
        shopManager = ShopObject.GetComponent<ShopScript>();
        buildManager = BuildManager.instance;
        upgradeManager = GameObject.FindGameObjectWithTag("Upgrade").GetComponent<UpgradeSystem>();
        upgradeAnimator = GameObject.FindGameObjectWithTag("Upgrade").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        rend.material.color = hoverColor;
        if(turret!=null)
        {
            DrawRadius();
        }
    }

    public void DrawRadius()
    {
        Turret tur = turret.GetComponent<Turret>();
        RectTransform radTransform = radiusImage.GetComponent<RectTransform>();
        radTransform.sizeDelta = new Vector2(tur.range, tur.range);
        radiusImage.enabled = true;
    }

    public void EraseRadius()
    {
        radiusImage.enabled = false;
    }

    void OnMouseExit()
    {
        if (turret != null)
        {
            radiusImage.enabled = false;
        }
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if(turret != null)
        {
            //UPGRADE
            Debug.Log("Can't Build There! - DO THIS ON SCREEN");
            DrawRadius();
            upgradeManager.SetNode(gameObject);
            OpenUpgrader();
            return;
        }

        shopManager.SetNode(gameObject);
        buildManager.SetSelectedNode(this);
        OpenShop();

        //Build a turret
        /*
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, turretToBuild.transform.rotation);
        */
    }

    public void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        Instantiate(buildEffect, transform.position, Quaternion.identity);

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position, Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;
        turret.GetComponent<Turret>().firstprice = turretBlueprint.cost;
        turret.GetComponent<Turret>().sellworth = turret.GetComponent<Turret>().firstprice;

        //GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);

        Debug.Log("Turret build!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turret.GetComponent<Turret>().sellworth;
        Destroy(turret);
        turretBlueprint = null;
    }


    void OpenUpgrader()
    {
        //
        shopManager.CloseShop();
        upgradeManager.UpdateText();
        upgradeAnimator.SetBool("SlideUp", true);
    }

    void OpenShop()
    {
        upgradeManager.CloseUpgrade();
        shopAnimator.SetBool("SlideUp", true);
    }

}
