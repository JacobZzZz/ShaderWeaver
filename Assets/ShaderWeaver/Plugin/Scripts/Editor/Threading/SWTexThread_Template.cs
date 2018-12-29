//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using ShaderWeaver;
	using System.Threading;

	public class SWTexThread_Template : SWTexThread_Tex {
		public SWTexThread_Template(SWTexture2DEx _tex,SWBrush _brush):base(_tex,_brush)
		{
			texColorBuffer = tex.GetPixels ();
		}
		protected override void ThreadMission_Pixel(int i,int j)
		{
		}
	}
}