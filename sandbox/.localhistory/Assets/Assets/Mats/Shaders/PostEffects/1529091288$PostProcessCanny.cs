using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PostProcessCanny : MonoBehaviour {

    [Range(0,4096)]public float blurResolution = 1024;
    private RenderTexture blurTex;
    private Material blurMat;

    //
    [Range(0, 4096)] public float sobelResolution = 1024;
    private RenderTexture sobelTex;
    private Material sobelMat;

    // Use this for initialization
    void Start ()
    {
        blurMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-GBlur"));
        sobelMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-Sobel"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        blurMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-GBlur"));
        sobelMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-Sobel"));
        blurMat.SetFloat("_Res", blurResolution);
        float off = 1024;
        Graphics.BlitMultiTap(source, blurTex, blurMat, 
            new Vector2(-off, -off),
            new Vector2(-off, off),
            new Vector2(off, off),
            new Vector2(off, -off));

        Graphics.BlitMultiTap(blurTex, sobelTex, sobelMat,
            new Vector2(-off, -off),
            new Vector2(-off, off),
            new Vector2(off, off),
            new Vector2(off, -off));

        Graphics.Blit(sobelTex, destination);
    }
}
