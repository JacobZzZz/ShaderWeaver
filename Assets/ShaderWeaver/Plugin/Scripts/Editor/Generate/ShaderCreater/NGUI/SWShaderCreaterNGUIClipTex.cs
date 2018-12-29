//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;

	/// <summary>
	/// Create Shader
	/// </summary>
	public class SWShaderCreaterNGUIClipTex:SWShaderCreaterNGUIClipBase{
		public SWShaderCreaterNGUIClipTex(SWWindowMain _window):base(_window)
		{
			postFix_fileName = " NGUI ClipTex";
			postFix_shaderName = " (TextureClip)";
		}

		protected override void PropertyDeclare ()
		{
			base.PropertyDeclare ();
			StringAddLine ("\t\t\tsampler2D _ClipTex;");
			StringAddLine ("\t\t\tfloat4 _ClipRange0 = float4(0.0, 0.0, 1.0, 1.0);");
		}
		protected override void Struct_v2f ()
		{
			base.Struct_v2f ();
			StringAddV2d ("float2", "clipUV", "COLOR3");
		}
		protected override void Vert ()
		{
			base.Vert ();
			StringAddLine ("\t\t\t\tOUT.clipUV = (IN.vertex.xy * _ClipRange0.zw + _ClipRange0.xy) * 0.5 + float2(0.5, 0.5);");
		}
		public override void ProcessExtra (SWNodeBase root)
		{
			base.ProcessExtra (root);
			StringAddLine ("\t\t\t\tresult.a *= tex2D(_ClipTex, i.clipUV).a;");
		}
	}
}