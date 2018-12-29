// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//----------------------------------------------
//            Shader Weaver
// Copyright © 2017 Jackie Luo
//----------------------------------------------
Shader "Hidden/Shader Weaver/Rect" {
	Properties {
		_MainTex ("Texture", 2D) = "white" { }
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
	  	Tags {
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
		}
		Lighting Off 
		Cull Off 
		ZTest Always 
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			sampler2D _MainTex;
			float4 _MainTex_ST;

			float4 rectShow;
			float4 _Color;

			struct appdata_t {
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD;
			};
			struct v2f {
			    float4  pos : SV_POSITION;
			    float2  uv : TEXCOORD0;
			    float2  mpos:TEXCOORD1;
			} ;
			v2f vert (appdata_t v)
			{
			    v2f o;
			   	o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				o.mpos = o.pos;
			    return o;
			}
			float4 frag (v2f i) : COLOR
			{
			 	if(i.mpos.x<rectShow.x 
			    ||i.mpos.x>rectShow.z 
			     ||i.mpos.y<rectShow.y
			      ||i.mpos.y>rectShow.w 
			    )
			    	return float4(0,0,0,0);

			    
			    float2 uv = i.uv;
			 

				fixed4 texCol = tex2D(_MainTex, uv);

				 if(uv.x < 0 || uv.x >1 ||uv.y < 0 || uv.y >1)
                {
                	float x;
                	if(uv.x<0)
                		x = -uv.x;
					else if(uv.x>1)
					    x = uv.x-1;

					float y;
                	if(uv.y<0)
                		y = -uv.y;
					else if(uv.y>1)
					    y = uv.y-1;
                    float a = max(x,y);
                    a = 0.4*(1 - a);
					texCol.a *= a;
                }
				return texCol*_Color;
			}
			ENDCG
		}
	}
}