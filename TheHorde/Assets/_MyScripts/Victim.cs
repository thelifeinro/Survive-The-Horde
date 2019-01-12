using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Victim : MonoBehaviour {

    public GameObject attacker;
    public GameObject deathEffect;
    public Animator animator;
    public NavMeshAgent nav;
    private Transform QuarantineDest;
    public Patrol patrol;

	// Use this for initialization
	void Start () {
        nav.isStopped = true;
        QuarantineDest = GameObject.FindGameObjectWithTag("Quarant").transform;
    }
	
	// Update is called once per frame
	void Update () {
		if(nav.isStopped == false)
            //got to quarantine
            if (pathComplete())
                Destroy(gameObject);
	}

    public void Killed()
    {
        PlayerStats.instance.Kill(1);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }

    public void Infected()
    {
        // add to infected player stats
        if (patrol != null)
        {
            patrol.enabled = false;
        }
        PlayerStats.instance.Infect(1);
        // set animation to running
        nav.SetDestination(QuarantineDest.position);
        nav.isStopped = false;
        animator.SetBool("isInfected", true);
        //infected effect
        //run to quarantine
    }

    protected bool pathComplete()
    {
        //Debug.Log("distance: " + Vector3.Distance(_navMeshAgent.destination, _navMeshAgent.transform.position));
        if (Vector3.Distance(nav.destination, nav.transform.position) - 1 <= nav.stoppingDistance)
        {
            return true;
        }

        return false;
    }
}
