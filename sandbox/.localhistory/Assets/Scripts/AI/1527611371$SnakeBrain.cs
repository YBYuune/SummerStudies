using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SnakeBrain : MonoBehaviour {

    public Transform[] pieces;

    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        Vector3 vel = body.velocity;
        vel.x = Input.GetAxis("Horizontal") * 5.0f;
        vel.z = Input.GetAxis("Vertical")   * 5.0f;
        body.velocity = vel;
    }

    // Update is called once per frame
    void Update () {

        float torqueDir = 1.0f;

        for (int i = 0; i < pieces.Length; i++)
        {
            //if (pieces[i].GetComponent<CharacterJoint>()) torqueDir = 1.0f;
            //if (pieces[i].transform.localEulerAngles.z >= 45) torqueDir = -1.0f;

            //print("[" + i + "] " + getJointRotation(pieces[i].GetComponent<CharacterJoint>()).eulerAngles);
            //pieces[i].GetComponent<Rigidbody>().AddRelativeTorque(torqueDir * (Vector3.back * 15.0f / Mathf.Pow(i+1,2)));
        }
	}

    public Quaternion getJointRotation(CharacterJoint joint)
    {
        return (Quaternion.FromToRotation(joint.axis, joint.connectedBody.transform.rotation.eulerAngles));
    }
}
