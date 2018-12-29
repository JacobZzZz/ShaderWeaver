//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using System.IO;
	using System.Threading;

	/// <summary>
	/// For texture of different sizes,use standard bottom-left pivot uv cordinate to calculate
	/// </summary>
	public class SWTextureProcess{
		/// <summary>
		/// Resize for editing
		/// </summary>
		public static bool TextureResize(SWTexture2DEx tex,int size)
		{
			if (size != tex.Texture.width) {
				var cs = tex.Texture.GetPixels ();
				var oldW = tex.width;
				var oldH = tex.height;
	
				var colorsNew = TextureResizeBase (cs, oldW, oldH, size, size);
				tex.Texture.Resize (size, size);
				tex.SetPixels (colorsNew);
				tex.Apply ();
				return true;
			}
			return false;
		}


		public static SWTexture2DEx TextureResize(Texture2D tex,int newWidth,int newHeight)
		{
			SWTexture2DEx newTex = SWCommon.TextureCreate ( newWidth,  newHeight,TextureFormat.ARGB32);
			var colors = TextureResizeBase (tex.GetPixels (), tex.width, tex.height, newWidth, newHeight);
			newTex.SetPixels (colors);
			newTex.Apply ();
			return newTex;
		}
		protected static Color[] TextureResizeBase(Color[] colorsOrigin,int oldW,int oldH,int newW,int newH)
		{
			Color[] colorsNew = new Color[newW * newH];
			for (int i = 0; i < newW; i++) {
				for (int j = 0; j < newH; j++) {
					int index = XYtoIndex (newW, newH, i, j);
					Vector2 uv = SWTextureProcess.TexUV (newW, newH, i, j);
					colorsNew[index] = SWTextureProcess.GetColor_UV (oldW, oldH, colorsOrigin, uv);
				}
			}
			return colorsNew;
		}
//		public static SWTexture2DEx TextureResize(Texture2D tex,int newWidth,int newHeight)
//		{
//			SWTexture2DEx newTex = SWCommon.TextureCreate ( newWidth,  newHeight,TextureFormat.ARGB32);
//			var colorsOrigin = tex.GetPixels ();
//			var colors = newTex.GetPixels ();
//			for (int i = 0; i < newWidth; i++) {
//				for (int j = 0; j < newHeight; j++) {
//					Vector2 uv = SWTextureProcess.TexUV (newWidth, newHeight, i, j);
//					int index =  TexUV2Index(newTex.width,newTex.height,uv);
//					colors[index] = SWTextureProcess.GetColor_UV (tex.width, tex.height, colorsOrigin, uv);
//				}
//			}
//			newTex.SetPixels (colors);
//			newTex.Apply ();
//			return newTex;
//		}



		public static int TexUV2Index(int width,int height,Vector2 uv)
		{
			int x = 0;
			int y = 0;
			TexUV2Index (width, height, uv, ref x, ref y);
			return (height - y - 1) * width + x;
		}
		public static int XYtoIndex(int width,int height,int x,int y)
		{
			return (height - y - 1) * width + x;
		}

		public static void TexUV2Index(int width,int height,Vector2 uv,ref int x,ref int y)
		{
			x = Mathf.RoundToInt(uv.x * (float)width);
			y = Mathf.RoundToInt(uv.y * (float)height);

			x = Mathf.Clamp (x, 0, width-1);
			y = Mathf.Clamp (y, 0, height-1);
		}

		public static Vector2 TexUV(float width,float height,float x,float y)
		{
			x = Mathf.Clamp (x, 0, width-1);
			y = Mathf.Clamp (y, 0, height-1);
			return new Vector2 (x/width, y/height);
		}

		//lsy : new remap ratio
		public static Vector2 TexUV_NoClamp(float width,float height,float x,float y)
		{
			return new Vector2 (x/width, y/height);
		}
		public static Vector2 TexUV_RemapThread(float width,float height,float x,float y)
		{
			var xratio = SWWindowDrawRemap.Instance.XRatio ();
			var yratio = SWWindowDrawRemap.Instance.YRatio ();
			var v = TexUV (width, height, x, y);
			return new Vector2 (v.x * xratio, v.y * yratio);
		}

		public static Color GetColor_UV(int width,int height,Color[] c, Vector2 uv)
		{
			int x = 0;
			int y = 0;
			TexUV2Index (width, height, uv, ref x, ref y);
			return c [(height-y-1) * width + x];
		}

		public static float Match(Color c,List<Color> cs)
		{
			float d = float.MaxValue;
			float tmp =0;
			foreach (var item in cs) {
				tmp = Match (c, item);
				if (tmp < d)
					d = tmp;
			}
			return d;
		}



//This version get blank area will go error
//		public static float Match(Color c,Color item)
//		{
//			float d = Mathf.Abs (item.r - c.r) + Mathf.Abs (item.g - c.g) + Mathf.Abs (item.b - c.b) + Mathf.Abs (item.a - c.a);
//			return d*255;
//		}

//This version can't get black text
//		public static float Match(Color c1,Color c2)
//		{
//			Vector3 v1 = new Vector3 (c1.r, c1.g, c1.b);
//			v1 *= c1.a;
//			Vector3 v2 = new Vector3 (c2.r, c2.g, c2.b);
//			v2 *= c2.a;
//			return Vector3.Distance (v1, v2) * 255;
//		}
		public static float Match(Color c1,Color c2)
		{
			Vector4 v1 = new Vector4 (c1.r, c1.g, c1.b,1f);
			v1 *= c1.a;
			Vector4 v2 = new Vector4 (c2.r, c2.g, c2.b,1f);
			v2 *= c2.a;
			return Vector4.Distance (v1, v2) * 255;
		}


		public static int LoopID(int id,int count)
		{
			while (id < 0)
				id += count;
			while (id >= count)
				id -= count;
			return id;
		}



		#region Draw
		public static float PointOnSegPcgSign(Vector2 p,Vector2 a,Vector2 b)
		{
			Vector2 v1 = p - a;
			Vector2 v2 = b - a;

			float v = (Vector2.Dot (v1, v2)/ v2.magnitude)/v2.magnitude;
			return v;
		}

		public static float Point2LineDisSign(Vector2 p,Vector2 v1,Vector2 v2)
		{
			return ((v1.y - v2.y) * p.x + (v2.x - v1.x) * p.y + (v1.x * v2.y - v2.x * v1.y)) /(Mathf.Sqrt(Mathf.Pow (v2.x - v1.x, 2) + Mathf.Pow (v2.y - v1.y, 2)));
		}

		public static float Point2SegDis(Vector2 p,Vector2 a,Vector2 b)
		{
			return Mathf.Abs (Point2SegDisSign (p, a, b));
		}
		public static float Point2SegDisSign(Vector2 p,Vector2 a,Vector2 b)
		{
			if (Vector2.Dot (p - a, b - a) < 0)
				return Vector2.Distance (p, a);
			if (Vector2.Dot (p - b, a - b) < 0)
				return Vector2.Distance (p, b);
			return ((a.y - b.y) * p.x + (b.x - a.x) * p.y + (a.x * b.y - b.x * a.y)) / Mathf.Sqrt ((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
		}
		public static bool PointInSeg(Vector2 p,Vector2 a,Vector2 b,float size)
		{
			if (Vector2.Dot (p - a, b - a) < 0)
				return false;
			if (Vector2.Dot (p - b, a - b) < 0)
				return false;

			float dis = Point2SegDis(p,a,b);
			return dis < size;
		}
		public static bool Point2SegOnlyDisSign(Vector2 p,Vector2 a,Vector2 b,ref float dis)
		{
			if (Vector2.Dot (p - a, b - a) < 0)
				return false;
			if (Vector2.Dot (p - b, a - b) < 0)
				return false;


			dis = ((a.y - b.y) * p.x + (b.x - a.x) * p.y + (a.x * b.y - b.x * a.y)) / Mathf.Sqrt ((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
			return true;
		}


		/// <summary>
		/// 单次笔刷,例如魔棒,取色.单次操作不会重复叠加,因此直接加上去即可.
		/// </summary>
		public static void Brush_ApplyOnce(ref Color c,SWBrush brush,float disPcg)
		{
			float pcg = Brush_CalPcg (brush, disPcg);
			Brush_SetColorOnce (ref c, brush, pcg);
		}
		/// <summary>
		/// 连续笔刷,例如 笔刷/橡皮. 一笔操作需防止重复叠加,又因橡皮的公式为*(1-x) x量不可以拆分,因此统一处理为 初始值x最大笔刷量
		/// </summary>
		public static void Brush_Apply(ref Color c,SWBrush brush,float disPcg,int x,int y)
		{
			float pcg = Brush_CalPcg (brush, disPcg);
			if (pcg <= brushBuffer [x,y]) {
				return;
			}
			brushBuffer [x,y] = pcg;

			int id = XYtoIndex (brushBuffer.GetLength (0), brushBuffer.GetLength (1),x,y);
			Brush_SetColor (ref c, brush, pcg,id);
		}
			
		private static float Brush_CalPcg(SWBrush brush,float disPcg = -1)
		{
			float realPcg = 0;
			if (disPcg < 0.55f)
				realPcg = 1;
			else
				realPcg = Mathf.Lerp (1, 0, (disPcg - 0.55f) / 0.45f);
			realPcg *= brush.trans;
			return realPcg;
		}

		private static void Brush_SetColorOnce(ref Color c,SWBrush brush,float applyPcg)
		{
			if (brush.mode == SWBrushMode.erase) {
				c *= (1 - applyPcg);
				return;
			}
			if (brush.mask [0])
				c.r += brush.color.r*applyPcg;
			if (brush.mask [1])
				c.g += brush.color.g*applyPcg;
			if (brush.mask [2])
				c.b += brush.color.b*applyPcg;
			if (brush.mask [3])
				c.a += brush.color.a*applyPcg;
		}

		private static void Brush_SetColor(ref Color c,SWBrush brush,float applyPcg,int id)
		{
			if (brush.mode == SWBrushMode.erase) {
				c = colorStartBuffer [id] * (1 - applyPcg);
				return;
			}
			if (brush.mask [0])
				c.r = colorStartBuffer [id].r + brush.color.r*applyPcg;
			if (brush.mask [1])
				c.g = colorStartBuffer [id].g + brush.color.g*applyPcg;
			if (brush.mask [2])
				c.b = colorStartBuffer [id].b + brush.color.b*applyPcg;
			if (brush.mask [3])
				c.a = colorStartBuffer [id].a + brush.color.a*applyPcg;
		}
		#endregion


		#region threading thread

		/// <summary>
		/// 为橡皮 和笔刷 记录初始色，防止一笔内的叠加
		/// </summary>
		static Color[] colorStartBuffer;
		/// <summary>
		/// 记录笔刷像素点最大量
		/// </summary>
		static float[,] brushBuffer;
		public static void ProcessMask_DrawPoint(SWTexture2DEx tex, Vector2 _uv,SWBrush _brush)
		{
			colorStartBuffer = tex.GetPixels ();
			brushBuffer = new float[tex.width, tex.height];

			SWUndo.RegisterCompleteObjectUndo (tex);
			SWTexThread_TexDrawPoint t = new SWTexThread_TexDrawPoint (tex, _brush);
			t.Process (_uv);
		}
		public static void ProcessMask_DrawLine(SWTexture2DEx tex, Vector2 _startUV,Vector2 _endUV,SWBrush brush)
		{
			SWUndo.RegisterCompleteObjectUndo (tex);
			SWTexThread_TexDrawLine t = new SWTexThread_TexDrawLine (tex, brush);
			t.Process (_startUV,_endUV);
		}

		public static void ProcessMask_Dropper(SWTexture2DEx tex,SWTexture2DEx texSource, Vector2 _uv,SWBrush _brush,float tolerance)
		{
			SWUndo.RegisterCompleteObjectUndo (tex);
			SWTexThread_ColorRange t = new SWTexThread_ColorRange (tex, texSource, _brush);
			t.Process (_uv, tolerance);
		}
		public static void ProcessMask_Wand(SWTexture2DEx tex,SWTexture2DEx texSource, Vector2 _uv,SWBrush _brush,float tolerance)
		{
			SWUndo.RegisterCompleteObjectUndo (tex);
			texSource.filterMode = FilterMode.Point;
			SWTexThread_Wand t = new SWTexThread_Wand (tex, texSource, _brush);
			t.Process (_uv, tolerance);
		}
		public static void ProcessMask_Invert(SWTexture2DEx tex)
		{
			SWUndo.RegisterCompleteObjectUndo (tex);
			SWTexThread_Invert t = new SWTexThread_Invert (tex, null);
			t.Process (null);
		}


		public static void ProcessRemap_Dir(SWTexture2DEx tex,SWTexture2DEx texSource,Vector2 offset,bool precise,int _pixelBack)
		{
			SWUndo.RegisterCompleteObjectUndo (tex);
			SWTexThread_RemapDir d = new SWTexThread_RemapDir (tex, texSource,null);
			if(precise)
				d.Process (offset,0.01f,_pixelBack);
			else
				d.Process (offset,0.1f,_pixelBack);
		}
		public static void ProcessRemap_Line(SWTexture2DEx tex, RemapLineInfo info,float size)
		{
			SWUndo.RegisterCompleteObjectUndo (tex);
			//if (info.stitch)
			if (SWWindowDrawRemap.Instance.rData.l.st)
				ProcessRemap_LineStitch (tex, info, size);
			else
				ProcessRemap_LineNormal (tex, info, size);
		}
		public static void ProcessRemap_LineNormal(SWTexture2DEx tex, RemapLineInfo info,float size)
		{
			TexThread_RemapLine t = new TexThread_RemapLine (tex, null);
			t.Process (info, size);
		}
		public static void ProcessRemap_LineStitch(SWTexture2DEx tex, RemapLineInfo info,float size)
		{
			TexThread_RemapLineStitch t = new TexThread_RemapLineStitch (tex, null);
			t.Process (info, size);
		}

		public static void ProcessTexture_Clean(SWTexture2DEx tex)
		{
			SWUndo.RegisterCompleteObjectUndo (tex);
			Color[] colors = new Color[tex.width*tex.height];
			tex.SetPixels (colors);
			tex.Apply ();
		}
		#endregion
	}
}