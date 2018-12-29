//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using System;


	public class SWShaderProcessReflection:SWShaderProcessImage{
		public  SWShaderProcessReflection():base()
		{
			type = SWNodeType.reflect;
			receiveOutputTypes.Add (SWNodeType.alpha);
			receiveOutputTypes.Add (SWNodeType.refract);
			receiveOutputTypes.Add (SWNodeType.coord);
			receiveOutputTypes.Add (SWNodeType.color);
			receiveOutputTypes.Add (SWNodeType.mask);
			receiveOutputTypes.Add (SWNodeType.dummy);
			receiveOutputTypes.Add (SWNodeType.refract);
			receiveOutputTypes.Add (SWNodeType.reflect);
			receiveOutputTypes.Add (SWNodeType.remap);
			receiveOutputTypes.Add (SWNodeType.retro);
			receiveOutputTypes.Add (SWNodeType.root);
			receiveOutputTypes.Add (SWNodeType.uv);
		}

		protected override void UVParamInit ()
		{
			uvParam = string.Format ("uv{0}", node.data.iName);
			StringAddLine( string.Format("\t\t\t\tfloat2  {0} = i._uv_Screen;",uvParam));
		}

		public override void FinalUVStage ()
		{
			base.FinalUVStage ();
			StringAddLine ("\t\t\t\t#if UNITY_UV_STARTS_AT_TOP");
			StringAddLine ("\t\t\t\t\tif (_ProjectionParams.x > 0)");
			StringAddLine ("\t\t\t\t\t\t_ReflectionLine = 1-_ReflectionLine;");
			StringAddLine ("\t\t\t\t#endif");

			StringAddLine (string.Format("\t\t\t\t{0} = FlipUV_Y({0},_ReflectionLine);",uvParam));
		}
	}
}