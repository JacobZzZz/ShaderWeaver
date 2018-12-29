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

	public partial class SWWindowMain:SWWindowLayoutV{
		#region UI
		public readonly float spacing = 5f;
		public readonly float bigSpacing = 50f;
		public float halfWidth
		{
			get{ 
				return rightUpRect.width * 0.5f-20;
			}
		}
		#endregion

		[SerializeField]
		protected bool titleEditing = false;
		[SerializeField]
		protected string nameEditing;
		[SerializeField]
		protected SWNodeCode nodeCode;




		void DrawRightUp_Code()
		{
			if (string.IsNullOrEmpty (nodeCode.data.code))
				NewCategoryReset ();

			GUILayout.Space (spacing);
			GUILayout.BeginHorizontal ();
			GUILayout.Space (spacing);
			DrawModuleTitle ("Code");
			Tooltip_Rec (SWTipsText.Code_Module);
			GUILayout.EndHorizontal ();
			GUILayout.Space (10);

			SelectCategory ();
			SelectCode ();

			if (nodeCode.dataCode == null) {
				return;
			}
			GUILayout.Space (10);
			ShowTitle ();
			CodeDescription ();

			if (nodeCode.dataCode == null) {
				return;
			}

			CodeParam ();
			CodeBody ();

			GUILayout.Label ("________________________",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight));
			GUI.color = Color.white;

			DrawModuleTitle ("Blend");
			OpBlend ();


			GUILayout.Label ("________________________",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight));
			GUI.color = Color.white;

			Factor_CustomParamCreation ();
		}

	
		void SelectCategory()
		{
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Output:", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width(SWGlobalSettings.LabelWidth));
			Tooltip_Rec (SWTipsText.Code_Category);
			var newType = SelectTypeOutput (nodeCode.data.codeType,SWGlobalSettings.FieldWidth);
			if (newType != nodeCode.data.codeType) {
				nodeCode.data.codeType = newType;
				NewCategoryReset ();
			}
			GUILayout.EndHorizontal ();
		}

		void NewCategoryReset()
		{
			var list = dataCode.CodeOfCate (nodeCode.data.codeType);
			if (list != null && list.Count > 0)
				nodeCode.data.code = list [0];
		}

		void  SelectCode()
		{
			List<string> strs = dataCode.CodeOfCate (nodeCode.data.codeType);
			int index = strs.IndexOf(nodeCode.data.code);

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Code:", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width(SWGlobalSettings.LabelWidth));
			Tooltip_Rec (SWTipsText.Code_Code);
			var temp = EditorGUILayout.Popup ("",index, strs.ToArray(),GUILayout.Width(SWGlobalSettings.FieldWidth));
			if (temp != index) {
				index = temp;
				Code_Select (strs [index]);
			}

			if (GUILayout.Button ("+",GUILayout.Width(20))) {
				SWUndo.Record (this);
				SWUndo.Record (nodeCode);
				Code_Add ();
			}
			GUILayout.EndHorizontal ();
		}

		public string NextCodeParam()
		{
			for(int i=1;i<=int.MaxValue;i++)
			{
				string str = "NewCode" + i;
				if (CodeNameUnique (str)) {
					return str;
				}
			} 
			return SWDataManager.NewGUID ();
		}
		protected void ShowTitle()
		{
			GUILayout.BeginHorizontal ();
			if (!titleEditing) {
				GUILayout.Space (spacing);
				GUILayout.Label (nodeCode.dataCode.name, SWEditorUI.Style_Get (SWCustomStyle.eTxtLight));

				if (Event.current.type == EventType.MouseUp &&
					GUILayoutUtility.GetLastRect ().Contains (Event.current.mousePosition)) {
					titleEditing = true;
					nameEditing = nodeCode.dataCode.name;
				}
			} else {
				GUILayout.Space (spacing+2);
				if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Return) {
					titleEditing = false;

					nameEditing = SWRegex.NameLegal(nameEditing);
					if (CodeNameUnique (nameEditing)) {
						SWUndo.Record (this);
						SWUndo.Record (nodeCode);
						Code_SetName (nameEditing);
					}
				}
				nameEditing = EditorGUILayout.TextField (nameEditing,SWEditorUI.Style_Get (SWCustomStyle.eTxtLight));
			}

			if (GUILayout.Button ("D", GUILayout.Width (20))) {
				SWUndo.Record (this);
				SWUndo.Record (nodeCode);
				Code_Duplicate (nodeCode.dataCode);
			}
			Tooltip_Rec (SWTipsText.Code_Duplicate);


			if (GUILayout.Button ("-", GUILayout.Width (20))) {
				SWUndo.Record (this);
				SWUndo.Record (nodeCode);
				Code_Remove ();
			}
			Tooltip_Rec (SWTipsText.Code_Delete);
			GUILayout.EndHorizontal ();
		}
		bool CodeNameUnique(string name)
		{
			if (string.IsNullOrEmpty (name))
				return false;
			foreach (var item in dataCode.codes) {
				if (item.name == name)
					return false;
			}
			return true;
		}


		void CodeParam()
		{
			GUILayout.BeginHorizontal ();

			GUILayout.BeginVertical ();
			CodeParamInput ();
			GUILayout.EndVertical ();
			GUILayout.Space (8);
			GUILayout.BeginVertical ();
			CodeParamOutput ();
			GUILayout.EndVertical ();

			GUILayout.EndHorizontal ();
		}

		void CodeParamInput()
		{
			GUILayout.Label("Inputs:", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width(SWGlobalSettings.LabelWidth));
			Tooltip_Rec (SWTipsText.Code_Input);

			int toDeleteIndex = -1;
			for (int i = 0; i < nodeCode.dataCode.inputs.Count; i++) {
				var item = nodeCode.dataCode.inputs [i];
				GUILayout.BeginHorizontal ();
				var newName = EditorGUILayout.TextField (item.name,GUILayout.Width(SWGlobalSettings.LabelWidth));
				if (newName != item.name) {
					SWUndo.Record (this);
					item.name = newName;
				}

				var newType = SelectTypeInput (item.type,50);
				if (newType != item.type) {
					SWUndo.Record (this);
					item.type = newType;
				}

				if (GUILayout.Button ("-", GUILayout.Width (20))) {
					toDeleteIndex = i;
				}
				GUILayout.EndHorizontal ();


				if (item.type == CodeParamType.Range) {
					GUILayout.BeginHorizontal ();
					//GUILayout.Space (8);
					item.v = EditorGUILayout.FloatField (item.v, GUILayout.Width (28));
					GUILayout.Label ("Min", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width (22));
					item.min = EditorGUILayout.FloatField (item.min, GUILayout.Width (28));
					GUILayout.Label ("Max", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width (27));
					item.max = EditorGUILayout.FloatField (item.max, GUILayout.Width (28));
					GUILayout.EndHorizontal ();

					item.v = Mathf.Clamp (item.v, item.min, item.max);
				} else if (item.type == CodeParamType.Float) {
					item.v = EditorGUILayout.FloatField (item.v, GUILayout.Width (28));
				}

				GUILayout.Space (2);
			}


			if (toDeleteIndex >= 0) {
				SWUndo.Record (this);
				nodeCode.dataCode.inputs.RemoveAt (toDeleteIndex);
			}

			GUILayout.BeginHorizontal ();
			GUILayout.Space (SWGlobalSettings.LabelWidth *2 + 2);
			if (GUILayout.Button ("+",GUILayout.Width(20))) {
				nodeCode.dataCode.AddParam ();
			}
			GUILayout.EndHorizontal ();
		}

		void CodeParamOutput()
		{
			GUILayout.Label("Output:", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width(SWGlobalSettings.LabelWidth));
			Tooltip_Rec (SWTipsText.Code_Output);

			GUILayout.BeginHorizontal ();
			var newName = EditorGUILayout.TextField (nodeCode.dataCode.output.name,GUILayout.Width(SWGlobalSettings.LabelWidth));
			if (newName != nodeCode.dataCode.output.name) {
				SWUndo.Record (this);
				nodeCode.dataCode.output.name = newName;
			}


			var tempType = SelectTypeOutput (nodeCode.dataCode.output.type,50);
			if (tempType != nodeCode.dataCode.output.type) {
				SWUndo.Record (this);
				nodeCode.dataCode.output.type = tempType;
				nodeCode.data.codeType = nodeCode.dataCode.output.type;
			}
			GUILayout.EndHorizontal ();
		}

		void CodeBody()
		{
			TextArea ("Content:", ref nodeCode.dataCode.code, 200,SWTipsText.Code_Content);
		}

		void CodeDescription()
		{
			if (nodeCode.dataCode.description == null)
				nodeCode.dataCode.description = "";
			TextArea ("Description:", ref nodeCode.dataCode.description, 30f,SWTipsText.Code_Description);

			//for tooltip text add spacing around '\n'
			List<int> indexes = new List<int> ();
			var cs = nodeCode.dataCode.description.ToCharArray();
			for (int i = 1; i < cs.Length; i++) {
				if (cs [i] == '\n' && cs [i - 1] != ' ')
					indexes.Add (i);
			}
			foreach (var index in indexes) {
				if(index+1<nodeCode.dataCode.description.Length)
					nodeCode.dataCode.description = nodeCode.dataCode.description.Insert (index+1, " ");
				if(index<nodeCode.dataCode.description.Length)
					nodeCode.dataCode.description = nodeCode.dataCode.description.Insert (index, " ");
			}
		}

		void TextArea(string title,ref string txt,float minHeight,string tooltop)
		{
			EditorStyles.textField.wordWrap = true;
			GUILayout.Label(title, SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
			Tooltip_Rec (tooltop);

			var newtxt = EditorGUILayout.TextArea (txt,
				GUILayout.Width(rightUpRect.width-al_scrollBarWidth),
				GUILayout.MinHeight(minHeight)
			);
			if (newtxt != txt) {
				SWUndo.Record (this);
				txt = newtxt;
			}
			EditorStyles.textField.wordWrap = false;
		}

		#region common
		protected CodeParamType SelectTypeInput(CodeParamType type,float width)
		{
			List<string> strs = new List<string>{ "Color", "UV", "Alpha","Float","Range","CustomParam" };
			return SelectType (type, width, strs);
		}

		protected CodeParamType SelectTypeOutput(CodeParamType type,float width)
		{
			List<string> strs = new List<string>{ "Color", "UV", "Alpha" };
			return SelectType (type, width, strs);
		}

		protected CodeParamType SelectType(CodeParamType type,float width,List<string> strs)
		{
			int index = strs.IndexOf(type.ToString());
			var temp = EditorGUILayout.Popup ("",index, strs.ToArray(), GUILayout.Width(width));
			if (temp != index) {
				index = temp;
				//performance impact
				//type = (CodeParamType)Enum.Parse(typeof(CodeParamType),strs[index]);
				type = (CodeParamType)index;
			}
			return type;
		}
		#endregion

		#region Blend
		protected void OpBlend()
		{
			float labelWith = 42;
			if ( nodeCode.data.GetCodeType() == SWDataType._Color) {
				EffectDataColor _data = nodeCode.data.effectDataColor;
				GUILayout.Space (2);
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Op", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width (labelWith));
				_data.op = (SWOutputOP)EditorGUILayout.EnumPopup (_data.op, GUILayout.Width (128 - labelWith));
				GUILayout.EndHorizontal ();
				GUILayout.Space (2);
				SWWindowMain.Instance.Factor_Pick (ref _data.param, PickParamType.node, "Factor", null, 128);

				GUILayout.Space (2);
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Depth", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width (labelWith));
				var dep = EditorGUILayout.IntField (nodeCode.data.depth, GUILayout.Width (128 - labelWith));
				if (dep != 0 && dep != nodeCode.data.depth) {
					SWUndo.Record (this);
					nodeCode.data.depth = dep;
				}
				GUILayout.EndHorizontal ();
			} else if (nodeCode.data.GetCodeType() == SWDataType._UV) {

				EffectDataUV dataUV = nodeCode.data.effectDataUV;
				GUILayout.Space (2);
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Op", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width (labelWith));
				dataUV.op = (SWUVop)EditorGUILayout.EnumPopup (dataUV.op, GUILayout.Width (128 - labelWith));
				GUILayout.EndHorizontal ();
				GUILayout.Space (2);
				SWWindowMain.Instance.Factor_Pick (ref dataUV.param, PickParamType.node, "Factor", null, 128);

			} else if (nodeCode.data.GetCodeType() == SWDataType._Alpha) {

			}
		}
		#endregion

		#region base
		void Code_Add(SWDataCode code = null)
		{
			if(code == null)
				code = new SWDataCode (NextCodeParam() ,nodeCode.data.codeType);
			dataCode.codes.Add (code);
			Code_Select (code.name);
		}
		void Code_Select(string name)
		{
			nodeCode.data.code = name;
			nodeCode.ResetCode ();
		}
		void Code_SetName(string name="")
		{
			nodeCode.dataCode.name = name;
			nodeCode.data.code = name;
		}


		void Code_Remove()
		{
			var codeName = nodeCode.data.code;
			List<string> strs = dataCode.CodeOfCate (nodeCode.data.codeType);
			int index = strs.IndexOf (codeName);
			string nextCode = "";
			if(index>0)
				nextCode = strs [index-1];
			else if(index+1 < strs.Count)
				nextCode = strs [index + 1];
			else
				nextCode = "";
			dataCode.codes.Remove (nodeCode.dataCode);
			nodeCode.data.code = nextCode;
			nodeCode.ResetCode ();
		}

		void Code_Duplicate(SWDataCode item)
		{
			string name = item.name;
			int index = -1;
			int number = 1;
			var cs = name.ToCharArray ();
			for (int i = cs.Length - 1; i >= 0; i--) {
				if (cs [i] >= '0' && cs [i] <= '9')
					index = i;
				else
					break;
			}
			if (index >= 0) {
				name = item.name.Substring (0, index);
				number = int.Parse (item.name.Substring (index)) + 1;
			} else {
				name = item.name;
				number = 1;
			}
			


			string newName = name + number;
			while (dataCode.CodeOfName (newName) != null) {
				number++;
				newName = name + number;
			}

			SWDataCode newItem = item.Clone (newName);
			Code_Add (newItem);
		}
		#endregion
	}
}