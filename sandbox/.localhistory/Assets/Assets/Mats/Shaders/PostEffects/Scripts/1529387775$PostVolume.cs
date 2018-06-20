using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostVolume : MonoBehaviour {

    //

    bool isVisable = false;




    //
    [Range(0, 4096)] public float blurResolution = 1024;

    [Range(0, 4096)] public float sobelResolution = 1024;

    [Range(0, 4096)] public float supressResolution = 4096;
    [Range(0, 1)] public float upperThreshold = .5f;
    [Range(0, 1)] public float lowerThreshold = .5f;
    public float PostEffect_outline_
    private PostProcessCanny PostEffect_outline = null;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        isVisable = GetComponent<BoxCollider>().bounds.Contains(Camera.main.transform.position);
        PostEffect_outline = PostEffect_outline != null ? null : Camera.main.gameObject.AddComponent<PostProcessCanny>();
    }
}
