//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using UnityEditor;

	/// <summary>
	/// Manage Editor Undo
	/// </summary>
	public static class SWUndo{
		public static void Record(Object obj,string txt = "")
		{
			Undo.RecordObject (obj, SWDataManager.NewGUID());
		}
		public static void Record(Object[] objs,string txt = "")
		{
			Undo.RecordObjects (objs, SWDataManager.NewGUID());
		}
		public static void RegisterCompleteObjectUndo(Object obj,string txt = "")
		{
			Undo.RegisterCompleteObjectUndo (obj, txt);
		}
		public static void RegisterCompleteObjectUndo(Object[] obj,string txt = "")
		{
			Undo.RegisterCompleteObjectUndo (obj, txt);
		}

		public static void RegisterCompleteObjectUndo(SWTexture2DEx tex,string txt = "")
		{
			Undo.RegisterCompleteObjectUndo (tex.Texture, txt);
		}
	}
}
