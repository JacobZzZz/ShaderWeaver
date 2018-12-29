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
	public class SWWindowDrawRemap : SWWindowLayoutV {
		public static SWWindowDrawRemap Instance;
		public SWDataNodeRemap rData{get{ return node.data.rd;}}


		[SerializeField]
		Rect drawRect;
		[SerializeField]
		SWNodeRemap node;
		[SerializeField]
		Vector2 lastMousePos;
		[SerializeField]
		SWEnumPopup ePopup_texRes;
		public float oWidth{get{return node.texChildOrigin_Width;}}
		public float oHeight{get{return node.texChildOrigin_Height;}}
		public float oMin{get{return Mathf.Min(oWidth,oHeight);}}


		#region dir
		protected static float IconSize_Arrow = 20;
		[SerializeField]
		public Vector2 ArrowOff;
		[SerializeField]
		bool dragging = false;
		#endregion

		#region line
		protected static float IconSize_Center = 10;
		public int brushSize{
			get{ 
				return rData.l.bs;
			}
			set{ 
				rData.l.bs = value;
			}
		}
		public float brushSizeUV
		{
			//lsy : new remap ratio
			get{ return (float)brushSize / oMin;}
			//get{ return (float)brushSize / (float)node.textureEx.width;}
		}
		#endregion



		public static void ShowEditor(SWNodeRemap _node) {
			if (Instance != null)
				Instance.Close ();
			var window =EditorWindow.GetWindow<SWWindowDrawRemap> (true,"Remap");
			window.Init (_node);
			window.InitOnce ();
		}

		#region SerializedProperty
		protected override void SerializedInit ()
		{
			base.SerializedInit ();
			if(slotBox_left!=null)
				slotBox_left.Init (OnSelect);
		}
		#endregion




		public override void OnAfterDeserialize ()
		{
			base.OnAfterDeserialize ();
		}

		public override void Update ()
		{
			Instance = this;
			if (!CanUpdate ())  
				return;
			base.Update ();

			//[Main window close]    or    [related node deleted] ===>  close this window
			if (SWWindowMain.Instance == null || !SWWindowMain.Instance.NodeAll().ContainsKey(node.data.id)) {
				Close ();  
				return;
			}
			SWWindowMain.Instance.NodeAll() [node.data.id] = (SWNodeBase)node;
		}

		public void Init(SWNodeRemap _node)
		{
			hasRightup = false;
			node = _node;
			ePopup_texRes = new SWEnumPopup(new string[]{"128x128","256x256","512x512","1024x1024"},(int)node.data.reso,SWEditorUI.MainSkin.button,
				delegate(int index){
					node.data.reso = (SWTexResolution)index;
					pause = true;
					EditorCoroutineRunner.StartEditorCoroutine (ResizeTex (node.data.reso));
				});
		}

		public override void InitOnce ()
		{
			base.InitOnce ();
		}

		public float XRatio()
		{
			if (oWidth > oMin)
				return oWidth / oMin;
			return 1;
		}
		public float YRatio()
		{
			if (oHeight > oMin)
				return oHeight / oMin;
			return 1;
		}
		public override void InitUI () 
		{
			base.InitUI ();

			//node.lineInfo.baseRect = new Rect (0, 0, 512, 512);
			zScaleMin = 0.6f;
			zScaleMax = 3f;

			//lsy : new remap ratio
			drawRect = new Rect(al_rectMain.width*0.5f-oWidth*0.5f,al_rectMain.height*0.5f-oHeight*0.5f,oWidth,oHeight);
			//drawRect = new Rect(al_rectMain.width*0.5f-size*XRatio()*0.5f,al_rectMain.height*0.5f-size*YRatio()*0.5f,size*XRatio(),size*YRatio());
			//drawRect = new Rect(al_rectMain.width*0.5f-size*0.5f,al_rectMain.height*0.5f-size*0.5f,size,size);
			for (int i = 0; i < node.lineInfo.pts.Count; i++) {
				var pt = node.lineInfo.pts [i];
				pt.mousePos = drawRect.position+new Vector2(pt.uv.x*oMin,pt.uv.y*oMin);
			}



			mapTL = new Vector2 (-mapFloatSmall+xHalf, -mapFloatSmall+yHalf);
			mapBR = new Vector2 (mapFloatSmall+xHalf, mapFloatSmall+yHalf);
			mapSize = mapBR - mapTL;

			List<SWSlot> slotsNodebox = new List<SWSlot> ();
			slotsNodebox.Add(new SWSlot("Drag",SWTipsText.Remap_t_Drag,null,KeyCode.D));
			slotsNodebox.Add(new SWSlot("Line",SWTipsText.Remap_t_Line,null,KeyCode.L));
			slotBox_left = ScriptableObject.CreateInstance<SWSlotBox_Select> ();
			slotBox_left.InitSlot(this,
				new Rect(0,	al_topHeight,	al_leftWidth,	position.height-al_spacing-al_topHeight),
				slotsNodebox,OnSelect,new Vector2(al_leftWidth,al_leftWidth*0.6f),(int)rData.mode);
		}

		void OnSelect(SWSlot slot,Vector2 mp)
		{
			rData.mode = (DrawRemapMode)slotBox_left.selection; 
		}

		public override void Clean ()
		{
			base.Clean ();
		}


		public override void DrawTop ()
		{
			Rect llRect = new Rect (0, 0, 0, 0);
			base.DrawTop ();
			if ( (DrawRemapMode)slotBox_left.selection == DrawRemapMode.dir) {
				var tmp = SWEditorUI.Vector2Field ("Drag:", rData.d.v);
				if (tmp !=  rData.d.v) {
					SWUndo.Record (this);
					rData.d.v = tmp;
				}
				Tooltip_Rec (SWTipsText.Remap_Drag,TopElementRect(0,190));

				GUILayout.Space (al_spacingBig);
				GUILayout.Label ("Deviation:", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
				llRect = GUILayoutUtility.GetLastRect ();
				var tmpPixelBack = EditorGUILayout.IntSlider("",rData.d.pb,-20,20,
					GUILayout.Width(SWGlobalSettings.SliderWidth) );
				if(tmpPixelBack!= rData.d.pb)
				{
					SWUndo.Record (this);
					rData.d.pb = tmpPixelBack;
				}
				Tooltip_Rec (SWTipsText.Remap_Deviation,-llRect.width-10,0);


				GUILayout.Space (al_spacingBig);
				GUILayout.Label ("Precise:",
					SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
				Tooltip_Rec (SWTipsText.Remap_Precise,0,25);
				var p = GUILayout.Toggle(rData.d.pre,"",GUILayout.Width(80));
				if (p != rData.d.pre) {
					SWUndo.Record (node.lineInfo);
					rData.d.pre = p;
				}
			} else {
				GUILayout.Label ("Size:", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
				llRect = GUILayoutUtility.GetLastRect ();
				var tmpBrushSize = EditorGUILayout.IntSlider("",brushSize,1,60,
					GUILayout.Width(SWGlobalSettings.SliderWidth) );
				if(tmpBrushSize!= brushSize)
				{
					SWUndo.Record (this);
					brushSize = tmpBrushSize;
				}
				Tooltip_Rec (SWTipsText.Remap_Size,-llRect.width-10,0);

				GUILayout.Space (al_spacingBig);
				GUILayout.Label ("Stitch:",
					SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
				Tooltip_Rec (SWTipsText.Remap_Stitch,0,25);
				var stitch = GUILayout.Toggle(rData.l.st,"",GUILayout.Width(80));
				if (stitch != rData.l.st) {
					SWUndo.Record (node.lineInfo);
					rData.l.st = stitch;
				}
			}


			Rect btRect = TopElementRect(position.width - 100,position.width-SWGlobalSettings.UIGap);
			ePopup_texRes.Show (btRect);
			Tooltip_Rec (SWTipsText.Remap_TexSize, btRect);
		}

		IEnumerator ResizeTex(SWTexResolution temp)
		{
			yield return new WaitForSeconds (0.1f);
			bool b = EditorUtility.DisplayDialog ("Warning", "Change resolution may loss quality,continue?", "Yes","No");
			pause = false;
			if (b) {
				//SWUndo.Record (node);
				node.data.reso = temp;

				EditorUtility.DisplayProgressBar ("Texture resizing", "Please wait", 0.5f);
				node.UpdateTex ();
				SWTextureProcess.TextureResize(node.textureEx, node.data.resolution);
				EditorUtility.ClearProgressBar ();
			}
		}


		public override void SetInsideRects ()
		{
			base.SetInsideRects ();
			drawRect = SetInsideRect (drawRect);

			for (int i = 0; i < node.lineInfo.pts.Count; i++) {
				node.lineInfo.pts [i].mousePos = SetInsidePos (node.lineInfo.pts [i].mousePos);
			}
		}
		public override void DrawMainInside1 ()
		{
			base.DrawMainInside1 ();
			Draw ();
		}
		public override void DrawMainBot ()
		{
			base.DrawMainBot ();
			SWEditorTools.DrawTiledTexture(al_rectMain, SWEditorTools.backdropTexture);
		}



		void Draw()
		{
			////		Don't need,it looks OK
			//			if(pause)
			//				return;
			GUI.DrawTexture (drawRect,node.texChildResized.Texture,ScaleMode.StretchToFill);
			GUI.DrawTexture (drawRect,node.textureEx.Texture,ScaleMode.StretchToFill);
			if ( (DrawRemapMode)slotBox_left.selection == DrawRemapMode.dir)
				DrawDir ();
			else
				DrawLine ();

			DrawFrame (drawRect);
		}

		void DrawDir()
		{
			Cursor.visible = true;

			var rect = SWCommon.GetRect (drawRect.center + ArrowOff,new Vector2(IconSize_Arrow,IconSize_Arrow));
			Set_Material (matBase,Vector2.zero,0,Vector2.one);
			matBase.SetColor ("_Color", dragging ? Color.green:Color.white);
			Graphics.DrawTexture(SWCommon.GetRect (drawRect.center,new Vector2(8,8)),
				SWEditorUI.Texture(SWUITex.effectCenter),matBase);

			matBase.SetFloat ("r", -SWCommon.Vector2Angle(ArrowOff));
			Graphics.DrawTexture(rect,SWEditorUI.Texture(SWUITex.effectArrow),matBase);


			matBase.SetFloat ("r", 0f);
			matBase.SetTexture ("_MainTex", SWEditorUI.Texture (SWUITex.effectLine));
			SWDraw.DrawLine (drawRect.center, rect.center, Color.white,3f,matBase);

			float factor = 1f / drawRect.size.x; 
			if (SWCommon.GetMouseDown (0) && InMap()) {
				RepaintGetDirty ();
				if (rect.Contains (Event.current.mousePosition)) {
					dragging = true;
					lastMousePos = Event.current.mousePosition;
				} else
					dragging = false;
			} else if (dragging) {
				if (SWCommon.GetMouse (0) && InMap()) {
					RepaintGetDirty ();
					ArrowOff += Event.current.mousePosition - lastMousePos;

					if (Event.current.shift) {
						if (Mathf.Abs(ArrowOff.x) > Mathf.Abs(ArrowOff.y))
							ArrowOff = new Vector2 (ArrowOff.x, 0);
						else
							ArrowOff = new Vector2 (0,ArrowOff.y);
					}

					lastMousePos = Event.current.mousePosition;
					SWUndo.Record (this);
					rData.d.v = new Vector2 (ArrowOff.x * factor, -ArrowOff.y * factor);
				} 
				if (SWCommon.GetMouseUp (0)) {
					dragging = false;
					SWUndo.Record (this);
					rData.d.v = new Vector2 (ArrowOff.x * factor, -ArrowOff.y * factor);
				}
			} else {
				ArrowOff = new Vector2( rData.d.v.x / factor,- rData.d.v.y / factor);
			}



			if (Event.current.type == EventType.KeyDown) {
				if (Event.current.keyCode == KeyCode.Return) {
					node.data.dirty = true;
					SWTextureProcess.ProcessRemap_Dir (node.textureEx,node.texChildResized, new Vector2( rData.d.v.x,- rData.d.v.y),rData.d.pre,rData.d.pb);
				}
			}
		}


		void LinePlaceUpdate()
		{
			//Add Point
			//if (SWCommon.GetMouseDown (0) && al_rectMain.Contains(mousePosOut+new Vector2(0,al_startY))) {
			if (SWCommon.GetMouseDown (0) && InMap() ) {
				if (!al_rectMain.Contains(mousePosOut + new Vector2(0,al_startY)))
					return;
				if (!drawRect.Contains (mousePos)) {
					return;
				}

				SWUndo.Record (node.lineInfo);

				RemapWayPoint pt = new RemapWayPoint ();
				UpdateRemapWayPoint (pt, Event.current.mousePosition);
				pt.matArrow = new Material (SWEditorUI.GetShader ("RectTRS"));
				node.lineInfo.pts.Add (pt);
			}
		}

		void UpdateRemapWayPoint(RemapWayPoint pt,Vector2 mPos)
		{
			Vector2 m = mPos - drawRect.position;
			//lsy : new remap ratio
			var uv = SWTextureProcess.TexUV_NoClamp (oMin, oMin, (int)m.x, (int)m.y);

			pt.uv = uv;
			pt.mousePos = mPos;
		}

		void DrawLine()
		{
			if (node.lineInfo.pts.Count <= 2)
				rData.l.st = false;

			RepaintGetDirty ();
			if (LineEditMode ()) {
				LineEditUpdate ();
			} else {
				LinePlaceUpdate ();
			}
			DrawPoints ();

			//Draw Cursor
			if (focusedWindow == this) {
				if (al_rectMain.Contains(mousePosOut + new Vector2(0,al_startY)) && drawRect.Contains (mousePos)) {
					if (LineEditMode()) {
						Cursor.visible = true;
					} else {
						GUI.DrawTexture (new Rect (mousePos.x - brushSize * 1f, mousePos.y - brushSize * 1f, brushSize * 2f, brushSize * 2f), SWEditorUI.Texture (SWUITex.cursor));
						GUI.DrawTexture (new Rect (mousePos.x - 8, mousePos.y - 8, 16, 16), SWEditorUI.Texture (SWUITex.cursorCenter));
						Cursor.visible = false;
					}
				} else {
					Cursor.visible = true;
				}
			}
			//Key Command
			if (Event.current.type == EventType.KeyDown) {
				if (Event.current.keyCode == KeyCode.Return) {
					if (node.lineInfo.pts.Count <= 1)
						return;
					node.data.dirty = true;
					SWTextureProcess.ProcessRemap_Line (node.textureEx, node.lineInfo, brushSizeUV);
				}


				if (Event.current.keyCode == KeyCode.C) {
					SWUndo.Record (node.lineInfo);
					node.lineInfo.pts.Clear ();
				}
			}
		}






		void DrawPoints()
		{
			for (int i = 0; i < node.lineInfo.pts.Count; i++) {
				if (i < node.lineInfo.pts.Count - 1 || rData.l.st) {
					Vector2 v1 = node.lineInfo.pts [i].mousePos;
					Vector2 v2 = node.lineInfo.pts [(i + 1) % node.lineInfo.pts.Count].mousePos;

					Vector2 dir = (v2 - v1);
					float dis = dir.magnitude;
					dir.Normalize ();
					float angle = SWCommon.Vector2Angle (v2 - v1);


					Set_Material (node.lineInfo.pts [i].matArrow, Vector2.zero, -angle, Vector2.one);


					var item = new Material (SWEditorUI.GetShader ("Rect"));
					Set_Material (item);

					Vector2 mid = (v2 + v1)*0.5f;
					Graphics.DrawTexture (SWCommon.GetRect (mid, new Vector2(IconSize_Arrow,IconSize_Arrow)), SWEditorUI.Texture (SWUITex.effectArrow),node.lineInfo.pts [i].matArrow);
					SWDraw.DrawLine (v1, v2 , Color.white, 1, item);
				}
				GUI.DrawTexture (new Rect (node.lineInfo.pts [i].mousePos.x - brushSize * 1f, node.lineInfo.pts [i].mousePos.y - brushSize * 1f, brushSize * 2f, brushSize * 2f), 
					SWEditorUI.Texture(SWUITex.cursor));
			}


			if (LineEditMode ()) {
				LineEdit_Draw ();
			}
		}

		public override void KeyCmd_HotkeyDown (KeyCode code)
		{
			base.KeyCmd_HotkeyDown (code);
			if (rData.mode == DrawRemapMode.dir) {
				if (code == KeyCode.LeftBracket) {
					SWUndo.Record (this);
					rData.d.pb += -1;
				}
				if (code == KeyCode.RightBracket) {
					SWUndo.Record (this);
					rData.d.pb += 1;
				}
			} else {
				if (code == KeyCode.LeftBracket) {
					SWUndo.Record (this);
					brushSize += -1;
				}
				if (code == KeyCode.RightBracket) {
					SWUndo.Record (this);
					brushSize += 1;
				}
			}
		}

		public override void KeyCmd_Dragmove (Vector2 delta)
		{
			//		Debug.Log (delta);
			base.KeyCmd_Dragmove (delta);
			drawRect.position += new Vector2 (1f * delta.x, 1f * delta.y);

			for (int i = 0; i < node.lineInfo.pts.Count; i++) {
				node.lineInfo.pts[i].mousePos += new Vector2 (1f * delta.x, 1f * delta.y);
			}
		}

		public override void KeyCmd_Delete ()
		{
			base.KeyCmd_Delete ();
			SWUndo.Record (node.lineInfo);
			if (node.textureEx == null) {
				node.textureEx = SWCommon.TextureCreate (node.data.resolution, node.data.resolution, TextureFormat.ARGB32); 
			} else {
				SWTextureProcess.ProcessTexture_Clean (node.textureEx);
			}
			RepaintGetDirty ();
		}



		#region line point edit
		bool LineEditMode()
		{
			return Event.current.control || Event.current.shift || Event.current.alt;
		}

		void LineEdit_Draw()
		{
			for (int i = 0; i < node.lineInfo.pts.Count; i++) {
				var pt = node.lineInfo.pts [i];
				var rect = SWCommon.GetRect (pt.mousePos, new Vector2 (IconSize_Center, IconSize_Center));
				GUI.DrawTexture (rect, SWEditorUI.Texture (SWUITex.effectPos));
			}
		}

		RemapWayPoint editingPoint;
		void LineEditUpdate()
		{
			if (!InMap ())
				return;
			if (!al_rectMain.Contains(mousePosOut + new Vector2(0,al_startY)))
				return;
			if (!drawRect.Contains (mousePos)) {
				return;
			}
			RemapWayPoint cp = CloestPt (mousePos);

			if (SWCommon.GetMouseDown (0)) {
				editingPoint = cp; 
			}
			if (SWCommon.GetMouse (0)) {
				if (editingPoint != null) {
					SWUndo.Record (node.lineInfo);
					UpdateRemapWayPoint (editingPoint, Event.current.mousePosition);
				}
			}
			if (SWCommon.GetMouseUp (0)) {
				if (editingPoint != null) {
					SWUndo.Record (node.lineInfo);
					UpdateRemapWayPoint (editingPoint, Event.current.mousePosition);
				}
				editingPoint = null;
			}

			if (SWCommon.GetMouseUp (1)) {
				if (cp != null) {
					SWUndo.Record (node.lineInfo);
					node.lineInfo.pts.Remove (cp);
				}
			}
		}

		RemapWayPoint CloestPt(Vector2 v)
		{
			RemapWayPoint cp = null;
			float minDis = float.MaxValue;
			for (int i = 0; i < node.lineInfo.pts.Count; i++) {
				RemapWayPoint p = node.lineInfo.pts[i];
				float dis = Vector2.Distance (v, p.mousePos);
				if (dis < minDis) {
					minDis = dis;
					cp = p;
				}
			}

			if (minDis < IconSize_Center)
				return cp;
			return null;
		}
		#endregion
	}
}

