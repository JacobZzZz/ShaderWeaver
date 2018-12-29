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
	public class SWWindowMixerEditor : EditorWindow {
		static readonly float Gap = 20;
		[SerializeField]
		protected SWGradientFrame opItem;
		[SerializeField]
		protected SWGradient gradient;
		[SerializeField]
		protected Texture icon;
		[SerializeField]
		protected Texture iconSelect;
		[SerializeField]
		protected SWGradientMode mode;
		[SerializeField]
		protected Rect baseRect;
		[SerializeField]
		protected Rect texRect;
		[SerializeField]
		protected Rect timeRect;
		[SerializeField]
		protected Rect valueRect;
		[SerializeField]
		protected bool repaintDirty = true;
		protected void RepaintGetDirty()
		{
			repaintDirty = true;
			gradient.UpdateTex ();
		}


		public static void Show(SWGradient _gradient)
		{
			var edit = EditorWindow.GetWindow<SWWindowMixerEditor> (true,"Mixer Editor");
			edit.minSize = new Vector2 (400, 160);
			edit.Init (_gradient);
		}

		public void Init(SWGradient _gradient)
		{
			gradient = _gradient;

			icon = EditorGUIUtility.Load ("gradient_down_swatch") as Texture;
			iconSelect = EditorGUIUtility.Load ("gradient_down_swatch_overlay_on") as Texture;

			if(gradient.frames.Count>0)
				opItem = gradient.frames [0]; 
		}
		void Update()
		{
			if (repaintDirty) {
				Repaint ();
				repaintDirty = false;
			}

			if (SWWindowMain.Instance == null)
				Close ();
		}
		void OnGUI()
		{
			baseRect = new Rect (Gap, Gap, position.width - Gap*2, 20);
			texRect = new Rect (Gap, baseRect.yMax, position.width - Gap*2, 50);
			timeRect = new Rect (Gap, texRect.yMax+Gap, position.width - Gap*2, 20);
			valueRect = new Rect (Gap, timeRect.yMax, position.width - Gap*2, 20);

			if (SWCommon.GetMouseDown (1)) {
				if (!baseRect.Contains (Event.current.mousePosition)) {
					mode = SWGradientMode.no;
					opItem = null;
					RepaintGetDirty();
				}
			}

			if (SWCommon.GetMouseDown (0)) {
				bool doit = false;
				if (mode == SWGradientMode.no) {
					for (int i = 0; i < gradient.frames.Count; i++) {
						var item = gradient.frames [i];
						Rect rect = CalRect (item);
						if (rect.Contains (Event.current.mousePosition)) {
							doit = true;
							mode = SWGradientMode.select;
							opItem = item;
							RepaintGetDirty();
						}
					}
				}
				else if (mode == SWGradientMode.select) {
					for (int i = 0; i < gradient.frames.Count; i++) {
						var item = gradient.frames [i];
						Rect rect = CalRect (item);
						if (rect.Contains (Event.current.mousePosition)) {
							doit = true;
							opItem = item;
							if (opItem == item) {
								mode = SWGradientMode.move;
							}
							RepaintGetDirty();
						}
					}
				}
				else if (mode == SWGradientMode.move) {
					doit = true;
					opItem.time = TimeOnPos ();
					RepaintGetDirty();
				}

				if (!doit) {
					if (baseRect.Contains (Event.current.mousePosition)) {
						SWUndo.Record (this);
						var item = new SWGradientFrame ();
						item.time = TimeOnPos ();
						gradient.frames.Add (item);
						mode = SWGradientMode.select;
						opItem = item;
						gradient.Sort ();
						RepaintGetDirty();
					}
				}
			} 
			if (SWCommon.GetMouse (0)) {
				if (mode == SWGradientMode.no) {
				}
				else if (mode == SWGradientMode.select) {
				}
				else if (mode == SWGradientMode.move) {
					SWUndo.Record (this);
					opItem.time = TimeOnPos ();
					RepaintGetDirty();
				}
			}
			if (SWCommon.GetMouseUp (0)) {
				if (mode == SWGradientMode.no) {
				}
				else if (mode == SWGradientMode.select) {
				}
				else if (mode == SWGradientMode.move) {
					mode = SWGradientMode.select;
					RepaintGetDirty();
				}
			}
			if (Event.current.type == EventType.KeyDown) {
				if (Event.current.keyCode == KeyCode.Delete || Event.current.keyCode == KeyCode.Backspace) {
					if (mode == SWGradientMode.select && opItem != null) {
						SWUndo.Record (this);
						gradient.frames.Remove (opItem);
						RepaintGetDirty();
					}
				}
			}


			gradient.Sort ();
			GUI.color = Color.black;
			GUI.DrawTexture (baseRect, SWEditorTools.blankTexture);
			GUI.color = Color.white;

			SWEditorTools.DrawTiledTexture (texRect, SWEditorTools.backdropTexture);
			GUI.DrawTexture (texRect, gradient.Tex);
			for (int i = 0; i < gradient.frames.Count; i++) {
				var item = gradient.frames [i];
				Rect rect = CalRect (item);
				GUI.DrawTexture (rect, icon);
				if (opItem == item)
					GUI.DrawTexture (rect, iconSelect);
			}

			if (opItem != null) {
				var t = EditorGUI.Slider(timeRect,"Position:",opItem.time, 0, 1);
				if (t != opItem.time) {
					SWUndo.Record (this);
					opItem.time = t;
					gradient.UpdateTex ();
				}

				var v = EditorGUI.Slider(valueRect,"Value:",opItem.value, 0, 1);
				if (v != opItem.value) {
					SWUndo.Record (this);
					opItem.value = v;
					gradient.UpdateTex ();
				}
			}
		}

		float TimeOnPos()
		{
			var v = (Event.current.mousePosition.x - baseRect.xMin)/ baseRect.width;
			v = Mathf.Clamp01 (v);
			return v;
		}

		Rect CalRect(SWGradientFrame g)
		{
			float width = 10;
			float height = 20;
			return new Rect (baseRect.xMin + g.time * baseRect.width - width*0.5f, baseRect.y, width, height);
		}
	}

}