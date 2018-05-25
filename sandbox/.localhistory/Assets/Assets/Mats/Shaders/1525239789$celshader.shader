Shader "Unlit/CelShader"
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
				vec2 offsets[9] = vec2[](
					vec2(-offset,  offset), // top-left
					vec2(0.0f,    offset), // top-center
					vec2(offset,  offset), // top-right
					vec2(-offset,  0.0f),   // center-left
					vec2(0.0f,    0.0f),   // center-center
					vec2(offset,  0.0f),   // center-right
					vec2(-offset, -offset), // bottom-left
					vec2(0.0f,   -offset), // bottom-center
					vec2(offset, -offset)  // bottom-right    
					);

				float kernel[9] = float[](
					-1, -1, -1,
					-1, 9, -1,
					-1, -1, -1
					);

				fixed4 textures[9];
				for (int i = 0; i < 9; i++) 
				{
					textures[i] = half4(tex2D(_MainTex, i.uv + offsets[i]));
				}


				fixed3 col = tex2D(_MainTex, i.uv);

				float average = (col.r + col.g + col.b) / 3.0;
				col = fixed4(average, average, average, 1.0);
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
