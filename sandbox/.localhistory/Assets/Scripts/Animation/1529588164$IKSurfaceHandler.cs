using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementCurves))]
public class IKSurfaceHandler : MonoBehaviour {

    public float errorDistance = 0.6f;
    public float smoothness = 0.6f;
    private MovementCurves mc;

    float startY;

	void Awake () {
        mc = GetComponent<MovementCurves>();
        startY = transform.position.y;

    }
	
	void LateUpdate () {

        RaycastHit hit;
        Vector3 pos = transform.position;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, errorDistance))
        {
            Vector3 incomingVec = hit.point - transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
            pos.y = Mathf.Lerp(pos.y, hit.point.y + errorDistance, smoothness);
            transform.position = pos;
        }
        else
        {
            transform.position = new 
        }

	}
}
