// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/texturedStencilFilter"
{
	Properties {
		_Color("Color", Color) = (1,1,1,1)

		// CompareFunction
		[Enum(Equal, 3, NotEqual, 6)] _StencilTest("Stencil Test", int) = 6
		[NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
	}
	
	SubShader {

		Color[_Color]
		Stencil{
		Ref 1
		Comp[_StencilTest]
	}

	Pass {
		CGPROGRAM
		// use "vert" function as the vertex shader
		#pragma vertex vert
		// use "frag" function as the pixel (fragment) shader
		#pragma fragment frag

		// vertex shader inputs
		struct appdata
		{
			float4 vertex : POSITION; // vertex position
			float2 uv : TEXCOORD0; // texture coordinate
		};

		// vertex shader outputs ("vertex to fragment")
		struct v2f
		{
			float2 uv : TEXCOORD0; // texture coordinate
			float4 vertex : SV_POSITION; // clip space position
		};

		// vertex shader
		v2f vert(appdata v)
		{
			v2f o;
			// transform position to clip space
			// (multiply with model*view*projection matrix)
			o.vertex = UnityObjectToClipPos(v.vertex);
			// just pass the texture coordinate
			o.uv = v.uv;
			return o;
		}

		// texture we will sample
		sampler2D _MainTex;

		// pixel shader; returns low precision ("fixed4" type)
		// color ("SV_Target" semantic)
		fixed4 frag(v2f i) : SV_Target
		{
			// sample texture and return it
			fixed4 col = tex2D(_MainTex, i.uv);
		return col;
		}
			ENDCG
		}
	}
}
