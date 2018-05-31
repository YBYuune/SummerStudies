Shader "Casey/CelShading - Bubble"
{
	//////////////////////////////////////////////////////////////
	//														   	//
	//			This shader was written by Casey MacNeil		//	   
	//														   	//
	//////////////////////////////////////////////////////////////
	Properties{
		_SpecularMap("Specular", 2D) = "white" {}
	[IntRange] _Gloss("Specular Intensity", Range(0, 256)) = 0
	}
		SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		ZWrite off
		Blend DstColor SrcColor
		AlphaToMask On
		CGPROGRAM
#pragma surface surf CelShading fullforwardshadows alpha:fade

	half _Specular = 1.0;
	fixed _Gloss;
	half3 vDir = half3(0,0,0);

	half4 LightingCelShading(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
		//diffuse
		half NdotL = dot(s.Normal, lightDir);
		if (NdotL <= 0.0)NdotL = 0.0;
		else NdotL = 1.0;

		//specular

		half3 h = normalize(lightDir + viewDir);
		float nh = (dot(s.Normal, h));

		float spec = pow(nh, s.Gloss) * s.Specular;
		if (spec > .5) spec = 1.0;
		else spec = 0.0;

		half4 c;
		half3 specular = _LightColor0.rgb * spec;

		c.rgb = specular;
		c.a = s.Alpha;// *length(specular);
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
		o.Specular = tex2D(_SpecularMap, IN.uv_SpecularMap).r;
		texel *= _ColorBlend;

		clip(texel.a - 0.1);
		o.Albedo = texel.rgb;
		o.Alpha = texel.a;
		o.Emission = tex2D(_EmissiveMap, IN.uv_EmissiveMap).rgb;
	}
	ENDCG
	}
		Fallback "Diffuse"
}