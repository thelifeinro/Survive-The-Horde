using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingObstacle : MonoBehaviour {

    public GameObject nonblockingObj;
    public GameObject blockingObj;
    public GameObject particlePrefab;
    public Outline hoverOutline;

	// Use this for initialization
	void Start () {
        blockingObj.active = false;
        hoverOutline = nonblockingObj.GetComponent<Outline>();
        hoverOutline.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseEnter()
    {
        if (!enabled || !GlobalVariables.objectsAreInteractible ) return;
        hoverOutline.enabled = true;
    }
    private void OnMouseExit()
    {
        if (!enabled) return;
        hoverOutline.enabled = false;
    }

    private void OnMouseDown()
    {
        if (!enabled || !GlobalVariables.objectsAreInteractible) return;
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        blockingObj.active = true;
        Destroy(nonblockingObj);
        enabled = false;
    }
}
