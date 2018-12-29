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
	using System;

	[System.Serializable]
	public class RemapWayPoint
	{
		public Vector2 mousePos;
		public float pcg;


		public Vector2 uv;

		public Vector2 pre;
		public Vector2 after;
		public float prePcg;
		public float afterPcg;


		public Vector2 center;

		public Material matArrow;
	}
	[System.Serializable]
	public class RemapLineInfo:ScriptableObject
	{
		public List<RemapWayPoint> pts = new List<RemapWayPoint>();
	}


	[System.Serializable]
	public class SWNodeRemap :SWNodeBase {
		[SerializeField]
		public Texture2D texChildOrigin;
		[SerializeField]
		public SWTexture2DEx texChildResized;
		[SerializeField]
		public float texChildOrigin_Width;
		[SerializeField]
		public float texChildOrigin_Height;
		[SerializeField]
		public RemapLineInfo lineInfo;



		public override void Init (SWDataNode _data, SWWindowMain _window)
		{
			childPortSingleConnection = true;
			styleID = 2;
			base.Init (_data, _window);
			data.outputType.Add (SWDataType._UV);
			data.inputType.Add (SWDataType._Color);

			if (textureEx.IsNull)
				ResetTex ();

			lineInfo = ScriptableObject.CreateInstance<RemapLineInfo>();
		}

		public override bool PortMatch (int port, SWNodeBase child, int childPort)
		{
			return base.PortMatch (port, child, childPort) && (child.data.type == SWNodeType.image || child.data.type == SWNodeType.dummy);
		}

		void ResetTex()
		{
			textureEx = SWCommon.TextureCreate( data.resolution,data.resolution,TextureFormat.ARGB32);
		}

		protected override void DrawHead ()
		{
			base.DrawHead ();
		}

		protected override void DrawNodeWindow (int id)
		{
			base.DrawNodeWindow (id);
			if (data.useCustomTexture) {
				DrawWinCustom (id);
			} else {
				DrawWin (id);
			}
		}

		void DrawWin(int id)
		{
			base.DrawNodeWindow (id);
			if (GetChildTexture () != null) {
				if (texChildOrigin != GetChildTexture ()) {
					UpdateTex ();
				}

				//lsy : new remap ratio
				texChildOrigin_Width = texChildOrigin.width;
				texChildOrigin_Height = texChildOrigin.height;

				if (textureEx != null) {
					GUI.DrawTexture (rectArea, textureEx.Texture, ScaleMode.StretchToFill);
				}  


				if (GUI.Button (new Rect(rectBotButton.x,rectBotButton.y,rectBotButton.width- buttonHeight,rectBotButton.height),"Edit",SWEditorUI.MainSkin.button)) {
					SWWindowDrawRemap.ShowEditor (this);
				}
				if (GUI.Button (new Rect(rectBotButton.x+rectBotButton.width - buttonHeight,rectBotButton.y,buttonHeight,rectBotButton.height),"+",SWEditorUI.MainSkin.button)) {
					data.useCustomTexture = !data.useCustomTexture;
				}
			}

			DrawNodeWindowEnd ();
		}

		void DrawWinCustom(int id)
		{
			base.DrawNodeWindow (id);
			SelectTexture ();
			if (GUI.Button (rectBotButton,"Switch",SWEditorUI.MainSkin.button)) {
				data.useCustomTexture = !data.useCustomTexture;
				if (!data.useCustomTexture)
					ResetTex ();
			}
			DrawNodeWindowEnd ();
		}



		public void UpdateTex()
		{  
			texChildOrigin = GetChildTexture();
			texChildResized = SWTextureProcess.TextureResize (texChildOrigin,  data.resolution,  data.resolution);
			texChildResized.filterMode = FilterMode.Point;
		}
		public override void DrawSelection ()  
		{
			base.DrawSelection ();
		}


		#region save load
		public override void BeforeSave ()
		{
			base.BeforeSave ();

			data.rd.l.pts.Clear ();
			for (int i = 0; i < lineInfo.pts.Count; i++) {
				var uv = lineInfo.pts [i].uv;
				RemapWayPointData pData = new RemapWayPointData ();
				pData.uv = uv;

				data.rd.l.pts.Add (pData);
			}
		}

		public override void AfterLoad ()
		{
			base.AfterLoad ();
			if (GetChildTexture () != null) {
				texChildOrigin = GetChildTexture();
				texChildResized = SWTextureProcess.TextureResize (texChildOrigin, data.resolution, data.resolution);
			}
			if (textureEx != null) {
				if (data.resolution != textureEx.width) {
					data.reso = (SWTexResolution)SWDataNode.resoList.IndexOf (textureEx.width);
				}
			}


			if (data.rd != null && data.rd.l != null && data.rd.l.pts != null) {
				for (int i = 0; i < data.rd.l.pts.Count; i++) {
					var pData = data.rd.l.pts [i];
					RemapWayPoint pt = new RemapWayPoint ();
					pt.uv = pData.uv;
					pt.matArrow = new Material (SWEditorUI.GetShader ("RectTRS"));
					lineInfo.pts.Add (pt);
				}
			}
		}
		#endregion
	}
}