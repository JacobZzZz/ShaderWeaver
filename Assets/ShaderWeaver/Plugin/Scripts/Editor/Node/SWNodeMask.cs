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
	public class SWNodeMask :SWNodeBase {
		[SerializeField]
		public SWTexture2DEx texMask; 
		public int maskSize
		{
			get{ 
				return texMask.width;
			}
		}
		[SerializeField]
		protected TextureImporter texImporterGray; 
		public TextureImporter TexImporterGray
		{
			get{ 
				if (textureGray == null)
					return null;
				if (texImporterGray == null) {
					string assetPath = AssetDatabase.GetAssetPath (textureGray);
					texImporterGray = AssetImporter.GetAtPath (assetPath) as TextureImporter;
				}
				return texImporterGray;
			}
		}

		public override void ReImport() {
			base.ReImport ();
			if (data.useGray == true) {
				if (TexImporterGray != null) {
					if (TexImporterGray.textureType != TextureImporterType.SingleChannel
					   || TexImporterGray.alphaSource != TextureImporterAlphaSource.FromGrayScale
					   || TexImporterGray.alphaIsTransparency != false) {
						SWCommon.TextureReImportGray (textureGray);
					}
				}
			}
		}

		void ResetMask()
		{
			texMask = SWCommon.TextureCreate( data.resolution,data.resolution,TextureFormat.ARGB32);
		}
		public override void Init (SWDataNode _data, SWWindowMain _window)
		{
			styleID = 1;
			base.Init (_data, _window);
			data.outputType.Add (SWDataType._Color);
			data.outputType.Add (SWDataType._UV);
			data.outputType.Add (SWDataType._Alpha);
			data.inputType.Add (SWDataType._Color);
			data.inputType.Add (SWDataType._UV);
			data.inputType.Add (SWDataType._Alpha);
			if (texMask.IsNull)
				ResetMask ();
		}

		public override void Update ()  
		{
			base.Update ();
		}
		protected override void DrawHead ()
		{
			base.DrawHead ();
		}
		 
		protected override void DrawNodeWindow (int id)
		{
			if (data.useGray) {
				DrawWinCustom (id);
			} else {
				DrawWin (id);
			}
		}


		void DrawWin(int id)
		{
			base.DrawNodeWindow (id);
			DrawPreview(rectArea);

			if (GUI.Button (new Rect(rectBotButton.x,rectBotButton.y,rectBotButton.width- buttonHeight,rectBotButton.height),"Edit",SWEditorUI.MainSkin.button)) {
				SWWindowDrawMask.ShowEditor (this);
			}

			if (GUI.Button (new Rect(rectBotButton.x+rectBotButton.width - buttonHeight,rectBotButton.y,buttonHeight,rectBotButton.height),"+",SWEditorUI.MainSkin.button)) {
				data.useGray = !data.useGray;
			}

			DrawNodeWindowEnd ();
		}

		void DrawWinCustom(int id)
		{
			base.DrawNodeWindow (id);
			SelectTextureGray ();

			if (GUI.Button (rectBotButton,"Switch",SWEditorUI.MainSkin.button)) {
				data.useGray = !data.useGray;
				if (!data.useGray)
					ResetMask ();
			}
			DrawNodeWindowEnd ();
		}


		void DrawPreview(Rect rect)
		{
			//When pressing in the area,show masked area
			if (SWWindowMain.Instance.mousePressing && rect.Contains (Event.current.mousePosition)) {
				if (HasParentTexture ()) {
					List<SWNodeBase> parentNodes = GetParentNodes ();
					foreach (var item in parentNodes) {
						if (item.texture == null)
							continue;
						Material matBotMasked = new Material (SWEditorUI.GetShader ("RectTRSMask"));
						matBotMasked.SetVector ("t", -item.data.effectData.t_startMove);
						matBotMasked.SetFloat ("r", item.data.effectData.r_angle);
						matBotMasked.SetVector ("s", item.data.effectData.s_scale);
						matBotMasked.SetTexture ("_Mask", texMask.Texture);
						matBotMasked.SetVector ("rectShow", SWWindowMain.Instance.rectShow);

						Graphics.DrawTexture (rect, item.texture, matBotMasked);
					}
				} else {
					GUI.DrawTexture (rect, texMask.Texture);
				}
			} 
			//Show Green Mask usually
			else {
				GUI.DrawTexture (rect,texMask.Texture);
			}
		}
	
		bool HasParentTexture()
		{
			List<SWNodeBase> parentNodes = GetParentNodes ();
			foreach (var item in parentNodes) {
				if (item.texture != null)
					return true;
			}
			return false;
		}

		public override void DrawSelection ()
		{
			base.DrawSelection ();
		}


		#region save load
		public override void BeforeSave ()
		{
			base.BeforeSave ();
		}

		public override void AfterLoad ()
		{
			base.AfterLoad ();
			if (texture != null) {
				if (data.resolution != texture.width) {
					data.reso = (SWTexResolution)SWDataNode.resoList.IndexOf (texture.width);
					ResetMask ();
				}

				var colorful = texture.GetPixels (); 
				for (int i = 0; i < colorful.Length; i++) {
					if (data.maskChannel == SWChannel.r)
						colorful [i] = new Color (0, colorful [i].r, 0, colorful [i].r);
					if (data.maskChannel == SWChannel.g)
						colorful [i] = new Color (0, colorful [i].g, 0, colorful [i].g);
					if (data.maskChannel == SWChannel.b)
						colorful [i] = new Color (0, colorful [i].b, 0, colorful [i].b);
					if (data.maskChannel == SWChannel.a)
						colorful [i] = new Color (0, colorful [i].a, 0, colorful [i].a);
				}
				texMask.SetPixels (colorful);
				texMask.Apply ();
			}
		}
		#endregion

		/// <summary>
		/// Apply just before Update
		/// </summary>
		public void GrayApply()
		{
			if (data.useGray) {
				data.dirty = true;
				if (textureGray != null) {
					if (textureGray.width == textureGray.height && SWDataNode.resoList.Contains (textureGray.width)) 
						texMask = new SWTexture2DEx (textureGray);
					else
						texMask = SWTextureProcess.TextureResize (textureGray, SWGlobalSettings.TexDefaultSize, SWGlobalSettings.TexDefaultSize);
				} else {
					texMask = SWCommon.TextureCreate (SWGlobalSettings.TexDefaultSize,SWGlobalSettings.TexDefaultSize,TextureFormat.ARGB32);
				}
			}
		}
	}
}