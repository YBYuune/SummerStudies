﻿using System.Collections;
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
        Graphics.Blit(source, destination, material);
    }
}
