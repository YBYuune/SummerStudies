Shader "Unlit/WaterShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_NormalMap("Normal Map", 2D) = "bump" {}
		_ColorBlend ("Color Blend", Color) = (1,1,1,1)
			_RefractAmount("Refract Amount", Range(0,3)) = 0.0
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" }
		LOD 100

		GrabPass
		{
			"_BackgroundTexture"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : Normal;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;

				float4 grabPos : TEXCOORD1;
				float4 R : TEXCOORD2;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _BackgroundTexture;
			sampler2D _MainTex;
			sampler2D _NormalMap;
			float4 _MainTex_ST;
			float4 _ColorBlend;
			float _RefractAmount;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				float3 viewDir = WorldSpaceViewDir(o.vertex);
				o.grabPos = ComputeGrabScreenPos(o.vertex);

				float norm = UnpackNormal(tex2D(_NormalMap, o.uv));

				o.R =  float4(refract(normalize(viewDir),v.normal.xyz, 1.0/_RefractAmount),v.normal.w);

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2Dproj(_BackgroundTexture, UNITY_PROJ_COORD(i.grabPos + i.R));
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col * _ColorBlend;
			}
			ENDCG
		}
	}
}
