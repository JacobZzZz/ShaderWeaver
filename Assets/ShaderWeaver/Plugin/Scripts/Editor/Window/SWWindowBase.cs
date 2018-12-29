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
	/// All sw editor windows' base class
	/// </summary>
	[System.Serializable]
	public class SWWindowBase:EditorWindow,ISerializationCallbackReceiver{
		[SerializeField]
		public static GUISkin skinDefault;
		[SerializeField]
		public string folderPath; 
		[SerializeField]
		public string dataPath;  
		[SerializeField]
		protected bool needInit = false;

		[SerializeField]
		public Vector2 mousePos;
		[SerializeField]
		public Vector2 mousePosDown;
		[SerializeField]
		public Vector2 mousePosLast;
		[SerializeField]
		public Vector2 mousePosOut;
		[SerializeField]
		public Vector2 mousePosOutLast;
		[SerializeField]
		public bool mousePressing;
		[SerializeField]
		public bool pause;
		[SerializeField]
		protected bool repaintDirty = true;
		public void RepaintGetDirty()
		{
			repaintDirty = true;
		}

		#region SerializedProperty
		public SerializedObject so;
		protected virtual void SerializedInit()
		{
			so = new SerializedObject (this);
		}
		#endregion



		#region Init & Serialization
		public virtual void Awake()
		{
			SerializedInit ();
		}

		public virtual void OnBeforeSerialize()  
		{
	//		Debug.Log ("OnBeforeSerialize");   
		} 
		 
		public virtual void OnAfterDeserialize()
		{
	//		Debug.Log ("OnAfterDeserialize"); 
	//		SerializedInit ();
		}

		public bool CanUpdate()
		{
			return (!needInit);
		}

		/// <summary>
		/// Reals the init.
		/// </summary>
		public virtual void InitOnce()
		{
			needInit = true;
		}

		/// <summary>
		/// UI Init
		/// </summary>
		public virtual void InitUI()
		{
			minSize = new Vector2 (770.0f, 590.0f);
			wantsMouseMove = true;
		}

		void OnDestroy()
		{
			Clean ();
		}
		public virtual void Clean()
		{

		}
		#endregion
		  

		public virtual void OnGUI()
		{
			if (so == null)
				SerializedInit ();
			so.Update ();
	
			SWTooltip.Start (this);
			mousePosOut = Event.current.mousePosition;

			if (needInit) { 
				needInit = false;
				InitUI ();
			}
		}

		public virtual void Update()
		{
			//Debug.Log (EditorApplication.timeSinceStartup);
			if (repaintDirty) {
				Repaint ();
				repaintDirty = false;
			}
		}
			
	

		public void DrawBG(Rect rect,bool frame = true,bool isLight = false)
		{
			var defColor = GUI.color;
			GUI.color = EditorGUIUtility.isProSkin
				? (Color)new Color32(56, 56, 56, 255)
				: (Color)new Color32(194, 194, 194, 255);

			GUI.color = new Color32 (56, 56, 56, 255);
			if(isLight)
				GUI.color = new Color32 (194, 194, 194, 255);

			GUI.DrawTexture(rect, EditorGUIUtility.whiteTexture);
			GUI.color = defColor;
			if(frame)
				GUI.Box(rect,"",SWEditorUI.Style_Get(SWCustomStyle.eLine));
		}


		protected void DrawFrame(Rect rect)
		{
			GUI.Box (rect, "", SWEditorUI.Style_Get (SWCustomStyle.eImageFrame));
		}

		protected virtual void OnFocus()
		{

		}
		protected virtual void OnLostFocus()
		{
			Cursor.visible = true; 
		}
	}
}