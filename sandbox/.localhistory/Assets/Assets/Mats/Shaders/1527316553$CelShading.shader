Shader "Custom/CelShading" 
{
	//////////////////////////////////////////////////////////////
	//														   	//
	//			This shader was written by Casey MacNeil		//	   
	//														   	//
	//////////////////////////////////////////////////////////////
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Specular("Specular Power", Range(0, 1)) = 0
		_Gloss("Specular Intensity", Float) = 0
		_Emissive("Emissive Texture", 2D) = "white" {}
		_ColorBlend("Color", Color) = (1,1,1,1)
		[MaterialToggle]_isTerrain("Is Terrain", Float) = 0
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf CelShading fullforwardshadows

		fixed _Specular;

		half4 LightingCelShading(SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot(s.Normal, lightDir);
			if (NdotL <= 0.0)NdotL = 0.0;
			else NdotL = 1.0;

			half TAtten = atten;

			if (TAtten > .0) TAtten = 1.0;
			else TAtten = 0.0;

			half4 c;
			c.rgb = (((s.Albedo * _LightColor0.rgb) + (_LightColor0.rgb * s.Specular * s.Gloss))  * (NdotL * TAtten));
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_MainTex; 
			float3 viewDir;
			float3 worldPos;
		};

		sampler2D _MainTex;
		half3 _ColorBlend;
		bool _isTerrain;

		void surf(Input IN, inout SurfaceOutput o) {
			half4 texel = tex2D(_MainTex, IN.uv_MainTex);
			if (texel.a > 0.1)
				o.Albedo = texel.rgb*_ColorBlend.rgb;
			else if(!_isTerrain)
				discard;
		}
		ENDCG
	}
		Fallback "Diffuse"
}