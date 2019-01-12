using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    public Transform[] wayPoints;
    public float speed; 
    public Animator animator;
    private Transform target;
    private bool arrived;
    private int wpIndex = 0;
	// Use this for initialization
	void Start () {
        target = wayPoints[0];
	}
	
	// Update is called once per frame
	void Update () {
        if (!arrived)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speed).eulerAngles;
            gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                arrived = true;
                animator.SetBool("isLooking", true);
            }
        }
        else 
        {
            //arrived; play look animation
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
            {
                animator.SetBool("isLooking", false);
                GetNextWaypoint();
            }
            else
            {
                return;
            }
        }

	}
    void GetNextWaypoint()
    {
        wpIndex = (wpIndex + 1) % wayPoints.Length;
        target = wayPoints[wpIndex];
        arrived = false;
    }
}
