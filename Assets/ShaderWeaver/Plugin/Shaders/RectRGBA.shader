// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//----------------------------------------------
//            Shader Weaver
// Copyright © 2017 Jackie Luo
//----------------------------------------------
Shader "Hidden/Shader Weaver/RectRGBA" {
	Properties {
		_MainTex ("Texture", 2D) = "white" { }
		_Color ("Color", Color) = (1,1,1,0.8)
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
			int cmode;// 0=all 1=r 2=g 3=b 4=a

			int useLoop;
			int loopX;// 0=loop 1= single
			int loopY;// 0=loop 1= single
			float gapX;
			float gapY;

			float4 rectAnimationSheet;

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



			float2 UV_STD2Rect(float2 uv,float4 rect)
			{
				uv.x = lerp(rect.x,rect.x+rect.z, uv.x);
				uv.y = lerp(rect.y,rect.y+rect.w, uv.y);
				return uv;
			}

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
			    float2 uv_orgin = uv;

				if(useLoop==1)
				{
					uv = float2(
					uv.x>0 ? fmod(uv.x,1+gapX) 	: 	(1+gapX)-fmod(abs(uv.x),1+gapX), 
					uv.y>0 ? fmod(uv.y,1+gapY)	:	(1+gapY)-fmod(abs(uv.y),1+gapY));
				}

				//Process Animation Sheet
				float2 uvNodeSpace =  uv;
				uvNodeSpace = UV_STD2Rect(uvNodeSpace,rectAnimationSheet);

				fixed4 texCol = tex2D(_MainTex, uvNodeSpace);
			  	if(useLoop==1)
				{
					texCol= ( uv.x<=1 &&  uv.y<=1)?texCol:float4(0,0,0,0);
					if(loopX == 1 &&( uv_orgin.x<0||uv_orgin.x>1))
						texCol = float4(0,0,0,0);
					if(loopY == 1 &&( uv_orgin.y<0||uv_orgin.y>1))
						texCol = float4(0,0,0,0);
				}
				if(cmode == 1)
                  texCol = float4(texCol.r,0,0,texCol.r);
                if(cmode == 2)
                  texCol =  float4(0,texCol.g,0,texCol.g);
                if(cmode == 3)
                  texCol =  float4(0,0,texCol.b,texCol.b);
                if(cmode == 4)
                  texCol =  float4(texCol.a,texCol.a,texCol.a,texCol.a);

                  //UV pos to set alpha
				if(uv_orgin.x < 0 || uv_orgin.x >1 ||uv_orgin.y < 0 || uv_orgin.y >1)
                {
                	float x;
                	if(uv_orgin.x<0)
                		x = -uv_orgin.x;
					else if(uv_orgin.x>1)
					    x = uv_orgin.x-1;

					float y;
                	if(uv_orgin.y<0)
                		y = -uv_orgin.y;
					else if(uv_orgin.y>1)
					    y = uv_orgin.y-1;
                    float a = max(x,y);
					texCol.a *= lerp(clamp(1 - a,0,1),0.3,0.8);
                }
				return texCol*_Color;
			}
			ENDCG
		}
	}
}