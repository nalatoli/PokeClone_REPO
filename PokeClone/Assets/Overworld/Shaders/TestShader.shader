Shader "Shaders/SpriteOutsline"{

	Properties{
		_Maintex("Texture,2D") = "white" {}
		Color("Color", Color) = {1,1,1,1}
	}

		SubShader{

			Cull Off

			Pass{

			CGPROGRAM

#pragma vertext vertexFunc
#pragma fragment fragmentFunc

#include "UnityCG.cginc"

			sampler2D _MainText;

		struct v2f
		{
			float4 pos : SV_POSITION;
			half2 uv : TEXCOORD0;
		};

		v2f vertexFunc(appdata_base v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord;
			return o;
		}

		fixed4 _Color;
		float4 _MainText_TexelSize;

		fixed4 fragmentFunc(v2f i) : COLOR{
			half4 c = text2D(_mAINtEXT, i.uv);

			return c;
		}

			ENDCD


            }

		}
}