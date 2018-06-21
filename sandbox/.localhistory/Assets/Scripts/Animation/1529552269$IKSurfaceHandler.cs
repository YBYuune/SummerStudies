using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementCurves))]
public class IKSurfaceHandler : MonoBehaviour {

    private MovementCurves mc;

	void Awake () {
        mc = GetComponent<MovementCurves>();
	}
	
	void Update () {
		
	}
}
