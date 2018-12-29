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

	[Serializable]
	public class SWEnumPopup
	{
		private string[] strs;
		private List<int> ids;
		private int index;
		private System.Action<int> changedAct;
		private bool customStyle;
		private GUIStyle style;

		public SWEnumPopup(Type e,int _index,bool massOrder = true,GUIStyle _style=null,System.Action<int> _changedAct = null)
		{
			
			index = 0;
			strs = Enum.GetNames(e);

			if (massOrder) {
				ids = new List<int> ();
				var ary = Enum.GetValues(e);
				for (int i = 0; i < ary.Length; i++) {
					ids.Add ((int)ary.GetValue(i));
					if (ids [i] == _index)
						index = i;
				}
			}
			else
				index = _index;

			changedAct = _changedAct;

			if (_style != null) {
				customStyle = true;
				style = _style;
			}
		}


		public SWEnumPopup(string[] _strs,int _index,GUIStyle _style=null,System.Action<int> _changedAct = null)
		{
			index = _index;
			strs = _strs;
			changedAct = _changedAct;
			if (_style != null) {
				customStyle = true;
				style = _style;
			}
		}

		public void Show(float width,string title="",float titleWidth=0,bool wrapInHorizontal = true)
		{
			if(wrapInHorizontal)
				GUILayout.BeginHorizontal ();
			if (!string.IsNullOrEmpty(title)) {
				GUILayout.Label (title, SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width (titleWidth));
			}
			var newIndex = 0;
			if(customStyle == false)
				newIndex = EditorGUILayout.Popup (index, strs,GUILayout.Width(width));
			else
				newIndex = EditorGUILayout.Popup (index, strs,style,GUILayout.Width(width));
			NewIndex (newIndex);
			if(wrapInHorizontal)
				GUILayout.EndHorizontal ();
		}
		public void Show(Rect rect)
		{
			var newIndex = 0;
			if(customStyle == false)
			 	newIndex = EditorGUI.Popup (rect, index, strs);
			else
				newIndex = EditorGUI.Popup (rect, index, strs,style);
			NewIndex (newIndex);
		}
			
		void NewIndex(int newIndex)
		{
			if (newIndex != index) {
				index = newIndex;
				if (changedAct != null) {
					if(ids==null)
						changedAct (index);
					else
						changedAct (ids[index]);
				}
			}
		}
	}
}