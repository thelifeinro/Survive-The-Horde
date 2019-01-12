using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Napalm : SpecialAttack {

    public GameObject projectilePrefab;
    public GameObject AimEffect;
    public float HeightToDrop = 70;
    public bool activeAim = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (activeAim && Input.GetKeyDown(KeyCode.E))
        {
                activeAim = false;
                Vector3 position = new Vector3(AimEffect.transform.position.x, HeightToDrop, AimEffect.transform.position.z);
                Instantiate(projectilePrefab, position, Quaternion.identity);
                StopAttack();
        }
	}

    public override void StartAttack()
    {
        activeAim = true;
        AimEffect.SetActive(true);
    } 

    public void StopAttack()
    {
        activeAim = false;
        AimEffect.SetActive(false);
        
    }

}
