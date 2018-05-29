Shader "Travis/StencilShader_T"
{
	//////////////////////////////////////////////////////////////
	//														   	//
	//			This shader was written by Travis Milne-Ruttan	//	   
	//														   	//
	//////////////////////////////////////////////////////////////
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_OutlineColour("Outline Colour", Color) = (1,1,1,1)
		_OutlineThickness("Outline Thickness", Range(0.0, 1.0)) = 0.05

	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" "Queue" = "Transparent" }
			LOD 100



			Pass
			{
				Stencil{
					Ref 5
					Comp always
					Pass replace
				}

			Blend SrcAlpha OneMinusSrcAlpha
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

			sampler2D _MainTex;
			float4 _MainTex_ST;

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
				fixed4 col = tex2D(_MainTex, i.uv);
			return col;

			}

				ENDCG
		}

			Pass
			{
				Name "Outline"
				Stencil{
				Ref 5
				Comp NotEqual
				Pass Keep
			}

				Blend SrcAlpha OneMinusSrcAlpha
				CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"


				float4 _OutlineColour;
				float _OutlineThickness;

				struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
					float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;

				float4 modVert = v.vertex;
				modVert.xyz += v.normal * _OutlineThickness;
				o.vertex = UnityObjectToClipPos(modVert);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
			return _OutlineColour;
			}
				ENDCG
			}
		}
}
