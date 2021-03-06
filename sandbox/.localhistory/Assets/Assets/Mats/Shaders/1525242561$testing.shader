﻿Shader "Unlit/testing"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float offset = 1.0 / 300.0;
				float2 offsets[9] = {
					float2(-offset,  offset), // top-left
					float2(0.0,    offset), // top-center
					float2(offset,  offset), // top-right
					float2(-offset,  0.0),   // center-left
					float2(0.0,    0.0),   // center-center
					float2(offset,  0.0),   // center-right
					float2(-offset, -offset), // bottom-left
					float2(0.0,   -offset), // bottom-center
					float2(offset, -offset)  // bottom-right    
				};

				float kernel[9] = {
					-1.0, -1.0, -1.0,
					-1.0, 9.0, -1.0,
					-1.0, -1.0, -1.0
				};

				fixed3 textures[9];
				for (int j = 0; j < 9; j++)
				{
					textures[j] = tex2D(_MainTex, i.uv + offsets[j]).rgb;
				}

				fixed3 col = fixed3(0.0, 0.0, 0.0);
				for (j = 0; j < 9; j++)
				{
					col += textures[j] * kernel[j];
				}

				fixed4 FragColor = fixed4(col, 1.0);
				if(col.r >= 0.1 || col.g >= 0.1 || col.b >= 0.1)
				{
					FragColor = tex2D(_MainTex, i.uv);
				}
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return FragColor;
			}
			ENDCG
		}
	}
}
