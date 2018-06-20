using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessing_ColorBlend : MonoBehaviour {

    public Color color;
    private Material mat;    

    // Use this for initialization
    void Start()
    {
        mat = new Material(Shader.Find("Casey-Screen/ColorBlend"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        mat.SetColor("_Color", color);
        Graphics.Blit(source, destination, mat);
    }
}
