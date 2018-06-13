using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    private void Start()
    {
    }

    void LateUpdate () {
        //
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        
	}
}
