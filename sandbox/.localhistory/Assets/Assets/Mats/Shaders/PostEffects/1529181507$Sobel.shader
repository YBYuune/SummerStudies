Shader "Casey-Screen/CannyEdgeDetect-Sobel"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Res("Resolution", Float) = 1024
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
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			////////////////////////////////////////////////////

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float2 offsets[9];
				GetOffsets3x3(_Res, offsets);

				fixed3 textures[9];
				for (int j = 0; j < 9; j++)
				{
					textures[j] = tex2D(_MainTex, i.uv + offsets[j]).rgb;
				}

				fixed4 col = ApplySobel(textures);
				col.r *= col.a
				return col;
			}
			ENDCG
		}
	}
}
