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

	public class SWTexThread_TexDrawLine : SWTexThread_Tex {
		protected Vector2 startUV;
		protected Vector2 endUV;

		public SWTexThread_TexDrawLine(SWTexture2DEx _tex,SWBrush _brush):base(_tex,_brush)
		{
		}

		public void Process( Vector2 _startUV,Vector2 _endUV)
		{
			startUV = _startUV;
			endUV = _endUV;
			uvs = new List<Vector2> ();
			uvs.Add (_startUV);
			uvs.Add (_endUV);
			Process ();
		}
		protected override void CalRect ()
		{
			rect = GetBrushRect(uvs);
		}
		protected override void ThreadMission_Pixel(int i,int j)
		{
			base.ThreadMission_Pixel (i, j);
			Vector2 _uv = SWTextureProcess.TexUV (texWidth, texHeight, i, j);
			float dis =	SWTextureProcess.Point2SegDis (_uv, startUV, endUV);
			float isize = (float)brush.size / (float)texWidth;
			if (dis < isize) {
				float disPcg = dis / isize;
				SWTextureProcess.Brush_Apply(ref texColorBuffer [(texHeight-j-1) * texWidth + i] ,brush,disPcg,i,j);
			}
		}
	}
}