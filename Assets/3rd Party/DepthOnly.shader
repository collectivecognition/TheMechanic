Shader "Custom/DepthOnly"
{
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
	SubShader{
		Pass {
			Tags {
				"RenderType"="Opaque"
				"Queue"="Background"
			}
			Zwrite on
			Offset 0.5, 0.5
			SetTexture[_MainTex]{
				constantColor[_Color]
				Combine texture * constant, texture * constant
			}
		}
	}
}