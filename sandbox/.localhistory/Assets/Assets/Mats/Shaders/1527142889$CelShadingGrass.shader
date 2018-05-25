Shader "Custom/CelShadingTerrain" 
{
	//////////////////////////////////////////////////////////////
	//														   	//
	//			This shader was written by Casey MacNeil		//	   
	//														   	//
	//////////////////////////////////////////////////////////////
	Properties{
		// Splat Map Control Texture
		[HideInInspector] _Control("Control (RGBA)", 2D) = "red" {}

		// Textures
		[HideInInspector] _Splat3("Layer 3 (A)", 2D) = "white" {}
		[HideInInspector] _Splat2("Layer 2 (B)", 2D) = "white" {}
		[HideInInspector] _Splat1("Layer 1 (G)", 2D) = "white" {}
		[HideInInspector] _Splat0("Layer 0 (R)", 2D) = "white" {}

		// Normal Maps
		[HideInInspector] _Normal3("Normal 3 (A)", 2D) = "bump" {}
		[HideInInspector] _Normal2("Normal 2 (B)", 2D) = "bump" {}
		[HideInInspector] _Normal1("Normal 1 (G)", 2D) = "bump" {}
		[HideInInspector] _Normal0("Normal 0 (R)", 2D) = "bump" {}
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf CelShading

		half4 LightingCelShading(SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot(s.Normal, lightDir);
			if (NdotL <= 0.0)NdotL = 0.0;
			else NdotL = 1.0;
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten);
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_Control;
		};

		sampler2D _Control;
		sampler2D _Splat0;
		sampler2D _Splat1;
		sampler2D _Splat2;
		sampler2D _Splat3;

		void surf(Input IN, inout SurfaceOutput o) {

			half4 texel = tex2D(_Control, IN.uv_MainTex);
			o.Albedo = texel.rgb;
		}
		ENDCG
	}
		Fallback "Diffuse"
}