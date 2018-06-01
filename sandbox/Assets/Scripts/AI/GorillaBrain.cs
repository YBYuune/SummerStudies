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


    [Header("Front")]
    public Vector3 FrontUpperLegRight;
    public Vector3 FrontUpperLegLeft;

    public Vector3 FrontLowerLegRight;
    public Vector3 FrontLowerLegLeft;

    [Header("Back")]
    public Vector3 BackUpperLegRight;
    public Vector3 BackUpperLegLeft;

    public Vector3 BackLowerLegRight;
    public Vector3 BackLowerLegLeft;

    void Start () {
		
	}
	

	void Update () {
        // front
        FrontUpperLegs[0].eulerAngles = FrontUpperLegRight;
        FrontUpperLegs[1].eulerAngles = FrontUpperLegLeft;

        FrontLowerLegs[0].eulerAngles = FrontLowerLegRight;
        FrontLowerLegs[1].eulerAngles = FrontLowerLegLeft;

        // back
        BackUpperLegs[0].eulerAngles = BackUpperLegRight;
        BackUpperLegs[1].eulerAngles = BackUpperLegLeft;

        BackLowerLegs[0].eulerAngles = BackLowerLegRight;
        BackLowerLegs[1].eulerAngles = BackLowerLegLeft;

    }
}
