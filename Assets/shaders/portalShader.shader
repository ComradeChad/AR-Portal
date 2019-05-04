Shader "Custom/portalShader"
{
	SubShader
	{

		ZWrite off // Don't write to Z buffer
		ColorMask 0 // Transparent shader
		Cull off

		Stencil {
			Ref 1
			Pass replace
		}

		Pass
		{
		}
	}
}
