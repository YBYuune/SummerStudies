using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostVolume : MonoBehaviour {
    
    public 

    //

    bool isVisable = false;

    private PostProcessCanny PostEffect_outline;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        isVisable = GetComponent<BoxCollider>().bounds.Contains(Camera.main.transform.position);
        Camera.main.gameObject.AddComponent<>;
    }
}
