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
    public bool isInfected = false;
    public bool targetedByEnemy = false;
    public Quarantine quarantine;

	// Use this for initialization
	void Start () {
        //nav.isStopped = true;
        QuarantineDest = GameObject.FindGameObjectWithTag("Quarant").transform;
        quarantine = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Quarantine>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isInfected == true)
            //got to quarantine
            if (pathComplete())
            {
                //it got to quarantine; checking in yay
                quarantine.CheckIn();
                Destroy(gameObject);
            }
	}

    public void Killed()
    {
        PlayerStats.instance.Kill(1);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }

    public void Targeted()
    {
        targetedByEnemy = true;
        //start coroutine to return to untargeted after a while
        StartCoroutine(ResetCountdown());
    }

    private IEnumerator ResetCountdown()
    {
        float normalizedTime = 0;
        while (normalizedTime <= 20)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        //if in 12 seconds you don't get oinfected then the enemy targeting you died before getting to you!!
        if(gameObject.tag!="Infected")
            targetedByEnemy = false;
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
        isInfected = true;
        animator.SetBool("isInfected", true);

        //set tag to infected
        gameObject.tag = "Infected";
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
