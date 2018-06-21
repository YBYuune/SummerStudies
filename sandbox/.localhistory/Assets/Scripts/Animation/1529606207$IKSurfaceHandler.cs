using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementCurves))]
public class IKSurfaceHandler : MonoBehaviour {

    public float errorDistance = 0.6f;
    public float smoothness = 0.6f;

    public Transform parentRef;

    private MovementCurves mc;

    float startY;

	void Awake () {
        mc = GetComponent<MovementCurves>();
        startY = transform.localPosition.y;

    }
	
	void LateUpdate () {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, errorDistance))
        {
            Vector3 incomingVec = hit.point - transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y, hit.point.y + errorDistance, smoothness);
            transform.position = pos;



        }
        else
        {
            Vector3 pos = transform.localPosition;
            pos.y = Mathf.Lerp(transform.localPosition.y, startY,0.1f);
            transform.localPosition = pos;
        }


    }
}
