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
	public class SWNodeCode :SWNodeBase {
		[SerializeField]
		int paramCount = 0;
		readonly float unitHeight=20;
		public SWDataCode dataCode
		{
			get{
				return SWWindowMain.Instance.dataCode.CodeOfName (data.code);
			}
		}
			
		public override void Init (SWDataNode _data, SWWindowMain _window)
		{
			childPortSingleConnection = true;
			styleID = 1;
			nodeWidth = 190;
			nodeHeight = NodeBigHeight+60;
			base.Init (_data, _window);
			data.outputType.Add (SWDataType._UV);
			data.inputType.Add (SWDataType._Color);
		}

		public void ResetCode()
		{
			DeleteAllParent ();
			DeleteAllChild ();
			ResetParamValue ();
		}

		void ResetParamValue()
		{
			for (int i = 0; i < dataCode.inputs.Count; i++) {
				var item = dataCode.inputs [i];
				if (!item.IsParam ())
					continue;

				var itemUse = data.GetCodeParamUse (item.name);
				itemUse.fv = item.v;
			}
		}

		public override void Update ()
		{
			base.Update ();
			if (dataCode != null) {
				data.childPortNumber = dataCode.RealInputs().Count;

				nodeHeight = paramCount *unitHeight + headerHeight+40+ Mathf.Clamp( data.childPortNumber,1,1000) * 20;
			} else {
				nodeHeight = paramCount *unitHeight + headerHeight+60;
			}
		}

		public override bool PortMatch (int port, SWNodeBase child, int childPort)
		{

			//cant connect self
			if (data.id == child.data.id)
				return false;
			//connected already
			if (data.children.Contains (child.data.id)) {
				int index = data.children.IndexOf (child.data.id);
				if(data.childrenPort[index] == port)
					return false;
			}
			//cant connect circle
			if (GetParentNodeAllAll ().ContainsKey (child.data.id))
				return false;


			var typeRequire = dataCode.RealInputs() [port].GetType();
			return typeRequire == child.data.outputType [0];

			//	return false;
		}
		protected override Rect CalRectVertical(float xPos,int i,int count = 1)
		{
			var rt = new Rect (
				xPos, 
				data.rect.y + 20 + headerHeight + paramCount * unitHeight+ (data.rect.height - headerHeight - paramCount * unitHeight-20) * 0.5f - portHeight * 0.5f - (float)(count - 1) / 2 * (portSpacing + portHeight) + (portSpacing + portHeight) * i, 
				portWidth, 
				portHeight
			);
			return rt;
		}

		protected override void Tooltip ()
		{
			base.Tooltip ();
			if (dataCode != null) {
				Rect rt = data.rect;
				SWWindowMain.Instance.Tooltip_Rec (dataCode.description, new Rect(
					rt.x,
					rt.y,
					rt.width,
					headerHeight
				));
			}
		}

		protected override void DrawHead ()
		{
			base.DrawHead ();
		}

		protected override void DrawNodeWindow (int id)
		{
			base.DrawNodeWindow (id);
			try
			{
				DrawSub();
			}
			catch(System.Exception e) {
			
			}
			DrawNodeWindowEnd ();
		}

		void DrawSub()
		{
			if (dataCode != null) {
				GUILayout.Space (gap + 2);
				GUILayout.BeginHorizontal ();
				GUILayout.Space (gap);
				GUILayout.Label (dataCode.name, SWEditorUI.Style_Get (SWCustomStyle.eCodeLeft));
				GUILayout.EndHorizontal ();

				paramCount = 0;

				for (int i = 0; i < dataCode.inputs.Count; i++) {
					var item = dataCode.inputs [i];
					if (!item.IsParam ())
						continue;

					var itemUse = data.GetCodeParamUse (item.name);
					GUILayout.BeginHorizontal ();
					paramCount++;
					if (item.type == CodeParamType.Float) {
						GUILayout.Label (item.name, SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width (SWGlobalSettings.LabelWidth));
						var f = EditorGUILayout.FloatField (itemUse.fv);
						if (f != itemUse.fv) {
							itemUse.fv = f;
							SWProperties.SetParam (data, itemUse);
						}
					}
					if (item.type == CodeParamType.Range) {
						GUILayout.Label (item.name, SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width (SWGlobalSettings.LabelWidth));
						var f = EditorGUILayout.Slider (itemUse.fv, item.min, item.max);
						if (f != itemUse.fv) {
							itemUse.fv = f;
							SWProperties.SetParam (data, itemUse);
						}
					}
					if (item.type == CodeParamType.CustomParam) {
						GUILayout.Label (item.name, SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width (SWGlobalSettings.LabelWidth));
						itemUse.v = EditorGUILayout.TextField (itemUse.v);
					}
					GUILayout.EndHorizontal ();
				}

				float width = 40;
				for (int i = 0; i < rectLefts.Count; i++) {
					Rect rt = rectLefts [i];
					rt = new Rect (5, rt.y - data.rect.y, width, rt.height);
					GUI.Label (rt, dataCode.RealInputs () [i].name, SWEditorUI.Style_Get (SWCustomStyle.eCodeLeft));
				}

				for (int i = 0; i < rectRights.Count; i++) {
					Rect rt = rectRights [i];
					rt = new Rect (data.rect.width - width - 5, rt.y - data.rect.y, width, rt.height);
					GUI.Label (rt, dataCode.output.name, SWEditorUI.Style_Get (SWCustomStyle.eCodeRight));
				}
			}
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