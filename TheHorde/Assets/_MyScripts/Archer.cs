using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour {
    private Transform target;
    private Enemy targetEnemy;

    [Header("Attributes")]
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    [Header("Set Up Fields")]
    public GameObject tower;
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;

    [Header("Use Water")]
    public float damageOverTime = 10f;
    public float slowAmount = 0.4f;
    public bool useWater = false;
    public LineRenderer linerenderer;
    public ParticleSystem impactParticles;


    public GameObject bulletPrefab;
    public Transform firePoint;
    
	// Use this for initialization
	void Start () {
        //updating target twice every second
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            
            if (useWater)
            {
                if (linerenderer.enabled)
                {
                    linerenderer.enabled = false;
                    impactParticles.Stop();
                }
            }
            return;
        }
           

        LockOnTarget();

        if (useWater)
        {
            WaterPump();
        }
        
        if(fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void WaterPump()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime * 0.25f);
        targetEnemy.Slow(slowAmount);

        if (linerenderer.enabled == false)
        {
            linerenderer.enabled = true;
            impactParticles.Play();
        }
        linerenderer.SetPosition(0, firePoint.position);
        linerenderer.SetPosition(1, target.position);
        Vector3 dir = firePoint.position - target.position;
        impactParticles.transform.position = (target.position + dir.normalized * 0.5f);
        impactParticles.transform.rotation = Quaternion.LookRotation(dir);
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, firePoint.rotation.y+90, 0f));
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bulletGO != null)
            bullet.Seek(target);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(tower.transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                nearestEnemy = enemy;
                shortestDistance = distanceToEnemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= tower.GetComponent<Turret>().range)
        {
            target = nearestEnemy.transform;
            targetEnemy = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
}
