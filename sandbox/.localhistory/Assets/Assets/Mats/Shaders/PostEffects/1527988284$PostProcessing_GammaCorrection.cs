using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessing_GammaCorrection : MonoBehaviour {


    private Material material;


    void Awake()
    {
        material = new Material(Shader.Find("Screen/GammaCorrection"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
