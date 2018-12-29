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

	[System.Serializable]
	public class SWWindowEffectDummy : SWWindowEffectImage {
		public new static SWWindowEffectDummy Instance;

		public new static void ShowEditor(SWNodeEffector e) { 
			if (Instance != null)
				Instance.Close ();
			var window =EditorWindow.GetWindow<SWWindowEffectDummy> (true,"Dummy");
			window.Init (e);
			window.InitOnce ();
		} 
	}
}

