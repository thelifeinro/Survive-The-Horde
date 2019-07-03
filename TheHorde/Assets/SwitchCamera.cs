using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCamera : MonoBehaviour {

    public Camera mainCam;
    public Camera followCam;
    public GameObject rotatingBlade;
    public float waitTimeBeforeExit;
    public FlyingBlade fbscript;

    public int expAwarded = 0;
    int enemiesHit = 0;

    public Transform EffectDropzone;
    public GameObject effectPrefab;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        followCam.enabled = true;
        mainCam.enabled = false;
        fbscript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<FlyingBlade>();
        EffectDropzone = GameObject.FindGameObjectWithTag("EXPZone").transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void AwardEXP()
    {
        expAwarded++;
        fbscript.awardedExp++;
        SpawnEffect("+" + expAwarded +" EXP");
    }

    void DoubleEXP()
    {
        //Debug.Log("Doubling EXP");
        expAwarded *= 2;
        fbscript.awardedExp *= 2;
        SpawnEffect("X 2");
    }

    void SpawnEffect(string content)
    {
        GameObject effect = Instantiate(effectPrefab, EffectDropzone);
        effect.GetComponent<Text>().text = content;
        Destroy(effect, 2);
    }

    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(waitTimeBeforeExit);
        
        mainCam.enabled = true;
        followCam.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        //end this
        fbscript.StopAttack();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("hit!");
        if (collision.transform.tag != "Enemy")
        {
            rotatingBlade.GetComponent<rotateBlade>().disableRotation();
            gameObject.GetComponent<ControlFlight>().disableMoving();
            //change back to mainCamera
            StartCoroutine(waiter());
        }
        else
        {
            Enemy e = collision.transform.GetComponent<Enemy>();
            e.Die();
            enemiesHit++;
            //Debug.Log("HitEnemy");
            if (enemiesHit == 7)
            {
                DoubleEXP();
                return;
            }
            AwardEXP();
        }
        
       
       // return;
    }
}
