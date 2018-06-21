using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementCurves))]
public class IKSurfaceHandler : MonoBehaviour {

    public float errorDistance = 0.6f;
    public float smoothness = 0.6f;
    private MovementCurves mc;

    private RaycastHit hit;

    [HideInInspector] public bool touching = false;

    void Awake () {
        mc = GetComponent<MovementCurves>();
	}

    void LateUpdate () {

        if (touching)
        {
            Vector3 incomingVec = hit.point - transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y,hit.point.y + errorDistance, smoothness);
            transform.position = pos;

        }

	}
}
