using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessCanny : MonoBehaviour {

    private Material material;
    // Use this for initialization
    void Start () {
        material = new Material(Shader.Find("Screen/PostProcessingAdvanced"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_OutlineThickness", (float)Thickness * 1024.0f);
        material.SetFloat("_DepthSlider", Depth);
        Graphics.Blit(source, destination, material);
    }
}
