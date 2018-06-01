Shader "Casey/CelShading" 
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

		[Space(25)]_ColorBlend("Color", Color) = (1, 1, 1, 1)
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf CelShading fullforwardshadows

		half _Ambient;
		half _Specular;
		fixed _Gloss;

		half3 _EmissiveBlend;

		half4 LightingCelShading(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
			//diffuse
			half NdotL = dot(s.Normal, lightDir);

			half TAtten = atten;
			if (TAtten > .0) TAtten = 1.0;
			else TAtten = 0.0;

			//specular

			half3 h = normalize(lightDir + viewDir);
			float nh = (dot(s.Normal, h));
			float spec = pow(nh, s.Gloss) * s.Specular;
			half4 c;

			half3 ambient = _LightColor0.rgb * _Ambient;
			half3 diffuse = _LightColor0.rgb * pow(NdotL * 0.5 + 0.5, 2.0);
			half3 specular = _LightColor0.rgb * spec;

			c.rgb = ((ambient + diffuse) * (TAtten * 2) * s.Albedo) + (s.Emission * _EmissiveBlend) + specular;
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalMap;
			float2 uv_SpecularMap;
			float2 uv_EmissiveMap;
			float3 viewDir;
			float3 worldPos;
		};

		//_NormalMap("Normal Map", 2D) = "gray" {}
		sampler2D _MainTex; 
		sampler2D _NormalMap;
		sampler2D _SpecularMap;
		sampler2D _EmissiveMap;
		half4 _ColorBlend;

		void surf(Input IN, inout SurfaceOutput o) {
			half4 texel = tex2D(_MainTex, IN.uv_MainTex);

			o.Gloss = _Gloss;
			o.Specular = _Specular * tex2D(_SpecularMap, IN.uv_SpecularMap).r;
			texel *= _ColorBlend;

			clip(texel.a - 0.1);
			o.Albedo = texel.rgb;
			o.Alpha = texel.a;
			o.Emission = tex2D(_EmissiveMap, IN.uv_EmissiveMap).rgb;
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
		}
		ENDCG
	}
		Fallback "Diffuse"
}