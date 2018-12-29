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
	using System.IO;
	using System.Text.RegularExpressions;
	using System.Reflection;

	public class SWCommon{
		public static void HRDColor_Switch(SWWindowBase window, ref bool hdr)
		{
			var rect = GUILayoutUtility.GetLastRect ();
			if (SWCommon.GetMouseDown (1) ) {
				if (rect.Contains (Event.current.mousePosition)) {
					hdr = !hdr;
					window.RepaintGetDirty();
				}
			}
		}
		public static void HRDColor_Field(Color color,bool eyeDropper,bool hdr,float width,System.Action<Color> act)
		{
			#if UNITY_2018_1_OR_NEWER
			var c = EditorGUILayout.ColorField (new GUIContent(""),color, eyeDropper, true, hdr, GUILayout.Width (width));
			if (c != color) {
			act (c);
			}
			#else
			var c = EditorGUILayout.ColorField (new GUIContent(""),color, eyeDropper, true, hdr, null, GUILayout.Width (width));
			if (c != color) {
				act (c);
			}
			#endif
		}


		public static bool RectContainsRect(Rect rect,Rect rect2,bool fullyContains = true)
		{
			if (fullyContains) {
				if (!rect.Contains (new Vector2 (rect2.x, rect2.y)))
					return false;
				if (!rect.Contains (new Vector2 (rect2.x + rect2.width, rect2.y)))
					return false;
				if (!rect.Contains (new Vector2 (rect2.x, rect2.y + rect2.height)))
					return false;
				if (!rect.Contains (new Vector2 (rect2.x + rect2.width, rect2.y + rect2.height)))
					return false;
				return true;
			} else {
				if(rect.Contains( new Vector2(rect2.x,rect2.y)))
					return true;
				if(rect.Contains( new Vector2(rect2.x+rect2.width,rect2.y)))
					return true;
				if(rect.Contains( new Vector2(rect2.x,rect2.y+rect2.height)))
					return true;
				if(rect.Contains( new Vector2(rect2.x+rect2.width,rect2.y+rect2.height)))
					return true;
				return false;
			}
		}

		public static bool IsReallyNull(System.Object obj) { 
			return System.Object.ReferenceEquals(obj, null); 
		}

		public static Rect SpriteRect01(Sprite sprite)
		{
			Rect rect = sprite.textureRect;
			rect = new Rect (
				rect.x / sprite.texture.width, 
				rect.y / sprite.texture.height, 
				rect.width / sprite.texture.width, 
				rect.height / sprite.texture.height
			);
			return rect;
		}
		public static bool TexureOriginSize(Texture2D asset, out int width, out int height) {
			if (asset != null) {
				string assetPath = AssetDatabase.GetAssetPath(asset);
				TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;

				if (importer != null) {
					object[] args = new object[2] { 0, 0 };
					MethodInfo mi = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
					mi.Invoke(importer, args);

					width = (int)args[0];
					height = (int)args[1];

					return true;
				}
			}

			height = width = 0;
			return false;
		}


		public static string ObjectGUID(Object obj)
		{
			string path = AssetDatabase.GetAssetPath (obj);
			string guid = AssetDatabase.AssetPathToGUID (path);
			return guid;
		}
		public static string ProductFolder()
		{
			string editorFolder = "";
			string defaultGUID = "47742781ee329644d8129348c35145f2";
			string key = SWGlobalSettings.ProductName + "Path";
			if (!EditorPrefs.HasKey (key)) {
				EditorPrefs.SetString (key, defaultGUID);
			}

			string guid = EditorPrefs.GetString (key);
			string path = AssetDatabase.GUIDToAssetPath (guid);
			if (PathIsProductFolder (path))
				editorFolder = path;

			//performan impact
			var allpath = AssetDatabase.GetAllAssetPaths ();
			foreach (var item in allpath) {
				if (PathIsProductFolder (item)) {
					string tmpguid = AssetDatabase.AssetPathToGUID(item);
					EditorPrefs.SetString (key, tmpguid);
					editorFolder = item;
				}
			}
			return editorFolder +"/Plugin";
		}

		private static bool PathIsProductFolder(string path)
		{
			if (string.IsNullOrEmpty (path))
				return false;
			//performan impact
			string[] ary = path.Split ('/');
			if (ary [ary.Length - 1] == SWGlobalSettings.ProductName) {
				string pluginPath = path + "/Plugin";
				if(Directory.Exists(pluginPath))
					return true;
			}
			return false;
		}

		public static Vector2 VectorMul(Vector2 v1,Vector2 v2)
		{
			return new Vector2 (v1.x * v2.x, v1.y * v2.y);
		}
		public static Vector2 VectorDivide(Vector2 v1,Vector2 v2)
		{
			return new Vector2 (v1.x / v2.x, v1.y / v2.y);
		}
		public static float Vector2Angle(Vector2 v)
		{
			float a = Mathf.Atan2 (v.y , v.x);
			return a;
		}

		/// <summary>
		/// Only for node on main map
		/// </summary>
		public static Rect RectNew(Vector2 center,Vector2 size)
		{
			Rect rect = new Rect ();
			rect.size = size;
			rect.center = center;

			rect = Ceil (rect);
			return rect;
		}

		/// <summary>
		/// If a node's rect is (175.5,100,100,100)  .5 will cause it look blury, so ceil it to (176,100,100,100)
		/// </summary>
		public static Rect Ceil(Rect rect)
		{
			return new Rect (Mathf.Ceil (rect.x), Mathf.Ceil (rect.y), Mathf.Ceil (rect.width), Mathf.Ceil (rect.height));
		}
		public static Vector2 Ceil(Vector2 v)
		{
			return new Vector2 (Mathf.Ceil (v.x), Mathf.Ceil (v.y));
		}

		public static Rect RectScale(Rect rect,Vector2 zScale)
		{
			Vector2 c = rect.center;
			rect.size = new Vector2(rect.size.x*zScale.x,rect.size.y*zScale.y);
			rect.center = c;
			return rect;
		}

		public static Vector2 RectBottomLeft(Rect rect)
		{
			Vector2 v = new Vector2 (rect.x, rect.y + rect.height);
			return v;
		}

		public static Rect GetRect(Vector2 center,Vector2 size)
		{
			Rect rect = new Rect ();
			rect.size = size;
			rect.center = center;
			return rect;
		}

		/// <summary>
		/// Use it with context matched rect
		/// For DrawMain, pass rect like al_rectMain
		/// For DrawMainInside1, pass rect like al_rectMainInsideZoom
		/// </summary>
		public static bool GetMouseDown(int id,Rect rect,bool containUsed = true)
		{
			if (GetMouseDown (id)) {
				if (rect.Contains (Event.current.mousePosition))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Use it with context matched rect
		/// For DrawMain, pass rect like al_rectMain
		/// For DrawMainInside1, pass rect like al_rectMainInsideZoom
		/// </summary>
		public static bool GetMouseUp(int id,Rect rect)
		{
			if (GetMouseUp (id)) {
				if (rect.Contains (Event.current.mousePosition))
					return true;
			}
			return false;
		}
		public static bool GetMouseDown(int id,bool containUsed = true)
		{
			//Event.current.type   outwindow->ignore   clickTextureField->used
			//Debug.Log (Event.current.type + " "+ Event.current.rawType);
			if (Event.current.rawType == EventType.MouseDown ||(containUsed && Event.current.rawType == EventType.Used)) {
				if (Event.current.button == id) {
					return true;
				}
			}
			return false;
		}

		public static bool GetMouseUp(int id)
		{
			if (Event.current.rawType == EventType.MouseUp) {
				if (Event.current.button == id) {
					return true;
				}
			}
			return false;
		}
			
		public static bool GetMouse(int id)
		{
			if (Event.current.rawType == EventType.MouseDrag) {
				if (Event.current.button == id) {
					return true;
				}
			}
			return false;
		}

		public static T SaveReload<T>(T t,string path)
			where T:Object
		{
			string adbPath = Path2AssetDBPath (path);
			AssetDatabase.CreateAsset (t, adbPath);
			AssetDatabase.ImportAsset(adbPath, ImportAssetOptions.ForceUpdate);
			t = AssetDatabase.LoadAssetAtPath<T> ( adbPath);
			return t;
		}
			

		public static SWTexture2DEx SaveReloadTexture2d(SWTexture2DEx t,string path,bool alphaIsTransparency = true)
		{
			var bs = t.EncodeToPNG ();
			string fullPath = Path2FullPath (path);
			string adbPath = Path2AssetDBPath (path);
			File.WriteAllBytes (fullPath, bs);
			AssetDatabase.Refresh ();
			TextureReImport(adbPath,alphaIsTransparency);
			var tex = AssetDatabase.LoadAssetAtPath<Texture2D> ( adbPath);
			t.Texture = tex;
			return t;
		}

		public static SWTexture2DEx Texture2dResave(SWTexture2DEx t,string adbPath)
		{
			var bs = t.EncodeToPNG ();
			string path = AssetDBPath2Path (adbPath);
			string fullPath = Path2FullPath (path);
			File.WriteAllBytes (fullPath, bs);
			return t;
		}

//		public static SWTexture2DEx TextureCopy(SWTexture2DEx f)
//		{
//			SWTexture2DEx t = TextureCreate (f.width, f.height, f.format, f.alphaIsTransparency);
//			var colors = f.GetPixels ();
//			for(int i =0;i<colors.Length;i++) {
//				colors[i].a *= 0.6f;
//			}
//			t.SetPixels (colors);
//			t.Apply ();
//			return t;
//		}

		public static SWTexture2DEx TextureCreate(int w,int h,TextureFormat format)
		{
			SWTexture2DEx t = new SWTexture2DEx(w,h,format,false,false);
			t.filterMode = FilterMode.Trilinear;

			var colors = t.GetPixels ();
			for(int i =0;i<colors.Length;i++) {
				colors [i] = new Color (0, 0, 0, 0);
			}
			t.SetPixels (colors);
			t.Apply ();
			return t;
		}
		public static void TextureReImportGray( Texture2D texture)
		{
			if ( null == texture ) return;
			string assetPath = AssetDatabase.GetAssetPath( texture );
			TextureReImportGray (assetPath);
		}
		protected static void TextureReImportGray( string adbPath)
		{
			var tImporter = AssetImporter.GetAtPath( adbPath ) as TextureImporter;
			if ( tImporter != null )
			{
				TextureImporterSettings setings = new TextureImporterSettings();
				tImporter.ReadTextureSettings (setings);
				setings.readable = true;
				tImporter.SetTextureSettings (setings);


				tImporter.textureType = TextureImporterType.SingleChannel;
				//tImporter.normalmap = false;
				tImporter.isReadable = true;
				tImporter.npotScale = TextureImporterNPOTScale.None;
				//tImporter.textureFormat = TextureImporterFormat.ARGB32;
				tImporter.textureCompression = TextureImporterCompression.Uncompressed;
				tImporter.mipmapEnabled = false;
				tImporter.alphaSource = TextureImporterAlphaSource.FromGrayScale;
				tImporter.alphaIsTransparency = false;
				AssetDatabase.ImportAsset( adbPath);
				AssetDatabase.Refresh ();  
			}
		}


		public static void TextureReImport( Texture2D texture)
		{
			if ( null == texture ) return;
			string assetPath = AssetDatabase.GetAssetPath( texture );
			TextureReImport (assetPath,true);
		}
		protected static void TextureReImport( string adbPath,bool alphaIsTransparency = true)
		{
			var tImporter = AssetImporter.GetAtPath( adbPath ) as TextureImporter;

			TextureImporterSettings setings = new TextureImporterSettings();
			tImporter.ReadTextureSettings (setings);

			if ( tImporter != null )
			{
				if (setings.readable == true && tImporter.isReadable == true &&  tImporter.mipmapEnabled == false   
					&&  tImporter.npotScale == TextureImporterNPOTScale.None
				//	&&  tImporter.textureFormat == TextureImporterFormat.ARGB32
					&&  tImporter.textureCompression == TextureImporterCompression.Uncompressed
					&&  tImporter.alphaIsTransparency == alphaIsTransparency
				//	&&  tImporter.normalmap == false
				)
					return;
				
				setings.readable = true;
				tImporter.SetTextureSettings (setings);

				//iNormal
				if (tImporter.textureType == TextureImporterType.NormalMap) {
					tImporter.isReadable = true;
				} else {
					//tImporter.textureType = TextureImporterType.Default;
					//tImporter.normalmap = false;
					tImporter.isReadable = true;
					tImporter.npotScale = TextureImporterNPOTScale.None;
					//tImporter.textureFormat = TextureImporterFormat.ARGB32;
					tImporter.textureCompression = TextureImporterCompression.Uncompressed;
					tImporter.mipmapEnabled = false;
					tImporter.alphaIsTransparency = alphaIsTransparency;
				}
				AssetDatabase.ImportAsset( adbPath);
				AssetDatabase.Refresh ();  
			}
		}
		public static float TextureReImportSprite(Sprite sprite )
		{
			Texture2D texture = SpriteGetTexture2D (sprite);
			if ( null == texture ) return 0;
			string assetPath = AssetDatabase.GetAssetPath( texture );
			return TextureReImportSprite (assetPath);
		}
		protected static float TextureReImportSprite( string adbPath)
		{
			var tImporter = AssetImporter.GetAtPath( adbPath ) as TextureImporter;
			if ( tImporter != null )
			{
				TextureImporterSettings setings = new TextureImporterSettings();
				tImporter.ReadTextureSettings (setings);

				if (setings.readable == true && tImporter.isReadable == true && tImporter.mipmapEnabled == false && setings.spriteMeshType == SpriteMeshType.FullRect 
					&& tImporter.npotScale == TextureImporterNPOTScale.None
//					&& tImporter.textureFormat == TextureImporterFormat.ARGB32
					&& tImporter.alphaIsTransparency == true
				)
					return tImporter.spritePixelsPerUnit; 
				setings.spriteMeshType = SpriteMeshType.FullRect;
				tImporter.SetTextureSettings (setings);

				//tImporter.textureType = TextureImporterType.Sprite;
				//tImporter.normalmap = false;
				tImporter.isReadable = true;
				tImporter.npotScale = TextureImporterNPOTScale.None;
//				tImporter.textureFormat = TextureImporterFormat.ARGB32;
				tImporter.mipmapEnabled = false;
				tImporter.alphaIsTransparency = true;
				AssetDatabase.ImportAsset( adbPath );
				AssetDatabase.Refresh();
				return tImporter.spritePixelsPerUnit; 
			}
			return 0;
		}
	

		public static Texture2D SpriteGetTexture2D(Sprite sprite)
		{
			string path = AssetDatabase.GetAssetPath (sprite);
			return AssetDatabase.LoadAssetAtPath<Texture2D> (path);
		}

		public static Texture2D Sprite2Texture2D(Sprite sprite)
		{
			Texture2D t2d = new Texture2D( (int)sprite.rect.width, (int)sprite.rect.height );
			var pixels = sprite.texture.GetPixels(  (int)sprite.textureRect.x, 
				(int)sprite.textureRect.y, 
				(int)sprite.textureRect.width, 
				(int)sprite.textureRect.height );
			t2d.SetPixels( pixels );
			t2d.Apply();
			return t2d;
		}

		/// <summary>
		/// Get file name without extension at path
		/// </summary>
		public static string GetName(string path)
		{
			string[] ary = path.Split ('/');
			string str = ary [ary.Length - 1];
			int index = str.LastIndexOf ('.');
			string title = str.Substring(0,index);
			return title;
		}


		public static string AssetDBPath2Path(string path)
		{
			return path.Substring (7);
		}
		public static string FullPath2Path(string path)
		{
			int startIndex = Application.dataPath.Length + 1;
			return path.Substring (startIndex);
		}
		public static string Path2AssetDBPath(string path)
		{
			return string.Format ("Assets/{0}", path);
		}
		public static string Path2FullPath(string path)
		{
			return  Application.dataPath + "/"+  path;
		}


		public static bool Raycast_CamLayer(string layer,out RaycastHit info)
		{
			return Raycast_Shoot (MainCamera.transform.position- new Vector3 (0,0.07f,0), MainCamera.transform.forward, layer, Raycast_IfMatchByLayer, out info);
		}

		public static bool Raycast_Shoot(Vector3 pos,Vector3 dir,string param,System.Func<RaycastHit,string,bool> matchFunc,out RaycastHit info)
		{
	//		LayerMask layerMask = ~((1 << 5)&(1 << 12));
			LayerMask layerMask = ~((1 << 16)|(1 << 8)|(1 << 9)|(1 << 0)|(1 << 13)|(1 << 14)|(1 << 15)|(1<<19)|(1<<18)|(1<<21));
	//		LayerMask layerMask = 12;

			if (Physics.Raycast (pos, dir, out info,1000f,layerMask.value)) {
				if (matchFunc (info, param))
					return true;
			}
			return false;
		}
		public static bool Raycast_IfMatchByLayer(RaycastHit info,string param)
		{

			return string.IsNullOrEmpty (param) || info.collider.gameObject.layer == LayerMask.NameToLayer (param);
		}
		static Camera item;
		public static Camera MainCamera
		{
			get{ 
				foreach (var item in GameObject.FindObjectsOfType<Camera>()) {
					if (item.enabled == true)
						return item;
				}
				return null;
			}
	//		get{
	//			if (GameManager.Instance != null) {
	//				item = GameManager.Instance.cam.transform.GetChild (0).GetChild (0).gameObject.GetComponent<Camera>();
	//				return item;
	//			}
	//			return null;
	//		}

		}


		public static void TransformFindChild(Transform tran,string s,ref Transform obj)
		{
			for (int i = 0; i < tran.childCount; i++) {
				if (tran.GetChild (i).name.Contains(s))
					obj = tran.GetChild (i);
				else
					TransformFindChild (tran.GetChild (i), s,ref obj);
			}
		}

		/// <summary>
		/// Determine the signed angle between two vectors, with normal 'n'
		/// as the rotation axis.
		/// </summary>
		public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
		{
			return Mathf.Atan2(
				Vector3.Dot(n, Vector3.Cross(v1, v2)),
				Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
		}

		public static Vector3 VectorPlane(Vector3 v)
		{
			return new Vector3 (v.x, 0, v.z);
		}

		public static Vector3 RotateVector(Vector3 v,Vector3 angle)
		{
			Matrix4x4 ma = Matrix4x4.TRS (Vector3.zero, Quaternion.Euler (angle), Vector3.one);
			return ma.MultiplyVector (v);
		}
	}
}