Shader "Custom/DepthOnly"
{
	SubShader{
		pass {
			Tags {
				"RenderType"="Opaque"
				"Queue"="Background"
			}
				Zwrite on
				ColorMask 0
				Offset 0.5, 0.5
		}
	}
}