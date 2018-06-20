using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PostProcessCanny : MonoBehaviour {

    [Range(512,4096)]public float blurResolution = 2048;

    private RenderTexture blurTex;
    private Material blurMat;
    // Use this for initialization
    void Start () {
        blurMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-GBlur"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        blurMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-GBlur"));
        blurMat.SetFloat("_Res", blurResolution);
        Graphics.Blit(source, blurTex, blurMat);
    }
}
