Shader "Unlit/WaterShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_NormalMap("Normal Map", 2D) = "bump" {}
		_ColorBlend ("Color Blend", Color) = (1,1,1,1)
			_RefractAmount("Refract Amount", Range(0,3)) = 0.0
			_WaveHeight("Wave Height", Range(0,10)) = 0.0
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
				float3 viewDir : TEXCOORD2;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _BackgroundTexture;
			sampler2D _MainTex;
			sampler2D _NormalMap;
			float4 _MainTex_ST;
			float4 _ColorBlend;
			float _RefractAmount;
			float _WaveHeight;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				o.vertex.y += sin(pow(_Time,_WaveSpeed) + v.vertex.x + v.vertex.z) * _WaveHeight;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				float3 viewDir = WorldSpaceViewDir(o.vertex);
				o.grabPos = ComputeGrabScreenPos(o.vertex);


				o.viewDir = viewDir;

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture

				float3 norm = tex2D(_NormalMap, i.uv);
				float4 R = float4(refract(normalize(i.viewDir), norm, 1.0 / _RefractAmount), 1.0);

				fixed4 col = tex2Dproj(_BackgroundTexture, UNITY_PROJ_COORD(i.grabPos + R));
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col * _ColorBlend;
			}
			ENDCG
		}
	}
}
