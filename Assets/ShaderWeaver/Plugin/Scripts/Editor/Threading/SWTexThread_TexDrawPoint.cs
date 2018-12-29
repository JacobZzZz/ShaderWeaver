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

	public class SWTexThread_TexDrawPoint : SWTexThread_Tex {
		public SWTexThread_TexDrawPoint(SWTexture2DEx _tex,SWBrush _brush):base(_tex,_brush)
		{
			texColorBuffer = tex.GetPixels ();
		}


		public void Process(Vector2 _uv)
		{
			uv = _uv;
			uvs = new List<Vector2> ();
			uvs.Add (uv);
			Process ();
		}
		protected override void CalRect ()
		{
			rect = GetBrushRect(uvs);
		}
		protected override void ThreadMission_Pixel(int i,int j)
		{
			base.ThreadMission_Pixel (i, j);
			if (brush.size == 1) {
				if (i == centerX && j == centerY) {
					SWTextureProcess.Brush_Apply(ref texColorBuffer [(texHeight-j-1) * texWidth + i] ,brush,0,i,j);
				}
				return;
			}

			Vector2 _uv = SWTextureProcess.TexUV (texWidth, texHeight, i, j);
			float dis = Vector2.Distance (uv, _uv);
			float isize = (float)brush.size / (float)texWidth;
			if (dis < isize) {
				float disPcg = dis / isize;
				SWTextureProcess.Brush_Apply(ref texColorBuffer [(texHeight-j-1) * texWidth + i] ,brush,disPcg,i,j);
			}
		}
	}
}