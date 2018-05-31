using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SnakeBrain : MonoBehaviour {

    public Transform[] pieces;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float torqueDir = 1.0f;

        for (int i = 0; i < pieces.Length-1; i++)
        {
            if (pieces[i].GetComponent<CharacterJoint>()) torqueDir = 1.0f;
            if (pieces[i].transform.localEulerAngles.z >= 45) torqueDir = -1.0f;

            print(pieces[i].transform.eulerAngles);
            pieces[i].GetComponent<Rigidbody>().AddRelativeTorque(torqueDir * (Vector3.back * 15.0f / Mathf.Pow(i+1,2)));
            print(getJointRotation())
        }
	}

    public Quaternion getJointRotation(CharacterJoint joint)
    {
        return (Quaternion.FromToRotation(joint.axis, joint.connectedBody.transform.rotation.eulerAngles));
    }
}
