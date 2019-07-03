using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType // your custom enumeration
    {
        Arrow,
        Water,
        Rocket
    };

public class Bullet : MonoBehaviour {

    private Transform Target;
    public float speed = 70f;
    public GameObject explosionPrefab;
    public int damage = 15;
    public BulletType type;
    public float range = 5;

    public void Seek(Transform _target)
    {
        Target = _target;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = Target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame + 0.2) // the +0.1 is to fix the drone which hovers up and douwn and is never hit by bullet
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = rotation;
    }

    void Damage(Transform enemy)
    {
        Enemy enem = enemy.GetComponent<Enemy>();
        if(enem != null)
            enem.TakeDamage(damage, type);

        //rocket inflicts damage in a radius
        if (type == BulletType.Rocket)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject en in enemies)
            {
                if (en != enemy)
                {
                    float dst = Vector3.Distance(transform.position, en.transform.position);
                    if (dst <= range)
                    {
                        int collateralDamage = (int)((dst / range) * damage);
                        en.GetComponent<Enemy>().TakeDamage(collateralDamage, type);
                    }
                }
            }
        }
    }

    void HitTarget()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, Target.position, Quaternion.identity);
        }
        Damage(Target);
        Destroy(gameObject);
    }
}
