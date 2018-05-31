﻿Shader "Casey/CelShading - Bubble" 
{
	//////////////////////////////////////////////////////////////
	//														   	//
	//			This shader was written by Casey MacNeil		//	
	//				Modified by: Travis Milne-Ruttan			//
	//														   	//
	//////////////////////////////////////////////////////////////
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Ambient("Ambient Strength", Range(0,1)) = 0.1

		[Space(25)][Toggle]_Specular("Use Specular", Float) = 0
		_SpecularMap("Specular", 2D) = "white" {}
		[IntRange] _Gloss("Specular Intensity", Range(0, 256)) = 0

		[Space(25)]_EmissiveMap("Emissive Texture", 2D) = "black" {}
		_EmissiveBlend("Emissive Blend", Color) = (1, 1, 1, 1)

		[Space(25)]_ColorBlend("Color", Color) = (1, 1, 1, 1)
		[MaterialToggle]_isTerrain("Is Terrain", Float) = 0


			_OutlineColour("Outline Colour", Color) = (1,1,1,1)
			_OutlineThickness("Outline Thickness", Range(0.0, 1.0)) = 0.05
	}
		SubShader{
			Tags{ "RenderType" = "Opaque" }

			Stencil{
			Ref 7
			Comp always
			Pass replace
		}
			CGPROGRAM
			
			#pragma surface surf CelShading fullforwardshadows

			half _Ambient;
			half _Specular;
			fixed _Gloss;

			half3 _EmissiveBlend;

			half3 vDir = half3(0,0,0);

			half4 LightingCelShading(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
				//diffuse
				half NdotL = dot(s.Normal, lightDir);
				if (NdotL <= 0.0)NdotL = 0.0;
				else NdotL = 1.0;

				half TAtten = atten;
				if (TAtten > .0) TAtten = 1.0;
				else TAtten = 0.0;

				//specular

				half3 h = normalize(lightDir + viewDir);
				float nh = (dot(s.Normal, h));
				float spec = pow(nh, s.Gloss) * s.Specular;
				if (spec > .5) spec = 1.0;
				else spec = 0.0;
				half4 c;

				half3 ambient = _LightColor0.rgb * _Ambient;
				half3 diffuse = _LightColor0.rgb * NdotL;
				half3 specular = _LightColor0.rgb * spec;

				c.rgb = specular;
				if (length(specular) <= 0.0)
					discard;
				c.a = s.Alpha;
				return c;
			}

			struct Input {
				float2 uv_MainTex;
				float2 uv_SpecularMap;
				float2 uv_EmissiveMap;
				float3 viewDir;
				float3 worldPos;
			};

			sampler2D _MainTex;
			sampler2D _SpecularMap;
			sampler2D _EmissiveMap;
			half4 _ColorBlend;
			bool _isTerrain;

			void surf(Input IN, inout SurfaceOutput o) {
				half4 texel = tex2D(_MainTex, IN.uv_MainTex);

				vDir = IN.viewDir;

				o.Gloss = _Gloss;
				o.Specular = _Specular * tex2D(_SpecularMap, IN.uv_SpecularMap).r;
				texel *= _ColorBlend;

				clip(texel.a - 0.1);
				o.Albedo = texel.rgb;
				o.Alpha = texel.a;
				o.Emission = tex2D(_EmissiveMap, IN.uv_EmissiveMap).rgb;
			}
			ENDCG


				Stencil
			{
				Ref 7
				Comp NotEqual
				Pass Keep
			}
				CGPROGRAM

#pragma surface surf Standard vertex:vert finalcolor:colour
				struct Input {
				float3 worldPos;
			};


			float4 _OutlineColour;
			float _OutlineThickness;

			void vert(inout appdata_full v)
			{
				v.vertex.xyz += v.normal * _OutlineThickness;
			}

			void colour(Input IN, SurfaceOutputStandard o, inout fixed4 color)
			{
				color = _OutlineColour;
				discard;
			}

			void surf(Input IN, inout SurfaceOutputStandard o) {
			}
				ENDCG
			
		}
		Fallback "Diffuse"
}