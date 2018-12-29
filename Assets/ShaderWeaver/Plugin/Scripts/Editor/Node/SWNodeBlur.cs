//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using UnityEditor;
	using System;

	[System.Serializable]
	public class SWNodeBlur :SWNodeBase {

		public override void Init (SWDataNode _data, SWWindowMain _window)
		{
			styleID = 2;
			nodeWidth = NodeBigWidth;
			nodeHeight = NodeBigHeight;
			base.Init (_data, _window);
			data.outputType.Add (SWDataType._UV);
			//data.inputType.Add (SWDataType._Color);
		}

		protected override void DrawHead ()
		{
			base.DrawHead ();
		}

		protected override void DrawNodeWindow (int id)
		{
			base.DrawNodeWindow (id);
			GUILayout.Space (gap+2);
			GUILayout.BeginHorizontal ();
			GUILayout.Space (gap+2);


			GUILayout.BeginVertical (GUILayout.Width(42f));

			GUILayout.Label ("X", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
			GUILayout.BeginHorizontal ();
			data.blurX = EditorGUILayout.Slider (data.blurX,0,1f,GUILayout.Width(32));
			GUILayout.Label ("x", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
			GUILayout.EndHorizontal ();

			GUILayout.Space (10);

			GUILayout.Label ("Y", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
			GUILayout.BeginHorizontal ();
			data.blurY = EditorGUILayout.Slider (data.blurY,0,1f,GUILayout.Width(32));
			GUILayout.Label ("x", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
			GUILayout.EndHorizontal ();

			GUILayout.EndVertical ();



			GUILayout.BeginVertical (GUILayout.Width(110));
			SWWindowMain.Instance.Factor_Pick (ref data.blurXParam, PickParamType.mul, "", this,86);
			GUILayout.Space (10);
			SWWindowMain.Instance.Factor_Pick (ref data.blurYParam, PickParamType.mul, "", this,86);
			GUILayout.EndVertical ();

			GUILayout.EndHorizontal ();
			DrawNodeWindowEnd ();
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
		}
		#endregion
	}
}