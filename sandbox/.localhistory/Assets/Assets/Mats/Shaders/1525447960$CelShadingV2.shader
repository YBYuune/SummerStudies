Shader "Screen/PostProcessingAdvanced"
{
	//////////////////////////////////////////////////////////////
	//														   	//
	//			This shader was written by Casey MacNeil		//	   
	//														   	//
	//////////////////////////////////////////////////////////////
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Outline("Outline", Color) = (0,0,0,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
//	#pragma exclude_renderers d3d11 gles
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
			half4 _Outline;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			// --------------------------------------------------
			// ---------------------- FRAGMENT ------------------
			// --------------------------------------------------

			fixed3 ApplyKernel(float kernel[9], fixed3 textures[9]) // function for applying basic kernels
			{
				fixed3 col = fixed3(0.0, 0.0, 0.0);
				for (int j = 0; j < 9; j++)
				{
					col += textures[j] * (kernel[j]);
				}
				return col;
			}

			fixed3 Grayscale(fixed3 frag) 
			{
				float average = 0.2126 * frag.r + 0.7152 * frag.g + 0.0722 * frag.b;
				return fixed3(average, average, average);
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

				float KERNEL_SHARPEN[9] = {
					0.0, -1.0, 0.0,
					-1.0, 5.0, -1.0,
					0.0, -1.0, 0.0
				};
				float KERNEL_SHARPEN2[9] = {
					-1.0, -1.0, -1.0,
					-1.0, 9.0,  -1.0,
					-1.0, -1.0, -1.0
				};

				float KERNEL_EDGEDETECT[9] = {
					1.0, 1.0, 1.0,
					1.0, -8.0, 1.0,
					1.0, 1.0, 1.0
				};

				float KERNEL_GBLUR[9] = {
					1.0/16.0, 2.0/16.0, 1.0/16.0,
					2.0/16.0, 4.0/16.0, 2.0/16.0,
					1.0/16.0, 2.0/16.0, 1.0/16.0
				};


				fixed3 textures[9];
				for (int j = 0; j < 9; j++)
				{
					textures[j] = tex2D(_MainTex, i.uv + offsets[j]).rgb;
				}


				fixed4 FragColor = fixed4(ApplyKernel(KERNEL_EDGEDETECT, textures), 1.0);
				fixed4 FragColor = fixed4(Grayscale(FragColor.rgb), 1.0);

				return FragColor;
			}
			ENDCG
		}
	}
}
