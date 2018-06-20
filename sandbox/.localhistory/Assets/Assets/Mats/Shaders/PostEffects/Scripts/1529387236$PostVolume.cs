using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        bool isVisable = GetComponent<BoxCollider>().bounds.Contains(Camera.main.transform.position);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    { 



    }
}
