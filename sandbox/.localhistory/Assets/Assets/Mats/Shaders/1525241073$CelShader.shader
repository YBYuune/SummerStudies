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
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 gles
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
				float2 offsets[9] = vec2[](
					float2(-offset,  offset), // top-left
					float2(0.0f,    offset), // top-center
					float2(offset,  offset), // top-right
					float2(-offset,  0.0f),   // center-left
					float2(0.0f,    0.0f),   // center-center
					float2(offset,  0.0f),   // center-right
					float2(-offset, -offset), // bottom-left
					float2(0.0f,   -offset), // bottom-center
					float2(offset, -offset)  // bottom-right    
					);

				float kernel[9] = float[](
					-1, -1, -1,
					-1, 9, -1,
					-1, -1, -1
					);

				fixed4 textures[9];
				for (int i = 0; i < 9; i++) 
				{
					textures[i] = fixed3(tex2D(_MainTex, i.uv + offsets[i]).rgb);
				}

				fixed3 col = vec3(0.0);
				for (int i = 0; i < 9; i++)
					col += textures[i] * kernel[i];
				

				fixed4 FragColor = fixed4(col, 1.0);
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return FragColor;
			}
			ENDCG
		}
	}
}
