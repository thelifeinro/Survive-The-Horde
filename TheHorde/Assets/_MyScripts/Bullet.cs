using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform Target;
    public float speed = 70f;
    public GameObject explosionPrefab;
    public int damage = 15;

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

        if (dir.magnitude <= distanceThisFrame)
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
            enem.TakeDamage(damage);
    }

    void HitTarget()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, Target.position, Quaternion.identity);
        }
        Damage(Target);
        Destroy(gameObject);
        Debug.Log("we hit sth");
    }
}
