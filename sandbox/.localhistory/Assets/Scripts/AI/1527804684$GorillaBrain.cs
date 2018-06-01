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

    public float angle = 0.0f;

    void Start () {
		
	}
	

	void Update () {
		
	}
}
