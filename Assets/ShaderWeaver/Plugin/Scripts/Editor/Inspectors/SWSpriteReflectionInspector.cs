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
	/// SWSpriteReflection Inspector
	/// </summary>
	[CustomEditor( typeof( SWSpriteReflection ) )]
	public class SWSpriteReflectionInspector : SWSpriteComponentInspector {
		public override void OnInspectorGUI() {
			base.OnInspectorGUI ();
		}
	}
}