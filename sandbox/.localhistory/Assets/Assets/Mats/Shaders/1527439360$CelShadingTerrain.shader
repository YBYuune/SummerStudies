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
		LOD 200

		CGPROGRAM
		#pragma surface surf CelShading fullforwardshadows
		#pragma target 3.0
		#pragma debug

		half4 LightingCelShading(SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot(s.Normal, lightDir);
			if (NdotL <= 0.0)NdotL = 0.0;
			else NdotL = 1.0;
			half4 c;

			half TAtten = atten;

			if (TAtten > .0) TAtten = 1.0;
			else TAtten = 0.0;

			half3 ambient = _LightColor0.rgb * 0.1;
			c.rgb = (s.Albedo * _LightColor0.rgb * (NdotL * TAtten)) + ambient;
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_Control;
			float2 uv_Splat0;
		};

		sampler2D _Control;
		sampler2D _Splat0;
		sampler2D _Splat1;
		sampler2D _Splat2;
		sampler2D _Splat3;

		void surf(Input IN, inout SurfaceOutput o) {

			half4 c = tex2D(_Control, IN.uv_Control);
			half4 splat0 = tex2D(_Splat0, IN.uv_Splat0);
			half4 splat1 = tex2D(_Splat1, IN.uv_Splat0);
			half4 splat2 = tex2D(_Splat2, IN.uv_Splat0);
			half4 splat3 = tex2D(_Splat3, IN.uv_Splat0);
			o.Albedo = splat0 * c.r + splat1 * c.g + splat2 * c.b + splat3 * c.a;
		}
		ENDCG
	}
		Fallback "Diffuse"
}