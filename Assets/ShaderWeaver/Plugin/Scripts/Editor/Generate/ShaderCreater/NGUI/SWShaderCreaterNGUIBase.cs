//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;

	public class SWShaderCreaterNGUIBase :SWShaderCreaterBase {
		public SWShaderCreaterNGUIBase(SWWindowMain _window):base(_window)
		{
			IsSprite = true;
		}
			
		protected override void Includes ()
		{
			base.Includes ();
			StringAddLine ("\t\t\t#include \"UnityUI.cginc\"");
		}

		protected override void Struct_v2f ()
		{
			base.Struct_v2f ();
			StringAddV2d ("float4", "rect_Sprite", "COLOR1");
		}

		protected override void Tags ()
		{
			base.Tags ();
			StringAddLine ("\t\t\t\"IgnoreProjector\"=\"True\" ");
			StringAddLine ("\t\t\t\"PreviewType\"=\"Plane\"");
			StringAddLine ("\t\t\t\"CanUseSpriteAtlas\"=\"True\"");

			//NGUI
			//StringAddLine ("\t\t\t\"DisableBatching\" = \"True\"");
		}
		protected override void Ops ()
		{
			StringAddLine ("\t\t\tCull Off");
			StringAddLine ("\t\t\tLighting Off");
			StringAddLine ("\t\t\tZWrite Off");

			//NGUI
			StringAddLine ("\t\t\tFog { Mode Off }");
			StringAddLine ("\t\t\tOffset -1, -1");
		}

		protected void Rect_Sprite()
		{
			StringAddLine(string.Format( "\t\t\t\tOUT.rect_Sprite = float4({0},{1},{2},{3});"
				,window.data.spriteRect.x
				,window.data.spriteRect.y
				,window.data.spriteRect.width
				,window.data.spriteRect.height
			));
		}

		protected override void Vert ()
		{
			Rect_Sprite ();
			//StringAddLine ("\t\t\t\tOUT.worldPosition = IN.vertex;");

			StringAddLine( "\t\t\t\tOUT.pos = mul(UNITY_MATRIX_MVP,IN.vertex);   ");
			//StringAddLine ("\t\t\t\t#ifdef UNITY_HALF_TEXEL_OFFSET\n\t\t\t\tOUT.pos.xy += (_ScreenParams.zw-1.0)*float2(-1,1);\n\t\t\t\t#endif");
			StringAddLine ("\t\t\t\tOUT.color = IN.color * _Color;");
			StringAddLine ("\t\t\t\tOUT._uv_MainTex = TRANSFORM_TEX(IN.texcoord,_MainTex);");
			Screen_Vert ();
			Vert_UV_STD ();
		}
		protected override void Vert_UV_STD ()
		{
			StringAddLine ("\t\t\t\tOUT._uv_STD = float2((IN.texcoord.x - OUT.rect_Sprite.x)/OUT.rect_Sprite.z,(IN.texcoord.y - OUT.rect_Sprite.y)/OUT.rect_Sprite.w);");
			StringAddLine ("\t\t\t\tOUT._uv_STD = TRANSFORM_TEX(OUT._uv_STD,_MainTex);  ");
		}
	}
}