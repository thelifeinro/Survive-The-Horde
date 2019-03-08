using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    [SerializeField]
    Transform _destination;
    NavMeshAgent _navMeshAgent;
    public float yOffsetForShooting = 0f;
    public Image healthBar;
    public GameObject deathparticlesPrefab;
    public Animator animator;
    public GameObject floatTextPrefab;

    [Header("Enemy Stats")]
    public float startSpeed;
    float speed;
    public float attackSpeed;
    public float maxHealth;
    private float Health;
    public int Worth;
    public bool doesKill;
    public int infect_count;

    private int victim_count = 0;
    public bool attackMode = false;
    private bool isAttacking = false;

    public GameObject chosenTarget;
    private Victim victimComp;
    float elapsed = 0;

    [Header("Enemy Resistances: <1 -> more resistant; 0 = immune; >1 = weak")]
    public float arrowRes = 1;
    public float rocketRes = 1;
    public float waterRes = 1;

    void Start ()
    {
        Health = maxHealth;
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _destination = GameObject.FindGameObjectWithTag("Destination").transform;
        if(_navMeshAgent == null)
        {
            Debug.LogError("NavMesh is not attached to " + gameObject.name);
        }
        else
        {
            //giveRandomRadius();
            setDestination();
            _navMeshAgent.speed = startSpeed;
        }
	}
    private void Update()
    {
        if(AttackDone())
        {
            Debug.Log("victims: " + victim_count.ToString() + "  toinfect:" + infect_count.ToString());
            Die();
        }
        if (pathComplete() && !attackMode)
        {
            //Arrived at Community
            Worth = (int)(0.25f * Worth);
            //PlayerStats.instance.Kill(kills_count);
            //PlayerStats.instance.Infect(infect_count);
            _navMeshAgent.speed = attackSpeed;
            FindNearestVictim();
            attackMode = true;
            //Destroy(gameObject);
        }

        //Arrived at victim
        if (pathComplete() && attackMode)
        {
            if(!isAttacking)
                StartCoroutine(AttackAnim());
            ///victim_count++;
            //next victim
            //FindNearestVictim();
        }
        
        elapsed += Time.deltaTime;
        if (elapsed >= 0.2f)
        {
            elapsed = elapsed % 0.2f;
            if (attackMode && !pathComplete())
            {
                //update destination in case target is mobile
                _navMeshAgent.SetDestination(chosenTarget.transform.position);
            }
        }
            
    }

    IEnumerator AttackAnim()
    {
        
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        transform.LookAt(chosenTarget.transform);
        _navMeshAgent.isStopped = true;

        //ATTENTION! stops victim
        victimComp.nav.isStopped = true;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length /*+ animator.GetCurrentAnimatorStateInfo(0).normalizedTime*/);
        Debug.Log("Attack has ended!");
        animator.SetBool("isAttacking", false);
        isAttacking = false;
        _navMeshAgent.isStopped = false;
        DamageVictim(victimComp);
        victim_count++;

        if (!AttackDone())
            FindNearestVictim();
    }

    public void DamageVictim(Victim victim)
    {
        if (doesKill)
            victim.Killed();
        else
            victim.Infected();
    }

    public bool AttackDone()
    {
        if (victim_count == infect_count)
            return true;
        return false;
    }

    public void FindNearestVictim()
    {
        GameObject[] possibleVictims = GameObject.FindGameObjectsWithTag("Healthy");
        float minDist = float.MaxValue;
        GameObject chosenVictim = null;
        foreach (GameObject victim in possibleVictims)
        {
            float dist = Vector3.Distance(transform.position, victim.transform.position);
            if(dist < minDist)
            {
                minDist = dist;
                chosenVictim = victim;
            }
        }
        SetVictim(chosenVictim);
    }

    public void SetVictim(GameObject chosen)
    {
        if (chosen == null)
        {
            Die();
        }
        else
        {
            chosen.tag = "Infected";
            chosenTarget = chosen;
            victimComp = chosen.GetComponent<Victim>();
            _navMeshAgent.SetDestination(chosen.transform.position);
            Debug.Log(gameObject.name + "   Chose " + chosen.name);
        }
    }

    public void TakeDamage(float amount, BulletType type)
    {
        Debug.Log("taking damage  " + amount);
            switch ((BulletType)type)
            {

                case BulletType.Arrow:
                    amount *= arrowRes;
                    break;

                case BulletType.Rocket:
                    amount *= rocketRes;
                    break;

                case BulletType.Water:
                    amount *= waterRes;
                    break;
            }
        TakeDamage(amount);
    }

    public void TakeDamage(float amount)
    { 
        Health -= amount;
        if(Health <= 0)
        {
            Die();
        }
        healthBar.fillAmount = (float) Health / maxHealth;
    }

    public void Slow(float percent)
    {
        speed = startSpeed * (1 - percent);
        _navMeshAgent.speed = speed;
    }

    public void Die()
    {
        //show worth
        ShowFloatingText();
        PlayerStats.instance.AddMoney(Worth);
        //Spawn Effect Maybe
        GameObject deathEffect = Instantiate(deathparticlesPrefab, transform.position, Quaternion.identity);
        Destroy(deathEffect, 2f);
        Destroy(gameObject);
    }

    void ShowFloatingText()
    {
        var text = Instantiate(floatTextPrefab, transform.position, Quaternion.identity); 
        text.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + Worth.ToString();
        Destroy(text, 5);
    }

    void setDestination ()
    {
        if (_destination != null) {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
	}

    void giveRandomRadius()
    {
        if (_navMeshAgent != null)
        {
            _navMeshAgent.radius = Random.Range(0.37f, 2f);
        }
    }

    protected bool pathComplete()
    {
        //Debug.Log("distance: " + Vector3.Distance(_navMeshAgent.destination, _navMeshAgent.transform.position));
        // if (Vector3.Distance(_navMeshAgent.destination, _navMeshAgent.transform.position) - 1 <= _navMeshAgent.stoppingDistance)
        //{
        /*if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
        {
            return true;
        }*/
        //      return true;
        // }
        //
        //  return false;
        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

  
