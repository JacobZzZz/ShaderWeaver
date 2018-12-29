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
	public class SWShaderCreaterNGUIClip1:SWShaderCreaterNGUIClipSoftBase{
		public SWShaderCreaterNGUIClip1(SWWindowMain _window):base(_window)
		{
			clipCount = 1;
			InitName ();
		}
		protected override void Struct_v2f ()
		{
			base.Struct_v2f ();
			StringAddV2d ("float2", "worldPos", "COLOR3");
		}
		protected override void Vert ()
		{
			base.Vert ();
			StringAddLine ("\t\t\t\tOUT.worldPos = IN.vertex.xy * _ClipRange0.zw + _ClipRange0.xy;");
		}
		public override void ProcessExtra (SWNodeBase root)
		{
			base.ProcessExtra (root);
			//step 1:Cal Factor
			StringAddLine ("\t\t\t\t// Softness factor");
			StringAddLine ("\t\t\t\tfloat2 factor = (float2(1.0, 1.0) - abs(i.worldPos)) * _ClipArgs0;");
			//step 2:Mul aplha
			StringAddLine ("\t\t\t\t// Apply clipping");
			StringAddLine ("\t\t\t\tresult.a *= clamp( min(factor.x, factor.y), 0.0, 1.0);");
		}
	}
}