using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceNormalRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos() // visualise the joint location
    {
        if (Visualise)
        {
            UltiDraw.Begin();
            UltiDraw.DrawSphere(transform.position, Quaternion.identity, 0.25f, UltiDraw.Purple.Transparent(0.5f));

            UltiDraw.DrawArrow(transform.position, transform.position + 0.25f * (transform.rotation * Axis), 0.8f, 0.02f, 0.1f, UltiDraw.Cyan.Transparent(0.5f));
            UltiDraw.End();
        }
    }
}
