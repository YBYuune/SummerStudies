using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCurves : MonoBehaviour {

    public AnimationCurve XAxisCurve;
    public AnimationCurve YAxisCurve;
    public AnimationCurve ZAxisCurve;

    [Space(25)] public float TimeOffset;

    public float YOffset;

    [HideInInspector] public RaycastHit hit;

    private Vector3 pos;
    // Use this for initialization
    void Start () {
		pos = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = pos;

        newPos.x = XAxisCurve.Evaluate(Time.time + TimeOffset);
        newPos.y = YAxisCurve.Evaluate(Time.time + TimeOffset) + YOffset;

        IKSurfaceHandler surf = gameObject.GetComponent<IKSurfaceHandler>();

        if (surf != null)
        {
            surf.touching = (Physics.Raycast(transform.position, Vector3.down, out hit, surf.errorDistance));
        }

        transform.localPosition = newPos;
    }
}
