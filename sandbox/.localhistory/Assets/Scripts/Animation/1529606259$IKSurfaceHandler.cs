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
        GetComponent<ParticleSystem>().Stop();
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

            if (parentRef != null)
            {
                if (parentRef.GetComponent<Rigidbody>().velocity.magnitude > 1.0f)
                {
                    GetComponent<ParticleSystem>().Emit(3);
                }
            }

        }
        else
        {
            Vector3 pos = transform.localPosition;
            pos.y = Mathf.Lerp(transform.localPosition.y, startY,0.1f);
            transform.localPosition = pos;
        }


    }
}
