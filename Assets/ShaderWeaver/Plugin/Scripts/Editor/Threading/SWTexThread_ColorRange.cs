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

	public class SWTexThread_ColorRange :SWTexThread_SrcTex {
		List<Color> colors = new List<Color> ();
		float tolerance;

		public SWTexThread_ColorRange(SWTexture2DEx _tex,SWTexture2DEx _texSrc,SWBrush _brush):base(_tex,_texSrc,_brush)
		{
			displayProgress = true;
		}


		public void Process(Vector2 uv,float _tolerance)
		{
			int index = SWTextureProcess.TexUV2Index (texSrcWidth,texSrcHeight,uv);
			colors.Add (texSrcColorBuffer [index]);

			tolerance = _tolerance;
			Process ();
		}


		protected override void ThreadMission_Pixel(int i,int j)
		{
			base.ThreadMission_Pixel (i, j);
			var cc = texSrcColorBuffer[(texSrcHeight-j-1) * texSrcWidth + i];
			float m = SWTextureProcess.Match (cc, colors);
			if (m <= tolerance) {
				//Vector2 _uv = TexUV (t_Width, t_Height, i, j);
				int index = SWTextureProcess.XYtoIndex(texSrcWidth,texSrcHeight,i,j);
				SWTextureProcess.Brush_ApplyOnce(ref texColorBuffer[index],brush,0);
			}
		}
	}
}