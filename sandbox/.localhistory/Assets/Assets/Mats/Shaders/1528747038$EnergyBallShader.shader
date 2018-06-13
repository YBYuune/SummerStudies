Shader "Unlit/EnergyBallShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_FColor("Fresnel Color", Color) = (1,1,1,1)
		_FScale("Fresnel Scale", Float) = 1.0
		_FPower("Fresnel Scale", Float) = 1.0

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
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float R;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);

				float3 worldPos = mul(_Object2World, v.vertex).xyz;
				float3 normWorldPos = normalize(mul(float3x3(_Object2World), v.normal));

				float3 I = normalize(posWorld - _WorldSpaceCameraPos.xyz);
				o.R = _FScale * pow(1.0 + dot(I, normWorld), _FPower);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return lerp(col,_FColor, i.R);
			}
			ENDCG
		}
	}
}
