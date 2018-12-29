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
	public class SWNodeAlpha :SWNodeEffector {
		public override void Init (SWDataNode _data, SWWindowMain _window)
		{
			styleID = 3;
			base.Init (_data, _window);
			data.outputType.Add (SWDataType._Alpha);
			data.inputType.Add (SWDataType._UV);
		}
		protected override void DrawNodeWindow (int id)
		{
			base.DrawNodeWindow (id);
			SelectTexture ();
			if (texture != null) {
				if (GUI.Button (rectBotButton,"Edit",SWEditorUI.MainSkin.button)) {
					SWWindowEffectAlpha.ShowEditor (this);
				}
			}
			DrawNodeWindowEnd ();
		}
	}
}