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

			half3 viewDir = normalize(_WorldSpaceCameraPos - );
			half spec = pow(nh, 48);
			
			if (spec <= 0.0)spec = 0.0;
			else spec = 1.0;

			half TAtten = atten;

			if (TAtten > .0) TAtten = 1.0;
			else TAtten = 0.0;

			half4 c;
			c.rgb = ((s.Albedo * _LightColor0.rgb)  * (NdotL * TAtten)) + ((s.Albedo * _LightColor0.rgb)  * (spec * TAtten * _Specular));
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_MainTex;
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