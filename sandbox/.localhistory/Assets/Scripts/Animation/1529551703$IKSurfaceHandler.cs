using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementCurves))]
public class IKSurfaceHandler : MonoBehaviour {

    private MovementCurves mc;

	void Start () {
        mc = GetComponent<MovementCurves>();
	}
	
	void Update () {
		
	}
}
