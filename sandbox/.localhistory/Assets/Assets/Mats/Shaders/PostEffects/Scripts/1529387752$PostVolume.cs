using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostVolume : MonoBehaviour {

    //

    bool isVisable = false;

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
