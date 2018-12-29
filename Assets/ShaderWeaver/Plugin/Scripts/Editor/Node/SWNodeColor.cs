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
	public class SWNodeColor :SWNodeBase {
		[SerializeField]
		SWEnumPopup ePopup_op;

		public override void Init (SWDataNode _data, SWWindowMain _window)
		{
			styleID = 0;
			nodeWidth = 144;
			nodeHeight = NodeBigHeight;
			base.Init (_data, _window);
			data.outputType.Add (SWDataType._Color);
			data.inputType.Add (SWDataType._Alpha);

			ePopup_op = new SWEnumPopup(typeof(SWOutputOP),(int)data.effectDataColor.op,false,null,
				delegate(int index){
					data.effectDataColor.op = (SWOutputOP)index;
				});
		}

		protected override void DrawNodeWindow (int id)
		{
			base.DrawNodeWindow (id);
			GUILayout.Space (7);
			GUILayout.BeginHorizontal ();
			GUILayout.Space (7);
			GUILayout.BeginVertical ();

			float labelWith = 42;

			EffectDataColor _data = data.effectDataColor;
			string name = _data.hdr ? "[HDR]" : "Color";
			GUILayout.BeginHorizontal ();
			GUILayout.Label (name, SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width(labelWith));
			SWCommon.HRDColor_Switch (window, ref data.effectDataColor.hdr);
			SWCommon.HRDColor_Field (_data.color, true, _data.hdr, 128 - labelWith, delegate(Color c) {
				_data.color = c;
				SWProperties.SetColor (data, _data.color);
			}
			);
			GUILayout.EndHorizontal ();
		
			GUILayout.Space (2);
			ePopup_op.Show (128 - labelWith, "Op", labelWith);
			GUILayout.Space (2);
			SWWindowMain.Instance.Factor_Pick (ref _data.param,PickParamType.node,"Factor",null,128);

			GUILayout.Space (2);
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Depth",  SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width(labelWith));
			var dep = EditorGUILayout.IntField (data.depth,GUILayout.Width(128-labelWith));
			if (dep != 0 && dep!= data.depth) {
				SWUndo.Record (this);
				data.depth = dep;
			}
			GUILayout.EndHorizontal ();

			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();
			DrawNodeWindowEnd ();
		}
	}
}