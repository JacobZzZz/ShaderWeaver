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

	public class SWTexThread_RemapDir :SWTexThread_SrcTex {
		float threhold = 0.1f;
		Vector2 offset;
		Vector2 center;
		Matrix4x4 matrix;
		float accuracyUnit;
		int pixelBack;

		//Vector2 dir;
		Vector2 dirHor;
		public SWTexThread_RemapDir(SWTexture2DEx _tex,SWTexture2DEx _texSrc,SWBrush _brush):base(_tex,_texSrc,_brush)
		{
			texColorBuffer = new Color[texSrcColorBuffer.Length];
			displayProgress = true;
		}


		public void Process(Vector2 _offset,float _accuracyUnit,int _pixelBack)
		{
			pixelBack = _pixelBack;
			offset = _offset;
			accuracyUnit = _accuracyUnit;
			Vector2 v1 = new Vector2 (0, -1);
			Vector2 v2 = offset;
			float angle = SWCommon.AngleSigned (v1, v2, new Vector3 (0, 0, 1));
			matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.Euler (0, 0, angle), Vector3.one);
			center =new Vector2 (0.5f, 0.5f);

			//dir = offset.normalized;
			dirHor = SWCommon.RotateVector (offset, new Vector3(0,0,90)).normalized;
			Process ();
		}
		protected override void ThreadMission_Pixel(int i,int j)
		{
			base.ThreadMission_Pixel (i, j);
			Vector2 uv = SWTextureProcess.TexUV (texSrcWidth, texSrcHeight, i, j);
			Vector2 fromUV = uv - offset;
			Vector2 dis = fromUV - uv;
			float alpha = 0;
			float pcg = 0;
			Color c = SWTextureProcess.GetColor_UV(texSrcWidth,texSrcHeight,texSrcColorBuffer,uv);
			Color c2 = SWTextureProcess.GetColor_UV(texSrcWidth,texSrcHeight,texSrcColorBuffer,uv - dis.normalized*3f/512f);
			Vector2 newUV = uv - dis.normalized * pixelBack / 512f;
			int newI = 0;
			int newJ = 0;
			SWTextureProcess.TexUV2Index (texSrcWidth, texSrcHeight, newUV, ref newI, ref newJ);
			if (!IsImage(c) && !IsImage(c2)) {
//				int index = 0;
				while (pcg<=1) {
					Vector2 xuv = uv + dis * pcg;
					Color xColor = SWTextureProcess.GetColor_UV (texSrcWidth, texSrcHeight, texSrcColorBuffer, xuv);
					Vector2 xpos = uv;
					xpos -= center;
					xpos =	matrix.MultiplyPoint (xpos);
					xpos += center;
					if (xColor.a > 0) {
						alpha = pcg;
						float g = alpha;
						float b = 0;
						float a = GreenToAlpha (g);
						texColorBuffer [(texSrcHeight - newJ - 1) * texSrcWidth + newI] = new Color (xpos.x, alpha, b, a);
						break;
					}


					pcg += accuracyUnit;
				}
			}
		}

		protected override void MissionEnd ()
		{
			Process (ThreadMission_Pixel2, MissionEnd2);
		}

		void ThreadMission_Pixel2(int i,int j)
		{
			int index = SWTextureProcess.XYtoIndex (texWidth, texHeight, i, j);
			Vector2 uv = SWTextureProcess.TexUV (texWidth, texHeight, i, j);
			Color c = texColorBuffer [index];

			float dis = offset.magnitude * threhold;
			float alpha = -1;
			for (float v = 0; v < 1; v+=0.01f) {
				Vector2 newUV = uv + dirHor * v * dis;
				int newIndex = SWTextureProcess.TexUV2Index(texWidth,texHeight,newUV);
				Color newC = texColorBuffer[newIndex];
				if (newC.a == 0) {
					alpha = v;
					break;
				}


				newUV = uv - dirHor * v * dis;
				newIndex = SWTextureProcess.TexUV2Index(texWidth,texHeight,newUV);
				newC = texColorBuffer[newIndex];
				if (newC.a == 0) {
					alpha = v;
					break;
				}
			}

//			if (alpha < 0.5f)
//				alpha = 0;

			if(alpha>=0)
				texColorBuffer [index] = new Color(c.r,c.g,c.b,c.a*alpha);
		}

		protected void MissionEnd2 ()
		{
			base.MissionEnd ();
		}
		static bool IsImage(Color c)
		{
			if (c.a <= 0.8f)
				return false;
			return true;
		}


		float GreenToAlpha(float g)
		{
			float a = 0;
			if (g < threhold)
				a= 1 - (threhold - g) / threhold;
			if (g > 1- threhold)
				a= 1 - (g - (1- threhold)) / threhold;
			else
				a=1;
			return a;
		}
	}
}