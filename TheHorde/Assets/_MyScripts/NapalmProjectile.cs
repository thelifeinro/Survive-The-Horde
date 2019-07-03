using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NapalmProjectile : MonoBehaviour {

    public float speed = 70f;
    public GameObject explosionPrefab;
    public int maxDamage = 120;
    public float range = 18;
    private Vector3 targetPosition;

    public int expAwarded = 0;
    int enemiesHit = 0;

    public Transform EffectDropzone;
    public GameObject effectPrefab;
    private MoveCamera mc;

    // Use this for initialization
    void Start () {
        targetPosition = new Vector3(transform.position.x, 0, transform.position.z);
        EffectDropzone = GameObject.FindGameObjectWithTag("EXPZone").transform;
        mc = Camera.main.GetComponent<MoveCamera>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 dir = targetPosition - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = rotation;
        /*if (dir.magnitude <= distanceThisFrame ) // the +0.1 is to fix the drone which hovers up and douwn and is never hit by bullet
        {
            HitSth();
        }*/

       
    }


    void AwardEXP()
    {
        SpawnEffect("+" + expAwarded + " EXP");
    }


    void SpawnEffect(string content)
    {
        GameObject effect = Instantiate(effectPrefab, EffectDropzone);
        effect.GetComponent<Text>().text = content;
        Destroy(effect, 2);
    }

    void OnCollisionEnter(Collision collision)
    {
        HitSth();
    }

    void HitSth()
    {
        if(mc!=null)
            mc.Shake();
        // iterate all enemies and damage the ones in range
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float dst = Vector3.Distance(transform.position, enemy.transform.position);
            if (dst <= range)
            {
                //int amount = (int)((dst / range) * maxDamage);
                //Damage(enemy, amount);
                enemy.GetComponent<Enemy>().Die();
                enemiesHit++;
                //Debug.Log("HitEnemy");
                if (enemiesHit == 7)
                {
                    expAwarded *= 2;
                    return;
                }
                expAwarded++;
            }
        }
        AwardEXP();
        PlayerStats.instance.AddEXP(expAwarded);
        Destroy(gameObject);
    }

    void Damage(GameObject enemy, int amount)
    {
        Enemy enem = enemy.GetComponent<Enemy>();
        if (enem != null)
            enem.TakeDamage(amount);
    }
}
