// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/EnergyBallShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "black" {}
		_Color("Color Blend", Color) = (1,1,1,1)
		_FColor("Fresnel Color", Color) = (1,1,1,1)
		_FScale("Fresnel Scale", Float) = 1.0
		_FPower("Fresnel Power", Float) = 1.0

	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members R)

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
				float R : TANGENT;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			fixed4 _FColor;
			fixed4 _Color;
			float _FScale;
			float _FPower;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);

				float3 posWorld = mul(unity_ObjectToWorld, v.vertex).xyz;
				float3 normWorld = normalize(mul(unity_ObjectToWorld, v.normal));

				float3 I = normalize(posWorld - _WorldSpaceCameraPos.xyz);
				o.R = _FScale * pow(1.0 + dot(I, normWorld), _FPower);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return lerp(col * _Color,_FColor, i.R);
			}
			ENDCG
		}
	}
}
