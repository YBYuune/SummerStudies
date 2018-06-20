using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostVolume : MonoBehaviour {

    //

    bool isVisable = false;




    //
    bool usesOutline = false;

        [Range(0, 4096)] public float PostEffect_outline_blurResolution = 1024;

        [Range(0, 4096)] public float PostEffect_outline_sobelResolution = 1024;

        [Range(0, 4096)] public float PostEffect_outline_supressResolution = 4096;
        [Range(0, 1)] public float PostEffect_outline_upperThreshold = .5f;
        [Range(0, 1)] public float PostEffect_outline_lowerThreshold = .5f;
        public Color PostEffect_outline_outlineColor = Color.black;

        private PostProcessCanny PostEffect_outline = null;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        isVisable = GetComponent<BoxCollider>().bounds.Contains(Camera.main.transform.position);

        // EDGE DETECTION (OUTLINE)
        PostEffect_outline = PostEffect_outline != null ? null : Camera.main.gameObject.AddComponent<PostProcessCanny>();

        PostEffect_outline.blurResolution = PostEffect_outline_blurResolution;

        PostEffect_outline.sobelResolution = PostEffect_outline_sobelResolution;

        PostEffect_outline.supressResolution = PostEffect_outline_supressResolution;
        PostEffect_outline.upperThreshold = PostEffect_outline_upperThreshold;
        PostEffect_outline.lowerThreshold = PostEffect_outline_lowerThreshold;
        PostEffect_outline.outlineColor = PostEffect_outline_outlineColor;
    }
}
