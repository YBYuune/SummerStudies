﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessing_GammaCorrection : MonoBehaviour {


    private Material material;
    [Range(0,1)]
    public float gamma = 1.0f;

    void Awake()
    {
        material = new Material(Shader.Find("Screen/GammaCorrection"));
        print(material.ToString());
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
