using System.Collections;//----------------------------------------------
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


	public class SWShaderProcessDummy:SWShaderProcessImage{
		protected override void UVParamInit ()
		{
			uvParam = string.Format ("uv{0}", node.data.iName);
			if(SWShaderCreaterBase.Instance is SWShaderCreaterUIFont)
				StringAddLine( string.Format("\t\t\t\tfloat2  {0} = i._uv_MainTex;",uvParam));
			else
				StringAddLine( string.Format("\t\t\t\tfloat2  {0} = i._uv_STD;",uvParam));
		}

		public  SWShaderProcessDummy():base()
		{
			type = SWNodeType.dummy;
		}
	}
}