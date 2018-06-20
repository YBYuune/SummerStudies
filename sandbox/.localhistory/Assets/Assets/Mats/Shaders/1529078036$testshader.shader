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

		struct v2f
	{
		float4 grabPos : TEXCOORD0;
		float4 pos : SV_POSITION;
	};

	v2f vert(appdata_base v) {
		v2f o;
		// use UnityObjectToClipPos from UnityCG.cginc to calculate 
		// the clip-space of the vertex
		o.pos = UnityObjectToClipPos(v.vertex);
		// use ComputeGrabScreenPos function from UnityCG.cginc
		// to get the correct texture coordinate
		float3 viewDir = WorldSpaceViewDir(o.pos);
		float3 normWorld = refract(normalize(viewDir), normalize(o.pos), .0001);
		o.grabPos = ComputeGrabScreenPos(o.pos*float4(normWorld.x, normWorld.y, normWorld.z,1.0));
		return o;
	}

	sampler2D _BackgroundTexture;

	half4 frag(v2f i) : SV_Target
	{
		half4 bgcolor = tex2Dproj(_BackgroundTexture, i.grabPos);
		return 1 - bgcolor;
	}
		ENDCG
	}

	}
}