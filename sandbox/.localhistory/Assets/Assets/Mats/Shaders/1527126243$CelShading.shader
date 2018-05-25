Shader "Custom/CelShading" {
		Properties{
			_MainTex("Texture", 2D) = "white" {}
		}
		SubShader{
			Tags{ "RenderType" = "Opaque" }
			CGPROGRAM
			#pragma surface surf SimpleLambert

			half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half atten) {
				half NdotL = dot(s.Normal, lightDir);
				if (NdotL <= 0.0)NdotL = 0.0;
				else NdotL = 1.0;
				half4 c;
				c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten);
				c.a = s.Alpha;
				return c;
			}

			struct Input {
				float2 uv_MainTex;
			};

			sampler2D _MainTex;

			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			}
			ENDCG
		}
			Fallback "Diffuse"
	}