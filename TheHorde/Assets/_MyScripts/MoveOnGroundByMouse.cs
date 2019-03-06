using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnGroundByMouse : MonoBehaviour {

    public Transform objToMove;
    public Camera gameCamera;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo)){
            objToMove.position = hitInfo.point;
        }
	}
}
