using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WayPointType {
    Look,
    Lean,
    Sit
};

public class NPCWayPoint : MonoBehaviour
{
    public WayPointType type;
    private bool taken = false;
    private GameObject occupant;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Occupy(GameObject me)
    {
        if (taken == false)
        {
            occupant = me;
            taken = true;
        }
    }
    public void Free(GameObject me)
    {
        if (occupant == me)
        {
            taken = false;
            occupant = null;
        }
    }

    public bool IsOccupied(GameObject me)
    {
        if(occupant != me)
            return taken;
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
