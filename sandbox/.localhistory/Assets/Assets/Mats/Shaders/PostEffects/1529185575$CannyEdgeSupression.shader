Shader "Casey-Screen/CannyEdgeDetect-NonMaxSupression"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_UpperThreshold("Upper Threshold", Float) = 0.5
		_Res("Resolution", Float) = 4096
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "PostFunctions.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Res;
			float _UpperThreshold;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				
				float2 offsets[9];
				GetOffsets3x3(_Res, offsets);

				fixed4 textures[9];
				for (int j = 0; j < 9; j++)
				{
					textures[j] = tex2D(_MainTex, i.uv + offsets[j]);
				}

				fixed4 col = textures[4];
				
				float orientation = (degrees(col.a));
				float mag = length(col.rgb);
				//col.rgb = fixed3(0, 1, 0);
				if (mag >= _UpperThreshold)
				{
					// diag, bl to tr
					if (orientation >= 22.5 && orientation <= 67.5)
					{
						if (length(textures[0].rgb) < mag && length(textures[8].rgb) < mag)
							col.rgb = fixed3(1, 0, 1);
					}
					// horiz
					if (orientation >= 67.5 && orientation <= 112.5)
					{
						if (length(textures[1].rgb) < mag && length(textures[7].rgb) < mag)
							col.rgb = fixed3(1, 0, 0);

					}
					// diag, br to tl
					if (orientation >= 112.5 && orientation <= 157.5)
					{
						if (length(textures[2].rgb) < mag && length(textures[6].rgb) < mag)
							col.rgb = fixed3(0, 0, 1);
					}
					// vert
					if ((orientation >= 157.5 && orientation <= 180.0) || (orientation >= 0.0 && orientation <= 22.5))
					{
						if (length(textures[0].rgb) < mag && length(textures[8].rgb) < mag)
							col.rgb = fixed3(0, 1, 0);
					}
				}
				else 
				{
					col.rgb = fixed3(0,0,0);
				}

				return col;
			}
			ENDCG
		}
	}
}
