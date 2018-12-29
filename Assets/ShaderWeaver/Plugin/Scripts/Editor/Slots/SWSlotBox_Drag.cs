//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;

	/// <summary>
	/// Use by main window's nodes selection menu
	/// </summary>
	[System.Serializable]
	public class SWSlotBox_Drag:SWSlotBox{
		[SerializeField]
		public SWSlot moving;
		[SerializeField]
		protected float dragBoxSizeX = 50;
		[SerializeField]
		protected float dragBoxSizeY = 50;


		public override void Init (System.Action<SWSlot, Vector2> _delegat)
		{
			base.Init (_delegat);
			moving = null;
		}

		public override void OnGUI()
		{
			base.OnGUI ();
			Vector2 mp = Event.current.mousePosition;
			if (moving !=null && !string.IsNullOrEmpty (moving.name)) {
				Rect rect = new Rect (mp.x - dragBoxSizeX*0.5f, mp.y- dragBoxSizeY*0.5f, dragBoxSizeX, dragBoxSizeY);

				GUI.Box (rect, "", moving.StyleDrag);
				GUI.Label (rect, moving.name, SWEditorUI.Style_Get (SWCustomStyle.eTxtLight));

				if (Event.current.type == EventType.MouseUp) {
					if (regionTarget.Contains (mp)) {
						Vector2 posInRect = mp;
						delegat (moving,posInRect);
					}
					moving = null;
				}
				RepaintGetDirty ();
			}
		}  
		protected override void OnClick (SWSlot item, Vector2 mp)
		{
			base.OnClick (item, mp);  
			moving = item;
		}
	}
}