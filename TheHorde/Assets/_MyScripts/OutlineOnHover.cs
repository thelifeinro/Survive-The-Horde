using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineOnHover : MonoBehaviour {
    public Outline hoverOutline;
	// Use this for initialization
	void Start () {
        hoverOutline.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseEnter()
    {
        if (!enabled) return;
        hoverOutline.enabled = true;
    }
    private void OnMouseExit()
    {
        if (!enabled) return;
        hoverOutline.enabled = false;
    }
}
