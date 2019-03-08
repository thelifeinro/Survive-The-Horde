using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWpMaster : MonoBehaviour
{
    public GameObject[] wayPoints;
    // Start is called before the first frame update
    void Start()
    {
        //finds all available wayPoints in scene
        wayPoints = GameObject.FindGameObjectsWithTag("NPCWP");

    }

    //this returns a wayPoint different from the current one given as a paramater (wp)
    public GameObject GetRandomWayPoint(GameObject wp = null)
    {
        int i = Random.Range(0, wayPoints.Length - 1);
        while (wayPoints[i] == wp)
        {
            i = Random.Range(0, wayPoints.Length - 1);
        }
        return wayPoints[i];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
