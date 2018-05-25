Shader "Unlit/NewUnlitShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Diffuse("Diffuse", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
		LOD 100

		Pass
		{ 
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
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

			half4 _Diffuse;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			//float _Time;

			v2f vert (appdata v)
			{
				float time = _Time + 100.0;

				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex) + round(sin(v.vertex.z * time * time));
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv.x += _Time*_Time;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				float time = _Time + 100.0;
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				//if(col.a < .1)
					//discard;
				
				return col * _Diffuse * sin(time + i.vertex.x);
			}
			ENDCG
		}
	}
}
