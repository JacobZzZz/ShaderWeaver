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
	public class SWShaderCreaterNGUIClipBase:SWShaderCreaterNGUIBase{
		public SWShaderCreaterNGUIClipBase(SWWindowMain _window):base(_window)
		{
		}

		protected override string TitleProcess (string name)
		{
			return string.Format ("Hidden/{0}{1}", name, postFix_shaderName);
		}
	}
}