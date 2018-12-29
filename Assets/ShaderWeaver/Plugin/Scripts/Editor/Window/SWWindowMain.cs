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

	/// <summary>
	/// Main Window
	/// </summary>
	[System.Serializable]
	public partial class SWWindowMain:SWWindowLayoutV{
		public static SWWindowMain Instance; 
	
		[SerializeField]
		public SWData data = new SWData();
		[SerializeField]
		public SWData_Codes dataCode = new SWData_Codes();

		[SerializeField]
		public Shader shader;
		[SerializeField]
		public List<string> selection = new List<string>();
		[SerializeField]
		public SWViewWindow viewWindow;
		public Dictionary<string,Texture2D> textures = new Dictionary<string, Texture2D> ();

		[SerializeField]
		public Rect backgroundRect;
		[SerializeField]
		public Rect bgUV;
		[SerializeField]
		public float bgUVRatio;
		[SerializeField]
		public bool lineStartNodeFromLeft;
		[SerializeField]
		public int lineStartNodePort;
		[SerializeField]
		public SWNodeBase lineStartNode;


		#region dictionary
		[SerializeField]
		public SWNodeRoot nRoot;
		/// <summary>
		/// All nodes include root node
		/// </summary>
		[SerializeField]
		public List<SWNodeBase> nodes = new List<SWNodeBase>();
		protected string prefKey{
			get{
				return string.Format ("{0}dataPath", SWGlobalSettings.ProductName);
			}
		}
		/// <summary>
		/// Use when save as a new file
		/// </summary>
		public bool newCopy;
		[SerializeField]
		public string newName;

		public void NodeClear()
		{
			nodes.Clear ();
		}

		/// <summary>
		/// All nodes include root node
		/// </summary>
		public Dictionary<string,SWNodeBase> NodeAll()
		{
			var _NodeAll = new Dictionary<string, SWNodeBase> ();
			foreach (var item in nodes)
				_NodeAll.Add (item.data.id, item);
			return _NodeAll;
		}


		public void NodeRemoveAt(string key)
		{
			//Debug.Log ("NodeRemoveAt");
			if (key == nRoot.data.id) {
				nRoot.Delete ();
				nRoot = null;
			}
			Remove (nodes, key);
		}
		public bool Remove<T>(List<T> ns,string key)
			where T:SWNodeBase
		{
			for (int i = ns.Count - 1; i >= 0; i--) {
				if (ns [i].data.id == key) {
					ns [i].Delete ();
					ns.RemoveAt (i);
					return true;
				}
			}
			return false;
		}
			
		public override void InitOnce ()
		{
			al_startY = 21;
			rightupWidth = 280;
			base.InitOnce ();  
			NodeClear ();

			backgroundRect = new Rect (-5000, -5000, 10000, 10000);
			float xy = backgroundRect.width / backgroundRect.height;
			bgUV = new Rect (0, 0, xy * 50f, 50f);
			titleContent = new GUIContent (SWGlobalSettings.ProductName);

//			//Load ShaderWeaver/code
			dataCode = SWDataManager_Code.Load (SWCommon.ProductFolder ());

			InitRightUp ();
		}

		public override void InitUI ()
		{
			base.InitUI ();
			zScaleMin = 0.5f;
			zScaleMax = 3f;
			viewWindow = new SWViewWindow ();
			CreateNode (SWNodeType.root);

			List<SWSlot> slotsNodebox = new List<SWSlot> ();
			slotsNodebox.Add(new SWSlot("Color",SWTipsText.Main_t_Color,null,KeyCode.None,0));
			slotsNodebox.Add(new SWSlot("Image",SWTipsText.Main_t_Image,null,KeyCode.None,0));
			slotsNodebox.Add(new SWSlot("Dummy",SWTipsText.Main_t_Dummy,null,KeyCode.None,0));
			slotsNodebox.Add(new SWSlot("Refract",SWTipsText.Main_t_Refraction,null,KeyCode.None,0));
			slotsNodebox.Add(new SWSlot("Reflect",SWTipsText.Main_t_Reflect,null,KeyCode.None,0));

			slotsNodebox.Add(new SWSlot("UV",SWTipsText.Main_t_UV,null,KeyCode.None,2));
			slotsNodebox.Add(new SWSlot("Coord",SWTipsText.Main_t_Coord,null,KeyCode.None,2));
			slotsNodebox.Add(new SWSlot("Remap",SWTipsText.Main_t_Remap,null,KeyCode.None,2));
			slotsNodebox.Add(new SWSlot("Blur",SWTipsText.Main_t_Blur,null,KeyCode.None,2));
			slotsNodebox.Add(new SWSlot("Retro",SWTipsText.Main_t_Retro,null,KeyCode.None,2));

			slotsNodebox.Add(new SWSlot("Alpha",SWTipsText.Main_t_Alpha,null,KeyCode.None,3));

			slotsNodebox.Add(new SWSlot("Mask",SWTipsText.Main_t_Mask,null,KeyCode.None,1));
			slotsNodebox.Add(new SWSlot("Mixer",SWTipsText.Main_t_Mixer,null,KeyCode.None,1));
			slotsNodebox.Add(new SWSlot("Code",SWTipsText.Main_t_Code,null,KeyCode.None,1));
		

			slotBox_left = ScriptableObject.CreateInstance<SWSlotBox_Drag> ();
			slotBox_left.InitSlot(this,
				new Rect(0,	al_topHeight,	al_leftWidth,	position.height-al_spacing-al_topHeight),
				slotsNodebox,OnReleaseNode,new Vector2(al_leftWidth,38f));
		

			//Path Manage 1: Open from Shader Inspector
			if (shader!=null) {
				string adbPath = AssetDatabase.GetAssetPath (shader);
				string path =  SWCommon.AssetDBPath2Path (adbPath);
				ProcessPath (path);
				Load ();
				ProcessTitle(SWCommon.GetName (path));
				shader = null;
			}
		}
		#endregion

		[MenuItem("Window/Shader Weaver")]
		static void ShowEditor() { 
			if (Instance != null)
				Instance.Close ();
			var window = EditorWindow.GetWindow<SWWindowMain> ();
			window.InitOnce ();
		}
		public static void OpenFromInspector(Shader shader)
		{
			if (Instance != null)
				Instance.Close ();
			var window = EditorWindow.GetWindow<SWWindowMain> ();
			window.shader = shader;
			window.InitOnce ();
		}

		protected override void SerializedInit ()
		{
			base.SerializedInit ();

			if(slotBox_left!=null)
				slotBox_left.Init (OnReleaseNode);
		}
			
		public override void Awake ()
		{
			al_topHeight = SWGlobalSettings.al_topHeightMain;
			base.Awake ();
			Instance = this;
		}


		public override void Update ()
		{
			if (!CanUpdate ())
				return;
			base.Update ();

			foreach (var item in nodes) {
				item.Update ();
			}
			viewWindow.Update ();
			if (viewWindow.repaintDirty) {
				RepaintGetDirty ();
				viewWindow.repaintDirty = false;
			}
		}


		public void Save()
		{
			data.version = SWGlobalSettings.version;
			data.nodes.Clear ();
			foreach (var item in nodes) {
				item.BeforeSave ();
				data.nodes.Add (item.data);
			}
			SWDataManager.Save (SWCommon.Path2FullPath(dataPath), data);
		}
		public void Load()
		{
			data.nodes.Clear ();
			NodeClear ();
			data = SWDataManager.Load (SWCommon.Path2FullPath(dataPath));
			Material mat = SWEditorTools.GUIDToObject<Material> (data.materialGUID);

			for (int i=0;i< data.nodes.Count;i++) {
				//data.nodes [i].dirty = true;
				CreateNode (data.nodes[i]);

			}

			foreach (var node in nodes) {
				node.AfterLoad (); 
			}
			viewWindow.SetMaterial (mat,data,nRoot.sprite);
			InitRightUp ();
		}

		public override void Clean ()
		{
			base.Clean ();
			if (viewWindow!=null) {
				viewWindow.Clean (); 
			}
		}

		public override void OnAfterDeserialize ()
		{
			base.OnAfterDeserialize ();
			Instance = this;
		}
			
		void OnReleaseNode(SWSlot slot,Vector2 posInRegion)
		{
//			//performance impact
			SWNodeType type = (SWNodeType)System.Enum.Parse (typeof(SWNodeType), slot.name.ToLower ());

			SWUndo.Record (this);
			var node = CreateNode (type, mousePos);

			selection.Clear ();
			selection.Add (node.data.id);
			selectMode = SelectMode.one;
		}

		public bool IsLoadFromStartup()
		{
			return !needInit && ( nRoot == null || nRoot.data == null);
		}


		public override void OnGUI ()
		{
			if (IsLoadFromStartup ()) {
				InitOnce ();
				needInit = true;
			}
			base.OnGUI ();
		}

		void TopBt(Rect rect,string txt,string tooltip,SWUITex tex,System.Action dele)
		{
			if (GUI.Button (rect,SWEditorUI.Texture(tex),SWEditorUI.Style_Get(SWCustomStyle.none))) 
			{
				if (dele != null)
					dele ();
			}

			Tooltip_Rec (tooltip,rect);
			GUI.Label (new Rect(rect.x,rect.y + rect.height,rect.width,10),txt,SWEditorUI.Style_Get(SWCustomStyle.eTxtLight));
		}

		public bool ProjectIsDirty
		{
			get{
				return false;
			}
		}
		public bool ProjectIsSaved
		{
			get{
				return !string.IsNullOrEmpty (dataPath);
			}
		}
		public override void DrawTop ()
		{
			base.DrawTop ();
			float spacing = 20;
			Rect rect = new Rect (10, 5, 40, 40);
			TopBt (rect, "Open", SWTipsText.Main_Open, SWUITex.open, PressOpen);
			rect.x += rect.width + spacing;
			TopBt (rect, "Save", SWTipsText.Main_Save, SWUITex.save, PressSave);
			rect.x += rect.width + spacing;
			if (ProjectIsSaved) {
				TopBt (rect, "Update", SWTipsText.Main_Update, SWUITex.update, PressUpdate);
			} else {
				GUI.color = Color.gray;
				TopBt (rect, "Update", SWTipsText.Main_Update, SWUITex.updateGray, null);
				GUI.color = Color.white;
			}
		}

		public override void DrawLeft ()
		{
			base.DrawLeft ();
		}

		public override void DrawMainBot ()
		{
			base.DrawMainBot ();
		}

		public override void DrawMainInside1 ()
		{
			base.DrawMainInside1 ();
			GUI.DrawTextureWithTexCoords(backgroundRect, 
				SWEditorUI.Texture(SWUITex.canvasBG),bgUV, true);
		}
		public override void DrawMainMid ()
		{
			base.DrawMainMid ();
			GUI.DrawTexture( al_rectMain,SWEditorUI.Texture(SWUITex.canvasFG),ScaleMode.StretchToFill); 
		}
		public override void DrawMainInside2 ()
		{
			UpdateNodes ();
			DrawNodes();
			base.DrawMainInside2 ();

		}
		public override void SetInsideRects ()
		{
			base.SetInsideRects ();
			foreach (var node in nodes) {
				node.data.rect = SetInsideRect (node.data.rect);
			}
		}	

		public override void OnGUITop ()
		{
			base.OnGUITop ();
			if(viewWindow!=null)
				viewWindow.OnGUI (new Rect(position.width - 200 -20,al_topHeight + 20,200,200));
		}

		public override void GUIEnd ()
		{
			base.GUIEnd ();
		}


		public void ProcessTitle(string newTitle)
		{
			data.title = newTitle;
			newName = newTitle;
			dataPath = string.Format ("{0}{1}.shader", folderPath, data.title); 
		}
		public void ProcessPath(string path)
		{
			string[] ary = path.Split ('/');
			if (ary.Length == 1)
				folderPath = "";
			else
				folderPath = path.Substring (0, path.Length - ary [ary.Length - 1].Length - 1) + "/";
			dataPath = path;
		}

		public void PressRename()
		{
			if (newName == data.title)
				return;
			//Path Manage 4: Rename
			//Mask Texture
			foreach (var guid in data.masksGUID) {
				RenameAssetGUID (guid,true);
			}
			//Material
			RenameAssetGUID (data.materialGUID,false);
			//Remap Texture
			foreach (var node in nodes) {
				if (node.data.type == SWNodeType.remap) {
					RenameAssetGUID (node.data.textureExGUID,true);
				}
			}


			//Shader File
			string path = string.Format ("{0}{1}.shader", folderPath, data.title);
			//string fullPath = SWCommon.Path2FullPath (path);
			string adbPath =  SWCommon.Path2AssetDBPath (path);
			RenameAssetPath (adbPath,false);
			//Data
			ProcessTitle(newName);
			SaveUpdate ();
		}
		void RenameAssetGUID(string guid,bool autoIndexRes)
		{
			if (string.IsNullOrEmpty (guid))
				return;
			string path = AssetDatabase.GUIDToAssetPath (guid);
			RenameAssetPath (path,autoIndexRes);
		}
		void RenameAssetPath(string adbPath,bool autoIndexRes)
		{
			if (string.IsNullOrEmpty (adbPath))
				return;
			string fileOldName = SWCommon.GetName(adbPath);
			if (autoIndexRes) {
				int index = fileOldName.LastIndexOf ("_");
				AssetDatabase.RenameAsset (adbPath, newName + fileOldName.Substring(index));
			}
			else if(fileOldName == data.title)
				AssetDatabase.RenameAsset (adbPath, newName);
		}



		public void PressSave()
		{
			if (!NodesOk ())
				return;
			string str = "Assets/";
			if (EditorPrefs.HasKey (prefKey)) {
				var path = EditorPrefs.GetString (prefKey);
				string id = AssetDatabase.AssetPathToGUID (path);
				if (!string.IsNullOrEmpty (id))
					str = path;
			}



			string adbpath = EditorUtility.SaveFilePanelInProject ("Save", string.Format("{0}.shader",data.title), "shader","Plz",str);
			if (!string.IsNullOrEmpty (adbpath) && adbpath.Contains ("Assets")) {

				string path = SWCommon.AssetDBPath2Path (adbpath);
				string[] ary = path.Split ('/');
				string newTitle = ary [ary.Length - 1].Split ('.') [0];
			
				newCopy = path != dataPath || newTitle != data.title;
				//Path Manage 3: Save from Shader Weaver menu
				ProcessPath (path);
				ProcessTitle (newTitle);
				string folderPath = adbpath.Substring (0, adbpath.Length - ary [ary.Length - 1].Length);
				EditorPrefs.SetString(prefKey,folderPath);
				SaveUpdate ();
			}
		}

		public void PressUpdate()
		{
			if (!NodesOk ())
				return;
			SaveUpdate ();
		}


		bool NodesOk()
		{

			foreach (var item in nodes) {
				if (!item.BelongRootTree())
					continue;

				if (item is SWNodeImage || item is SWNodeUV || item is SWNodeAlpha) {
					if (item.texture == null) {
						EditorUtility.DisplayDialog ("Before Save", string.Format ("Please assign {0}'s texture.",item.data.name), "OK");
						return false;
					}
				}


				if (item is SWNodeRemap) {
					if (!item.HasChild()) {
						EditorUtility.DisplayDialog ("Before Save", string.Format ("Please assign {0}'s child.", item.data.name), "OK");
						return false;
					}
				}
			}
			return true;
		}

		public void PressOpen()
		{
			string str = SWGlobalSettings.AssetsFullPath;
			if (EditorPrefs.HasKey (prefKey)) {
				var path = str.Substring (0, str.Length - 6) + EditorPrefs.GetString (prefKey);
				if (System.IO.Directory.Exists (path))
					str = path;
			}

			string fullpath = EditorUtility.OpenFilePanel ("Data", str, "shader");
			if (!string.IsNullOrEmpty (fullpath) && fullpath.Contains ("Assets")) {
				string path = SWCommon.FullPath2Path (fullpath);
				if (SWDataManager.IsSWShader (fullpath)) {
					//Path Manage 2: Open from Shader Weaver menu
					ProcessPath (path);
					Load ();
					ProcessTitle (SWCommon.GetName (path));
					EditorPrefs.SetString (prefKey, "Assets/" + path);
				} else {
					EditorUtility.DisplayDialog ("Shader", "Shader is not a Shader Weaver Shader.", "Close");
				}
			}
		}

		void UpdateNodes()
		{
//			//Performance Impact
//			foreach (var item in nodes) {
//				//remove empty child
//				for(int i = item.data.children.Count-1;i>=0;i--)
//				{
//					string child = item.data.children [i];
//					if (!NodeAll().ContainsKey (child))
//						item.data.children.Remove (child);
//				}
//
//				//remove empty parent
//				for(int i = item.data.parent.Count-1;i>=0;i--)
//				{
//					string par = item.data.parent [i];
//					if (!NodeAll().ContainsKey (par))
//						item.data.parent.Remove (par);
//				}
//			}
		}

		void DrawNodes()
		{
			BeginWindows();
			var tempNodeAll = NodeAll ();

			foreach (var node in nodes) {
				node.Draw ();
					
				for(int i=0;i<node.data.children.Count;i++)
				{
					var child = node.data.children [i];
					int port = node.data.childrenPort[i];
					var childNode = tempNodeAll[child];
					if (childNode.RightAvailable () && node.LeftAvailable ()) {
						//Sometime child's parent didnt contain parent's id
						if (!childNode.data.parent.Contains (node.data.id)) {
							childNode.data.parent.Add (node.data.id);
							childNode.data.parentPort.Add (0);
						}
						var rectLeft = node.GetRectLeft (childNode.data.id);
						var rectRight = childNode.GetRectRight (node.data.id);



						if(node.data.type == SWNodeType.mixer && port !=0)
							DrawNodeCurve (rectRight.center,new Vector2(rectRight.width*0.5f,0),rectLeft.center,new Vector2(0,rectLeft.height*0.5f) );
						else
							DrawNodeCurve (rectRight.center,new Vector2(rectRight.width*0.5f,0),rectLeft.center,new Vector2(-rectLeft.width*0.5f,0) );
					}	
				}
			}
			EndWindows();
			DragNodeLine ();
		}

		void NodeOperation()
		{
			if (!InMap ())
				return;	
			var tempNodeAll = NodeAll ();
			//Break connection
			if (((Event.current.alt||Event.current.control) && SWCommon.GetMouseDown (0,false))
				||SWCommon.GetMouseDown (1,false)||SWCommon.GetMouseDown (2,false)) 
			{
				foreach (var node in nodes) {
					for (int i = 0; i < node.rectLefts.Count; i++) {
						int portID = i;
						var rect = node.rectLefts [i];

						if (rect.Contains (Event.current.mousePosition)) {
							SWUndo.Record (node);
							foreach (var ii in node.data.children) {
								SWUndo.Record (tempNodeAll[ii]);
							}
							node.DeleteChildOfPort (portID);
							RepaintGetDirty ();
						}
					}

					for (int i = 0; i < node.rectRights.Count; i++) {
						int portID = i;
						var rect = node.rectRights [i];
						if (rect.Contains (Event.current.mousePosition)) {
							SWUndo.Record (node);
							foreach (var ii in node.data.children) {
								SWUndo.Record (tempNodeAll[ii]);
							}
							node.DeleteParentOfPort (portID);
							RepaintGetDirty ();
						}
					}
				}
				return;
			}

			//start drag a line
			if (SWCommon.GetMouseDown (0,false)) {
				foreach (var node in nodes) {
					if (node.LeftAvailable ()) {
						for (int i = 0; i < node.rectLefts.Count; i++) {
							var rect = node.rectLefts [i];
							if (rect.Contains (Event.current.mousePosition)) {
								lineStartNode = node;
								lineStartNodePort = i;
								lineStartNodeFromLeft = true;
								RepaintGetDirty ();
							}
						}
					}

					if (node.RightAvailable ()) {
						for (int i = 0; i < node.rectRights.Count; i++) {
							var rect = node.rectRights [i];
							if (rect.Contains (Event.current.mousePosition)) {
								lineStartNode = node;
								lineStartNodePort = i;
								lineStartNodeFromLeft = false;
								RepaintGetDirty ();
							}
						}
					}
				}
			}

			//make connection
			if (lineStartNode != null && SWCommon.GetMouseUp (0)) {
				foreach (var node in nodes) {
					if (lineStartNodeFromLeft && node.RightAvailable()) {
						for (int i = 0; i < node.rectRights.Count; i++) {
							var rect = node.rectRights [i];
							if (rect.Contains (Event.current.mousePosition) && 
								lineStartNode.PortMatch(lineStartNodePort,node,i)) {
								SWUndo.Record (lineStartNode);
								SWUndo.Record (node);

								SWNodeBase.AddConnection (node,i,lineStartNode,lineStartNodePort);
								RepaintGetDirty ();
							}
						}
					}
					if (!lineStartNodeFromLeft && node.LeftAvailable()) {
						for (int i = 0; i < node.rectLefts.Count; i++) {
							var rect = node.rectLefts [i];
							if (rect.Contains (Event.current.mousePosition) &&
								node.PortMatch(i,lineStartNode,lineStartNodePort)) {
								SWUndo.Record (lineStartNode);
								SWUndo.Record (node);

								SWNodeBase.AddConnection (lineStartNode,lineStartNodePort,node,i);
								RepaintGetDirty ();
							}
						}
					}
				}
				lineStartNode = null;
			}
		}
	
		void DragNodeLine()
		{
			NodeOperation ();
			if (lineStartNode != null) {
				RepaintGetDirty ();
				if (lineStartNodeFromLeft) {
					var rect = lineStartNode.rectLefts [lineStartNodePort];

					if (!rect.Contains (Event.current.mousePosition)) {
						var mouseOff = (rect.center - Event.current.mousePosition).normalized * 0.1f;


						if(lineStartNode.data.type == SWNodeType.mixer && lineStartNodePort !=0)
							DrawNodeCurve (Event.current.mousePosition,mouseOff,rect.center,new Vector2(0,rect.height*0.5f) );
						else
							DrawNodeCurve (Event.current.mousePosition,mouseOff,rect.center,new Vector2(-rect.width*0.5f,0) );
					}
				} else {
					var rect = lineStartNode.rectRights [lineStartNodePort];

					if (!rect.Contains (Event.current.mousePosition)) {
						var mouseOff = (rect.center - Event.current.mousePosition).normalized * 0.1f;
						DrawNodeCurve (rect.center,new Vector2(rect.width*0.5f,0),Event.current.mousePosition,mouseOff);
					}
				}
			}
		}

		#region key command
		public override void KeyCmd_Dragmove (Vector2 delta)
		{
			//		Debug.Log (delta);
			base.KeyCmd_Dragmove (delta);
			foreach (var node in nodes) {
				node.data.rect.center += new Vector2 (1f * delta.x, 1f * delta.y);
			}
			backgroundRect.center  += new Vector2 (1f * delta.x, 1f * delta.y);
		}





		public enum SelectMode
		{
			no,
			move,
			one,
			rect
		}
		public SelectMode selectMode;

		SWNodeBase mouseDownNode;

		bool showSelectionRect = false; 
		Rect selectRect;


		void KeyCmd_SelectClean()
		{
			selectMode = SelectMode.no;
			mouseDownNode = null;
			showSelectionRect = false;
			RepaintGetDirty ();
		}

		public override void KeyCmd_Select()
		{
			if (SWCommon.GetMouseDown (0,false) && InMap() ) {
				Vector2 mp = Event.current.mousePosition;
				KeyCmd_SelectClean ();
				foreach(var node in nodes)
				{
					var orect = node.data.rect;
					if(orect.Contains(Event.current.mousePosition))
					{
						//2 select node
						mouseDownNode = node;
						break;
					}
				}

				if (mouseDownNode!=null) {
					if (selection.Contains (mouseDownNode.data.id)) {
						selectMode = SelectMode.move;
					} else {
						selection.Clear ();
						selection.Add (mouseDownNode.data.id);
						selectMode = SelectMode.move;
						RepaintGetDirty();
					}
				} else {
					bool outside = true;
					foreach(var node in nodes)
					{
						var orect = node.rectBig;
						if(orect.Contains(Event.current.mousePosition))
						{
							outside = false;
							break;
						}
					}

					if (outside) {
						selection.Clear ();
						selectMode = SelectMode.rect;
						EditorGUIUtility.editingTextField = false;
						RepaintGetDirty();
					}
				}
			}
			if (selectMode == SelectMode.move) {
				if (SWCommon.GetMouse (0) && InMap() ) {
					Vector2 v = Event.current.mousePosition - mousePosLast;
					foreach (var item in selection) {
						NodeAll() [item].data.rect.center += v;
					}
					RepaintGetDirty();
				}

				if (SWCommon.GetMouseUp (0) && InMap() ) {
					KeyCmd_SelectClean ();
				}
			}


			if (selectMode == SelectMode.rect) {
				if (SWCommon.GetMouse (0) && InMap() ) {
					RepaintGetDirty();
					Vector3 v1 = Event.current.mousePosition;
					Vector3 v2 = mousePosDown;
					float mag = (v1 - v2).magnitude;

					showSelectionRect = mag > 30;


					if (showSelectionRect) {
						float x = Mathf.Min (v1.x, v2.x);
						float y = Mathf.Min (v1.y, v2.y);
						float w = Mathf.Abs (v1.x - v2.x);
						float h = Mathf.Abs (v1.y - v2.y);
						selectRect = new Rect (x, y, w, h);
					}
					RepaintGetDirty();
				}


				if (showSelectionRect) {
					GUI.Box (selectRect, "",SWEditorUI.Style_Get(SWCustomStyle.eSelectRect));
				}

				if (Event.current.type == EventType.MouseUp
					||Event.current.type == EventType.ContextClick
					||Event.current.type == EventType.Ignore) {
					if (showSelectionRect) {
						foreach (var node in nodes) {
							var orect = node.data.rect;
							if (selectRect.Contains (orect.center)) {
								selection.Add (node.data.id);
								RepaintGetDirty();
								//break;
							}
						}
					}

					KeyCmd_SelectClean ();
				}
			}

			if (selectMode == SelectMode.one) {
				if (SWCommon.GetMouseUp (0)) {
					foreach(var node in nodes)
					{
						var orect = node.data.rect;
						if(orect.Contains(Event.current.mousePosition))
						{
							if (node == mouseDownNode) {
								selection.Add (node.data.id);
							}
							//break;
						}
					}
					KeyCmd_SelectClean ();
					RepaintGetDirty();
				}
			}
		} 
		public override void KeyCmd_Delete ()
		{
			if (!InMap ())
				return;
			SWUndo.Record (this);
			base.KeyCmd_Delete ();

			foreach (var item in selection) {
				if (NodeAll().ContainsKey (item)) {
					if (!(NodeAll() [item] is SWNodeRoot)) {
						NodeRemoveAt (item);
					}
				}
			}
			selection.Clear ();
			RepaintGetDirty();
		}

		public override void Copy ()
		{
			if (!InMap ())
				return;
			base.Copy ();
			List<SWDataNode> datas = new List<SWDataNode> ();
			foreach (var item in selection) {
				NodeAll()[item].BeforeSave ();
				datas.Add (NodeAll() [item].data);
			}

			string str = SWDataManager.DataToText (datas);
			EditorGUIUtility.systemCopyBuffer = str;
		} 


		public override void Paste ()
		{
			if (!InMap ())
				return;
			selection.Clear ();
			base.Paste ();
			string buffer = EditorGUIUtility.systemCopyBuffer;
			if (!SWDataManager.StringIsData (buffer)) {
				//Debug.Log ("its not data:" + buffer);
				return;
			}


			List<SWDataNode> datas = new List<SWDataNode> ();
			try {
				datas = SWDataManager.TextToData (buffer);
			}
			catch (System.Exception e) {
				Debug.LogError (e);
				return;
			}
			Vector2 offset = new Vector2(120,30);
			var tempNodeAll = NodeAll ();
			foreach(var item in datas)
			{
				if (item.type == SWNodeType.root)
					continue;

				//check for id conflict
				while (tempNodeAll.ContainsKey (item.id)) {
					string oldId = item.id;
					item.AssingNewID ();
					string newId = item.id;
					item.rect.center += offset*0.5f;

					foreach (var go in datas) {
						for (int i = go.parent.Count - 1; i >= 0; i--) {
							if (go.parent [i] == oldId)
								go.parent [i] = newId; 
						}
					}

					foreach (var go in datas) {
						for (int i = go.children.Count - 1; i >= 0; i--) {
							if (go.children [i] == oldId)
								go.children [i] = newId; 
						}
					}
				}

			
				//check for name conflict
				bool hasSameName = true;
				while (hasSameName) {
					bool has = false;
					foreach (var _node in nodes) {
						if (_node.data.name == item.name) {
							has = true;
						}
					}
					hasSameName = has;
					if (hasSameName) {
						item.name = item.name + "copy";
						item.rect.center += offset;
					}
				}


				var node = CreateNode(item);
				item.layerMask.Clear ();
				node.AfterLoad ();
				selection.Add (node.data.id);
			}
	


			foreach (var item in datas) {
				for (int i = item.parent.Count - 1; i >= 0; i--) {
					bool has = false;
					foreach (var item2 in datas) {
						if (item2.id == item.parent [i]) {
							has = true;
						}
					}
					if (!has) {
						item.parent.RemoveAt (i);
					}
				}
				for (int i = item.children.Count - 1; i >= 0; i--) {
					bool has = false;
					foreach (var item2 in datas) {
						if (item2.id == item.children [i]) {
							has = true;
						}
					}
					if (!has) {
						item.children.RemoveAt (i);
					}
				}
			}
		}
		#endregion





		#region baisc function
		public enum PortDir
		{
			left,
			right,
			up,
			down
		}

		void DrawNodeCurve(Vector2 startCenter,Vector2 startOff,Vector2 endCenter,Vector2 endOff) {
			Vector2 left = startCenter + startOff;
			Vector2 right = endCenter + endOff;
			float dis = Vector2.Distance (left, right);
			float fac = dis * 0.25f;

			Vector2 startTan = left + startOff.normalized * fac;
			Vector2 endTan = right + endOff.normalized * fac;
			Color shadowCol = new Color(0, 0.5f, 0, 0.06f);
			for (int i = 0; i < 3; i++) 
				Handles.DrawBezier(left, right, startTan, endTan, shadowCol, null, 10);
			Handles.DrawBezier(left, right, startTan, endTan, SWEditorUI.ColorPalette(SWColorPl.green), null, 5);
		}

		/// <summary>
		/// (1)When create a new map: Create Root  
		/// </summary>
		public SWNodeBase CreateNode(SWNodeType _type)
		{
			return CreateNode (_type, GetNextPos ());
		}
		/// <summary>
		/// (1)Drag a node to map 
		/// (2)Invoked when create a new map
		/// </summary>
		public SWNodeBase CreateNode(SWNodeType _type,Vector2 pos)
		{
			SWDataNode data = new SWDataNode (_type);
			SWNodeBase node = CreateNode (data);
			node.SetPosition (pos);
			return node;
		}

		/// <summary>
		/// (1)Load / Paste
		/// (2)Invoked by all upper CreateNode method
		/// </summary>
		public SWNodeBase CreateNode(SWDataNode data)
		{
			SWNodeBase node = null;
			if (data.type == SWNodeType.root) {
				nRoot = ScriptableObject.CreateInstance<SWNodeRoot> ();
				node = nRoot;
			}
			else if (data.type == SWNodeType.color) {
				node = ScriptableObject.CreateInstance<SWNodeColor> ();
			}
			else if (data.type == SWNodeType.image) {
			 	node = ScriptableObject.CreateInstance<SWNodeImage> ();
			}
			else if (data.type == SWNodeType.uv) {
			 	node = ScriptableObject.CreateInstance<SWNodeUV> ();
			}
			else if (data.type == SWNodeType.mask) {
			 	node = ScriptableObject.CreateInstance<SWNodeMask> ();
			}
			else if (data.type == SWNodeType.remap) {
			 	node = ScriptableObject.CreateInstance<SWNodeRemap> ();
			}
			else if (data.type == SWNodeType.alpha) {
				node = ScriptableObject.CreateInstance<SWNodeAlpha> ();
			}
			else if (data.type == SWNodeType.mixer) {
				node = ScriptableObject.CreateInstance<SWNodeMixer> ();
			}
			else if (data.type == SWNodeType.blur) {
				node = ScriptableObject.CreateInstance<SWNodeBlur> ();
			}
			else if (data.type == SWNodeType.retro) {
				node = ScriptableObject.CreateInstance<SWNodeRetro> ();
			}
			else if (data.type == SWNodeType.dummy) {
				node = ScriptableObject.CreateInstance<SWNodeDummy> ();
			}
			else if (data.type == SWNodeType.coord) {
				node = ScriptableObject.CreateInstance<SWNodeCoord> ();
			}
			else if (data.type == SWNodeType.refract) {
				node = ScriptableObject.CreateInstance<SWNodeRefraction> ();
			}
			else if (data.type == SWNodeType.reflect) {
				node = ScriptableObject.CreateInstance<SWNodeReflection> ();
			}
			else if (data.type == SWNodeType.code) {
				node = ScriptableObject.CreateInstance<SWNodeCode> ();
			}
			else if (data.type == SWNodeType.coord) {
				node = ScriptableObject.CreateInstance<SWNodeCoord> ();
			}
			else if (data.type == SWNodeType.dummy) {
				node = ScriptableObject.CreateInstance<SWNodeDummy> ();
			}


			node.Init(data, this);
			nodes.Add (node);
			node.data.rect = SWCommon.Ceil (node.data.rect);
			return node;
		}

		public Vector2 GetNextPos()
		{
			if( nRoot == null)
				return new Vector2 (xHalf - SWNodeBase.NodeWidth*0.5f, 
					yHalf- SWNodeBase.NodeHeight*0.5f);
			SWNodeBase botRightNode = null;
			foreach (var node in nodes) 
			{
					botRightNode = node;
			}
			return new Vector2 (botRightNode.data.rect.x - 200, botRightNode.data.rect.y + 0);
		}

		public int GetNextIndex()
		{
			if( nodes.Count == 0)
				return 0;

			int max = int.MinValue;
			foreach (var node in nodes) 
			{
				if (node.index > max) {
					max = node.index;
				}
			}
			return max + 1;
		}
		#endregion

		#region export
		void SaveUpdate()
		{
			SWDataManager_Code.Save(SWCommon.ProductFolder (),dataCode);

			float f = Time.realtimeSinceStartup;
			EditorUtility.DisplayProgressBar (SWGlobalSettings.ProductTitle, "Processsing maps", 0.2f);
			ProcessRemap ();
			EditorUtility.DisplayProgressBar (SWGlobalSettings.ProductTitle, "Processsing masks", 0.4f);
			ProcessMasks ();
			EditorUtility.DisplayProgressBar (SWGlobalSettings.ProductTitle, "Processsing Textures", 0.6f);
			ProcessTextures ();
			EditorUtility.DisplayProgressBar (SWGlobalSettings.ProductTitle, "Creating Materials", 0.8f);
			Material mat = SWMaterialManager.CreateMaterial (this);
			EditorUtility.DisplayProgressBar (SWGlobalSettings.ProductTitle, "Setting Materials", 0.9f);
			viewWindow.SetMaterial (mat,data,nRoot.sprite);
			EditorUtility.DisplayProgressBar (SWGlobalSettings.ProductTitle, "Finish", 1f);
			Save ();
			EditorUtility.ClearProgressBar ();
			viewWindow.needRender = true;
			//Debug.Log ("Finish:"+(Time.realtimeSinceStartup - f));
		}

		/// <summary>
		/// Write remap texture as png. If use custom,just return
		/// </summary>
		void ProcessRemap()
		{
			foreach (var node in nodes) 
			{
				if (node.data.type == SWNodeType.remap){
					//CUSTOM
					if (node.data.useCustomTexture)
						continue;
					if (!newCopy && !node.data.dirty)
						continue;
					node.data.dirty = false;
					SWNodeRemap e = (SWNodeRemap)node;
					var t = node.textureEx;
					string guid = SWEditorTools.ObjectToGUID (t.Texture);
					if (newCopy || string.IsNullOrEmpty (guid)) {
						node.textureEx = SWCommon.SaveReloadTexture2d (e.textureEx,string.Format("{0}{1}_{2}.png",
							folderPath,data.title,e.data.name),true);
					} else {
						SWCommon.Texture2dResave (t, AssetDatabase.GetAssetPath (t.Texture));
					}
				}
			}
		}




		int groupIndex;
		void ProcessMasks()
		{
			groupIndex = -1;
			//step 1:gather all masks
			List<SWNodeMask> masks = new List<SWNodeMask> ();
			foreach (var node in nodes) {
				if (node.data.type == SWNodeType.mask ){
					SWNodeMask e = (SWNodeMask)node;

					//CUSTOM:If this mask node use custom mask,it will be always dirty,force texture merge
					e.GrayApply ();
					masks.Add (e);
				}
			}

			bool dirty = false;
			foreach (var item in masks) {
				if (item.data.dirty) {
					dirty = true;
					break;
				}
			}
			if (!dirty)
				return;




			Dictionary<int,List<SWNodeMask>> dic = new Dictionary<int, List<SWNodeMask>> ();
			foreach (var item in masks) {
				if (!dic.ContainsKey (item.maskSize))
					dic.Add (item.maskSize,new List<SWNodeMask>());
				dic [item.maskSize].Add (item);
			}

			var list = SWDataNode.resoList;
			for (int i = 0; i < list.Count; i++) {
				int size = list [i];
				if (dic.ContainsKey (size)) {
					ProcessMasksSub (size,dic[size]);
				}
			}
		}

		void ProcessMasksSub(int size,List<SWNodeMask> masks)
		{
			float x = masks.Count / 4f;
			int groupNum = Mathf.CeilToInt (x);

			List<string> guids = new List<string> ();
			for (int i = 0; i < groupNum; i++) {
				groupIndex++;

//				//If resame and no diry, just keep guid
//				if (!newCopy && !AnyDirty (masks, i * 4)) {
//					string tguid = "";
//					if (groupIndex < data.masksGUID.Count)
//						tguid = data.masksGUID [groupIndex];
//					else {
//						string pp = string.Format ("{0}{1}_{2}.png", folderPath, data.title, "mask" + groupIndex);
//						tguid = AssetDatabase.AssetPathToGUID (SWCommon.Path2AssetDBPath (pp));
//					}
//					if (!string.IsNullOrEmpty (tguid)) {
//						guids.Add (tguid);
//						continue;
//					}
//				}
				string path = ""; 
				if(newCopy || groupIndex>=data.masksGUID.Count)
					path = string.Format ("{0}{1}_{2}.png", folderPath, data.title, "mask" + groupIndex);
				else
					path = SWCommon.AssetDBPath2Path(AssetDatabase.GUIDToAssetPath (data.masksGUID [groupIndex]));

				Texture2D tmpTex =AssetDatabase.LoadAssetAtPath<Texture2D> (SWCommon.Path2AssetDBPath(path));
				SWTexture2DEx t;
				if (tmpTex == null) {
					t = SWCommon.TextureCreate (size, size, TextureFormat.ARGB32);
					CreateMask (size,t, masks, i);
					t = SWCommon.SaveReloadTexture2d (t, path, false);
				} else {
					t = new SWTexture2DEx (tmpTex);
					if (t.Texture.width != size)
						t.Texture.Resize (size, size);
					CreateMask (size,t, masks, i);
					SWCommon.Texture2dResave (t, AssetDatabase.GetAssetPath (t.Texture));
				}
				SetMask (masks,i * 4 + 0, "mask" + i, SWChannel.r,t);
				SetMask (masks,i * 4 + 1, "mask" + i, SWChannel.g,t);
				SetMask (masks,i * 4 + 2, "mask" + i, SWChannel.b,t);
				SetMask (masks,i * 4 + 3, "mask" + i, SWChannel.a,t);

				string guid = SWEditorTools.ObjectToGUID (t.Texture);
				guids.Add (guid);
			}
			foreach (var item in masks) {
				item.data.dirty = false;
			}
			data.masksGUID = guids;
		}

		protected void CreateMask(int size,SWTexture2DEx t,List<SWNodeMask> masks,int i)
		{
			var colors = new Color[size*size];
			int index = i * 4 + 0;
			Color[] ra = masks.Count > index ? masks [index].texMask.GetPixels () : null;
			index = i * 4 + 1;
			Color[] ga = masks.Count > index ? masks [index].texMask.GetPixels () : null;
			index = i * 4 + 2;
			Color[] ba = masks.Count > index ? masks [index].texMask.GetPixels () : null;
			index = i * 4 + 3;
			Color[] aa = masks.Count > index ? masks [index].texMask.GetPixels () : null;

			for (int j = 0; j < colors.Length; j++) {
				float r = ra != null ? ra [j].a : 0;
				float g = ga != null ? ga [j].a : 0;
				float b = ba != null ? ba [j].a : 0;
				float a = aa != null ? aa [j].a : 0;
				colors [j] = new Color (r, g, b, a);
			}
			t.SetPixels (colors);
			t.Apply ();
		}

		bool AnyDirty(List<SWNodeMask> masks,int start)
		{
			for(int i=0;i<4;i++)
			{
				int index = start + i;
				if (index < masks.Count && masks [index].data.dirty)
					return true;
			}
			return false;
		}

		void SetMask(List<SWNodeMask> masks,int index, string name,SWChannel ch,SWTexture2DEx _tex)
		{
			if (index >= masks.Count)
				return;
			SWNodeMask node = masks [index];
			//node.data.name = name;
			node.data.maskChannel = ch;
			node.texture = _tex.Texture;
		}

		/// <summary>
		/// If there are two abc.png in different folders used in SW. They will appear in shader as abc and abc_1
		/// </summary>
		void ProcessTexturesSuffix()
		{
			Dictionary<string,List<string>> name_guidPair = new Dictionary<string, List<string>> ();
			foreach (var node in nodes){
				var tex = node.Texture;
				if (tex == null)
					continue;
				string guid = SWCommon.ObjectGUID (tex);

				if (!name_guidPair.ContainsKey (tex.name)) {
					name_guidPair.Add (tex.name, new List<string> ());
				}

				if (!name_guidPair [tex.name].Contains (guid)) {
					name_guidPair [tex.name].Add (guid);
				}
			}


			foreach (var node in nodes){
				var tex = node.Texture;
				if (tex == null)
					continue;
				string guid = SWCommon.ObjectGUID (tex);
				for (int i = 0; i<name_guidPair [tex.name].Count; i++) {
					if (name_guidPair [tex.name] [i] == guid) {
						node.textureDuplicatedSuffix = i;
					}
				}
			}
		}

		void ProcessTextures()
		{
			ProcessTexturesSuffix ();
			textures.Clear ();
			foreach (var node in nodes) {
				Texture2D tex = node.Texture;
				if (node.data.type == SWNodeType.root) {
					textures.Add ("_" + node.TextureShaderName (), tex);
				} else {
					if(!node.BelongRootTree())
						continue;
					if (tex == nRoot.texture)
						continue;
					if (tex == null)
						continue;
					if (textures.ContainsKey ("_" + node.TextureShaderName ())) 
						continue;
					textures.Add ("_" + node.TextureShaderName (), tex);
				}
			}
				
			if (SupportNormalMap ()) {
				ProcessTexturesNormalMap ();
			}
		}

		#region Sprite Light Normal mapping
		public bool SupportNormalMap()
		{
			return SWWindowMain.Instance.data.shaderType == SWShaderType.sprite && SWWindowMain.Instance.data.spriteLightType != SWSpriteLightType.no;
		}
		void ProcessTexturesNormalMap()
		{
			foreach (var node in nodes) {
				if (node.UseNormalMap()) {
					if (textures.ContainsKey ("_" + node.TextureShaderName_NormalMap ())) 
						continue;
					textures.Add ("_" + node.TextureShaderName_NormalMap (), node.textureNormalMap);
				}
			}
		}
		#endregion
		#endregion

		protected override void OnLostFocus ()
		{
			base.OnLostFocus ();

			if (selectMode == SelectMode.rect) {
				KeyCmd_SelectClean ();
			}
		}
	}
}

