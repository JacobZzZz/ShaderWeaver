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
	public class SWNodeBase:ScriptableObject{
	//	private static int nextIndex = 1;
		[SerializeField]
		public int index;
		[SerializeField]
		public SWDataNode data;
		[SerializeField]
		public Texture2D texture;
		[SerializeField]
		public Texture2D textureGray;
		[SerializeField]
		public SWTexture2DEx textureEx;
		public Texture2D Texture{
			get
			{
				if (texture != null)
					return texture;
				if (!textureEx.IsNull)
					return textureEx.Texture;
				return null;
			}
		}	

		//Sprite Light Normal mapping
		[SerializeField]
		public Texture2D textureNormalMap;

		[SerializeField]
		public Sprite sprite;
		[SerializeField]
		public Rect rectBig; 
		[SerializeField]
		public List<Rect> rectLefts = new List<Rect>();
		[SerializeField]
		public List<Rect> rectRights = new List<Rect> ();
		[SerializeField]
		public SWOutput shaderOutput;
		[SerializeField]
		protected bool titleEditing = false;
		[SerializeField]
		protected string nameEditing;
		#region layout
		public static readonly float NodeWidth = 100;
		public static readonly float NodeHeight = 130;
		public static readonly float NodeBigWidth = 152; 
		public static readonly float NodeBigHeight = 130; 
		[SerializeField]
		public float nodeWidth = NodeWidth;
		[SerializeField]
		public float nodeHeight = NodeHeight;


		protected static readonly float headerHeight = 20;
		protected float contentWidth
		{
			get{ 
				return nodeWidth - gap * 2;
			}
		}
		protected static readonly float buttonHeight = 18;
		protected float gap = 5;
		[SerializeField]
		protected Rect rectTop;
		[SerializeField]
		protected Rect rectArea;
		[SerializeField]
		protected Rect rectBotButton; 

		[SerializeField]
		protected float portWidth = 12;
		[SerializeField]
		protected float portHeight = 18;

		/// <summary>
		/// Different Colors
		/// </summary>
		[SerializeField]
		public int styleID;

		[SerializeField]
		public int textureDuplicatedSuffix = 0;
		[SerializeField]
		protected float portSpacing = 5;

		/// <summary>
		/// Child port Single Connection
		/// </summary>
		public bool childPortSingleConnection = false;

		protected SWWindowMain window
		{
			get{
				return SWWindowMain.Instance;
			}
		}

		#endregion

		#region SerializedProperty
		public SerializedObject so;
		protected virtual void SerializedInit()
		{
			so = new SerializedObject (this);
		}
		#endregion

		#region init
		public virtual void Init(SWDataNode _data,SWWindowMain _window)
		{
			SerializedInit ();
			data = _data;
			index = SWWindowMain.Instance.GetNextIndex ();
			if (string.IsNullOrEmpty (data.name))
				data.name = data.type.ToString () + index;
			if (this is SWNodeRoot) {
				data.name = "ROOT";
				data.parentPortNumber = 0;
			}

			InitLayout ();
			SetRectsAll ();
			data.outputType.Clear ();
			data.inputType.Clear ();
		}
		public virtual void InitLayout()
		{
			rectTop = new Rect (0, 1, nodeWidth, headerHeight+2);
			rectBotButton = new Rect (gap, nodeHeight - gap - buttonHeight, contentWidth, buttonHeight);
			rectArea = new Rect (gap, headerHeight + gap, contentWidth, 
				nodeHeight - headerHeight - gap*2 - (rectBotButton.height+gap));
		}

		#endregion
		public GUIStyle Style
		{
			get{
				//eNode0-eNode3
				return SWEditorUI.MainSkin.customStyles [14 + styleID];
			}
		}

		public virtual string TextureName()
		{
			return Texture.name;
		}

		public virtual string TextureShaderName()
		{
			if(SWWindowMain.Instance.nRoot.texture != null && texture == SWWindowMain.Instance.nRoot.texture)
				return "MainTex";

			string name = textureDuplicatedSuffix == 0 ? SWRegex.NameLegal (Texture.name) : SWRegex.NameLegal (Texture.name) + "_" + textureDuplicatedSuffix;
			return name;
		}

		#region Sprite Light Normal mapping
		public string TextureShaderName_NormalMap()
		{
			return SWRegex.NameLegal (textureNormalMap.name);
		}
			
		public bool UseNormalMap()
		{
			return data.nm && textureNormalMap != null;
		}
		#endregion
			
		public Texture2D GetParentTexture()
		{ 
			if (GetParentNode ().data.type == SWNodeType.mask) {
				SWNodeMask mask = (SWNodeMask)GetParentNode ();
				return mask.texMask.Texture;
				//return mask.info.texPreview;
			} else {
				return GetParentNode ().Texture;
			}
		}

		public virtual Texture2D GetChildTexture()
		{
			if (data.children.Count == 0)
				return null;
			return SWWindowMain.Instance.NodeAll()[data.children[0]].Texture;
		}

		#region node
		public virtual void Delete()
		{
			DeleteAllChild ();
			DeleteAllParent ();
		}
		#region parent
		public List<SWNodeBase> GetParentNodeAllAllList()
		{
			List<SWNodeBase> list = new List<SWNodeBase> ();
			foreach (var item in GetParentNodeAllAll()) {
				list.Add (item.Value);
			}
			list.Reverse ();
			return list;
		}
		public Dictionary<string,SWNodeBase> GetParentNodeAllAll()
		{
			var tempNodeAll = window.NodeAll ();
			Dictionary<string,SWNodeBase> dic = new Dictionary<string, SWNodeBase> ();
			foreach (var item in  data.parent) {
				GetParentNodeAllAllSub (dic, tempNodeAll[item]);
			}
			return dic;
		}
		public void GetParentNodeAllAllSub( Dictionary<string,SWNodeBase> dic,SWNodeBase e)
		{
			var tempNodeAll = window.NodeAll ();
			if(!dic.ContainsKey(e.data.id))
				dic.Add (e.data.id,e);
			foreach (var item in  e.data.parent) {
				GetParentNodeAllAllSub (dic, tempNodeAll[item]);
			}
		}
		public Dictionary<string,SWNodeBase> GetChildNodeAllAll()
		{
			var tempNodeAll = window.NodeAll ();
			Dictionary<string,SWNodeBase> dic = new Dictionary<string, SWNodeBase> ();
			foreach (var item in  data.children) {
				GetChildNodeAllAllSub (dic, tempNodeAll[item]);
			}
			return dic;
		}
		public void GetChildNodeAllAllSub( Dictionary<string,SWNodeBase> dic,SWNodeBase e)
		{
			var tempNodeAll = window.NodeAll ();
			if(!dic.ContainsKey(e.data.id))
				dic.Add (e.data.id,e);
			foreach (var item in  data.children) {
				GetChildNodeAllAllSub (dic, tempNodeAll[item]);
			}
		}


	


		public SWNodeBase GetParentNode()
		{
			if (data.parent.Count == 0)
				return null;

			return SWWindowMain.Instance.NodeAll() [data.parent [0]];
		}

		public List<SWNodeBase> GetParentNodes()
		{
			List<SWNodeBase> parentNodes = new List<SWNodeBase> ();
			var pnode = GetParentNode ();
			while (pnode != null) {
				parentNodes.Add (pnode);
				pnode = pnode.GetParentNode ();
			}
			parentNodes.Reverse ();
			return parentNodes;
		}



		public bool HasParent()
		{ 
			return data.parent.Count>0 && SWWindowMain.Instance.NodeAll().ContainsKey( data.parent[0]);
		}
		public bool HasChild()
		{
			return data.children.Count > 0 && SWWindowMain.Instance.NodeAll().ContainsKey( data.children[0]);
		}
		#endregion

		#region child
		public Rect GetRectRight(string parent)
		{
			int id = data.parent.IndexOf (parent);
			int index = data.parentPort [id];
			return rectRights [index];
		}
		public Rect GetRectLeft(string children)
		{
			int id = data.children.IndexOf (children);
			int index = data.childrenPort [id];
			return rectLefts [index];
		}

		public void DeleteAllParent()
		{
			var tempNodeAll = SWWindowMain.Instance.NodeAll ();
			for (int i = data.parent.Count - 1; i >= 0; i--) {
				string obj = data.parent [i];
				RemoveConnection (tempNodeAll[obj],this);
			}
		}

		public void DeleteParentOfPort(int portID)
		{
			var tempNodeAll = SWWindowMain.Instance.NodeAll ();
			for (int i = data.parent.Count - 1; i >= 0; i--) {
				if (data.parentPort [i] == portID) {
					string obj = data.parent [i];
					RemoveConnection (tempNodeAll[obj], this);
				}
			}
		}

		public void DeleteChildOfPort(int portID)
		{
			var tempNodeAll = SWWindowMain.Instance.NodeAll ();
			for (int i = data.children.Count - 1; i >= 0; i--) {
				if (data.childrenPort [i] == portID) {
					string obj = data.children [i];
					RemoveConnection (this, tempNodeAll[obj]);
				}
			}
		}

		public void DeleteAllChild()
		{
			var tempNodeAll = SWWindowMain.Instance.NodeAll ();
			for (int i = data.children.Count - 1; i >= 0; i--) {
				string obj = data.children [i];
				RemoveConnection (this, tempNodeAll[obj]);
			}
		}

		public static void AddConnection(SWNodeBase leftNode,int leftPort,SWNodeBase rightNode,int rightPort)
		{
			//If rightNode is child node single connection limited, than remove child to this port
			if (rightNode.childPortSingleConnection) {
				while (rightNode.data.childrenPort.Contains (rightPort)) {
					int index = rightNode.data.childrenPort.IndexOf (rightPort);
					var childToRemove = SWWindowMain.Instance.NodeAll()[rightNode.data.children[index]];
					RemoveConnection(rightNode,childToRemove);
				}
			}

			SWDataManager.AddConnection (leftNode.data,leftPort, rightNode.data,rightPort);
		}

		public static void RemoveConnection(SWNodeBase parent,SWNodeBase child)
		{
			SWDataManager.RemoveConnection (parent.data, child.data);
		}

		#endregion
		#endregion



		#region update
		public virtual void Update()
		{
			ReImport ();
		}


		public virtual void ReImport() {
			if (Texture != null) {
				try
				{
					Texture.GetPixel(0, 0);
				}
				catch(UnityException e)
				{
					SWCommon.TextureReImport (Texture);
				}
			}
		}
		#endregion

		#region ui
		public void Draw()
		{
			//performance	: only render nodes inside main rect
			if(SWCommon.RectContainsRect(window.al_rectMainInsideZoom,data.rect,false))
				data.rect = GUI.Window(index, data.rect, DrawNodeWindow, "",Style); 
			Tooltip ();

			DrawHead ();

			if (SWWindowMain.Instance.selection.Contains (data.id)) {
				Rect frameRect = SWCommon.GetRect (data.rect.center, new Vector2(
					data.rect.size.x + 20f,data.rect.size.y + 18f));
				GUI.Box (frameRect, "", SWEditorUI.Style_Get (SWCustomStyle.eNodeSelected));
			}
		}

		protected virtual void Tooltip()
		{
			//SWWindowMain.Instance.Tooltip_Rec ("",data.rect);	
		}

		public bool LeftAvailable()
		{
			if (data.inputType.Count > 0)
				return true;
			return false;
		}
		public bool RightAvailable()
		{
			if (data.outputType.Count > 0)
				return true;
			return false;
		}
			

		protected virtual void DrawLeftRect(int id,Rect rect)
		{
			
		}
		protected virtual void DrawRightRect(int id,Rect rect)
		{

		}

		protected void SetRectsAll()
		{
			data.rect = SWCommon.RectNew (data.rect.center, new Vector2 (nodeWidth,nodeHeight));
			SetRectsLeft (rectLefts,data.childPortNumber);
			SetRectsRight (rectRights,data.parentPortNumber);
		}
		public void SetPosition(Vector2 pos)
		{
			data.rect = SWCommon.RectNew (pos, new Vector2 (nodeWidth,nodeHeight));
		}

		protected virtual void SetRectsLeft(List<Rect> rects,int count = 1)
		{
			rects.Clear ();
			for (int i = 0; i < count; i++) {
				rects.Add(CalRectVertical (data.rect.x - portWidth, i, count));
			}
		}
		protected virtual void SetRectsRight(List<Rect> rects,int count = 1)
		{
			rects.Clear ();
			for (int i = 0; i < count; i++) {
				rects.Add(CalRectVertical (data.rect.x + data.rect.width, i, count));
			}
		}
	
		protected virtual Rect CalRectVertical(float xPos,int i,int count = 1)
		{
			return new Rect (
				xPos, 
				data.rect.y+ headerHeight + (data.rect.height-headerHeight)* 0.5f - portHeight * 0.5f  - (float)(count-1)/2 * (portSpacing+portHeight) + (portSpacing+portHeight)*i, 
				portWidth, 
				portHeight
			);
		}
		protected virtual Rect CalRectHorizontal(float yPos,int i,int count = 1)
		{
			float pw = portHeight;
			float ph = portWidth;

			return new Rect (
				data.rect.x+data.rect.width * 0.5f - pw * 0.5f  - (float)(count-1)/2f * (portSpacing+pw) + (portSpacing+pw)*i,
				yPos, 
				pw, 
				ph
			);
		}




		protected virtual void DrawHead()
		{
			
			SetRectsAll ();

			rectBig = SWCommon.GetRect (data.rect.center, data.rect.size * 1.25f);


			if (LeftAvailable()) {
				for (int i = 0; i < rectLefts.Count; i++) {
					if (!SWWindowMain.Instance.lineStartNodeFromLeft && SWWindowMain.Instance.lineStartNode != null && 
						PortMatch (i,SWWindowMain.Instance.lineStartNode,SWWindowMain.Instance.lineStartNodePort))
						GUI.color = SWEditorUI.ColorPalette (SWColorPl.green);
					else
						GUI.color = SWEditorUI.ColorPalette (SWColorPl.light);
					GUI.DrawTexture (rectLefts[i], EditorGUIUtility.whiteTexture);
				}
				GUI.color = Color.white;
			}

			if (RightAvailable()) {
				for (int i = 0; i < rectRights.Count; i++) {
					if (SWWindowMain.Instance.lineStartNodeFromLeft && SWWindowMain.Instance.lineStartNode != null && 
						SWWindowMain.Instance.lineStartNode.PortMatch (SWWindowMain.Instance.lineStartNodePort,this,i))
						GUI.color = SWEditorUI.ColorPalette (SWColorPl.green);
					else
						GUI.color = SWEditorUI.ColorPalette (SWColorPl.light);
					GUI.DrawTexture (rectRights[i], EditorGUIUtility.whiteTexture);
				}
				GUI.color = Color.white;
			}

			for (int i = 0; i < rectLefts.Count; i++) {
				DrawLeftRect (i,rectLefts[i]);
			}
			for (int i = 0; i < rectRights.Count; i++) {
				DrawRightRect (i,rectRights[i]);
			}
		}

		protected virtual void DrawNodeWindow(int id) {
			ShowTitle ();
			GUILayout.Space (headerHeight);
		}

		protected void ShowTitle()
		{
			if ((this is SWNodeRoot) || !titleEditing) {
				GUI.Label (rectTop, data.name, SWEditorUI.Style_Get (SWCustomStyle.eNodeTitle));
				if (Event.current.type == EventType.MouseUp &&
					rectTop.Contains (Event.current.mousePosition)) {
					titleEditing = true;
					nameEditing = data.name;
				}
			} else {
				if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Return) {
					titleEditing = false;

					nameEditing = SWRegex.NameLegal(nameEditing);
					if (NameUnique (nameEditing)) {
						data.name = SWRegex.NameLegal(nameEditing);
					}
				}
				nameEditing = EditorGUI.TextField (rectTop, nameEditing,SWEditorUI.Style_Get (SWCustomStyle.eNodeTitle));
			}
		}
	
		protected void SelectTexture()
		{
			var tex = (Texture2D)EditorGUI.ObjectField (rectArea, texture, typeof(Texture2D), true);
			if (tex != texture) {
				SWUndo.Record (this);
				texture = tex;

				if (texture != null) {
					//iNormal
					string assetPath = AssetDatabase.GetAssetPath (texture);
					var tImporter = AssetImporter.GetAtPath (assetPath) as TextureImporter;
					data.useNormal = tImporter.textureType == TextureImporterType.NormalMap;
				}
			}
		}
		protected void SelectTextureGray()
		{
			var tex = (Texture2D)EditorGUI.ObjectField (rectArea, textureGray, typeof(Texture2D), true);
			if (tex != textureGray) {
				SWUndo.Record (this);
				textureGray = tex;
			}
		}

		bool NameUnique(string name)
		{
			if (string.IsNullOrEmpty (name))
				return false;
			foreach (var node in SWWindowMain.Instance.nodes) {
				if (node.data.name == name)
					return false;
			}
			return true;
		}

		protected virtual void DrawNodeWindowEnd() {
		}
		public virtual void DrawSelection() {
		}
		#endregion

		public virtual bool PortMatch(int port, SWNodeBase child,int childPort)
		{
			//cant connect self
			if (data.id == child.data.id)
				return false;
			//connected already
			if (data.children.Contains (child.data.id)) {
				int index = data.children.IndexOf (child.data.id);
				if(data.childrenPort[index] == port)
					return false;
			}
			//cant connect circle
			if (GetParentNodeAllAll ().ContainsKey (child.data.id))
				return false;

			foreach (var item in data.inputType) {
				if (child.data.outputType.Contains (item))
					return true;
			}
			return false;
		}

		public virtual void BeforeSave()
		{
			if (texture != null) {
				data.textureGUID = SWEditorTools.ObjectToGUID (texture);
			} else {
				data.textureGUID = "";
			}

			if (textureGray != null) {
				data.textureGUIDGray = SWEditorTools.ObjectToGUID (textureGray);
			} else {
				data.textureGUIDGray = "";
			}
			if (!textureEx.IsNull) {
				data.textureExGUID = SWEditorTools.ObjectToGUID (textureEx);
			} else {
				data.textureExGUID = "";
			}


			if (sprite != null) {
				data.spriteName = sprite.name;
				data.spriteGUID =  SWEditorTools.ObjectToGUID(sprite);
			}

			//Sprite Light Normal mapping
			if (textureNormalMap != null) {
				data.nmid = SWEditorTools.ObjectToGUID (textureNormalMap);
			} else {
				data.nmid = "";
			}
		}
		public virtual void AfterLoad()
		{
			if (!string.IsNullOrEmpty (data.textureGUID)) {
				texture = SWEditorTools.GUIDToObject<Texture2D>(data.textureGUID);
			}
			if (!string.IsNullOrEmpty (data.textureGUIDGray)) {
				textureGray = SWEditorTools.GUIDToObject<Texture2D>(data.textureGUIDGray);
			}
			if (!string.IsNullOrEmpty (data.textureExGUID)) {
				var tex  = SWEditorTools.GUIDToObject<Texture2D>(data.textureExGUID);
				textureEx = new SWTexture2DEx (tex);
			}

			if (!string.IsNullOrEmpty (data.spriteGUID)) {
				string path = AssetDatabase.GUIDToAssetPath (data.spriteGUID);
				Object[] objs = AssetDatabase.LoadAllAssetsAtPath (path);
				foreach (var item in objs) {
					if (item is Sprite && item.name == data.spriteName) {
						//Debug.Log ("Found");
						sprite = (Sprite)item;
					}
				}
			}

			//Sprite Light Normal mapping
			if (!string.IsNullOrEmpty (data.nmid)) {
				textureNormalMap = SWEditorTools.GUIDToObject<Texture2D>(data.nmid);
			}
		}

		public virtual void OnEnable()
		{
			hideFlags = HideFlags.DontSave;
		}
			
		/// <summary>
		/// True:This node is under root tree or root itself
		/// </summary>
		public bool BelongRootTree()
		{
			Dictionary<string,SWNodeBase> allNodes = SWWindowMain.Instance.NodeAll();
			return BelongRootTreeSub (allNodes, this);
		}

		protected bool BelongRootTreeSub(Dictionary<string,SWNodeBase> allNodes,SWNodeBase node)
		{
			if (node.data.type == SWNodeType.root)
				return true;
			else {
				foreach (var item in node.data.parent) {
					if (!(allNodes [item] is SWNodeRemap) && BelongRootTreeSub (allNodes, allNodes [item]))
						return true;
				}
			}
			return false;
		}

		public bool HasColorAttribute()
		{
			return data.outputType.Count == 1 && data.outputType [0] == SWDataType._Color;
		}
	}
}