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
	/// SWSpriteAnimation Inspector
	/// </summary>
	[CustomEditor( typeof( SWSpriteAnimation ) )]
	public class SWSpriteAnimationInspector : SWSpriteComponentInspector {
		public override void OnInspectorGUI() {
			base.OnInspectorGUI ();
		}
	}
}