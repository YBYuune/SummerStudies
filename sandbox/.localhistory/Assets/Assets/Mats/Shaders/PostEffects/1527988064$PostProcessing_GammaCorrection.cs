using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GammaCorrection : MonoBehaviour {


    private Material material;


    void Awake()
    {
        material = new Material(Shader.Find("Screen/PostProcessingAdvanced"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_OutlineThickness", (float)Thickness * 1024.0f);
        Graphics.Blit(source, destination, material);
    }
}
