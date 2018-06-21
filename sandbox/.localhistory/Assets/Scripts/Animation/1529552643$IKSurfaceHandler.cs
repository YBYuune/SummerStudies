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
        if (Physics.Raycast(transform.position, Vector3.down, out hit, .6f))
        {
            Vector3 incomingVec = hit.point - transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
            Vector3 pos = transform.position;
            pos.y = hit.point.y;

        }

	}
}
