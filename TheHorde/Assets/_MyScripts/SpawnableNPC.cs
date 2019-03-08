using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableNPC : MonoBehaviour {

    //public Transform[] wayPoints;
    public float speed; 
    public Animator animator;
    private Transform target;
    public GameObject wp;
    private NPCWayPoint npcwp;
    public NpcWpMaster wpMaster;
    public bool arrived;

    Vector3 currVel;
    Vector3 prevPos;

    // Use this for initialization
    void Start () {
        //target = wayPoints[0];
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

        if (!arrived)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speed).eulerAngles;
            gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            if (Vector3.Distance(transform.position, target.position) <= 0.9f)
            {
                arrived = true;
                SetArriveAnimation(true);
                
            }
        }
        else 
        {
            //arrived; playing look animation
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
            {
                //animation is over
                SetArriveAnimation(false);
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

    void GetNextWaypoint()
    {
        wp = wpMaster.GetRandomWayPoint(wp);
        npcwp = wp.GetComponent<NPCWayPoint>();
        target = wp.transform;
        arrived = false;    
    }
}
