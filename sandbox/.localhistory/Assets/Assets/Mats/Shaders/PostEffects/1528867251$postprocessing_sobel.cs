using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessing_Sobel : MonoBehaviour {

    private Material material;
    [Range(1, 8)]
    public int Thickness = 4;


    void Awake () {
        MainCam.depthTextureMode = DepthTextureMode.Depth;
        material = new Material(Shader.Find("Screen/PostProcessingAdvanced"));
    }
	
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
        material.SetFloat("_OutlineThickness", (float)Thickness * 1024.0f);
        Graphics.Blit(source, destination, material);
    }
}
