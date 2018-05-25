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
			float2 uv_MainTex;
		};

		sampler2D _Control;
		half3 _ColorBlend;

		void surf(Input IN, inout SurfaceOutput o) {
			half4 texel = tex2D(_MainTex, IN.uv_MainTex);
			if (texel.a > 0.1)
				o.Albedo = texel.rgb*_ColorBlend.rgb;
			else
				discard;
		}
		ENDCG
	}
		Fallback "Diffuse"
}