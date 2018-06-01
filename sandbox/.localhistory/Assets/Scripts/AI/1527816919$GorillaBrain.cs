using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class GorillaBrain : MonoBehaviour {

    [Header("Joints")]
    public Transform[] FrontUpperLegs;
    public Transform[] FrontLowerLegs;
    [Space(25)]
    public Transform[] BackUpperLegs;
    public Transform[] BackLowerLegs;

    public Vector3 FrontUpperLegRight;
    public Vector3 FrontUpperLegLeft;

    public Vector3 FrontLowerLegRight;
    public Vector3 FrontLowerLegLeft;

    void Start () {
		
	}
	

	void Update () {
        //SoftJointLimit joint = FrontUpperLegs[0].swing2Limit;
        //joint.limit = angle;
        FrontUpperLegs[0].eulerAngles = FrontUpperLegRight;
        FrontUpperLegs[1].eulerAngles = FrontUpperLegLeft;

        FrontLowerLegs[0].eulerAngles = FrontLowerLegRight;
        FrontLowerLegs[0].eulerAngles = FrontLowerLegLeft;
    }
}
