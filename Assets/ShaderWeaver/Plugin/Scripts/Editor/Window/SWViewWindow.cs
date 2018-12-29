//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using UnityEditor;
	using System.Reflection;
	/// <summary>
	/// Manage preview
	/// </summary>
	[System.Serializable]
	public class SWViewWindow{
		[SerializeField]
		protected bool inFullRect;
		[SerializeField]
		private Vector3 startPos = new Vector3 (0, -10000, 0);
		[SerializeField]
		public SWPreview preview;
		[SerializeField]
		string name = "preview";
		[SerializeField]
		public Material material;
		[SerializeField]
		public double inRectStartTime = -1;
		[SerializeField]
		public bool isLargeRect = false;
//		[SerializeField]
//		public double lastRepaintDirtyTime = 0;
		[SerializeField]
		public bool repaintDirty = true;
		protected void RepaintGetDirty()
		{
			repaintDirty = true;
		}
		[SerializeField]
		public bool needRender = true;

		public float scale
		{
			get{ return SWWindowMain.Instance.data.ps;}
		}
		public float largeScale{
			get{ return SWWindowMain.Instance.data.psm;}
		}

		public SWWindowMain winMain{
			get{ 
				return SWWindowMain.Instance;
			}
		}

		public SWViewWindow()
		{  
		}

		void Init()
		{
			if (preview == null) {
				GameObject obj = GameObject.Find(name);
				if (obj == null) {
					GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject> (
						SWCommon.ProductFolder()+"/Prefabs/Preview.prefab");
					obj = GameObject.Instantiate (prefab);
					obj.name = name;
					obj.hideFlags = HideFlags.HideInHierarchy;
				}
				preview = obj.GetComponent<SWPreview> ();
				preview.Init (startPos);
			}
		}
			
		public void SetMaterial(Material mat,SWData data,Sprite sprite)
		{
			if (data.shaderType == SWShaderType.sprite) {
				int orginWidth = 0;
				int orginHeight = 0;
				if (sprite != null && SWCommon.TexureOriginSize (sprite.texture, out orginWidth, out orginHeight)) {
					Init ();
					material = mat;
					float mulX = (float)orginWidth / (float)sprite.texture.width;
					float mulY = (float)orginHeight / (float)sprite.texture.height;
					preview.SetMaterial (mat, data, sprite, mulX, mulY);
				}
			} else {
				Init ();
				material = mat;
				preview.SetMaterial (mat,data,sprite,0,0);
			}

		}

		public void Update()
		{
			if (inFullRect) {
				needRender = true;
				RepaintGetDirty ();

			} else {
//				if (EditorApplication.timeSinceStartup > lastRepaintDirtyTime + 10f) {
//					RepaintGetDirty ();
//					lastRepaintDirtyTime = EditorApplication.timeSinceStartup;
//				}
			}
		}
	
		public void OnGUI(Rect rect)
		{
			Init ();
			if (preview.cam == null)
				return;
			
	
			if (needRender || winMain.data.pum == false) {
				preview.cam.Render ();
				needRender = false;
				RepaintGetDirty ();
			}
			//Cal Rect
			if(winMain.data.shaderType == SWShaderType.normal)
				rect = NewRect (rect);
			else if(winMain.data.shaderType == SWShaderType.sprite 
				||winMain.data.shaderType == SWShaderType.ui ||winMain.data.shaderType == SWShaderType.ngui_ui2dSprite)
				rect = NewRectSprite (rect);
			if (winMain.showRight) {
				rect = new Rect (
					rect.x- winMain.rightUpRect.width,
					rect.y,
					rect.width,
					rect.height
				);
			}
			var showRect = LargeRect (rect, scale);
			var showRectMouseOver = LargeRect (rect, largeScale);


			if (material != null) {
				if (GUI.Button (rect, "", GUIStyle.none)) {
					Selection.activeObject = material;
					EditorGUIUtility.PingObject (material);
				}


				bool _isLargeRect = false;
				//performance impact
				//if (EditorWindow.mouseOverWindow == winMain && showRect.Contains (Event.current.mousePosition)) {
				if (showRect.Contains (Event.current.mousePosition)) {
					if (inRectStartTime < 0)
						inRectStartTime = EditorApplication.timeSinceStartup;
					_isLargeRect = EditorApplication.timeSinceStartup - inRectStartTime > 0.2f;
				} else {
					inRectStartTime = -1;
				}
				if (isLargeRect != _isLargeRect) {
					isLargeRect = _isLargeRect;
					RepaintGetDirty ();
				}

				if(isLargeRect)
					rect = showRectMouseOver;
				else 
					rect = showRect;
			}
		
			GUI.DrawTexture(rect, preview.cam.targetTexture); 
			inFullRect = rect.Contains (Event.current.mousePosition);
		}

		Rect LargeRect(Rect rect,float mul)
		{
			rect = new Rect (
				rect.x - rect.width*(mul-1), 
				rect.y, 
				rect.width*mul, 
				rect.height*mul
			);
			return rect;
		}

		public Rect NewRect(Rect rect)
		{
			float ratio = 1;
			if (winMain != null 
				&& winMain.nRoot != null 
				&& winMain.nRoot.texture != null) {

				var tex = winMain.nRoot.texture;
				ratio = (float)tex.width / (float)tex.height;
				var center = rect.center;
				var size = rect.size;
				if (ratio >= 1) {
					size = new Vector2 (size.x, size.y/ratio);
				} else {
					size = new Vector2 (size.x * ratio, size.y);
				}
				return SWCommon.RectNew (center, size);
			}
			return rect;
		}
		public Rect NewRectSprite(Rect rect)
		{
			float ratio = 1;
			if (winMain != null 
				&& winMain.nRoot != null 
				&& winMain.nRoot.sprite != null) {

				var tex = winMain.nRoot.sprite.rect;
				ratio = (float)tex.width / (float)tex.height;
				var center = rect.center;
				var size = rect.size;
				if (ratio >= 1) {
					size = new Vector2 (size.x, size.y/ratio);
				} else {
					size = new Vector2 (size.x * ratio, size.y);
				}
				return SWCommon.RectNew (center, size);
			}
			return rect;
		}



		public void Clean()
		{
			if (preview == null)
				return;
			if(preview.cam!=null && preview.cam.targetTexture != null)
				GameObject.DestroyImmediate (preview.cam.targetTexture);
			GameObject.DestroyImmediate (preview.gameObject);
		}
	}
}

