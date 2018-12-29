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

	public class SWTexThread_Tex : TexThread {
		public static int centerX;
		public static int centerY;

		protected SWBrush brush;
		protected SWTexture2DEx tex;
		protected static Color[] texColorBuffer;
		protected int texWidth;
		protected int texHeight;
		protected List<Vector2> uvs;
		protected Vector2 uv;
		protected static readonly int extraSize = 5;

		public SWTexThread_Tex(SWTexture2DEx _tex,SWBrush _brush):base()
		{
			tex = _tex;
			texWidth = tex.width;
			texHeight = tex.height;

			brush = _brush;
		}

		protected override void CalRect ()
		{
			rect.xMin = 0;
			rect.yMin = 0;
			rect.xMax = texWidth;
			rect.yMax = texHeight;
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

		protected IntRect GetBrushRect(List<Vector2> uvs)
		{
			int rectSize = brush.size + extraSize;

			IntRect rect = new IntRect ();
			foreach (var uv in uvs) {
				int centerX = 0;
				int centerY = 0;
				SWTextureProcess.TexUV2Index (texWidth, texHeight, uv, ref centerX, ref centerY);
				int xMin = centerX - rectSize;
				int xMax = centerX + rectSize;
				int yMin = centerY - rectSize;
				int yMax = centerY + rectSize;
				if (xMin < rect.xMin)
					rect.xMin = xMin;
				if (yMin < rect.yMin)
					rect.yMin = yMin;
				if (xMax > rect.xMax)
					rect.xMax = xMax;
				if (yMax > rect.yMax)
					rect.yMax = yMax;
			}
			rect.xMin = Mathf.Clamp (rect.xMin,0, texWidth);
			rect.xMax = Mathf.Clamp (rect.xMax,0, texWidth);
			rect.yMin = Mathf.Clamp (rect.yMin,0, texHeight);
			rect.yMax = Mathf.Clamp (rect.yMax,0, texHeight);
			return rect;
		}
	}
}