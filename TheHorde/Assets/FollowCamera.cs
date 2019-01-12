using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    public float smoothSpeed = 0.125f;
    
    public float maxTurn = 0.5f;
    public Vector3 offset;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void FixedUpdate()
    {
            float desiredAngley = target.transform.eulerAngles.y;
            float desiredAnglex = target.transform.eulerAngles.x;
            Quaternion rotation = Quaternion.Euler(desiredAnglex, desiredAngley, 0);
            transform.position = Vector3.Lerp(transform.position, target.transform.position - (rotation * offset), smoothSpeed);

            transform.LookAt(target.transform);
    }
}