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

	/// <summary>
	/// R=X asix G=Y asix  B=stitching a=mask
	/// </summary>
	public class TexThread_RemapLineStitch : SWTexThread_Tex {
		float threhold = 0.1f;
		RemapLineInfo info;
		float size;
		public TexThread_RemapLineStitch(SWTexture2DEx _tex,SWBrush _brush):base(_tex,_brush)
		{
			texColorBuffer = tex.GetPixels ();
			displayProgress = true;
		}


		public void Process(RemapLineInfo _info,float _size)
		{
			info = _info;
			size = _size;

			info.pts [0].pcg = 0;
			float len = 0;

			//step 1:Cal Point Pcg
			for (int i = 0; i < info.pts.Count; i++) {

				len += Vector2.Distance(info.pts[SWTextureProcess.LoopID(i,info.pts.Count)].uv,info.pts[SWTextureProcess.LoopID(i+1,info.pts.Count)].uv);
			}
			for (int i = 1; i < info.pts.Count; i++) {
				float dis = Vector2.Distance(info.pts[SWTextureProcess.LoopID(i,info.pts.Count)].uv,info.pts[SWTextureProcess.LoopID(i-1,info.pts.Count)].uv);
				info.pts [i].pcg = info.pts [SWTextureProcess.LoopID(i-1,info.pts.Count)].pcg+dis / len;
			}




			//step 2:before after pcg   center
			for (int k = 0; k < info.pts.Count; k++) {

				var p0 = info.pts [SWTextureProcess.LoopID(k-1,info.pts.Count)].uv;
				var p1 = info.pts [SWTextureProcess.LoopID(k,info.pts.Count)].uv;
				var p2 = info.pts [SWTextureProcess.LoopID(k+1,info.pts.Count)].uv;


				float angle = Vector2.Angle (p2 - p1, p0 - p1);
				float x = 90 - angle * 0.5f;
				float width = size * Mathf.Tan (x * Mathf.Deg2Rad);
				info.pts[k].pre = p1 + (p0 - p1).normalized * width;
				info.pts[k].after = p1 + (p2 - p1).normalized * width;



				var me = info.pts [SWTextureProcess.LoopID(k,info.pts.Count)];
				var item = info.pts [SWTextureProcess.LoopID(k-1,info.pts.Count)];
				var mePcg = me.pcg;
				var itemPcg = item.pcg;

				if (k == 0) {
					mePcg = 1;
				}

				float dis = Vector2.Distance (me.uv, item.uv);
				info.pts [k].prePcg = mePcg + (itemPcg - mePcg) * width / dis;




				item = info.pts [SWTextureProcess.LoopID(k+1,info.pts.Count)];
				itemPcg = item.pcg;
				if (k == info.pts.Count - 1) {
					itemPcg = 1;
				}


				dis = Vector2.Distance (me.uv, item.uv);
				info.pts [k].afterPcg = mePcg + (itemPcg - mePcg) * width /dis;
				if (k == 0) {
					info.pts [k].afterPcg = 1;
				}

				float angleSigned = SWCommon.AngleSigned (p2 - p1, p1 - p0, new Vector3 (0, 0, 1));
				Vector2 vv = p1 - p0;
				Matrix4x4 m = Matrix4x4.TRS (Vector3.zero, Quaternion.Euler (0, 0, Mathf.Sign (angleSigned) * -90), Vector3.one);
				vv = m.MultiplyVector (vv);
				info.pts[k].center = info.pts[k].pre + vv.normalized * size;
			}

			Process ();
		}
		protected override void ThreadMission_Pixel(int i,int j)
		{
			base.ThreadMission_Pixel (i, j);
			Vector2 uv = SWTextureProcess.TexUV_RemapThread (texWidth, texHeight, i, j);
			for (int k = 0; k < info.pts.Count; k++) {
				if (k == 0) {
					var p0 = info.pts [SWTextureProcess.LoopID(k-1,info.pts.Count)];
					var p1 = info.pts [SWTextureProcess.LoopID(k,info.pts.Count)];
					var p2 = info.pts [SWTextureProcess.LoopID(k+1,info.pts.Count)];
					float pcg = SWTextureProcess.PointOnSegPcgSign (uv, p1.after, p2.uv);


					if(k == info.pts.Count-1)
						pcg = p1.pcg + (1 - p1.pcg) * pcg;
					else
						pcg = p1.pcg + (p2.pcg - p1.pcg) * pcg;
					float innerDis = 0;
					bool seg = SWTextureProcess.Point2SegOnlyDisSign (uv, p1.after, p2.pre,ref innerDis);
					innerDis = -innerDis;

					if (seg && (Mathf.Abs (innerDis) <= size)) {
						float g = (innerDis/size + 1)*0.5f;
						float b = (pcg > 0.995f || pcg < 0.005f )? 1:0;
						float a = GreenToAlpha (g);
						texColorBuffer [(texHeight - j - 1) * texWidth + i] = new Color (pcg, g, b, a);
					} 


				} else {
					var p0 = info.pts [SWTextureProcess.LoopID (k - 1, info.pts.Count)];
					var p1 = info.pts [SWTextureProcess.LoopID (k, info.pts.Count)];
					var p2 = info.pts [SWTextureProcess.LoopID (k + 1, info.pts.Count)];
					float pcg = SWTextureProcess.PointOnSegPcgSign (uv, p1.uv, p2.uv);


					if (k == info.pts.Count - 1)
						pcg = p1.pcg + (1 - p1.pcg) * pcg;
					else
						pcg = p1.pcg + (p2.pcg - p1.pcg) * pcg;
					float innerDis = 0;
					bool seg = SWTextureProcess.Point2SegOnlyDisSign (uv, p1.after, p2.pre, ref innerDis);
					innerDis = -innerDis;

					if (seg && (Mathf.Abs (innerDis) <= size)) {
						float g = (innerDis / size + 1) * 0.5f;
						float b = (pcg > 0.995f || pcg < 0.005f )? 1:0;
						float a = GreenToAlpha (g);
						texColorBuffer [(texHeight - j - 1) * texWidth + i] = new Color (pcg, g, b, a);
					} 
				}
			}
		}

		protected override void MissionEnd ()
		{
			Process (ThreadMission_Pixel2,MissionEnd2);
		}



		protected void ThreadMission_Pixel2(int i,int j)
		{
			base.ThreadMission_Pixel (i, j);
			Vector2 uv = SWTextureProcess.TexUV_RemapThread (texWidth, texHeight, i, j);
			for (int k = 0; k < info.pts.Count; k++) {
				var p0 = info.pts [SWTextureProcess.LoopID(k-1,info.pts.Count)];
				var p1 = info.pts [SWTextureProcess.LoopID(k,info.pts.Count)];
				var p2 = info.pts [SWTextureProcess.LoopID(k+1,info.pts.Count)];
				float pcg = SWTextureProcess.PointOnSegPcgSign (uv, p1.uv, p2.uv);
				pcg = p1.pcg + (p2.pcg - p1.pcg) * pcg;
//				float innerDis = 0;
//				bool seg = SWTextureProcess.Point2SegOnlyDisSign (uv, p1.after, p2.pre,ref innerDis);

				if(texColorBuffer [(texHeight - j - 1) * texWidth + i].b <=0.5){
					float dis1 = SWTextureProcess.Point2SegDis (uv, p0.uv, p1.uv);
					float dis2 = SWTextureProcess.Point2SegDis (uv, p1.uv, p2.uv);


					bool e1 = Vector2.Dot (uv - p1.pre, p0.uv-p1.pre) < 0;
					bool e2 = Vector2.Dot (uv - p1.after, p2.uv-p1.after) < 0;


					if (e1 && e2 && (dis1 <= size || dis2 <= size)) {
						Vector2 v1 =  p1.pre - p1.center;
						Vector2 v2 =  uv - p1.center;
						Vector2 v3 =  p1.after - p1.center;

						float angle1 = Vector2.Angle (v2, v1);
						float angleAll = Vector2.Angle (v1, v3); 

						pcg = p1.prePcg + (p1.afterPcg - p1.prePcg) * angle1 /angleAll;




//						float disSigned1 = -SWTextureProcess.Point2LineDisSign (uv, p0.uv, p1.uv);
//						float disSigned2 = -SWTextureProcess.Point2LineDisSign (uv, p1.uv, p2.uv);
//						float disSigned = disSigned1 + (disSigned2 - disSigned1) * angle1 /angleAll;
						//float g =  ( disSigned / size+1)*0.5f;
						float g = 0;
						if(SWCommon.AngleSigned(v3,v1,new Vector3(0,0,1))   < 0)
							g =  Vector2.Distance (uv, p1.center) / (size * 2);
						else
							g =  1 - Vector2.Distance (uv, p1.center) / (size * 2);
						float b = (pcg > 0.995f || pcg < 0.005f )? 1:0;
						float a = GreenToAlpha (g);
						texColorBuffer [(texHeight - j - 1) * texWidth + i] = new Color (pcg, g, b, a);

					}
				}
			}
		}

		protected void MissionEnd2 ()
		{
			base.MissionEnd ();
		}


		float GreenToAlpha(float g)
		{
			if (g < threhold)
				return 1 - (threhold - g) / threhold;
			if (g > 1- threhold)
				return 1 - (g - (1- threhold)) / threhold;
			return 1;
		}
	}
}