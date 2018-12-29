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
	public enum SWBrushMode
	{
		brush=0,
		erase=1,
		wand=2,
		dropper=3
	}

	[System.Serializable]
	public class SWBrush
	{
		/// <summary>
		/// The size. Radius 
		/// </summary>
		[SerializeField]
		public int size = 10;
		[SerializeField]
		public Color color = Color.white;
		[SerializeField]
		public bool[] mask;
		[SerializeField]
		public SWBrushMode mode = SWBrushMode.brush;
		[SerializeField]
		public float trans = 1;
	}

	[System.Serializable]
	public class SWWindowDrawMask:SWWindowLayoutV{
		public static SWWindowDrawMask Instance;
		[SerializeField]
		public string nodeEditing;
		[SerializeField]
		public SWNodeMask node;
		[SerializeField]
		public SWBrush brush;
		[SerializeField]
		Vector2 lastUV;
		[SerializeField]
		Vector2 uv;
		[SerializeField] 
		Rect drawRect;
		[SerializeField]
		Rect previewRect;
		[SerializeField]
		int tolerance = 10;
		[SerializeField]
		bool hasFirstPoint;



		[SerializeField]
		SWEnumPopup ePopup_texRes;

		int texWidth
		{
			get{ 
				return node.texMask.width;
			}
		}
		int texHeight
		{
			get{ 
				return node.texMask.height;
			}
		}

		public static void ShowEditor(SWNodeMask node) {
			if (Instance != null)
				Instance.Close ();
			var window = EditorWindow.GetWindow<SWWindowDrawMask> (true,"Mask");
			window.Init (node);
			window.InitOnce (); 
		}

		#region SerializedProperty
		SerializedProperty spBrushsize;
		SerializedProperty spBrushTrans;
		SerializedProperty spTolerance;

		static readonly int BrushSizeMin =1;
		static readonly int BrushSizeMax =100;

		protected override void SerializedInit ()
		{
			base.SerializedInit ();
			spBrushsize = so.FindProperty ("brush.size");
			spBrushTrans = so.FindProperty ("brush.trans");  
			spTolerance = so.FindProperty ("tolerance");

			if(slotBox_left!=null)
				slotBox_left.Init (OnSelect);
		}
		#endregion

		public override void OnAfterDeserialize ()
		{
			base.OnAfterDeserialize ();
		}


		public override void Awake ()
		{
			base.Awake ();
			Instance = this;
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
		}  


		public void Init(SWNodeMask _node)
		{
			hasRightup = false;
			node = _node;
			SetLayerMask (node);

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
			brush = new SWBrush ();
			brush.size = 30;
			brush.mode = SWBrushMode.brush;
			brush.color = new Color (0, 1, 0, 1);
			brush.mask = new bool[]{true,true,true,true};

		}
		public override void InitUI () 
		{
			base.InitUI ();

			//Setting for scrolling, prevent problem when zScaleMin is too small
			mapFloatSmall = Mathf.Clamp(texWidth * 3,512*3,10000);
			if (texWidth == 256) {
				zScaleMin = 0.8f;
				zScaleMax = 3f;
			}
			else if (texWidth == 512) {
				zScaleMin = 0.8f;
				zScaleMax = 3f;
			}
			else if (texWidth == 1024) {
				zScaleMin = 0.5f;
				zScaleMax = 3f;
			}
			else if (texWidth == 2048) {
				zScaleMin = 0.3f;
				zScaleMax = 3f;
			}



			drawRect = new Rect(al_rectMain.width*0.5f-texWidth*1f,al_rectMain.height*0.5f-texHeight*0.5f,texWidth,texHeight);
			previewRect = new Rect (drawRect.x+drawRect.width,drawRect.y, drawRect.width, drawRect.height);

			mapTL = new Vector2 (-mapFloatSmall+xHalf, -mapFloatSmall+yHalf);
			mapBR = new Vector2 (mapFloatSmall+xHalf, mapFloatSmall+yHalf);
			mapSize = mapBR - mapTL;


			List<SWSlot> slotsNodebox = new List<SWSlot> ();
			slotsNodebox.Add(new SWSlot("Brush",SWTipsText.Mask_t_Brush,null,KeyCode.B));
			slotsNodebox.Add(new SWSlot("Eraser",SWTipsText.Mask_t_Eraser,null,KeyCode.E));
			slotsNodebox.Add(new SWSlot("Wand",SWTipsText.Mask_t_Wand,null,KeyCode.W));
			slotsNodebox.Add(new SWSlot("Dropper",SWTipsText.Mask_t_Dropper,null,KeyCode.D));
			slotBox_left = ScriptableObject.CreateInstance<SWSlotBox_Select> ();
			slotBox_left.InitSlot(this,
				new Rect(0,	al_topHeight,	al_leftWidth,	position.height-al_spacing-al_topHeight),
				slotsNodebox,OnSelect,new Vector2(al_leftWidth,al_leftWidth*0.6f));
		}

		void OnSelect(SWSlot slot,Vector2 mp)
		{
		}

		public override void Clean ()
		{
			base.Clean ();
		}

	
		public override void OnGUITop ()
		{
			base.OnGUITop ();
			if (Event.current.control) {
				ShowLayerMask (node);
			}
		}
		public override void DrawTop ()
		{
			Rect llRect = new Rect (0, 0, 0, 0);
			base.DrawTop ();
			if (brush.mode == SWBrushMode.wand || brush.mode == SWBrushMode.dropper) {
				GUILayout.Label ("Tolerance:", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
				llRect = GUILayoutUtility.GetLastRect ();
				EditorGUILayout.IntSlider (spTolerance,0,255,"",GUILayout.Width(SWGlobalSettings.SliderWidth) );
				Tooltip_Rec (SWTipsText.Mask_Tolerance,-llRect.width-10,0);
			}
			else {
				GUILayout.Label ("Size:", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
				llRect = GUILayoutUtility.GetLastRect ();
				EditorGUILayout.IntSlider (spBrushsize, BrushSizeMin, BrushSizeMax, "", GUILayout.Width (SWGlobalSettings.SliderWidth));
				Tooltip_Rec (SWTipsText.Mask_Size,-llRect.width-10,0);


				GUILayout.Space (al_spacingBig);
				GUILayout.Label ("Opacity:", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight));
				llRect = GUILayoutUtility.GetLastRect ();
				EditorGUILayout.Slider (spBrushTrans, 0, 1, "", GUILayout.Width (SWGlobalSettings.SliderWidth));
				Tooltip_Rec (SWTipsText.Mask_Opacity,-llRect.width-10,0);
			}

			GUI.color = SWEditorUI.ColorPalette (SWColorPl.light);



		
			Rect btRect2 = TopElementRect(position.width - 200,position.width-100-SWGlobalSettings.UIGap);
			ePopup_texRes.Show (btRect2);
			Tooltip_Rec (SWTipsText.Mask_TexSize,btRect2);

			Rect btRect = TopElementRect(position.width - 100,position.width-SWGlobalSettings.UIGap);
			if (GUI.Button (btRect, "Invert",SWEditorUI.MainSkin.button))
				SWTextureProcess.ProcessMask_Invert (node.texMask);
			GUI.color = Color.white;
			Tooltip_Rec (SWTipsText.Mask_Invert, btRect);
		}

		IEnumerator ResizeTex(SWTexResolution temp)
		{
			yield return new WaitForSeconds (0.2f);
			bool b = EditorUtility.DisplayDialog ("Warning", "Change resolution may loss quality,continue?", "Yes","No");
			pause = false;
			if (b) {
				//SWUndo.Record (node);
				node.data.reso = temp;

				EditorUtility.DisplayProgressBar ("Texture resizing", "Please wait", 0.5f);
				SWTextureProcess.TextureResize(node.texMask, node.data.resolution);

				InitUI ();
				EditorUtility.ClearProgressBar ();
			}
		}

		public override void DrawMainBot ()
		{
			base.DrawMainBot ();
			SWEditorTools.DrawTiledTexture(al_rectMain, SWEditorTools.backdropTexture);
		}

		public override void SetInsideRects ()
		{
			base.SetInsideRects ();
			drawRect = SetInsideRect (drawRect);
			previewRect = SetInsideRect (previewRect);
		}

		public override void DrawMainInside1 ()
		{
			base.DrawMainInside1 ();
			Draw ();
			Op ();
		}

		void Draw()
		{
			if(pause)
				return;
			Repaint ();
			if (focusedWindow == this) {
				ShowBG_Right ();
				if (brush.mode == SWBrushMode.wand || brush.mode == SWBrushMode.dropper) {
					ShowBG_Root ();
				} else {
					//Draw textures(choosed in laymask) normally except mask nodes
					ShowBG_Textures ();
					//Draw maskes(choosed in laymask)
					ShowBG_Masks ();
				}


				GUI.color = new Color (1, 1, 1, 0.5f);
				GUI.DrawTexture (drawRect,node.texMask.Texture,ScaleMode.StretchToFill);
				GUI.color = new Color (1, 1, 1, 1f);

				if (al_rectMain.Contains(mousePosOut + new Vector2(0,al_startY)) &&  drawRect.Contains (mousePos)) {
					float size = brush.size * 1f;
					Rect outRect = new Rect (mousePos.x - size, mousePos.y - size, size * 2f, size * 2f);
					size = 6f;
					size = Mathf.Min (size, brush.size);

					Rect innerRect = new Rect (mousePos.x - size, mousePos.y - size, size * 2f, size * 2f);
					size = 16;
					Rect innerRectBig = new Rect (mousePos.x - size, mousePos.y - size, size * 2f, size * 2f);
					if (brush.mode == SWBrushMode.wand) {
						GUI.DrawTexture (innerRectBig, SWEditorUI.Texture(SWUITex.cursorWand));
					} else if (brush.mode == SWBrushMode.dropper) {
						GUI.DrawTexture (innerRectBig, SWEditorUI.Texture(SWUITex.cursorDropper));
					} else {
						Texture2D icon = SWEditorUI.Texture (SWUITex.cursor);
						Texture2D iconCenter = SWEditorUI.Texture (SWUITex.cursorCenter);
						GUI.DrawTexture (outRect, icon);
						GUI.DrawTexture (innerRect, iconCenter);

						Rect outRectRight = SWCommon.GetRect (outRect.center + new Vector2(drawRect.width,0), outRect.size);
						GUI.DrawTexture (outRectRight, icon);
					}
					Cursor.visible = false;
				} else { 
					Cursor.visible = true;   
				}

			}

			DrawFrame (drawRect);
			DrawFrame (previewRect);
		}
		void ShowBG_Right()
		{
			var dic = node.GetParentNodeAllAll ();
			List<SWNodeBase> parentNodes = new List<SWNodeBase> ();
			foreach (var item in dic)
				parentNodes.Add (item.Value);
			parentNodes.Reverse ();

			foreach (var item in parentNodes) {
				if (item is SWNodeMask)
					continue;
				var tex = item.Texture;
				if (tex == null)
					continue;
				Material matBotMasked = new Material (SWEditorUI.GetShader ("RectTRSMask"));
				Set_MaterialMask (matBotMasked, -item.data.effectData.t_startMove, item.data.effectData.r_angle, item.data.effectData.s_scale, node.texMask.Texture);
				Graphics.DrawTexture (previewRect,tex,matBotMasked);
			}
			GUI.Label (new Rect(previewRect.x+previewRect.width - 100-30,previewRect.y+5,100,20), "Masked Area Preview",SWEditorUI.Style_Get(SWCustomStyle.eTxtLight));
		}
	
		void ShowBG_Root()
		{
			if (SWWindowMain.Instance.nRoot.texture != null) {
				var item = SWWindowMain.Instance.nRoot;
				var tex = item.Texture;
				if (tex == null)
					return;
				Material matBot = new Material (SWEditorUI.GetShader ("RectTRS"));
				Set_Material (matBot, -item.data.effectData.t_startMove, item.data.effectData.r_angle, item.data.effectData.s_scale);
				Graphics.DrawTexture (drawRect, tex, matBot);
			}
		}

		void ShowBG_Textures()
		{
			foreach (var tempNode in SWWindowMain.Instance.nodes) {
				if (tempNode.data.id == node.data.id)
					continue;
				if (tempNode is SWNodeMask)
					continue;
				var tex = tempNode.Texture;
				if (tex == null)
					continue;
				if (!node.data.layerMask.Has (tempNode.data.id))
					continue;

				var dt = tempNode.data;
				Material matBot = new Material (SWEditorUI.GetShader ("RectTRS"));
				Set_Material (matBot, -dt.effectData.t_startMove, dt.effectData.r_angle, dt.effectData.s_scale);
				Set_MaterialLoop (matBot,dt);
				Graphics.DrawTexture (drawRect, tex, matBot);
			}
		}

		void ShowBG_Masks()
		{
			foreach (var tempNode in SWWindowMain.Instance.nodes) {
				if (tempNode.data.id == node.data.id)
					continue;
				if (!(tempNode is SWNodeMask))
					continue;
				if (!node.data.layerMask.Has (tempNode.data.id))
					continue;
				var dt = tempNode.data;
				SWNodeMask nodeMask = (SWNodeMask)tempNode;
				Material matBot = new Material (SWEditorUI.GetShader ("RectTRSPremask"));
				Set_Material (matBot, -dt.effectData.t_startMove, dt.effectData.r_angle, dt.effectData.s_scale);
				Graphics.DrawTexture (drawRect, nodeMask.texMask.Texture, matBot);
			}
		}

		void Op()
		{
			//			Debug.Log (Event.current.type);
			brush.mode = (SWBrushMode)slotBox_left.selection;
			if (!al_rectMain.Contains(mousePosOut + new Vector2(0,al_startY)))
				return;
			if (Event.current.control || Event.current.alt)
				return;
			if (!drawRect.Contains (mousePos)) {
				return;
			}
			if (brush.mode == SWBrushMode.dropper) {
				if (SWWindowMain.Instance.nRoot.texture != null &&
					SWCommon.GetMouseDown (0, false) && al_rectMain.Contains (mousePosOut + new Vector2 (0, al_startY))) {
					CalUV ();
					SWTexture2DEx _tex = SWTextureProcess.TextureResize (SWWindowMain.Instance.nRoot.texture, texWidth, texHeight);
					SWTextureProcess.ProcessMask_Dropper (node.texMask, _tex, uv, brush, tolerance);
					lastUV = uv;
					node.data.dirty = true;
					hasFirstPoint = false;
				}
			} else if (brush.mode == SWBrushMode.wand) {
				if (SWWindowMain.Instance.nRoot.texture != null &&
					SWCommon.GetMouseDown (0, false) && al_rectMain.Contains (mousePosOut + new Vector2 (0, al_startY))) {
					CalUV ();
					SWTexture2DEx _tex = SWTextureProcess.TextureResize (SWWindowMain.Instance.nRoot.texture, texWidth, texHeight);
					SWTextureProcess.ProcessMask_Wand (node.texMask, _tex, uv, brush, tolerance);
					lastUV = uv;
					node.data.dirty = true;
					hasFirstPoint = false;
				}
			} else if (brush.mode == SWBrushMode.brush || brush.mode == SWBrushMode.erase) {
				if (SWCommon.GetMouseDown (0,false) && al_rectMain.Contains(mousePosOut+new Vector2(0,al_startY))) {
					if (Event.current.shift && hasFirstPoint)
						GoLine ();
					else
						GoPoint ();
				}
				else if (SWCommon.GetMouse(0)) {
					GoLine ();
				}
			}	
		}

		void GoPoint()
		{
			hasFirstPoint = true;
			CalUV ();
			SWTextureProcess.ProcessMask_DrawPoint (node.texMask, uv,brush);
			lastUV = uv;
			node.data.dirty = true;
		}
		void GoLine()
		{
			CalUV ();
			SWTextureProcess.ProcessMask_DrawLine (node.texMask,lastUV, uv,brush);
			lastUV = uv;
			node.data.dirty = true;
		}

		void CalUV()
		{
			uv = Event.current.mousePosition;
			//deviation:draw point is a little bit looks like below mouse center
			uv += new Vector2 (0, -1f);
			uv = uv - drawRect.position;
			SWTexThread_Tex.centerX = Mathf.RoundToInt(uv.x);
			SWTexThread_Tex.centerY = Mathf.RoundToInt(uv.y);
			uv= SWTextureProcess.TexUV (texWidth,texHeight, uv.x, uv.y);
		}

		public override void KeyCmd_HotkeyDown (KeyCode code)
		{
			base.KeyCmd_HotkeyDown (code);

			if (brush.mode == SWBrushMode.brush || brush.mode == SWBrushMode.erase) {
				if (code == KeyCode.LeftBracket) {
					SWUndo.Record (this);
					brush.size += -1;
					brush.size = Mathf.Clamp (brush.size, BrushSizeMin, BrushSizeMax);

				}
				if (code == KeyCode.RightBracket) {
					SWUndo.Record (this);
					brush.size += 1;
					brush.size = Mathf.Clamp (brush.size, BrushSizeMin, BrushSizeMax);
				}

				if (code == KeyCode.Equals) {
					SWUndo.Record (this);
					brush.trans += 0.01f;
					brush.trans = Mathf.Clamp (brush.trans, 0f, 1f);

				}
				if (code == KeyCode.Minus) {
					SWUndo.Record (this);
					brush.trans += -0.01f;
					brush.trans = Mathf.Clamp (brush.trans, 0f, 1f);
				}
			} else {
				if (code == KeyCode.LeftBracket) {
					SWUndo.Record (this);
					tolerance += -1;
					tolerance = Mathf.Clamp (tolerance, 0, 255);

				}
				if (code == KeyCode.RightBracket) {
					SWUndo.Record (this);
					tolerance += 1;
					tolerance = Mathf.Clamp (tolerance, 0, 255);
				}
			}
		}

		public override void KeyCmd_Dragmove (Vector2 delta)
		{
			//		Debug.Log (delta);
			base.KeyCmd_Dragmove (delta);
			drawRect.position += new Vector2 (1f * delta.x, 1f * delta.y);
			previewRect.position += new Vector2 (1f * delta.x, 1f * delta.y);
		}

		public override void KeyCmd_HotkeyUp (KeyCode code)
		{
			base.KeyCmd_HotkeyUp (code);

			if (code == KeyCode.I && Event.current.control)
				SWTextureProcess.ProcessMask_Invert (node.texMask);

		}

		public override void KeyCmd_Delete ()
		{
			base.KeyCmd_Delete ();
			SWTextureProcess.ProcessTexture_Clean (node.texMask);
		}
	}
}