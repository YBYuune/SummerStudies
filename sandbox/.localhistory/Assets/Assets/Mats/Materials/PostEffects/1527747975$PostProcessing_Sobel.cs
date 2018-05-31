using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessing_Sobel : MonoBehaviour {

    private Material material;
    [Range(1024, 4096)]
    public int Thickness = 1024;


    void Awake () {
        material = new Material(Shader.Find("Screen/PostProcessingAdvanced"));
    }
	
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
        material.SetFloat("_OutlineThickness", (float)Thickness);
        Graphics.Blit(source, destination, material);
    }
}
