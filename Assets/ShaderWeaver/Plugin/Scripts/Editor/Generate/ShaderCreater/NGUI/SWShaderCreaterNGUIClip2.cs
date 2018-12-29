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
	public class SWShaderCreaterNGUIClip2:SWShaderCreaterNGUIClipSoftBase{
		public SWShaderCreaterNGUIClip2(SWWindowMain _window):base(_window)
		{
			clipCount = 2;
			InitName ();
		}
		protected override void Struct_v2f ()
		{
			base.Struct_v2f ();
			StringAddV2d ("float4", "worldPos", "COLOR3");
		}
		protected override void Vert ()
		{
			base.Vert ();
			StringAddLine ("\t\t\t\tOUT.worldPos.xy = IN.vertex.xy * _ClipRange0.zw + _ClipRange0.xy;");
			StringAddLine ("\t\t\t\tOUT.worldPos.zw = NGUI_Rotate(IN.vertex.xy, _ClipArgs1.zw) * _ClipRange1.zw + _ClipRange1.xy;");
		}
		public override void ProcessExtra (SWNodeBase root)
		{
			base.ProcessExtra (root);
			//step 1:Cal Factor
			StringAddLine ("\t\t\t\t// First clip region");
			StringAddLine ("\t\t\t\tfloat2 factor = (float2(1.0, 1.0) - abs(i.worldPos.xy)) * _ClipArgs0.xy;");
			StringAddLine ("\t\t\t\tfloat f = min(factor.x, factor.y);");
			StringAddLine ("\t\t\t\t// Second clip region");
			StringAddLine ("\t\t\t\tfactor = (float2(1.0, 1.0) - abs(i.worldPos.zw)) * _ClipArgs1.xy;");
			StringAddLine ("\t\t\t\tf = min(f, min(factor.x, factor.y));");
			//step 2:Mul aplha
			StringAddLine ("\t\t\t\t// Apply clipping");
			StringAddLine ("\t\t\t\tresult.a *= clamp(f, 0.0, 1.0);");
		}
	}
}