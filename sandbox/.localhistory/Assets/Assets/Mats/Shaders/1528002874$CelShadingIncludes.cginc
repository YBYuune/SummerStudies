#ifndef CELSHADING_INCLUDE
#define CELSHADING_INCLUDE

#include "UnityCG.cginc"

inline half4 LightingCelShading(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
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
	half3 diffuse = _LightColor0.rgb * pow(NdotL * 0.5 + 0.5, 2.0);
	half3 specular = _LightColor0.rgb * spec;

	c.rgb = ((ambient + diffuse) * (TAtten * 2) * s.Albedo) + (s.Emission * _EmissiveBlend) + specular;
	c.a = s.Alpha;
	return c;
}

#endif