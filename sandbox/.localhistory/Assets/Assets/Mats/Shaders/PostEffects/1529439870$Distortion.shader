Shader "Hidden/Distortion"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_XSpeed ("X Speed")
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _Normal;

			fixed4 frag (v2f i) : SV_Target
			{

				fixed3 normal = UnityUnpackNormals(tex2D(_Normal, i.uv))

				fixed4 col = tex2D(_MainTex, i.uv + normal.xy);
				return col;
			}
			ENDCG
		}
	}
}
