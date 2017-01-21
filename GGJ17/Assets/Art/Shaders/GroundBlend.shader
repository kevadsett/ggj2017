Shader "Unlit/GroundBlend"
{
	Properties
	{
		_MaskTex ("Mask", 2D) = "white" {}
		_MainTex ("Texture", 2D) = "white" {}
		_WorldSize ("World Size", Vector) = (16,0,8,0)
	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off

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
				float2 wp : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _MaskTex;
			float4 _MaskTex_ST;

			float4 _WorldSize;
			
			v2f vert (appdata v)
			{
				v2f o;

				float4 ws = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1.0));

				o.vertex = mul(UNITY_MATRIX_VP, ws);
				o.uv = TRANSFORM_TEX(v.uv, _MaskTex);
				o.wp = TRANSFORM_TEX(ws.xz, _MainTex);
				o.wp /= _WorldSize.xz;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 mask = tex2D(_MaskTex, i.uv);
				fixed4 main = tex2D(_MainTex, i.wp);
				return fixed4(main.rgb, mask.r);
			}
			ENDCG
		}
	}
}
