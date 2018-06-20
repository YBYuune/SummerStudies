Shader "GrabPassInvert"
{
	SubShader
	{
		// Draw ourselves after all opaque geometry
		Tags{ "Queue" = "Transparent" }

		// Grab the screen behind the object into _BackgroundTexture
		GrabPass
	{
		"_BackgroundTexture"
	}
		Cull Off
		// Render the object with the texture generated above, and invert the colors
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

		struct v2f
	{
		float4 grabPos : TEXCOORD0;
		float3 normal : TEXCOORD1;
		float4 pos : SV_POSITION;
	};

	v2f vert(appdata v) {
		v2f o;
		// use UnityObjectToClipPos from UnityCG.cginc to calculate 
		// the clip-space of the vertex
		o.pos = UnityObjectToClipPos(v.vertex);
		// use ComputeGrabScreenPos function from UnityCG.cginc
		// to get the correct texture coordinate
		float3 viewDir = WorldSpaceViewDir(o.pos);
		float3 normWorld = normalize(viewDir);
		o.grabPos = ComputeGrabScreenPos(o.pos*float4(normWorld.x, normWorld.y, normWorld.z,1.0));
		o.normal = v.normal;
		return o;
	}

	sampler2D _BackgroundTexture;

	half4 frag(v2f i) : SV_Target
	{

		float3 dnx = ddx(i.normal);
		float3 dny = ddy(i.normal);

		float lines = length(dnx) + length(dny);

		lines = lines * 12.75;
		lines = lines - 1.0;
		lines = clamp(lines, 0.0, 1.0);

		half4 bgcolor = half4(dnx + dny * 100,1);// tex2Dproj(_BackgroundTexture, i.grabPos);
		bgcolor = lerp(bgcolor, half4(0, 0, 0, 1), lines);
		return bgcolor;
	}
		ENDCG
	}

	}
}