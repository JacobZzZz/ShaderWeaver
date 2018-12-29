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
	/// Base class.List of custom buttons. (use by top menu,left menu)
	/// </summary>
	[System.Serializable]
	public class SWSlotBox:ScriptableObject{
		[SerializeField]
		public Rect regionTarget;//for drag only
		[SerializeField]
		protected List<SWSlot> slots;
		[SerializeField]
		protected Vector2 slotSize;
		[SerializeField]
		public System.Action<SWSlot,Vector2> delegat;
		[SerializeField]
		protected Rect rectBase;
		[SerializeField]
		public int selection = -1;
		public SWSlot selectSlot
		{
			get{
				return slots[selection];
			}
		}
		[SerializeField]
		public float margin = 6;
		[SerializeField]
		public SWWindowLayoutV window;
		[SerializeField]
		protected Vector2 scollViewPos;
		[SerializeField]
		public bool repaintDirty = true;
		protected void RepaintGetDirty()
		{
			repaintDirty = true;
		}

		public virtual void InitSlot(SWWindowLayoutV _win,Rect _rectBase, List<SWSlot> _slots,System.Action<SWSlot,Vector2> _delegat,Vector2 _slotSize,int _selection = 0)
		{
			window = _win;
			for (int i = 0; i < _slots.Count; i++)
				_slots [i].id = i;
			rectBase = _rectBase;
			slotSize = _slotSize;
			slots = _slots;
			selection = _selection;
			Init (_delegat);
		}
		public virtual void Init(System.Action<SWSlot,Vector2> _delegat)
		{
			delegat = _delegat;
		}


		public  virtual void OnGUI()
		{
			float height = (slotSize.y + margin) * slots.Count;
			scollViewPos = GUI.BeginScrollView (
				new Rect (rectBase.x, rectBase.y, slotSize.x+window.al_scrollBarWidth, window.position.height-rectBase.y-window.al_scrollBarWidth), scollViewPos, new Rect (0, 0, slotSize.x-3, height));
			Vector2 mp = Event.current.mousePosition;

			for (int i = 0; i < slots.Count; i++) {
				var item = slots [i];
				if (selection == i)
					GUI.color = new Color (0.8f, 0.8f, 0.8f, 1);
				else
					GUI.color = Color.white;

				float x = 0;
				float y = 10;
				if (rectBase.height > rectBase.width)
					y += (slotSize.y+margin)*i;
				else
					x += slotSize.x*i;
				Rect rect = new Rect (x + margin, y, slotSize.x - margin*2, slotSize.y);


				if (this is SWSlotBox_Drag) {
					if (window.IsOperatingWindow ()) {
						GUI.Box (rect, "", item.StyleDrag);
					}
					else
						GUI.Box (rect, "",item.Style);
				}
				else {
					if (selection == i)
						GUI.Box (rect, "", item.StyleDown);
					else
						GUI.Box (rect, "", item.Style);
				}


				GUI.color = Color.white;
				GUI.Label (rect, item.content, SWEditorUI.Style_Get (SWCustomStyle.eTxtLight));
				window.Tooltip_Rec (item.eTooltip,rect,-8);
				if (window.IsOperatingWindow() && Event.current.type == EventType.MouseDown) {
					if (rect.Contains (mp)) {
						OnClick (item,mp);
					}
				}

				if (rect.Contains (mp)) {
					RepaintGetDirty();
				}
			}
			GUI.color = Color.white;
			GUI.EndScrollView ();
		}

		protected virtual void OnClick(SWSlot item, Vector2 mp)
		{
			
		}

		public virtual void OnEnable()
		{
			hideFlags = HideFlags.DontSave;
		}
	}
}