﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaCorrection : MonoBehaviour {


    private Material material;
    [Range(1, 4)]
    public int Thickness = 4;


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
