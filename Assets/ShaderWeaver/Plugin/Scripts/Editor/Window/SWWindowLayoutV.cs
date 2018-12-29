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
	/// SWWindowLayoutV: Organize UI structure
	/// </summary>
	[System.Serializable]
	public partial class SWWindowLayoutV : SWWindowBase {
		[SerializeField]
		protected float rightupWidth = 190;
		protected readonly float rightupSpacing = 10;
		protected readonly float rightUpUnitHeight = 20;
		[SerializeField]
		public bool hasRightup = true;
		[SerializeField]
		public bool showRight = true;
		[SerializeField]
		public bool showRightChange = false;
		[SerializeField]
		public Rect rightUpRect;
		/// <summary>
		/// '+' '-'
		/// </summary>
		[SerializeField]
		protected Rect rightUpExpandRect;


		[SerializeField]
		public float al_startX = 0;
		[SerializeField]
		public float al_startY = 0;
		[SerializeField]
		public float al_slotStartY = 10;
		[SerializeField]
		public float al_topHeight = 30;
		public float al_leftWidth = 80;

		public static float al_spacing = 10;
		public static float al_spacingBig = 30;

		public Rect topRect
		{
			get{ return new Rect (0, 0, position.width, al_topHeight);}
		}

		/// <summary>
		/// - Window Space -
		/// Map Displaying Area - Origin 
		/// </summary>
		[SerializeField]
		public Rect  al_rectMain;

		/// <summary>
		/// - Window Space -
		///  Map Displaying Area - Zoommed
		/// Used for  GUI.BeginGroup. Use with zScaleMatrix
		/// </summary>
		[SerializeField]
		public Rect  al_rectMainZoom;

		/// <summary>
		/// - Map Space -
		///  Map Displaying Area - Zoommed
		/// </summary>
		[SerializeField]
		public Rect  al_rectMainInsideZoom;//inside,
		[SerializeField]
		public float al_scrollBarWidth = 15;
		/// <summary>
		/// Cal Map
		/// </summary>
		protected Vector2 zoomTL
		{
			get{ return al_rectMainInsideZoom.position - mapOutsideOffset;}
		}
		protected Vector2 zoomBR
		{
			get{ return al_rectMainInsideZoom.position + al_rectMainInsideZoom.size - mapOutsideOffset;}
		}

		[SerializeField]
		public float zScale = 1f;
		[SerializeField]
		public float zScaleAll = 1f;
		[SerializeField]
		public Vector2 zScaleCenter;
		[SerializeField]
		public float zScaleMin = 0.5f;
		[SerializeField]
		public float zScaleMax = 3f;

		/// <summary>
		/// The scaled matrix. Scale by mouse wheel. Use with al_rectMainZoom
		/// </summary>
		[SerializeField]
		public Matrix4x4 zScaleMatrix;
		[SerializeField]
		public Matrix4x4 zScaleMatrixDefault;
		[SerializeField]
		public Vector2 zScaleCenterInside;
		[SerializeField]
		protected Vector2 mapTL;
		[SerializeField]
		protected Vector2 mapBR;
		[SerializeField]
		protected Vector2 mapSize;
		[SerializeField]
		protected Material matBase;
		[SerializeField]
		public Vector4 rectShow;
		[SerializeField]
		public SWSlotBox slotBox_left;
		[SerializeField]
		protected	Vector2 mapOutsideOffset;
		protected readonly float mapFloat = 5000;
		protected float mapFloatSmall = 1500;
		[SerializeField]
		protected Vector2 positionLastSize;

		/// <summary>
		/// Half width of map area
		/// </summary>
		protected float xHalf
		{
			get{
				return 0.5f * (position.width - al_leftWidth - al_scrollBarWidth);
			}
		}
		/// <summary>
		/// Half height of map area
		/// </summary>
		protected float yHalf
		{
			get{
				return 0.5f * (position.height - al_topHeight - al_scrollBarWidth);
			}
		}

		public Rect TopElementRect(float left,float right)
		{
			Rect rect = new Rect (left, SWGlobalSettings.UIGap, right - left, al_topHeight - SWGlobalSettings.UIGap * 2);
			return rect;
		}

		public override void InitOnce ()
		{
			base.InitOnce ();
			matBase = new Material(SWEditorUI.GetShader("RectTRS"));
		}

		public override void InitUI ()
		{
			base.InitUI ();
			al_rectMain = new Rect(
				al_leftWidth,
				al_startY + al_topHeight,
				position.width - al_leftWidth-al_scrollBarWidth,
				position.height- al_topHeight-al_scrollBarWidth);

			al_rectMainZoom = new Rect (al_rectMain);
			al_rectMainInsideZoom = new Rect (0, 0, al_rectMainZoom.width, al_rectMainZoom.height);

			zScaleAll = 1;
			zScaleMatrix = Matrix4x4.identity;


			mapTL = new Vector2 (-mapFloat+xHalf, -mapFloat+yHalf);
			mapBR = new Vector2 (mapFloat+xHalf, mapFloat+yHalf);
			mapSize = mapBR - mapTL;

			mapOutsideOffset = Vector2.zero;
		}
		public override void Update ()
		{
			base.Update ();
			if (slotBox_left.repaintDirty) {
				RepaintGetDirty ();
				slotBox_left.repaintDirty = false;
			}
		}


		void UpdateXX()
		{
			rightUpRect = new Rect (
				position.width - rightupWidth - al_scrollBarWidth * 1.8f, 
				al_topHeight + position.height*0f , 
				rightupWidth + al_scrollBarWidth * 0.8f, 
				position.height*1f - al_topHeight - al_scrollBarWidth);
			
			if (position.size != positionLastSize) {
				Vector2 v = al_rectMain.size +  (position.size - positionLastSize);
				Vector2 vl = al_rectMain.size;
				Vector2 _scale = SWCommon.VectorDivide(v,vl);
				MapSizeChange (_scale);
			}

			if (showRightChange) {
				showRightChange = false;
				if (showRight) {
					Vector2 v = al_rectMain.size + new Vector2(-rightupWidth,0);
					Vector2 vl = al_rectMain.size;
					Vector2 _scale = SWCommon.VectorDivide(v,vl);
					MapSizeChange (_scale);
				} else {
					Vector2 v = al_rectMain.size + new Vector2(rightupWidth,0);
					Vector2 vl = al_rectMain.size;
					Vector2 _scale = SWCommon.VectorDivide(v,vl);
					MapSizeChange (_scale);
				}

			}
			positionLastSize = position.size;
		}

		void MapSizeChange(Vector2 _scale)
		{
			al_rectMain.size = SWCommon.VectorMul (al_rectMain.size, _scale);
			al_rectMainZoom.size = SWCommon.VectorMul (al_rectMainZoom.size, _scale);
			al_rectMainInsideZoom.size = SWCommon.VectorMul (al_rectMainInsideZoom.size, _scale);
		}

		public override void OnGUI ()
		{
			// this can be null when start playing
			if (this == null)
			{
				return;
			}
			matBase = new Material(SWEditorUI.GetShader("RectTRS"));
			UpdateXX ();
			base.OnGUI ();


			float x = al_leftWidth / position.width;
			float y = (al_scrollBarWidth) / position.height;
			float z = 1- al_scrollBarWidth/ position.width;
			float w = 1- (al_startY+al_topHeight)/ position.height;
			x = x * 2 - 1;
			y = y * 2 - 1;
			z = z * 2 - 1;
			w = w * 2 - 1;
			rectShow = new Vector4 (x, y, z, w);
			matBase.SetVector ("rectShow", rectShow);





			EditorGUIUtility.labelWidth = SWGlobalSettings.LabelWidth;

			OnGUIBot ();

			DrawMain ();
			GUILayout.Space (5);
			GUILayout.BeginHorizontal (GUILayout.Height(al_topHeight));
			DrawTop ();
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();

			GUILayout.BeginVertical (GUILayout.Width(al_leftWidth));
			DrawLeft ();
			GUILayout.EndVertical ();

			GUILayout.BeginVertical ();
			DrawRight ();
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();

			OnGUITop ();
			EditorGUIUtility.labelWidth = 0f;
			GUIEnd ();
		}
		public virtual void GUIEnd()
		{
			KeyCommandsOut ();
			SWTooltip.Show ();
			so.ApplyModifiedProperties ();
		}

		public virtual void OnGUIBot()
		{
		}
		public virtual void OnGUITop()
		{

		}
		public void DrawRight()
		{
			if (!hasRightup)
				return;
			GUILayout.BeginHorizontal ();
			GUILayout.Space (position.width-rightupWidth-al_scrollBarWidth*1.8f + SWGlobalSettings.UIGap);
			GUILayout.BeginVertical ();

			if (!showRight) {
				rightUpExpandRect = new Rect (position.width - 50 - al_scrollBarWidth * 1f, position.height - 50 - al_scrollBarWidth, 50, 50);
				GUI.DrawTexture(rightUpExpandRect,SWEditorUI.Texture(SWUITex.effectRight1));
				if (rightUpExpandRect.Contains (Event.current.mousePosition)
					&& Event.current.type == EventType.MouseUp) {
					showRight = true;
					showRightChange = true;
					RepaintGetDirty ();
				}
			} else {
				rightUpExpandRect = new Rect (position.width - rightupWidth - 50 - al_scrollBarWidth * 1.8f, position.height - 50 - al_scrollBarWidth, 50, 50);
				GUI.DrawTexture(rightUpExpandRect,SWEditorUI.Texture(SWUITex.effectRight2));
				if (rightUpExpandRect.Contains (Event.current.mousePosition)
					&& Event.current.type == EventType.MouseUp) {
					showRight = false;
					showRightChange = true;
					RepaintGetDirty ();
				}

				DrawRightUp ();
			}
			GUILayout.EndVertical ();
		}

		protected virtual void DrawRightUp()
		{
			
		}

		public virtual void DrawTop()
		{
			DrawBG (new Rect (0, 0, position.width, al_topHeight));
			GUILayout.Space (al_spacing);
		}
		public virtual void DrawLeft()
		{
			DrawBG (new Rect(0,al_topHeight,al_leftWidth,position.height-al_topHeight));
			if (slotBox_left == null)
				return; 
			slotBox_left.regionTarget = al_rectMain;
			slotBox_left.OnGUI ();
		}

		void DrawMain()
		{
			if(this is SWWindowMain)
				GUI.EndGroup ();
			CalZoom ();
			DrawMainBot ();
			InsideDo (DrawMainInside1);
			DrawMainMid ();
			InsideDo (DrawMainInside2);
			DrawMainTop ();
			ScrollBarShowAndCal ();
			if(this is SWWindowMain)
				GUI.BeginGroup (new Rect (0, al_startY, Screen.width, Screen.width));
		}

		void ScrollBarShowAndCal()
		{
			float scrollX = al_rectMainInsideZoom.x-(mapTL.x) - mapOutsideOffset.x;
			float scrollXNew = GUI.HorizontalScrollbar (new Rect (al_rectMain.x,al_startY + position.height - al_scrollBarWidth, position.width - al_rectMain.x, al_scrollBarWidth), scrollX, 
				al_rectMainInsideZoom.width,  0f, mapSize.x);

			float scrollY = al_rectMainInsideZoom.y-(mapTL.y) - mapOutsideOffset.y;
			float scrollYNew = GUI.VerticalScrollbar (new Rect (position.width-al_scrollBarWidth, al_rectMain.y, al_scrollBarWidth, position.height-al_rectMain.y+ 9), scrollY, 
				al_rectMainInsideZoom.height,  0f, mapSize.y);

			if (scrollX != scrollXNew || scrollY!=scrollYNew) {
				Vector2 vv = new Vector2 (scrollXNew - scrollX,scrollYNew - scrollY);
				vv = -vv;
				KeyCmd_DragmoveOut (vv);
				KeyCmd_Dragmove (vv*zScaleAll);
			}
		}

		/// <summary>
		/// Calculate 
		/// </summary>
		void CalZoom()
		{
			bool scroll = Event.current.type == EventType.ScrollWheel;
			zScaleMatrixDefault = GUI.matrix;
			if (scroll && InMap()) {
				RepaintGetDirty ();
				zScale = 1 - Event.current.delta.y*0.05f;
				zScale = Mathf.Clamp (zScale,0.95f, 1.05f);
				if (zScaleAll < zScaleMin && zScale < 1) {
					//	Debug.Log ("small "+zScale);
					return;
				}
				if (zScaleAll > zScaleMax && zScale > 1) {
					///	Debug.Log ("big "+zScale);
					return;
				}


				zScaleCenterInside = zScaleCenter - al_rectMainZoom.position;
				//1 Cal Matrix
				Matrix4x4 t = Matrix4x4.TRS (zScaleCenter, Quaternion.identity, Vector3.one);
				Matrix4x4 s = Matrix4x4.Scale (new Vector3 (zScale, zScale, 1));
				var newzScaleMatrix = zScaleMatrix *t * s * t.inverse;
				//2 Cal RectShow
				Vector2 tl = new Vector2 (al_rectMainZoom.x, al_rectMainZoom.y);
				Vector2 br = new Vector2 (al_rectMainZoom.x + al_rectMainZoom.width, al_rectMainZoom.y + al_rectMainZoom.height);
				tl = GoPoint (tl, zScaleCenter, zScale);
				br = GoPoint (br, zScaleCenter, zScale);

				var newal_rectMainZoom = new Rect (tl.x, tl.y, (br - tl).x, (br - tl).y);

				bool fail = false;
				if (zoomTL.x < mapTL.x)
					fail = true;
				if (zoomTL.y < mapTL.y)
					fail = true;
				if (zoomBR.x > mapBR.x)
					fail = true;
				if (zoomBR.y > mapBR.y)
					fail = true;
				//Cal rectInside
				//Debug.Log ("fail "+fail);
				if (!fail) {
					zScaleAll *= zScale;
					zScaleMatrix = newzScaleMatrix;
					al_rectMainZoom = newal_rectMainZoom;
					SetInsideRects();
					Repaint ();
				}
			}
			al_rectMainInsideZoom = new Rect (0, 0, al_rectMainZoom.width, al_rectMainZoom.height);
		}

		/// <summary>
		/// - Window Space -
		/// </summary>
		public virtual void DrawMainTop()
		{

		}

		/// <summary>
		/// - Window Space -
		/// </summary>
		public virtual void DrawMainBot()
		{

		}

		/// <summary>
		/// - Window Space -
		/// </summary>
		public virtual void DrawMainMid()
		{

		}
		void InsideDo(System.Action act)
		{
			GUI.matrix = zScaleMatrixDefault * zScaleMatrix;
			zScaleCenter = Event.current.mousePosition;
			GUI.BeginGroup (al_rectMainZoom);
			if(act!=null)
				act ();
			GUI.EndGroup ();
			GUI.matrix = zScaleMatrixDefault;
		}

		/// <summary>
		/// - Map Space -
		/// </summary>
		public virtual void DrawMainInside1()
		{

		}

		/// <summary>
		/// - Map Space -
		/// </summary>
		public virtual void DrawMainInside2()
		{
			KeyCommands ();
		}
		#region key command


		public void KeyCommands()
		{
			mousePos = Event.current.mousePosition;
			if(Event.current.type == EventType.KeyDown)
			{
				KeyCmd_HotkeyDown (Event.current.keyCode);
				#if UNITY_EDITOR_WIN
				if(Event.current.keyCode == KeyCode.Delete)
				#else
				if(Event.current.keyCode == KeyCode.Delete || Event.current.keyCode == KeyCode.Backspace)
				#endif
				{
					KeyCmd_Delete ();
				}
			}
			if(Event.current.type == EventType.KeyUp)
			{
				KeyCmd_HotkeyUp (Event.current.keyCode);
				KeyCmd_CopyPaste ();
			}


//			//Depricated:	do it in CalZoom
//			if (Event.current.alt) {
//				if (Event.current.type == EventType.ScrollWheel) {
//					KeyCmd_Scroll (Event.current.delta);
//				} 
//			}

			if(InMap())
			{
				if((Event.current.alt&&SWCommon.GetMouse (0))
					|| SWCommon.GetMouse (1) || SWCommon.GetMouse (2))
				{
					Vector2 move = Event.current.mousePosition - mousePosLast;
					KeyCmd_Dragmove (move);
				}
			}

			if (!Event.current.alt)
				KeyCmd_Select ();



			if (Event.current.rawType == EventType.MouseDown) {
				mousePosDown = Event.current.mousePosition;
				mousePosLast = Event.current.mousePosition;
				mousePressing = true;
			}
			if (Event.current.rawType == EventType.MouseDrag) {
				mousePosLast = Event.current.mousePosition;
			}
			if (Event.current.rawType == EventType.MouseUp) {
				mousePressing = false;
			}
//			if (SWCommon.GetMouseDown (0)) {
//				mousePosDown = Event.current.mousePosition;
//				mousePosLast = Event.current.mousePosition;
//				mousePressing = true;
//			}
//			if (SWCommon.GetMouse (0)) {
//				mousePosLast = Event.current.mousePosition;
//			}
//			if (SWCommon.GetMouseUp (0)) {
//				mousePressing = false;
//			}
		}

		public bool InMapClick(int mouseID)
		{
			return SWCommon.GetMouseUp (mouseID) && (Vector2.Distance (mousePosDown, Event.current.mousePosition) < 3);
		}

		protected virtual bool InMap()
		{
			Rect rect = new Rect (
				al_rectMain.x+al_scrollBarWidth, 
				al_rectMain.y-al_startY, 
				al_rectMain.width-al_scrollBarWidth*2, 
				al_rectMain.height-al_scrollBarWidth+al_startY);
			if (rightUpExpandRect.width > 0) {
				if (showRight) {
					return  rect.Contains (mousePosOut) && !rightUpExpandRect.Contains (mousePosOut) && !rightUpRect.Contains (mousePosOut);
				} else {
					return  rect.Contains (mousePosOut)&& !rightUpExpandRect.Contains (mousePosOut);
				}
			} else {
				return  rect.Contains (mousePosOut);
			}


		}
		public virtual void KeyCmd_Delete()
		{

		}

		public virtual void KeyCmd_Dragmove(Vector2 delta)
		{
			RepaintGetDirty ();
		}
//		public virtual void KeyCmd_Scroll(Vector2 delta)
//		{
//
//		}

		public void KeyCmd_CopyPaste()
		{
			if (EditorGUIUtility.editingTextField)
				return;
			if(Event.current.control)
			{
				if (Event.current.keyCode  == KeyCode.C) {
					//Debug.Log ("Copy");
					Copy ();
				}
				if (Event.current.keyCode  == KeyCode.V) {
					//Debug.Log ("Paste");
					Paste();
				}
			}
		}

		public virtual void Copy()
		{
		}

		public virtual void Paste()
		{
			RepaintGetDirty ();
		}

		public virtual void KeyCmd_Select()
		{
		}

		public virtual void KeyCmd_HotkeyDown(KeyCode code)
		{
			if (code == KeyCode.S && Event.current.alt) {
				if(SWWindowMain.Instance.ProjectIsSaved)
					SWWindowMain.Instance.PressUpdate ();
				else
					SWWindowMain.Instance.PressSave ();
			}
			if (code == KeyCode.O && Event.current.alt) {
				SWWindowMain.Instance.PressOpen ();
			}
		}
		public virtual void KeyCmd_HotkeyUp(KeyCode code)
		{

		}


		public void KeyCommandsOut()
		{

			if (Event.current.alt) {
				if (SWCommon.GetMouse (0)) {
					Vector2 move = Event.current.mousePosition - mousePosOutLast;
					KeyCmd_DragmoveOut (move);
					mousePosOutLast = Event.current.mousePosition;
				}
			}

			if (SWCommon.GetMouseDown (0)) {
				mousePosOutLast = Event.current.mousePosition;
			}
			if (SWCommon.GetMouse (0)) {
				mousePosOutLast = Event.current.mousePosition;
			}
		}


		public void KeyCmd_DragmoveOut (Vector2 delta)
		{
			mapOutsideOffset  += new Vector2 (1f * delta.x, 1f * delta.y);
		}
		#endregion





		public virtual void SetInsideRects()
		{
		}

		public Rect SetInsideRect(Rect rect)
		{
			return GoPointInside (rect, zScaleCenterInside, zScale);
		}
		public Vector2 SetInsidePos(Vector2 pos)
		{
			return GoPointInside (pos, zScaleCenterInside, zScale);
		}
		Rect GoPointInside(Rect rect,Vector2 zScaleCenterInside,float zScale)
		{
			rect.position = GoPointInside(rect.position,zScaleCenterInside,zScale);
			return rect;
		}

		Vector2 GoPointInside(Vector2 p,Vector2 zScaleCenterInside,float zScale)
		{
			return p - zScaleCenterInside + zScaleCenterInside * 1 /zScale;
		}

		Vector2 GoPoint(Vector2 sum,Vector2 zScaleCenter,float zScale)
		{
			return (sum - zScaleCenter) / zScale + zScaleCenter;
		}

		public Rect RectNew(Vector2 center,Vector2 size)
		{
			Rect rect = new Rect ();
			rect.size = size;
			rect.center = center;
			return rect;
		}

		public Rect RectScale(Rect rect,Vector2 zScale)
		{
			Vector2 c = rect.center;
			rect.size = new Vector2(rect.size.x*zScale.x,rect.size.y*zScale.y);
			rect.center = c;
			return rect;
		}


		#region Materials
		public void Set_Material(Material mat,Vector2 t,float r,Vector2 s)
		{
			mat.SetVector ("t",t);
			mat.SetFloat ("r", r);
			mat.SetVector ("s", s);
			mat.SetVector ("rectShow", rectShow);
		}
		public void Set_MaterialMask(Material mat,Vector2 t,float r,Vector2 s,Texture tex)
		{
			Set_Material (mat, t, r, s);
			mat.SetTexture ("_Mask",tex);
		}
		public void Set_MaterialChannel(Material mat,Vector2 t,float r,Vector2 s,SWTexShowChannel channel)
		{
			Set_Material (mat, t, r, s);
			int i = (int)channel;
			mat.SetInt ("cmode", i);
		}

		public void Set_Material(Material mat)
		{
			mat.SetVector ("rectShow", rectShow);
		}

		public void Set_MaterialLoop(Material mat,SWDataNode data)
		{
			//iNormal
			mat.SetInt ("useNormal", data.useNormal ? 1 : 0);

			mat.SetInt ("useLoop", data.effectData.useLoop ? 1 : 0);
			mat.SetFloat ("gapX", data.effectData.gapX);
			mat.SetFloat ("gapY", data.effectData.gapY);
			mat.SetInt ("loopX", (int)data.effectData.loopX);
			mat.SetInt ("loopY", (int)data.effectData.loopY);
		}

		Vector4 AnimationSheet_RectSub(float row,float col,float rowMax,float colMax)
		{
			return new Vector4(col/colMax,row/rowMax,1f/colMax, 1f/rowMax);
		}
		Vector4 AnimationSheet_Rect(int numTilesX,int numTilesY,bool loop,bool singleRow,int rowIndex, int startFrame,float factor)
		{
			int count = singleRow? numTilesX : numTilesX*numTilesY;
			int f = (int)factor;
			if(loop)
				f = (startFrame+f)%count;
			else
				f = Mathf.Clamp((startFrame+f),0,count-1);

			int row = singleRow? rowIndex : (f / numTilesX);
			row = numTilesY - 1 - row;
			int col = singleRow? f : f % numTilesX;
			return  AnimationSheet_RectSub(row,col,numTilesY,numTilesX);
		}

		public void Set_MaterialTextureSheet(Material mat,SWNodeEffector node)
		{
			var data = node.data;
			if (data.effectData.animationSheetUse) {
				Vector4 rect = AnimationSheet_Rect (
					data.effectData.animationSheetCountX,
					data.effectData.animationSheetCountY,
					data.effectData.animationSheetLoop,
					data.effectData.animationSheetSingleRow,
					data.effectData.animationSheetSingleRowIndex,
					data.effectData.animationSheetStartFrame,
					Time.realtimeSinceStartup);

				mat.SetVector ("rectAnimationSheet", rect);
			} else if(node.sprite!=null){
				Rect r = SWCommon.SpriteRect01 (node.sprite);
				mat.SetVector ("rectAnimationSheet", new Vector4(r.x,r.y,r.width,r.height));
			} else {
				mat.SetVector ("rectAnimationSheet", new Vector4(0,0,1,1));
			}
		}
		#endregion

		public bool IsOperatingWindow()
		{
			//performance impact
			//return mouseOverWindow == this;
			return true;
		}
		/// <summary>
		/// For GUILayout
		/// </summary>
		public void Tooltip_Rec(string tip,float leftOff,float rightOff,float yoff =5)
		{
			if (!IsOperatingWindow())
				return;
			SWTooltip.Rec (this,tip,leftOff,rightOff,yoff);
		}

		/// <summary>
		/// For GUILayout
		/// </summary>
		public void Tooltip_Rec(string tip,float yoff =5)
		{
			if (!IsOperatingWindow())
				return;
			SWTooltip.Rec(this,tip,yoff);
		}

		/// <summary>
		/// For GUI.
		/// </summary>
		public void Tooltip_Rec(string _tip,Rect _rect,float yoff =5)
		{
			if (!IsOperatingWindow())
				return;
			SWTooltip.Rec(this,_tip,_rect,yoff);
		}

		private void LayerMaskUpdate(SWNodeBase node)
		{
			var layerMask = node.data.layerMask;
			var ns = SWWindowMain.Instance.NodeAll();
			foreach (var item in ns) {
				if(item.Value.data.id != node.data.id)
					layerMask.AddKey (item.Key);
			}
			for(int i=layerMask.strs.Count-1;i>=0;i--){
				var item = layerMask.strs [i];
				if (!ns.ContainsKey (item)) {
					layerMask.RemoveKey (item);
				}
			}
		}
		protected void ShowLayerMask(SWNodeBase node)
		{
			LayerMaskUpdate (node);
			var layerMask = node.data.layerMask;
			var ns = SWWindowMain.Instance.NodeAll();
			string[] strs = new string[layerMask.strs.Count];
			for (int i = 0; i < strs.Length; i++) {
				strs [i] = ns [layerMask.strs [i]].data.name;
			}


			Rect rect = new Rect (al_leftWidth + 10, al_topHeight + 10, 73, 10);
			var ll = EditorGUI.MaskField (rect, layerMask.mask, strs);
			if (ll != layerMask.mask) {
				SWUndo.Record (node);
				layerMask.mask = ll;
			}
		}
		protected void SetLayerMask(SWNodeBase node)
		{
			LayerMaskUpdate (node);
			if (node.data.layerMask.mask == 0) {
				var par = node.GetParentNodeAllAllList ();
				foreach (var item in par)
					node.data.layerMask.Set (item.data.id, true);
			}
		}
	}
}