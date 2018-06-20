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
        Vector2[] o = new Vector2[2];
        o[0] = new Vector2(0, 0);
        o[1] = new Vector2(1, 1);
        Graphics.BlitMultiTap(source, blurTex, blurMat,o);

        Graphics.BlitMultiTap(blurTex, sobelTex, sobelMat,o);

        Graphics.Blit(sobelTex, destination);
    }
}
