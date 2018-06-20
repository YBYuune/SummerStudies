using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostVolume : MonoBehaviour {

    //

    bool isVisable = false;

    //
    public bool usesColorBlend = false;

    //
    public bool usesDistortion = false;
    private PostProcessing_Distortion PostEffect_distortion;

    //
    public bool usesOutline = false;

        [Range(0, 4096)] public float PostEffect_outline_blurResolution = 1024;

        [Range(0, 4096)] public float PostEffect_outline_sobelResolution = 1024;

        [Range(0, 4096)] public float PostEffect_outline_supressResolution = 4096;
        [Range(0, 1)] public float PostEffect_outline_upperThreshold = .5f;
        [Range(0, 1)] public float PostEffect_outline_lowerThreshold = .5f;
        public Color PostEffect_outline_outlineColor = Color.black;

        private PostProcessCanny PostEffect_outline = null;
    //

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        isVisable = GetComponent<BoxCollider>().bounds.Contains(Camera.main.transform.position);

        // EDGE DETECTION (OUTLINE)
        if (isVisable)
        {
            if (usesOutline)
            {
                if (PostEffect_outline == null)
                    PostEffect_outline = Camera.main.gameObject.AddComponent<PostProcessCanny>();
                else
                    PostEffect_outline.enabled = true;

                PostEffect_outline.blurResolution = PostEffect_outline_blurResolution;

                PostEffect_outline.sobelResolution = PostEffect_outline_sobelResolution;

                PostEffect_outline.supressResolution = PostEffect_outline_supressResolution;
                PostEffect_outline.upperThreshold = PostEffect_outline_upperThreshold;
                PostEffect_outline.lowerThreshold = PostEffect_outline_lowerThreshold;
                PostEffect_outline.outlineColor = PostEffect_outline_outlineColor;
            }

            if (usesDistortion)
            {
                if (PostEffect_distortion == null)
                    PostEffect_distortion = Camera.main.gameObject.AddComponent<PostProcessing_Distortion>();
            }

        }
        else
        {
            if (PostEffect_outline != null)
                PostEffect_outline.enabled = false;
        }
    }
}
