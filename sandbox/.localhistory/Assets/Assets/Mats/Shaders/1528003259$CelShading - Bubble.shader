Shader "Casey/CelShading - Bubble"
{
	//////////////////////////////////////////////////////////////
	//														   	//
	//			This shader was written by Casey MacNeil		//	   
	//														   	//
	//////////////////////////////////////////////////////////////
	Properties
	{
		[Toggle]_Specular("Use Specular", Float) = 0
		[IntRange] _Gloss("Specular Intensity", Range(0, 256)) = 0
	}
		SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		ZWrite off
		Blend DstColor SrcColor
		AlphaToMask On
		CGPROGRAM
#pragma surface surf CelShadingBubble alpha:fade

		half _Ambient;
	half _Specular;
	fixed _Gloss;

	struct Input {
		float3 viewDir;
		float3 worldPos;
	};

	void surf(Input IN, inout SurfaceOutput o) {

		o.Gloss = _Gloss;
		o.Specular = _Specular;
		//
		o.Albedo = half3(1.0,1.0,1.0);
		o.Alpha = 1.0;
	}
	ENDCG
	}
		Fallback "Diffuse"
}