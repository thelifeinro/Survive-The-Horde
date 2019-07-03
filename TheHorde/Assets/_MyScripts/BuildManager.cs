using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            //Debug.Log("More than one build manager in this scene!");
        }
        instance = this;
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private Node selectedNode;
    private TurretBlueprint turretToBuild;

    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SetSelectedNode(Node _node)
    {
        selectedNode = _node;
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SelectTurretAndBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        selectedNode.BuildTurret(turretToBuild);
    }


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
