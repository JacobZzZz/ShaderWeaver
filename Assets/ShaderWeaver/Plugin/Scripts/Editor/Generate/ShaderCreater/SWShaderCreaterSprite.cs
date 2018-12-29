//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;

	public class SWShaderCreaterSprite :SWShaderCreaterBase {
		public SWShaderCreaterSprite(SWWindowMain _window):base(_window)
		{
			IsSprite = true;
		}
		protected override void Functions ()
		{
			base.Functions ();
		}



		protected override void PropertyField ()
		{
			base.PropertyField ();
			StringAddLine ("\t\t[MaterialToggle] PixelSnap (\"Pixel snap\", Float) = 0");
		} 
		protected override void PropertyDeclare ()
		{
			base.PropertyDeclare ();
			StringAddLine ("\t\t\tsampler2D _AlphaTex;");
			StringAddLine ("\t\t\tfloat _AlphaSplitEnabled;");
			PropertyDeclare_AnimatedSprite ();
		}

		protected void PropertyDeclare_AnimatedSprite ()
		{
			StringAddLine ("\t\t\tint _useSpriteAnimation;");
			StringAddLine ("\t\t\tfloat4 _AnimatedRect;");
		}



		protected override void Includes ()
		{
			base.Includes ();
		}
		protected override void Struct_Appdata ()
		{
			base.Struct_Appdata ();
		}
		protected override void Struct_v2f ()
		{
			base.Struct_v2f ();
			StringAddV2d ("float4", "rect_Sprite", "COLOR1");
		}
		protected override void Pragma ()
		{
			base.Pragma ();
			StringAddLine ("\t\t\t#pragma multi_compile _ PIXELSNAP_ON");
		}
		protected override void Tags ()
		{
			base.Tags ();
			StringAddLine ("\t\t\t\"IgnoreProjector\"=\"True\" ");
			StringAddLine ("\t\t\t\"PreviewType\"=\"Plane\"");
			StringAddLine ("\t\t\t\"CanUseSpriteAtlas\"=\"True\"");
		}
		protected override void Ops ()
		{
			StringAddLine ("\t\t\tCull Off");
			StringAddLine ("\t\t\tLighting Off");
			StringAddLine ("\t\t\tZWrite Off");
		}

		protected void Rect_Sprite()
		{
			StringAddLine ("\t\t\t\tif(_useSpriteAnimation==1)");
			StringAddLine ("\t\t\t\t\tOUT.rect_Sprite = _AnimatedRect;");
			StringAddLine ("\t\t\t\telse");
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
			StringAddLine( "\t\t\t\tOUT.pos = mul(UNITY_MATRIX_MVP,IN.vertex);   ");
			StringAddLine ("\t\t\t\tOUT.color = IN.color * _Color;");
			StringAddLine ("\t\t\t\tOUT._uv_MainTex = TRANSFORM_TEX(IN.texcoord,_MainTex);");
			Screen_Vert ();
			Vert_UV_STD ();

			StringAddLine ("\t\t\t\t#ifdef PIXELSNAP_ON");
			StringAddLine ("\t\t\t\tOUT.pos = UnityPixelSnap (OUT.pos);");
			StringAddLine ("\t\t\t\t#endif");
		}
		protected override void Vert_UV_STD ()
		{
			StringAddLine ("\t\t\t\tOUT._uv_STD = float2((IN.texcoord.x - OUT.rect_Sprite.x)/OUT.rect_Sprite.z,(IN.texcoord.y - OUT.rect_Sprite.y)/OUT.rect_Sprite.w);");
			StringAddLine ("\t\t\t\tOUT._uv_STD = TRANSFORM_TEX(OUT._uv_STD,_MainTex);  ");
		}


		protected override void Frag ()
		{
			base.Frag ();
		}

		public override void ProcessExtra (SWNodeBase root)
		{
			base.ProcessExtra (root);
			StringAddLine ("\t\t\t\t#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED\n\t\t\t\tif (_AlphaSplitEnabled)\n\t\t\t\tresult.a = tex2D (_AlphaTex, uv).r;\n\t\t\t\t#endif ");
		}
	}
}