using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaBrain : MonoBehaviour {

    [Header("Joints")]
    public Transform[] FrontUpperLegs;
    public Transform[] FrontLowerLegs;
    [Space(25)]
    public Transform[] BackUpperLegs;
    public Transform[] BackLowerLegs;

    public Vector3 torque;

    void Start () {
		
	}
	

	void Update () {
        //SoftJointLimit joint = FrontUpperLegs[0].swing2Limit;
        //joint.limit = angle;
        FrontUpperLegs[0].GetComponent<Rigidbody>().AddTorque(torque);
    }
}
