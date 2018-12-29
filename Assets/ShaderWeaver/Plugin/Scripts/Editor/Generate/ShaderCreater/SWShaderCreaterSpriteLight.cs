//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;

	public class SWShaderCreaterSpriteLight :SWShaderCreaterSprite {
		public SWShaderCreaterSpriteLight(SWWindowMain _window):base(_window)
		{
			
		}

		protected override void Struct_Appdata ()
		{
		}

		public override void PassWarp ()
		{
			Pass ();
		}

		protected override void Struct_v2fWarp ()
		{
			StringAddLine("\t\t\tstruct Input {  ");
			Struct_v2f ();
			StringAddLine("\t\t\t};  ");
		}
			
		protected override void Pragma ()
		{
			StringAddLine ("\t\t\t#pragma surface surf Lambert vertex:vert  nofog nolightmap nodynlightmap keepalpha");
			StringAddLine ("\t\t\t#pragma multi_compile _ PIXELSNAP_ON");
			StringAddLine ("\t\t\t#pragma multi_compile _ ETC1_EXTERNAL_ALPHA");
			PragmaModel ();
		}


		protected override void VertWrap ()
		{
			StringAddLine("\t\t\tvoid vert (inout appdata_full IN, out Input OUT)");
			StringAddLine("\t\t\t{");
			StringAddLine("\t\t\t\tUNITY_INITIALIZE_OUTPUT(Input,OUT);");
			Vert ();
			StringAddLine("\t\t\t}");
		}


		protected override void Vert ()
		{
			Rect_Sprite ();
			StringAddLine ("\t\t\t\tOUT.color = IN.color * _Color;");
			StringAddLine ("\t\t\t\tOUT._uv_MainTex = TRANSFORM_TEX(IN.texcoord,_MainTex);");
			Screen_Vert ();
			Vert_UV_STD ();
		}
	
		protected override void FragWarp ()
		{
			StringAddLine("\t\t\tvoid surf (Input i, inout SurfaceOutput o)");
			StringAddLine("\t\t\t{");
			Frag ();
			//StringAddLine("\t\t\t\to.Albedo = result.rgb;");
			StringAddLine("\t\t\t\to.Albedo = result.rgb*result.a;");
			StringAddLine("\t\t\t\to.Alpha = result.a;");
			StringAddLine("\t\t\t}");
		}
	}
}