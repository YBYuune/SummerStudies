using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceNormalRotation : MonoBehaviour {

    public Vector3 directionToCheck;
    public Transform spine;
    public float distanceToCheck;
    public float smoothness = .4f;
    public bool Visualise;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToCheck.normalized, out hit, distanceToCheck))
        {
            Quaternion rot = Quaternion.LookRotation(transform.forward, hit.normal);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, smoothness);
        }

	}

    void OnDrawGizmos() // visualise the joint location
    {
        if (Visualise)
        {
            UltiDraw.Begin();
            UltiDraw.DrawSphere(transform.position, Quaternion.identity, 0.25f, UltiDraw.Purple.Transparent(0.5f));

            UltiDraw.DrawArrow(transform.position, transform.position + (directionToCheck.normalized * distanceToCheck), 0.8f, 0.02f, 0.1f, UltiDraw.Cyan.Transparent(0.5f));
            UltiDraw.End();
        }
    }
}
