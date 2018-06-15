﻿Shader "Casey-Screen/PostProcessingAdvanced"
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
		_DepthSlider("Depth", Range(0.0,0.01)) = 0.0
		[IntRange]_OutlineThickness("Outline Quality", Range(1024,4096)) = 4096
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		Cull Off 
		ZWrite Off 
		ZTest Always
		Pass
		{
			CGPROGRAM
			//	#pragma exclude_renderers d3d11 gles
		#pragma vertex vert
		#pragma fragment frag
			// make fog work
		#pragma multi_compile_fog


		#include "PostFunctions.cginc"

			struct appdata
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
		};

		struct v2f
		{
			float2 uv : TEXCOORD0;
			float4 screenPos : TEXCOORD1;
			UNITY_FOG_COORDS(1)
			float4 vertex : SV_POSITION;
		};

		sampler2D _MainTex;
		half4 _Outline;
		float _OutlineThickness;
		float4 _MainTex_ST;

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			o.screenPos = ComputeGrabScreenPos(o.vertex);
			//UNITY_TRANSFER_FOG(o,o.vertex);
			return o;
		}

		// --------------------------------------------------
		// ---------------------- FRAGMENT ------------------
		// --------------------------------------------------
		//
		
		//
		
		sampler2D _CameraDepthTexture;
		float _DepthSlider;
		fixed4 frag(v2f i) : SV_Target
		{
			// sample the texture
			float offset = 1.0 / _OutlineThickness;
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

			float2 offsets5x5[25] = {
				float2(-offset * 2,  offset * 2), // top-left
				float2(-offset,  offset * 2), // top-left
				float2(0.0,    offset * 2), // top-center
				float2(offset,  offset * 2), // top-right
				float2(offset * 2,  offset * 2), // top-right

				float2(-offset * 2,  offset), // top-left
				float2(-offset,  offset), // top-left
				float2(0.0,    offset), // top-center
				float2(offset,  offset), // top-right
				float2(offset * 2,  offset), // top-right

				float2(-offset * 2,  0.0),   // center-left
				float2(-offset,  0.0),   // center-left
				float2(0.0,    0.0),   // center-center
				float2(offset,  0.0),   // center-right
				float2(offset * 2,  0.0),   // center-right

				float2(-offset * 2, -offset), // bottom-left
				float2(-offset, -offset), // bottom-left
				float2(0.0,   -offset), // bottom-center
				float2(offset, -offset),  // bottom-right
				float2(offset * 2, -offset),  // bottom-right

				float2(-offset * 2, -offset * 2), // bottom-left
				float2(-offset, -offset * 2), // bottom-left
				float2(0.0,   -offset * 2), // bottom-center
				float2(offset, -offset * 2),  // bottom-right
				float2(offset * 2, -offset * 2)  // bottom-right
			};

			float KERNEL_GBLUR[9] = {
				1.0 / 16.0, 2.0 / 16.0, 1.0 / 16.0,
				2.0 / 16.0, 4.0 / 16.0, 2.0 / 16.0,
				1.0 / 16.0, 2.0 / 16.0, 1.0 / 16.0
			};

			//5x5gblur
			
			//float KERNEL_GBLUR5X5[25] = {
			//	2.0 / 159.0,	4.0 / 159.0,	5.0 / 159.0,	4.0 / 159.0,	2.0 / 159.0,
			//	4.0 / 159.0,	9.0 / 159.0,	12.0 / 159.0,	9.0 / 159.0,	4.0 / 159.0,
			//	5.0 / 159.0,	12.0 / 159.0,	15.0 / 159.0,	12.0 / 159.0,	5.0 / 159.0,
			//	4.0 / 159.0,	9.0 / 159.0,	12.0 / 159.0,	9.0 / 159.0,	4.0 / 159.0,
			//	2.0 / 159.0,	4.0 / 159.0,	5.0 / 159.0,	4.0 / 159.0,	2.0 / 159.0
			//};

			//

			fixed3 textures[9];
			for (int j = 0; j < 9; j++)
			{
				textures[j] = tex2D(_MainTex, i.uv + offsets[j]).rgb;
			}

			fixed3 textures5x5[25];
			for (j = 0; j < 25; j++)
			{
				textures5x5[j] = tex2D(_MainTex, i.uv + offsets5x5[j]).rgb;
			}

			fixed3 texel = tex2D(_MainTex, i.uv).rgb;

			fixed4 FragColor = fixed4(texel, 1.0);

			{
				// gaussian blur applied before sobel
				fixed3 t00[9];
				fixed3 t01[9];
				fixed3 t02[9];

				fixed3 t10[9];
				fixed3 t11[9];
				fixed3 t12[9];

				fixed3 t20[9];
				fixed3 t21[9];
				fixed3 t22[9];

				Get3x3From5x5(textures5x5, 6, t00);
				Get3x3From5x5(textures5x5, 7, t01);
				Get3x3From5x5(textures5x5, 8, t02);

				Get3x3From5x5(textures5x5, 6 + 5, t10);
				Get3x3From5x5(textures5x5, 7 + 5, t11);
				Get3x3From5x5(textures5x5, 8 + 5, t12);

				Get3x3From5x5(textures5x5, 6 + 10, t20);
				Get3x3From5x5(textures5x5, 7 + 10, t21);
				Get3x3From5x5(textures5x5, 8 + 10, t22);

				fixed3 result[9];
				{
					result[0] = ApplyKernel3x3(KERNEL_GBLUR, t00);
					result[1] = ApplyKernel3x3(KERNEL_GBLUR, t01);
					result[2] = ApplyKernel3x3(KERNEL_GBLUR, t02);

					result[3] = ApplyKernel3x3(KERNEL_GBLUR, t10);
					result[4] = ApplyKernel3x3(KERNEL_GBLUR, t11);
					result[5] = ApplyKernel3x3(KERNEL_GBLUR, t12);

					result[6] = ApplyKernel3x3(KERNEL_GBLUR, t20);
					result[7] = ApplyKernel3x3(KERNEL_GBLUR, t21);
					result[8] = ApplyKernel3x3(KERNEL_GBLUR, t22);
				}

				FragColor = fixed4((ApplySobel(result)), 1.0);

				{ // stupid math to inverse colors to apply a color to the outline produced from the sobel.
					_Outline = fixed4(fixed3(1, 1, 1) - _Outline.rgb, 1.0);  // invert outline
					FragColor = fixed4(FragColor.rgb * _Outline, 1.0);       // inverse outline * sobel
					FragColor = fixed4(fixed3(1, 1, 1) - FragColor.rgb, 1.0);// inverse sobel
					FragColor = fixed4(FragColor.rgb * texel, 1.0);			 // inverse sobel * texel
				}
			}

			// depth stuff
			//float depthValue;
			//depthValue = Linear01Depth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)).r);

			//if (depthValue > _DepthSlider)
			//{
			//	FragColor = fixed4(1.0 - FragColor.r, 1.0 - FragColor.g, 1.0 - FragColor.b, 1.0);
			//}

			return FragColor;
		}
			ENDCG
		}
	}
}
