using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    private void Start()
    {
        transform.forward = -transform.forward;
    }

    void LateUpdate () {
        //
        transform.LookAt(Camera.main.transform.position,transform.up);
        
	}
}
