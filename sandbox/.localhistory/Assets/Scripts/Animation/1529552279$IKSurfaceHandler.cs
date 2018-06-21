using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementCurves))]
public class IKSurfaceHandler : MonoBehaviour {

    private MovementCurves mc;

	void Awake () {
        mc = GetComponent<MovementCurves>();
	}
	
	void LateUpdate () {

        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,out hit,.1f))

	}
}
