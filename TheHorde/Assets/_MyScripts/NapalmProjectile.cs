using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NapalmProjectile : MonoBehaviour {

    public float speed = 70f;
    public GameObject explosionPrefab;
    public int maxDamage = 120;
    public float range = 15;
    private Vector3 targetPosition;
    // Use this for initialization
    void Start () {
        targetPosition = new Vector3(transform.position.x, 0, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = targetPosition - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        HitSth();
    }

    void HitSth()
    {
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
                int amount = (int)((dst / range) * maxDamage);
                Damage(enemy, amount);
            }
        }
        Destroy(gameObject);
        Debug.Log("we hit sth");
    }

    void Damage(GameObject enemy, int amount)
    {
        Enemy enem = enemy.GetComponent<Enemy>();
        if (enem != null)
            enem.TakeDamage(amount);
    }
}
