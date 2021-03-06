﻿Shader "Casey/CelShading - Bubble"
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
#pragma surface surf CelShading alpha:fade

		half _Ambient;
	half _Specular;
	fixed _Gloss;

	half4 LightingCelShading(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {

		//specular

		half3 h = normalize(lightDir + viewDir);
		float nh = (dot(s.Normal, h));

		float spec = pow(nh, s.Gloss) * s.Specular;
		if (spec > .5) spec = 1.0;
		else spec = 0.0;

		half4 c;
		half3 specular = _LightColor0.rgb * spec;

		c.rgb = s.Albedo + specular;
		c.a = s.Alpha * length(specular);
		return c;
	}

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