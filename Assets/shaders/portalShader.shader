Shader "Custom/portalShader"
{
	SubShader
	{

		ZWrite off // Don't write to Z buffer
		ColorMask 0 // Transparent shader

		Stencil {
			Ref 1
			Pass replace
		}

		Pass
		{
		}
	}
}
