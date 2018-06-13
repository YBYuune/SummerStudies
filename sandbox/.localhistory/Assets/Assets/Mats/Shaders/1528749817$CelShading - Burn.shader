Shader "Casey/CelShading - Burn" 
{
	//////////////////////////////////////////////////////////////
	//														   	//
	//			This shader was written by Casey MacNeil		//	   
	//														   	//
	//////////////////////////////////////////////////////////////
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "gray" {}
		_Ambient("Ambient Strength", Range(0,1)) = 0.1

		[Space(25)][Toggle]_Specular("Use Specular", Float) = 0
		_SpecularMap("Specular", 2D) = "white" {}
		[IntRange] _Gloss("Specular Intensity", Range(0, 256)) = 0

		[Space(25)]_EmissiveMap("Emissive Texture", 2D) = "black" {}
		_EmissiveBlend("Emissive Blend", Color) = (1, 1, 1, 1)

		[Space(25)]_DissolveMap("Dissolve Texture", 2D) = "gray" {}
		_DissolveBlend("Dissolve Blend", Color) = (1, 0, 0, 1)
			_DissolveThreshold("Dissolve Threshold", Range(0,1.1)) = 0

		[Space(25)]_ColorBlend("Color", Color) = (1, 1, 1, 1)
	}
	SubShader{
			Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
			Blend One Zero
			CGPROGRAM

		#include "CelShadingIncludes.cginc"
		#pragma surface surf CelShading fullforwardshadows
		#pragma target 3.0

		half _Specular;
		fixed _Gloss;

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalMap;
			float2 uv_SpecularMap;
			float2 uv_EmissiveMap;
			float2 uv_DissolveMap;
			float3 viewDir;
		};

		sampler2D _MainTex;
		sampler2D _NormalMap;
		sampler2D _SpecularMap;
		sampler2D _EmissiveMap;
		sampler2D _DissolveMap;
		half4 _ColorBlend;
		half3 _DissolveBlend;
		float _DissolveThreshold;


		void surf(Input IN, inout SurfaceOutput o) {
			half4 texel = tex2D(_MainTex, IN.uv_MainTex);

			fixed dissolve = 1 - tex2D(_DissolveMap, IN.uv_DissolveMap).r;

			if (dissolve < _DissolveThreshold - 0.04)discard;

			o.Gloss = _Gloss;
			o.Specular = _Specular * tex2D(_SpecularMap, IN.uv_SpecularMap).r;
			texel *= _ColorBlend;

			clip(texel.a - 0.1);
			o.Albedo = texel.rgb;
			o.Alpha = texel.a;
			o.Emission = tex2D(_EmissiveMap, IN.uv_EmissiveMap).rgb + (lerp(1,0,1- saturate(_DissolveThreshold - dissolve) / 0.04) * _DissolveBlend);
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
		}
		ENDCG
	}
		Fallback "Diffuse"
}