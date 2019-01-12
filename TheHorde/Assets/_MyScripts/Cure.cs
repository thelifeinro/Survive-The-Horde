using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : MonoBehaviour {

    public int Worth;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        Debug.Log("hey");
        //Reward its worth
        PlayerStats.instance.AddCure(Worth);
        //play effect maybe
        //Destroy

        Destroy(gameObject);
        
    }
}
