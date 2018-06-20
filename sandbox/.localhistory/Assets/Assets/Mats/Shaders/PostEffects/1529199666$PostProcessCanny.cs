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


    //
    [Range(0, 4096)] public float supressResolution = 4096;
    [Range(0,1)]public float upperThreshold = .5f;
    private RenderTexture supressTex;
    private Material supressMat;

    // Use this for initialization
    void Start ()
    {
        blurMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-GBlur"));
        sobelMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-Sobel"));
        supressMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-NonMaxSupression"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //blurMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-GBlur"));
        //sobelMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-Sobel"));
        //supressMat = new Material(Shader.Find("Casey-Screen/CannyEdgeDetect-NonMaxSupression"));


        //blurTex = RenderTexture.GetTemporary(source.width, source.height);
        //sobelTex = RenderTexture.GetTemporary(source.width, source.height);
        //supressTex = RenderTexture.GetTemporary(source.width, source.height);

        blurMat.SetFloat("_Res", blurResolution);
        /Graphics.Blit(source, blurTex, blurMat);

        sobelMat.SetFloat("_Res", sobelResolution);
        Graphics.Blit(blurTex, sobelTex, sobelMat);

        supressMat.SetFloat("_Res", supressResolution);
        supressMat.SetFloat("_UpperThreshold", upperThreshold);
        supressMat.SetTexture("_SourceTex", source);
        //Graphics.Blit(sobelTex, supressTex, supressMat);

        Graphics.Blit(source, destination, supressMat);
    }
}
