//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEditor;
	using UnityEngine;
	using System.Collections.Generic;
	using System.Reflection;

	/// <summary>
	/// Tools for the editor
	/// </summary>
	public class SWEditorTools
	{
		static Texture2D mBackdropTex;
		static Texture2D mBackdropTexBright;
		static Texture2D mContrastTex;
		static Texture2D mGradientTex;
		static public Texture2D blankTexture
		{
			get
			{
				return EditorGUIUtility.whiteTexture;
			}
		}

		static public Texture2D backdropTextureBright
		{
			get
			{
				if (mBackdropTexBright == null) mBackdropTexBright = CreateCheckerTex(
					new Color(0.8f, 0.8f, 0.8f, 1f),
					new Color(0.6f, 0.6f, 0.6f, 1f));
				return mBackdropTexBright;
			}
		}

		static public Texture2D backdropTexture
		{
			get
			{
				if (mBackdropTex == null) mBackdropTex = CreateCheckerTex(
					new Color(0.1f, 0.1f, 0.1f, 0.5f),
					new Color(0.2f, 0.2f, 0.2f, 0.5f));
				return mBackdropTex;
			}
		}
		static public Texture2D contrastTexture
		{
			get
			{
				if (mContrastTex == null) mContrastTex = CreateCheckerTex(
					new Color(0f, 0.0f, 0f, 0.5f),
					new Color(1f, 1f, 1f, 0.5f));
				return mContrastTex;
			}
		}

		static public Texture2D gradientTexture
		{
			get
			{
				if (mGradientTex == null) mGradientTex = CreateGradientTex();
				return mGradientTex;
			}
		}
		static Texture2D CreateGradientTex ()
		{
			Texture2D tex = new Texture2D(1, 16);
			tex.name = "[Generated] Gradient Texture";
			tex.hideFlags = HideFlags.DontSave;

			Color c0 = new Color(1f, 1f, 1f, 0f);
			Color c1 = new Color(1f, 1f, 1f, 0.4f);

			for (int i = 0; i < 16; ++i)
			{
				float f = Mathf.Abs((i / 15f) * 2f - 1f);
				f *= f;
				tex.SetPixel(0, i, Color.Lerp(c0, c1, f));
			}

			tex.Apply();
			tex.filterMode = FilterMode.Bilinear;
			return tex;
		}

		static public void DrawTiledTexture (Rect rect, Texture tex)
		{
			GUI.BeginGroup(rect);
			{
				int width  = Mathf.RoundToInt(rect.width);
				int height = Mathf.RoundToInt(rect.height);

				for (int y = 0; y < height; y += tex.height)
				{
					for (int x = 0; x < width; x += tex.width)
					{
						GUI.DrawTexture(new Rect(x, y, tex.width, tex.height), tex);
					}
				}
			}
			GUI.EndGroup();
		}

		static public Object GUIDToObject (string guid)
		{
			if (string.IsNullOrEmpty(guid)) return null;
			string path = AssetDatabase.GUIDToAssetPath(guid);
			if (string.IsNullOrEmpty(path)) return null;
			return AssetDatabase.LoadAssetAtPath(path, typeof(Object));
		}
		static public T GUIDToObject<T> (string guid) where T : Object
		{
			Object obj = GUIDToObject(guid);
			if (obj == null) return null;

			System.Type objType = obj.GetType();
			if (objType == typeof(T) || objType.IsSubclassOf(typeof(T))) return obj as T;

			if (objType == typeof(GameObject) && typeof(T).IsSubclassOf(typeof(Component)))
			{
				GameObject go = obj as GameObject;
				return go.GetComponent(typeof(T)) as T;
			}
			return null;
		}
		static public string ObjectToGUID (SWTexture2DEx tex)
		{
			return ObjectToGUID (tex.Texture);
		}
		static public string ObjectToGUID (Object obj)
		{
			string path = AssetDatabase.GetAssetPath(obj);
			return (!string.IsNullOrEmpty(path)) ? AssetDatabase.AssetPathToGUID(path) : null;
		}
		static Texture2D CreateCheckerTex (Color c0, Color c1)
		{
			Texture2D tex = new Texture2D(16, 16);
			tex.name = "[Generated] Checker Texture";
			tex.hideFlags = HideFlags.DontSave;

			for (int y = 0; y < 8; ++y) for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c1);
			for (int y = 8; y < 16; ++y) for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c0);
			for (int y = 0; y < 8; ++y) for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c0);
			for (int y = 8; y < 16; ++y) for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c1);

			tex.Apply();
			tex.filterMode = FilterMode.Point;
			return tex;
		}
	}
}