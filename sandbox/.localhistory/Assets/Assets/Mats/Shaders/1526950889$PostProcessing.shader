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
		[MaterialToggle]_isOutlined("Is Outlined", Float) = 0
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
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
		bool _isOutlined;
		float4 _MainTex_ST;

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			//UNITY_TRANSFER_FOG(o,o.vertex);
			return o;
		}

		// --------------------------------------------------
		// ---------------------- FRAGMENT ------------------
		// --------------------------------------------------

		fixed3 ApplyKernel3x3(float kernel[9], fixed3 textures[9]) // function for applying basic kernels
		{
			fixed3 col = fixed3(0.0, 0.0, 0.0);
			for (int j = 0; j < 9; j++)
			{
				col += textures[j] * (kernel[j]);
			}
			return col;
		}

		fixed3 ApplyKernel5x5(float kernel[25], fixed3 textures[25]) // function for applying basic kernels
		{
			fixed3 col = fixed3(0.0, 0.0, 0.0);
			for (int j = 0; j < 25; j++)
			{
				col += textures[j] * (kernel[j]);
			}
			return col;
		}

		void Get3x3From5x5(fixed3 tex[25], int index, out fixed3 result[9])
		{
			fixed3 a[9] = { tex[index - 6], tex[index - 5], tex[index - 4],
							tex[index - 1], tex[index], tex[index + 1],
							tex[index + 4], tex[index + 5], tex[index + 6] };
			result = a;
		}

		fixed3 Grayscale(fixed3 frag)
		{
			float average = 0.2126 * frag.r + 0.7152 * frag.g + 0.0722 * frag.b;
			return fixed3(average, average, average);
		}

		fixed3 ApplySobel(fixed3 textures[9]) // applys the sobel xy kernel's on a grayscale version of the image passed in
		{
			float3x3 KERNEL_SOBELX = float3x3(
				-1.0, 0.0, 1.0,
				-2.0, 0.0, 2.0,
				-1.0, 0.0, 1.0
				);
			float3x3 KERNEL_SOBELY = float3x3(
				1.0, 2.0, 1.0,
				0.0, 0.0, 0.0,
				-1.0, -2.0, -1.0
				);

			float mag = 0.0;

			float mGx = 0.0;
			float mGy = 0.0;

			for (int i = 0, k = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++, k++)
				{
					float Gx = KERNEL_SOBELX[i][j] * Grayscale(textures[k]);
					float Gy = KERNEL_SOBELY[i][j] * Grayscale(textures[k]);

					mGx += Gx;
					mGy += Gy;
				}
			}

			mag = sqrt(pow(mGx, 2) + pow(mGy, 2));

			return (fixed3(mag, mag, mag)) * 1.5;

		}

		fixed4 frag(v2f i) : SV_Target
		{
			// sample the texture
			float offset = 1.0 / 2048.0;
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
				1.0 / 16.0, 2.0 / 16.0, 1.0 / 16.0,
				2.0 / 16.0, 4.0 / 16.0, 2.0 / 16.0,
				1.0 / 16.0, 2.0 / 16.0, 1.0 / 16.0
			};

			float KERNEL_GBLUR5X5[25] = {
				2.0 / 159.0,	4.0 / 159.0,	5.0 / 159.0,	4.0 / 159.0,	2.0 / 159.0,
				4.0 / 159.0,	9.0 / 159.0,	12.0 / 159.0,	9.0 / 159.0,	4.0 / 159.0,
				5.0 / 159.0,	12.0 / 159.0,	15.0 / 159.0,	12.0 / 159.0,	5.0 / 159.0,
				4.0 / 159.0,	9.0 / 159.0,	12.0 / 159.0,	9.0 / 159.0,	4.0 / 159.0,
				2.0 / 159.0,	4.0 / 159.0,	5.0 / 159.0,	4.0 / 159.0,	2.0 / 159.0
			};


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

			if (_isOutlined)
			{
				FragColor = fixed4((ApplySobel(textures)), 1.0);

				{ // stupid math to inverse colors to apply a color to the outline produced from the sobel.
					_Outline = fixed4(fixed3(1,1,1) - _Outline.rgb,1.0);
					FragColor = fixed4(FragColor.rgb * _Outline, 1.0);
					FragColor = fixed4(fixed3(1, 1, 1) - FragColor.rgb, 1.0);
					FragColor = fixed4(FragColor.rgb * texel, 1.0);
				}
			}
			else
			{

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
				Get3x3From5x5(textures5x5, 6, t01);
				Get3x3From5x5(textures5x5, 6, t02);

				Get3x3From5x5(textures5x5, 6, t10);
				Get3x3From5x5(textures5x5, 6, t11);
				Get3x3From5x5(textures5x5, 6, t12);

				Get3x3From5x5(textures5x5, 6, t20);
				Get3x3From5x5(textures5x5, 6, t21);
				Get3x3From5x5(textures5x5, 6, t22);

				fixed3 result[9];

				//for (int i = 0; i < 9; i++) 
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

				FragColor = fixed4(((result)), 1.0);
				//FragColor = fixed4((ApplyKernel3x3(KERNEL_GBLUR, t00)), 1.0);
			}
			return FragColor;
		}
			ENDCG
		}
	}
}
