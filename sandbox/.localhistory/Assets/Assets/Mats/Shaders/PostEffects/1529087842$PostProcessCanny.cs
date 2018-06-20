using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PostProcessCanny : MonoBehaviour {

    private RenderTexture blurTex;
    private Material blurMat;
    // Use this for initialization
    void Start () {
        blurMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-GBlur"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, blurMat);
    }
}
