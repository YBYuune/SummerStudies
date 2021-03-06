﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessing_GammaCorrection : MonoBehaviour {


    private Material material;
    [Range(0,2.2f)]
    public float gamma = 1.0f;

    void Awake()
    {
        material = new Material(Shader.Find("Casey-Screen/GammaCorrection"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Gamma", gamma);
        Graphics.Blit(source, destination, material);
    }
}
