using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlade : MonoBehaviour {

    public GameObject blade;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        Instantiate(blade, spawnPosition, spawnRotation);
    }
}
