//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using UnityEditor;

	[System.Serializable]
	public class SWNodeRoot :SWNodeBase {
		[SerializeField]
		public Texture2D texForNormal;
		[SerializeField]
		public Texture2D texForSprite;
		public override void Init (SWDataNode _data, SWWindowMain _window)
		{
			styleID = 0;
			base.Init (_data, _window);
			data.outputType.Add(SWDataType._Color);
			data.inputType.Add (SWDataType._Color);
			data.inputType.Add (SWDataType._UV);
			data.inputType.Add (SWDataType._Alpha);
			InitForSp ();

			//force set to no loop,in case of old data
			//new project should be OK, since SWDataNode' constructor has loop logic
			data.effectData.useLoop = false;
		}

		public override string TextureShaderName ()
		{
			return "MainTex";
		}
		protected override void DrawHead ()
		{
			base.DrawHead ();
		}
		protected override void DrawNodeWindow (int id)
		{
			base.DrawNodeWindow (id);
			if (SWWindowMain.Instance.data.shaderType == SWShaderType.normal) {
				DrawNormal ();
			}
			else if (SWWindowMain.Instance.data.shaderType == SWShaderType.sprite) {
				DrawSprite (); 
			}
			else if (SWWindowMain.Instance.data.shaderType == SWShaderType.ui) {
				DrawUIImage ();
			}
			else if (SWWindowMain.Instance.data.shaderType == SWShaderType.uiFont) {
				DrawUIText (); 
			}
			else if (SWWindowMain.Instance.data.shaderType == SWShaderType.ngui_ui2dSprite) {
				DrawUIImage ();
			}

			float labelWith = 38;
			GUILayout.Space (nodeHeight - 42);

		
			EffectDataColor _data = data.effectDataColor;
			string name = _data.hdr ? "[HDR]" : "Color";
			GUILayout.BeginHorizontal ();
			GUILayout.Label (name, SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width(labelWith));
			SWCommon.HRDColor_Switch (window, ref data.effectDataColor.hdr);
			SWCommon.HRDColor_Field (_data.color, false, _data.hdr, nodeWidth - 15 - labelWith, delegate(Color c) {
				_data.color = c;
				SWProperties.SetColor (data, _data.color);
			}
			);
			GUILayout.EndHorizontal ();


			DrawNodeWindowEnd ();
		}

		void DrawNormal()
		{
			SelectTexture ();
			texForNormal = texture;
		}
		void DrawSprite()
		{
			DrawSpBase ();
		}
		void DrawUIImage()
		{
			DrawSpBase ();
		}
		void DrawSpBase()
		{
			var sp = (Sprite)EditorGUI.ObjectField (rectArea, sprite, typeof(Sprite), true);
			if (sprite != sp) {
				//step 1 :record for undo
				Object[] objs = { this, SWWindowMain.Instance };
				SWUndo.Record (objs);

				sprite = sp;
				InitForSp ();
			}
		}

		void InitForSp()
		{
			if (sprite != null) {
				InitForSpSub ();
				Texture2D t2d = SWCommon.SpriteGetTexture2D (sprite);
				float x = sprite.rect.x / (float)t2d.width;
				float y = sprite.rect.y / (float)t2d.height;
				float width = sprite.rect.width / t2d.width;
				float height = sprite.rect.height / t2d.height;
				SWWindowMain.Instance.data.spriteRect = new Rect (x, y, width, height);
			} else {
				SWWindowMain.Instance.data.spriteRect = new Rect (0,0,1,1);
			}
		}

		void InitForSpSub()
		{
			if (sprite != null) {
				SWWindowMain.Instance.data.pixelPerUnit = SWCommon.TextureReImportSprite (sprite);
				texture = SWCommon.Sprite2Texture2D (sprite);
				texForSprite = texture;
			}
		}
		void DrawUIText()
		{
			GUI.Label (rectArea, "TEXT",SWEditorUI.Style_Get(SWCustomStyle.eTxtRoot));
		}


		public override void DrawSelection ()
		{
			base.DrawSelection ();
		}

		public override void BeforeSave ()
		{
			base.BeforeSave ();
		}

		public override void AfterLoad ()
		{
			base.AfterLoad ();
			texForNormal = texture;
			if (SWWindowMain.Instance.data.shaderType == SWShaderType.normal) {
				
			} else if (SWWindowMain.Instance.data.shaderType == SWShaderType.uiFont) {
				texture = null;
			} else {
				InitForSp ();
			}
		}
	}
}