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
	public class SWShaderCreaterNGUIClipSoftBase:SWShaderCreaterNGUIClipBase{
		protected int clipCount;
		public SWShaderCreaterNGUIClipSoftBase(SWWindowMain _window):base(_window)
		{
		}

		protected void InitName()
		{
			postFix_fileName = " NGUI Clip"+clipCount;
			postFix_shaderName = " "+clipCount;
		}
		protected override void PropertyDeclare ()
		{
			base.PropertyDeclare ();
			for (int i = 0; i < clipCount; i++) {
				StringAddLine (string.Format("\t\t\tfloat4 _ClipRange{0} = float4(0.0, 0.0, 1.0, 1.0);",i));
				StringAddLine (string.Format("\t\t\tfloat4 _ClipArgs{0} = float4(1000.0, 1000.0, 0.0, 1.0);",i));
			}
		}

		protected override void Functions ()
		{
			base.Functions ();
			StringAddLine ("\t\t\tfloat2 NGUI_Rotate (float2 v, float2 rot)\n\t\t\t{\n\t\t\t\tfloat2 ret;\n\t\t\t\tret.x = v.x * rot.y - v.y * rot.x;\n\t\t\t\tret.y = v.x * rot.x + v.y * rot.y;\n\t\t\t\treturn ret;\n\t\t\t}");
		}
	}
}