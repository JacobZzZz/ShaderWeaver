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
	/// Any custom button used in Shader Weaver
	/// </summary>
	[System.Serializable]
	public class SWSlot
	{
		[SerializeField]
		public int id;
		[SerializeField]
		public string name;
		[SerializeField]
		public string eTooltip;  
		[SerializeField]
		public Texture2D texture;
		[SerializeField]
		public GUIContent content;
		[SerializeField]
		public KeyCode hotkey;

		/// <summary>
		/// Different Colors
		/// </summary>
		[SerializeField]
		public int styleID;

		public SWSlot(string _name,string _tooltip,Texture2D _texture =null,KeyCode _hotkey = KeyCode.Escape,int _styleID = 0)
		{
			styleID = _styleID;
			hotkey = _hotkey;
			name = _name;
			eTooltip = _tooltip;
			texture = _texture;

			if (texture != null)
				content = new GUIContent (texture);
			else
				content = new GUIContent (name);

			if (_hotkey != KeyCode.Escape && _hotkey != KeyCode.None)
				eTooltip += string.Format (" ({0})", _hotkey.ToString ());
		}

		public GUIStyle Style
		{
			get{
				return SWEditorUI.MainSkin.customStyles [1 + styleID];
			}
		}
		public GUIStyle StyleDown
		{
			get{
				return SWEditorUI.MainSkin.customStyles [5 + styleID];
			}
		}
		public GUIStyle StyleDrag
		{
			get{
				return SWEditorUI.MainSkin.customStyles [9 + styleID];
			}
		}
	}
}