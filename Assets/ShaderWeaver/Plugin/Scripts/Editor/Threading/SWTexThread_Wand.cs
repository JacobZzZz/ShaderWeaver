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

	public struct PixelCom
	{
		public int x;
		public int y;
		public int colorFrom;
	}
	public class SWTexThread_Wand :SWTexThread_SrcTex {
		float tolerance;

		List<PixelCom> tasks;
		int[,] map;

		public SWTexThread_Wand(SWTexture2DEx _tex,SWTexture2DEx _texSrc,SWBrush _brush):base(_tex,_texSrc,_brush)
		{
			displayProgress = true;
		}


		public void Process(Vector2 uv,float _tolerance)
		{
			tasks = new List<PixelCom>();
			map = new int[texSrcWidth, texSrcHeight];



			tolerance = _tolerance;
			int index = SWTextureProcess.TexUV2Index (texSrcWidth,texSrcHeight,uv);
			int x = 0;
			int y = 0;
			SWTextureProcess.TexUV2Index(texSrcWidth,texSrcHeight,uv,ref x,ref y);

			AddTask (x,y,index);

			while (tasks.Count > 0) {
				var n = new List<PixelCom> (tasks);
				tasks.Clear ();
				foreach (var item in n) {
					PerPixel (item);
				}
			}

			MissionEnd ();
		}

		void AddTask(int x,int y,int cfrom)
		{
			if (x < 0 || x >= map.GetLength (0))
				return;
			if (y < 0 || y >= map.GetLength (1))
				return;
			if (map [x, y] != 0)
				return;
			map [x,y] = 1;
			PixelCom p = new PixelCom ();
			p.x = x;
			p.y = y;
			p.colorFrom = cfrom;
			tasks.Add (p);
		}

		void PerPixel(PixelCom p)
		{
			map [p.x, p.y] = 2;
			int index = SWTextureProcess.XYtoIndex (texSrcWidth, texSrcHeight, p.x, p.y);
			float m = SWTextureProcess.Match (texSrcColorBuffer [index], texSrcColorBuffer [p.colorFrom]);
			if (m <= tolerance) {
				SWTextureProcess.Brush_ApplyOnce (ref texColorBuffer [index], brush, 0);

				AddTask (p.x+1, p.y,index);
				AddTask (p.x-1, p.y,index);
				AddTask (p.x, p.y-1,index);
				AddTask (p.x, p.y+1,index);
			}
		}
	}
}

