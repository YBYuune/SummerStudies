using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameSettings : MonoBehaviour {
    [Header("Graphics")]
    public bool HDREnabled = true;

	void Start () {
		
	}
	

	void Update ()
    {
        //---------------GRAPHICS---------------
        Camera.main.allowHDR = HDREnabled;
        //---------------GRAPHICS---------------
    }
}
