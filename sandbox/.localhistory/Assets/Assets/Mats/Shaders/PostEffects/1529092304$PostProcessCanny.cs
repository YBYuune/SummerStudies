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


        Texture buffer = RenderTexture.GetTemporary(source.width, source.height);

        blurMat.SetFloat("_Res", blurResolution);
        Graphics.Blit(source, blurTex, blurMat);

        Graphics.Blit(blurTex, sobelTex, sobelMat);

        Graphics.Blit(sobelTex, destination);
    }
}
