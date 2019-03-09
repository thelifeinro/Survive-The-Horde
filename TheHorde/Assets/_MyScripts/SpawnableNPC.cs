using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnableNPC : MonoBehaviour {

    //public Transform[] wayPoints;
    public float speed; 
    public Animator animator;
    private Transform target;
    public GameObject wp;
    private NPCWayPoint npcwp;
    public NpcWpMaster wpMaster;
    public bool arrived;
    public Victim victim;

    public NavMeshAgent _navMeshAgent;

    Vector3 currVel;
    Vector3 prevPos;

    // Use this for initialization
    void Start () {
        //target = wayPoints[0];
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        victim = gameObject.GetComponent<Victim>();
        wpMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<NpcWpMaster>();
        //GetNextWaypoint();
    }
	
	// Update is called once per frame
	void Update () {
        if(target == null)
        {
            GetNextWaypoint();
            return;
        }


        if (npcwp.IsOccupied(gameObject))
        {
                GetNextWaypoint();
                //return;
        }

        if (!arrived)
        {
                /*
                Vector3 dir = target.position - transform.position;
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speed).eulerAngles;
                gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                */
            if (Vector3.Distance(transform.position, target.position) <= 0.9f)
            {
                if (npcwp.type == WayPointType.Lean || npcwp.type == WayPointType.Sit)
                {
                    gameObject.transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
                    //rotate to be same as target's forward
                    gameObject.transform.rotation = Quaternion.LookRotation(target.forward);

                }

                //we have arrived
                arrived = true;
                SetArriveAnimation(true);
                //taking the seat
                npcwp.Occupy(gameObject);
            }
        }
        else 
        {
            //we are now at the waypoint, performing the animation 
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
            {
                //animation is over
                
                SetArriveAnimation(false);
                npcwp.Free(gameObject);
                GetNextWaypoint();
            }
            else
            {
                return;
            }
        }

	}


    void SetArriveAnimation(bool value)
    {
        switch (npcwp.type)
        {
            case WayPointType.Look:
                animator.SetBool("isLooking", value);
                break;
            case WayPointType.Lean:
                animator.SetBool("isLeaning", value);
                break;
            case WayPointType.Sit:
                animator.SetBool("isSitting", value);
                break;
        }
    }

    protected bool pathComplete()
    {
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

    void GetNextWaypoint()
    {
        if (victim.isInfected)
            return;
        wp = wpMaster.GetRandomWayPoint(wp);
        npcwp = wp.GetComponent<NPCWayPoint>();
        target = wp.transform;
        _navMeshAgent.SetDestination(target.position);
        arrived = false;    
    }
}
