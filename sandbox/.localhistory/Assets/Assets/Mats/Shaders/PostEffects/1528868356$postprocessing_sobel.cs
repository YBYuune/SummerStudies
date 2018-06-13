using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessing_Sobel : MonoBehaviour {

    private Material material;
    [Range(1, 8)]
    public int Thickness = 4;
    [Range(0, .3f)]
    public float Depth = 0;


    void Awake () {
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
        material = new Material(Shader.Find("Screen/PostProcessingAdvanced"));
    }
	
	void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_OutlineThickness", (float)Thickness * 1024.0f);
        material.SetFloat("_DepthSlider", Depth);
        Graphics.Blit(source, destination, material);
    }
}
