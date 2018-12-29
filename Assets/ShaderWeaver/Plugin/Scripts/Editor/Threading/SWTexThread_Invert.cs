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

	public class SWTexThread_Invert : SWTexThread_Tex {
		public SWTexThread_Invert(SWTexture2DEx _tex,SWBrush _brush):base(_tex,_brush)
		{
			texColorBuffer = tex.GetPixels ();
		}
		public void Process(object obj)
		{
			Process ();
		}
		protected override void ThreadMission_Pixel(int i,int j)
		{
			int index = SWTextureProcess.XYtoIndex (texWidth, texHeight, i, j);
			texColorBuffer [index] = new Color (
				0,
				1-texColorBuffer [index].g,
				0,
				1-texColorBuffer [index].a);
		}
	}
}