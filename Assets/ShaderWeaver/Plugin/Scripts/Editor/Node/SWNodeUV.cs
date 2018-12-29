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
	public class SWNodeUV :SWNodeEffector {
		public override void Init (SWDataNode _data, SWWindowMain _window)
		{
			styleID = 2;
			base.Init (_data, _window);
			data.outputType.Add (SWDataType._UV);
			data.inputType.Add (SWDataType._UV);
		}
		protected override void DrawNodeWindow (int id)
		{
			base.DrawNodeWindow (id);
			SelectTexture ();
			if (texture != null) {
				if (GUI.Button (rectBotButton,"Edit",SWEditorUI.MainSkin.button)) {
					SWWindowEffectUV.ShowEditor (this);
				}
			}
			DrawNodeWindowEnd ();
		}
	}
}