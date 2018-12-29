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

	public enum PickParamType
	{
		top,
		right,
		node,
		mul
	}

	/// <summary>
	/// partial class of SWWindowLayoutV
	/// </summary>
	public partial class SWWindowLayoutV  {
		protected string paramName = "";
		protected string paramNameLastDeleted = "";

		protected void Factor_CustomParamCreation()
		{
			paramNameLastDeleted = "";
			DrawModuleTitle ("Custom Parameters");
			Rect lastRect = GUILayoutUtility.GetLastRect ();
			Rect rect = new Rect (lastRect.x, lastRect.y, lastRect.width+SWGlobalSettings.FieldWidth, lastRect.height+rightUpUnitHeight);
			Tooltip_Rec (SWTipsText.Right_CustomParam,rect);

			paramName = EditorGUILayout.TextField (paramName,GUILayout.Width(100));


			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("New Float",GUILayout.Width(80))) {
				paramName = SWRegex.NameLegal(paramName);
				if (ParamNameUnique (paramName)) {
					SWParam pa = new SWParam ();
					pa.type = SWParamType.FLOAT;
					pa.name = paramName;
					SWWindowMain.Instance.data.paramList.Add (pa);
				}
				paramName = "";
			}
			if (GUILayout.Button ("New Range",GUILayout.Width(80))) {
				paramName = SWRegex.NameLegal(paramName);
				if (ParamNameUnique (paramName)) {
					SWParam pa = new SWParam ();
					pa.type = SWParamType.RANGE;
					pa.name = paramName;
					SWWindowMain.Instance.data.paramList.Add (pa);
				}
				paramName = "";
			}
			GUILayout.EndHorizontal ();


			int toDeleteId = -1;
			for(int i=0;i<SWWindowMain.Instance.data.paramList.Count;i++)
			{
				//GUILayout.Space (SWGlobalSettings.UIGap);
				var item = SWWindowMain.Instance.data.paramList[i];
				GUILayout.BeginHorizontal ();
				float nameWidth = 64f;
				GUILayout.Label (item.name,SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight), GUILayout.Width (nameWidth));
				if (item.type == SWParamType.RANGE) {


					var f = EditorGUILayout.FloatField (item.defaultValue,GUILayout.Width (30));
					if (f != item.defaultValue) {
						item.defaultValue = f;
						SWProperties.SetFloat (item.name, item.defaultValue);
					}	

					f = GUILayout.HorizontalSlider (item.defaultValue,item.min, item.max,GUILayout.Width (40));
					if (f != item.defaultValue) {
						item.defaultValue = f;
						SWProperties.SetFloat (item.name, item.defaultValue);
					}


					GUILayout.Label ("Min",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight), GUILayout.Width (22));
					item.min = EditorGUILayout.FloatField ("", item.min, GUILayout.Width (25));
					GUILayout.Label ("Max",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight), GUILayout.Width (26));
					item.max = EditorGUILayout.FloatField ("", item.max, GUILayout.Width (25));


				
				}
				if (item.type == SWParamType.FLOAT) {
					var f = EditorGUILayout.FloatField (item.defaultValue,GUILayout.Width (rightupWidth - nameWidth - SWGlobalSettings.UIGap*2 - 18 - 2));
					if (f != item.defaultValue) {
						item.defaultValue = f;
						SWProperties.SetFloat (item.name, item.defaultValue);
					}	
				}

				lastRect = GUILayoutUtility.GetLastRect();

				if(GUI.Button(new Rect(position.width-33-SWGlobalSettings.UIGap,lastRect.y,18,17),"X"))
				{
					toDeleteId = i;
				}
				GUILayout.EndHorizontal ();
			}
			if (toDeleteId >= 0) {
				paramNameLastDeleted = SWWindowMain.Instance.data.paramList[toDeleteId].name;
				SWWindowMain.Instance.data.paramList.RemoveAt (toDeleteId);
			}
		}


		/// <summary>
		/// For factor field, pick a param or use custom field
		/// 
		/// if it is a defined param(in data.paramList or is _Time.y),such as  _Time.y , it just keep its way
		/// if it is a custom field , it should be something like (_Time.y*2), in Factor_Pick method, remove () first and let user do editing, then add ()back 
		/// </summary>
		public void Factor_Pick(ref string param,PickParamType type,string label = "Factor",Object obj = null,float width = -1)
		{
			//step 0: set UI size 
			if(type != PickParamType.top)
				GUILayout.BeginHorizontal ();
			float labelWidth = SWGlobalSettings.LabelWidth;
			float popWidth = SWGlobalSettings.FieldWidth;
			float fieldWidth = SWGlobalSettings.LabelWidth;
			if (type == PickParamType.mul) 
			{
				labelWidth = 0;
				popWidth = width;
				fieldWidth = width;
			}
			else if (type == PickParamType.node)
			{
				labelWidth = 42;
				popWidth = width - 42;
				fieldWidth = width;
			}
			else if (type == PickParamType.right)
			{
				labelWidth = SWGlobalSettings.LabelWidthLong;
				popWidth = SWGlobalSettings.FieldWidth;
				fieldWidth = SWGlobalSettings.LabelWidthLong + SWGlobalSettings.FieldWidth+4;
			}
			else if (type == PickParamType.top)
			{
				labelWidth = 40;
				popWidth = 90;
				fieldWidth = SWGlobalSettings.FieldWidth;
			}



			//step 1: set the defined param list
			if (width < 0)
				width = rightupWidth;
			List<string> strs = new List<string> ();
			strs.Add ("_Time.y");
			foreach (var item in  SWWindowMain.Instance.data.paramList) {
				strs.Add (item.name);
			}
			strs.Add ("Custom Field");
			string[] selectOpts = strs.ToArray ();

			//step 2: calculate paramIndex 
			int paramIndex = strs.IndexOf (param);
			if (!string.IsNullOrEmpty(paramNameLastDeleted) && param == paramNameLastDeleted) {
				paramIndex = 0;
				param = selectOpts [0];
			}
			else if (paramIndex < 0 || paramIndex > strs.Count - 1) {
				paramIndex = strs.Count - 1;
			}
				
			//step 3: Popup
			GUILayout.Label (label,SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight), GUILayout.Width(labelWidth));
			int _index = EditorGUILayout.Popup (paramIndex, selectOpts,GUILayout.Width(popWidth));
			if(type != PickParamType.top)
				GUILayout.EndHorizontal ();



			if (_index != paramIndex) {
				//step 4-1:user choose a new item in popup
				if(obj!=null)
					SWUndo.Record (obj);
				if (_index == selectOpts.Length - 1) {
					// new custom field default is (1)
					param = "(1)";
					string tmp = param.Substring (1,param.Length-2);
					tmp = EditorGUILayout.TextField (tmp, GUILayout.Width (fieldWidth));
					param = string.Format ("({0})",tmp);
				} else {
					// assgin chosen define param to param
					param = strs [_index];
					if (type == PickParamType.mul)
						GUILayout.Label (param, SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
					else
						GUILayout.Space (18);
				}
			} else {
				//step 4-2:user did not choose a new item

				if (_index == selectOpts.Length - 1) {
					//user is editing a custom field
					if(param.Length<=2)
						param = "(1)";
					string tmp = param.Substring (1,param.Length-2);
					tmp = EditorGUILayout.TextField (tmp, GUILayout.Width (fieldWidth));
					param = string.Format ("({0})",tmp);
				} else {
					//keep chosen define param
					param = strs [_index];
					if (type == PickParamType.mul)
						GUILayout.Label (param, SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
					else
						GUILayout.Space (18);
				}
			}
		}

		protected bool ParamNameUnique(string name)
		{
			if (string.IsNullOrEmpty (name))
				return false;
			foreach (var item in SWWindowMain.Instance.data.paramList) {
				if (item.name == name)
					return false;
			}
			return true;
		}

		protected void DrawModuleTitle(string title)
		{
			GUILayout.Label(string.Format("[{0}]",title),SWEditorUI.Style_Get(SWCustomStyle.eTxtLight));
		}
	}
}