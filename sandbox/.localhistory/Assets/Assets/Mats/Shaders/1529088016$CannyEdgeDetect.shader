Shader "Casey-Screen/CannyEdgeDetect-GBlur"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Res("Resolution", Range(512.0,4096.0)) = 2048.0
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				float2 offsets[25];
				GetOffsets5x5(_Res, offsets);

				fixed3 textures[25];
				for (int j = 0; j < 25; j++)
				{
					textures[j] = tex2D(_MainTex, i.uv + offsets[j]).rgb;
				}



				fixed4 col = fixed4(GaussianBlur5x5(textures),1.0);
				return col;
			}
			ENDCG
		}
	}
}
