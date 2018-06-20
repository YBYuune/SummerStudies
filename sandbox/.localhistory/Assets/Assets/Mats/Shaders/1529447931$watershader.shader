/*
Shader "Unlit/WaterShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_ColorBlend("Color Blend", Color) = (1,1,1,1)
		_RefractAmount("Refract Amount", Range(0,3)) = 0.0
		_WaveHeight("Wave Height", Range(0,10)) = 0.0
		_WaveSpeed("Wave Speed", Range(1,100)) = 1

		_FColor("Fresnel Color", Color) = (1,1,1,1)
		_FScale("Fresnel Scale", Float) = 1.0
		_FPower("Fresnel Power", Float) = 1.0
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" }
		LOD 100

		GrabPass
		{
			"_BackgroundTexture"
		}

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
				float4 normal : Normal;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;

				float4 grabPos : TEXCOORD1;
				float3 viewDir : TEXCOORD2;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float R : TANGENT;
			};

			sampler2D _BackgroundTexture;
			sampler2D _MainTex;
			sampler2D _NormalMap;
			float4 _MainTex_ST;
			float4 _ColorBlend;
			float _RefractAmount;
			float _WaveHeight;
			float _WaveSpeed;

			fixed4 _FColor;
			float _FScale;
			float _FPower;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				o.vertex.y += sin((_Time * _WaveSpeed) + v.vertex.x + v.vertex.z) * _WaveHeight;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				o.grabPos = ComputeGrabScreenPos(o.vertex);

				float3 viewDir = WorldSpaceViewDir(o.vertex);
				o.viewDir = viewDir;

				float3 posWorld = mul(unity_ObjectToWorld, v.vertex).xyz;
				//posWorld.y += sin((_Time * _WaveSpeed) + v.vertex.x + v.vertex.z) * _WaveHeight;
				float3 normWorld = normalize(mul(unity_ObjectToWorld, v.normal));
				//normWorld = refract(normalize(viewDir), normWorld, .0001);

				float3 I = normalize(posWorld - _WorldSpaceCameraPos.xyz);
				o.R = _FScale * pow(1.0 + dot(I, normWorld), _FPower);

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture

				float3 norm = tex2D(_NormalMap, i.uv);
				float4 R = float4(refract(normalize(i.viewDir), norm, 1.0 / _RefractAmount), 1.0);

				fixed4 col = tex2Dproj(_BackgroundTexture, UNITY_PROJ_COORD(i.grabPos + R));
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return lerp((col + tex2D(_MainTex,R)) * _ColorBlend, _FColor, i.R);
			}
			ENDCG
		}
	}
}
*/

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Casey-Unlit/WaterShader"
{
	Properties
	{
		_Normal("Normal", 2D) = "bump" {}
		_NormalScale("Normal Scale", Range(0,1)) = 0.1
		_Color("Color Blend", Color) = (1,1,1,1)
		_FColor("Fresnel Color", Color) = (1,1,1,1)
		_FScale("Fresnel Scale", Float) = 1.0
		_FPower("Fresnel Power", Float) = 1.0

		_WaveHeight("Wave Height", Range(0,10)) = 0.0
		_WaveSpeed("Wave Speed", Range(1,100)) = 1
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" }
		LOD 100
			GrabPass
		{
			"_BackgroundTexture"
		}
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
				float2 uv : TEXCOORD0;
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 grabPos : TEXCOORD1;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float R : TANGENT;
			};

			sampler2D _BackgroundTexture;
			sampler2D _Normal;
			float _NormalScale;

			fixed4 _FColor;
			fixed4 _Color;
			float _FScale;
			float _FPower;

			float _WaveHeight;
			float _WaveSpeed;
			v2f vert(appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.vertex.y += sin((_Time * _WaveSpeed) + v.vertex.x + v.vertex.z) * _WaveHeight;
				o.grabPos = ComputeGrabScreenPos(o.vertex);
				o.uv = v.uv;
				UNITY_TRANSFER_FOG(o,o.vertex);

				float3 posWorld = mul(unity_ObjectToWorld, v.vertex).xyz;
				float3 normWorld = normalize(mul(unity_ObjectToWorld, v.normal));
				float3 viewDir = WorldSpaceViewDir(o.vertex);
				//normWorld = refract(normalize(viewDir), normWorld, .0001);

				float3 I = normalize(posWorld - _WorldSpaceCameraPos.xyz);
				o.R = _FScale * pow(1.0 + dot(I, normWorld), _FPower);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				half3 normal = UnpackNormal(tex2D(_Normal,i.uv)) + sin((_Time ) + i.vertex.x + v.vertex.z);
				
			fixed4 col = tex2Dproj(_BackgroundTexture, UNITY_PROJ_COORD(i.grabPos + fixed4(normal * _NormalScale,0.0)));
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return lerp(col * _Color,_FColor, i.R);
			}
			ENDCG
		}
	}
}
