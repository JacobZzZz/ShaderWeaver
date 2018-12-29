//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;

	public class SWShaderCreaterUIFont :SWShaderCreaterBase {
		public SWShaderCreaterUIFont(SWWindowMain _window):base(_window)
		{
			IsTextureSampleAdd = true;
		}
		protected override void Functions ()
		{
			base.Functions ();
		}



		protected override void PropertyField ()
		{
			base.PropertyField ();
			StringAddLine ("\t\t_StencilComp (\"Stencil Comparison\", Float) = 8\n\t\t_Stencil (\"Stencil ID\", Float) = 0\n\t\t_StencilOp (\"Stencil Operation\", Float) = 0\n\t\t_StencilWriteMask (\"Stencil Write Mask\", Float) = 255\n\t\t_StencilReadMask (\"Stencil Read Mask\", Float) = 255\n\n\t\t_ColorMask (\"Color Mask\", Float) = 15\n\t\t\n\t\t[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip (\"Use Alpha Clip\", Float) = 0");
		} 
		protected override void PropertyDeclare ()
		{
			base.PropertyDeclare ();
			StringAddLine ("\t\t\tfixed4 _TextureSampleAdd;");
			StringAddLine ("\t\t\tbool _UseClipRect;");
			StringAddLine ("\t\t\tfloat4 _ClipRect;");
			StringAddLine ("\t\t\tbool _UseAlphaClip;");
		}
			


		protected override void Includes ()
		{
			base.Includes ();
			StringAddLine ("\t\t\t#include \"UnityUI.cginc\"");
		}
		protected override void Struct_Appdata ()
		{
			base.Struct_Appdata ();
		}
		protected override void Struct_v2f ()
		{
			base.Struct_v2f ();
			StringAddV2d ("float4", "worldPosition", "COLOR2");
		}
		protected override void Pragma ()
		{
			base.Pragma ();
			StringAddLine ("\t\t\t#pragma multi_compile __ UNITY_UI_ALPHACLIP");
		}
		protected override void Tags ()
		{
			base.Tags ();
			StringAddLine ("\t\t\t\"IgnoreProjector\"=\"True\" ");
			StringAddLine ("\t\t\t\"PreviewType\"=\"Plane\"");
		}
		protected override void Ops ()
		{
			StringAddLine ("\t\t\tCull Off");
			StringAddLine ("\t\t\tLighting Off");
			StringAddLine ("\t\t\tZWrite Off");

			StringAddLine ("\t\t\tStencil {\n\t\t\t\tRef [_Stencil]\n\t\t\t\tComp [_StencilComp]\n\t\t\t\tPass [_StencilOp] \n\t\t\t\tReadMask [_StencilReadMask]\n\t\t\t\tWriteMask [_StencilWriteMask]\n\t\t\t}\n\t\t\tZTest [unity_GUIZTestMode]\n\t\t\tColorMask [_ColorMask]");
		}


		protected override void Vert ()
		{
			StringAddLine ("\t\t\t\tOUT.worldPosition = IN.vertex;");

			StringAddLine( "\t\t\t\tOUT.pos = mul(UNITY_MATRIX_MVP,IN.vertex);   ");
			StringAddLine ("\t\t\t\t#ifdef UNITY_HALF_TEXEL_OFFSET\n\t\t\t\tOUT.pos.xy += (_ScreenParams.zw-1.0)*float2(-1,1);\n\t\t\t\t#endif");
			StringAddLine ("\t\t\t\tOUT.color = IN.color * _Color;");
			StringAddLine ("\t\t\t\tOUT._uv_MainTex = TRANSFORM_TEX(IN.texcoord,_MainTex);");
		

		
			Screen_Vert ();
			Vert_UV_STD ();
		}

		protected override void Vert_UV_STD ()
		{
			StringAddLine ("\t\t\t\tOUT._uv_STD= OUT.pos.xy*0.5f + 0.5f;");
		}


		protected override void Frag ()
		{
			base.Frag ();
		}

		public override void ProcessExtra (SWNodeBase root)
		{
			base.ProcessExtra (root);
			StringAddLine( "\t\t\t\tresult.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);");
			StringAddLine( "\t\t\t\t#ifdef UNITY_UI_ALPHACLIP\n\t\t\t\tclip (result.a - 0.001);\n\t\t\t\t#endif");
		}
	}
}