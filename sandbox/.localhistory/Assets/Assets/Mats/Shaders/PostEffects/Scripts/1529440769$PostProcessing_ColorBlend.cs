using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessing_ColorBlend : MonoBehaviour {

    private Material mat;

    // Use this for initialization
    void Start()
    {
        mat = new Material(Shader.Find("Casey-Screen/ColorBlend"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
}
