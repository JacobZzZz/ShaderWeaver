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
	public class SWNodeDummy :SWNodeEffector {
		public override void Init (SWDataNode _data, SWWindowMain _window)
		{
			styleID = 0;
			base.Init (_data, _window);
			data.outputType.Add (SWDataType._Color);
			data.inputType.Add (SWDataType._UV);
			data.inputType.Add (SWDataType._Alpha);

			//Color node can be child node of color node
			data.inputType.Add (SWDataType._Color);
		}

		public override void InitLayout()
		{
			base.InitLayout ();
			rectArea = new Rect (gap, headerHeight + gap, contentWidth, 
				nodeHeight - headerHeight - gap*2 - (rectBotButton.height+gap)*1.8f);
		}

		public override void Update ()
		{
			base.Update ();
			if (SWWindowMain.Instance.data.shaderType == SWShaderType.sprite || SWWindowMain.Instance.data.shaderType == SWShaderType.ui || SWWindowMain.Instance.data.shaderType == SWShaderType.ngui_ui2dSprite) {
				sprite = window.nRoot.sprite;
				if (sprite != null) {
					texture = sprite.texture;
				}
			} else {
				texture = window.nRoot.texture;
				sprite = null;
			}
		}

		public override string TextureShaderName ()
		{
			return "MainTex";
		}

		protected override void DrawNodeWindow (int id)
		{
			base.DrawNodeWindow (id);
			if (SWWindowMain.Instance.data.shaderType == SWShaderType.normal) {
				if(texture != null)
					GUI.DrawTexture (rectArea, texture);
			}
			else if (SWWindowMain.Instance.data.shaderType == SWShaderType.sprite || 
				SWWindowMain.Instance.data.shaderType == SWShaderType.ui || SWWindowMain.Instance.data.shaderType == SWShaderType.ngui_ui2dSprite) {
				if (sprite != null) {
					Rect rect = SWCommon.SpriteRect01 (sprite);
					Graphics.DrawTexture (rectArea, sprite.texture, rect, 0, 0, 0, 0);
				}
			}
			else if (SWWindowMain.Instance.data.shaderType == SWShaderType.uiFont) {
				GUI.Label (rectArea, "TEXT",SWEditorUI.Style_Get(SWCustomStyle.eTxtRoot));
			}


			Rect rL = new Rect (gap, NodeHeight - 1.84f*(gap + buttonHeight), contentWidth*0.5f, buttonHeight);
			Rect rR = new Rect (gap+contentWidth*0.5f, NodeHeight - 1.84f*(gap + buttonHeight), contentWidth*0.5f, 0.8f*buttonHeight);
			GUI.Label (rL,"Depth",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight));
			var dep = EditorGUI.IntField (rR,"", data.depth);
			if (dep != 0 && dep!= data.depth) {
				SWUndo.Record (this);
				data.depth = dep;
			}

			if (texture != null) {
				if (GUI.Button (rectBotButton,"Edit",SWEditorUI.MainSkin.button)) {
					SWWindowEffectDummy.ShowEditor (this);
				}
			}
			DrawNodeWindowEnd ();
		}
	}
}