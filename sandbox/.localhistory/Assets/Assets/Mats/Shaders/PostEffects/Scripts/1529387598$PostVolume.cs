using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostVolume : MonoBehaviour {
    
    public 

    //

    bool isVisable = false;



    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        isVisable = GetComponent<BoxCollider>().bounds.Contains(Camera.main.transform.position);
        Camera.main.AddComponent(SphereCollider);
    }
}
