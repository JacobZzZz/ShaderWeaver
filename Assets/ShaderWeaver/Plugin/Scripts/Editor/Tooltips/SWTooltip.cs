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
	using System.Reflection;
	using System;

	public class SWTooltip{
		static EditorWindow window;
		static string tip;
		static Rect rect;
		static Rect innerRect;
		static float timeStart;
		static string rawTip;
		public static void Start(EditorWindow _window)
		{
			window = _window;
			tip = "";
		}

		/// <summary>
		/// For GUILayout
		/// </summary>
		public static void Rec(SWWindowLayoutV win,string tip,float leftOff,float rightOff,float yoff =5)
		{
			var lastRect = GUILayoutUtility.GetLastRect ();
			Rec (win,tip, win.TopElementRect(lastRect.xMin+leftOff,lastRect.xMax+rightOff) ,yoff);
		}

		/// <summary>
		/// For GUILayout
		/// </summary>
		public static void Rec(SWWindowLayoutV win,string tip,float yoff =5)
		{
			Rect last = GUILayoutUtility.GetLastRect ();
			Rect newRect = SWCommon.GetRect (last.center, last.size);
			Rec(win,tip,newRect,yoff);
		}

		/// <summary>
		/// For GUI.
		/// </summary>
		public static void Rec(SWWindowLayoutV win,string _tip,Rect _rect,float yoff =5)
		{
			if (_rect.Contains (Event.current.mousePosition)) {
				win.RepaintGetDirty ();
				Tooltip_AssignRectAndTip (win,_rect, _tip,yoff);
			}
		}

		private static void Tooltip_AssignRectAndTip(SWWindowLayoutV win,Rect _rect,string _tip,float yoff =5)
		{
			if (rawTip == _tip) {
				if (Time.realtimeSinceStartup < timeStart+0.5f) {
					return;
				}
			} else {
				rawTip = _tip;
				timeStart = Time.realtimeSinceStartup;
				return;
			}
			tip = "";

			var style = SWEditorUI.Style_Get (SWCustomStyle.eTooltip);
			Font f = style.font;
			if (f == null)
				f = GUI.skin.font;
			f.RequestCharactersInTexture (_tip);
			float ratio = 1;
			if (style.fontSize != 0) {
				float f1 = style.fontSize;
				float f2 = f.fontSize;
				ratio = f1 / f2;
			}
			float width = 0;
			float height = 0;
			float maxWidth = 200;




			int lineCount = 1;
			Char space = ' ';
			CharacterInfo spaceInfo;
			f.GetCharacterInfo (space, out spaceInfo);
			float spaceWidth = (float)spaceInfo.advance * ratio;


			float lineWidth = 0;
			var strArray = _tip.Split (space);
			for (int i = 0; i < strArray.Length; i++) {
				bool lastN = false;
				if (strArray [i] == "\n") {
					lastN = true;
					i++;
					if (i >= strArray.Length)
						break;
				}


				float wordWidth=0;
				var charArray = strArray[i].ToCharArray ();
				for (int j = 0; j < charArray.Length; j++) {
					CharacterInfo info;
					Char _char = charArray [j];
					f.GetCharacterInfo (_char, out info);
					wordWidth += (float)info.advance * ratio;
				}

				if ((lineWidth + wordWidth + spaceWidth > maxWidth) || lastN == true) {
					tip += "\n" + strArray [i];
					lineWidth = wordWidth;
					lineCount++;
				} else {
					if (i == 0) {
						tip += strArray [i];
						lineWidth += wordWidth;
					} else {
						tip += " "+strArray [i];
						lineWidth += wordWidth + spaceWidth;
					}
				}
			}

			width = lineCount > 1 ? maxWidth : lineWidth;
			//height = lineCount *  (float)f.lineHeight * ratio;
			height = lineCount *  (float)f.lineHeight * ratio * (1f+lineCount*0.005f);

			float dence = 15;
			float x = _rect.center.x - width*0.5f - dence;
			float y = _rect.yMax + yoff;
			width = width + dence * 2;
			height = height + dence * 2;


			x = Mathf.Clamp (x, 0, window.position.width - width);
			y = Mathf.Clamp (y, 0,window.position.height - height);
			rect = new Rect (x, y, width, height);
		}

		public static void Show()
		{
			if (string.IsNullOrEmpty (tip))
				return;
			GUI.Label (rect,tip,SWEditorUI.Style_Get(SWCustomStyle.eTooltip));
		}
	}
}
