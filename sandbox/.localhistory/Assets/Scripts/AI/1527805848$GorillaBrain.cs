using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaBrain : MonoBehaviour {

    [Header("Joints")]
    public CharacterJoint[] FrontUpperLegs;
    public CharacterJoint[] FrontLowerLegs;
    [Space(25)]
    public CharacterJoint[] BackUpperLegs;
    public CharacterJoint[] BackLowerLegs;

    public Vector3 torque;

    void Start () {
		
	}
	

	void Update () {
        SoftJointLimit joint = FrontUpperLegs[0].swing2Limit;
        joint.limit = angle;
        FrontUpperLegs[0].GetComponent<Rigidbody>().AddTorque(torque)
    }
}
