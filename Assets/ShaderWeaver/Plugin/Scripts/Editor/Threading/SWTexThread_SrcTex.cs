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

	public class SWTexThread_SrcTex : SWTexThread_Tex {
		protected SWTexture2DEx texSrc;
		protected static Color[] texSrcColorBuffer;
		protected int texSrcWidth;
		protected int texSrcHeight;

		public SWTexThread_SrcTex(SWTexture2DEx _tex,SWTexture2DEx _texSrc,SWBrush _brush):base(_tex,_brush)
		{
			texColorBuffer = _tex.GetPixels ();
			
			texSrc = _texSrc;
			texSrcColorBuffer = _texSrc.GetPixels ();
			texSrcWidth = texSrc.width;
			texSrcHeight = texSrc.height;
		}

		protected override void CalRect ()
		{
			rect.xMin = 0;
			rect.yMin = 0;
			rect.xMax = texSrcWidth;
			rect.yMax = texSrcHeight;
		}

		protected override void ThreadMission_Pixel(int i,int j)
		{
			base.ThreadMission_Pixel (i, j);
		}

		protected override void MissionEnd ()
		{
			base.MissionEnd ();
			tex.SetPixels (texColorBuffer);
			tex.Apply ();
		}
	}
}
