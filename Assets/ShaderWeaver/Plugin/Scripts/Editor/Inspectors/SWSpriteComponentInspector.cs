//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
using System;
using UnityEditorInternal;
using UnityEngine;
using System.Reflection;
using System.Globalization;
using ShaderWeaver;

namespace UnityEditor {
	/// <summary>
	/// Base class of all SW components' inspector
	/// </summary>
	[CustomEditor( typeof( SWSpriteComponent ) )]
	public class SWSpriteComponentInspector : Editor {
		public override void OnInspectorGUI() {
			Texture2D icon = SWEditorUI.Texture (SWUITex.logo);
			GUILayout.Space (6);
			GUILayout.Box (icon, GUIStyle.none);
			base.OnInspectorGUI ();
		}
	}
}