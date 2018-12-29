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
	public class SWNodeCoord :SWNodeBase {
		[SerializeField]
		SWEnumPopup ePopup_coordMode;

		public override void Init (SWDataNode _data, SWWindowMain _window)
		{
			nodeHeight = 80;
			styleID = 2;
			base.Init (_data, _window);
			data.outputType.Add (SWDataType._UV);
			data.childPortNumber = 0;

			ePopup_coordMode = new SWEnumPopup (typeof(SWCoordMode),(int)data.coordMode, false, null,
				delegate(int index) {
					data.coordMode = (SWCoordMode)index;
				}
			);
		}

		protected override void DrawHead ()
		{
			base.DrawHead ();
		}

		protected override void DrawNodeWindow (int id)
		{
			base.DrawNodeWindow (id);
			GUILayout.Space (gap+2);
			ePopup_coordMode.Show (nodeWidth - 40, "Src", 25);
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