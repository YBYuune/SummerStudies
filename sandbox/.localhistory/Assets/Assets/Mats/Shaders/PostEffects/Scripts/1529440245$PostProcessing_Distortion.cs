using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessing_Distortion : MonoBehaviour {

    private Material mat;

	// Use this for initialization
	void Start () {
        mat = new Material(Shader.Find("Casey-Screen/Distortion"));
	}

    // Update is called once per frame
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

    }
}
