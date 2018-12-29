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

	/// <summary>
	/// Use by remap node editing window
	/// </summary>
	[System.Serializable]
	public class SWSlotBox_Select:SWSlotBox{
		
		public override void Init (System.Action<SWSlot,Vector2> _delegat)
		{
			base.Init (_delegat);
			delegat (slots[selection],Vector2.zero);
		} 

		public override void OnGUI()
		{ 
			base.OnGUI ();

			if (EditorGUIUtility.editingTextField == false) {
				Vector2 mp = Event.current.mousePosition;
				if (Event.current.type == EventType.KeyUp) {
					for (int i = 0; i < slots.Count; i++) {
						var item = slots [i];
						if (item.hotkey != KeyCode.Escape && item.hotkey == Event.current.keyCode) {
							selection = i;
							delegat (item, mp);
						}
					}
				}  
			}
		}

		protected override void OnClick (SWSlot item, Vector2 mp)
		{
			base.OnClick (item, mp);
			SWUndo.Record (this);
			selection = slots.IndexOf(item);
			delegat (item,mp);
		}
	}
}