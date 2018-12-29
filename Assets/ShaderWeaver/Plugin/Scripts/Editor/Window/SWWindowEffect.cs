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
	public enum SWEffectWindowEditMode
	{
		Basic = 0,
		Move,
		Spin,
		Radial,
		UV,
	}
	[System.Serializable]
	public enum SWEffectWindowOp
	{
		none,

		scaleX,
		scaleY,
		pos,
		angle,

		Move,
		Radial,
		Spin,
		UVR,
		UVG,
		UVB,
		UVA,
	}

	[System.Serializable]
	public class SWBaseInfo:ScriptableObject
	{
		[SerializeField]
		public SWNodeEffector effector;
		[SerializeField]
		public SWWindowEffect window;
		[SerializeField]
		public Rect baseRect;//
		[SerializeField]
		public float angle;//
		[SerializeField]
		public Rect imageRect;//
		[SerializeField]
		public Vector2 mousePosRotated;
		[SerializeField]     
		public Vector2 mousePosRotatedLast;
		[SerializeField]
		public Vector2 movement;
		[SerializeField]
		public Vector2 mousePos;
		[SerializeField]
		public Vector2 mousePosLast;

		public virtual void OnEnable()
		{
			hideFlags = HideFlags.DontSave;
		}
	}
	[System.Serializable]
	public class OpTool:ScriptableObject
	{
		[SerializeField]
		public Rect rect;
		[SerializeField]
		protected float boxSize = 20f; 
		[SerializeField]
		protected float centerSize = 18f; 
		[SerializeField]
		protected float lineWidth = 3f;
		[SerializeField]
		public bool repaintDirty = true;
		protected void RepaintGetDirty()
		{
			repaintDirty = true;
		}

		protected Material _mat; 
		protected Material mat{
			get{
				if(_mat == null)
					_mat = new Material(SWEditorUI.GetShader("RectTRS"));
				return _mat;
			}
		} 

		public virtual void Init(SWBaseInfo info)
		{
			Data2UI (info,false); 
		}

		public virtual void OnGUITool(SWBaseInfo info,bool isSelected)
		{
			var color = isSelected ? Color.green:Color.white;
			mat.SetColor ("_Color", color);
			info.window.Set_Material (mat, Vector2.zero, 0, Vector2.one);
			UI2Data (info, false);
		}
		public virtual void UI2Data(SWBaseInfo info,bool dorecord = true)
		{
			if (dorecord)
				repaintDirty = true;
		}

		public virtual void Data2UI(SWBaseInfo info,bool dorecord = true)
		{
		}


		public virtual void OnEnable()
		{
			hideFlags = HideFlags.DontSave;
		}
	}
	[System.Serializable]
	public class OpTool_scaleX:OpTool
	{
		public override void Init (SWBaseInfo info)
		{
			base.Init (info);
		}
		public override void OnGUITool(SWBaseInfo info,bool isSelected)
		{
			base.OnGUITool (info,isSelected);
			rect = SWCommon.GetRect (info.imageRect.center + new Vector2 (info.imageRect.width * 0.5f, 0), new Vector2 (boxSize, boxSize));

			mat.SetFloat("r",0);
			Graphics.DrawTexture(rect,SWEditorUI.Texture(SWUITex.effectArrow),mat);

			mat.SetFloat("r",0);
			mat.SetTexture ("_MainTex", SWEditorUI.Texture (SWUITex.effectLine));
			SWDraw.DrawLine (info.imageRect.center, rect.center, Color.white,lineWidth,mat);
		}

		public override void UI2Data(SWBaseInfo info,bool dorecord = true)
		{
			base.UI2Data (info,dorecord);
			if (dorecord) {
				SWUndo.Record (info);
				Vector2 Move = info.mousePosRotated - info.mousePosRotatedLast;
				info.imageRect = info.imageRect.ScaleSizeBy (
					new Vector2 (1 + Move.x / info.imageRect.width, 1f));
			}
			info.effector.data.effectData.s_scale = 
				new Vector2 (info.imageRect.size.x / info.baseRect.size.x, info.imageRect.size.y / info.baseRect.size.y);
		}

		public override void Data2UI (SWBaseInfo info,bool dorecord = true)
		{
			base.Data2UI (info);
			if(dorecord)
				SWUndo.Record (info);
			var center = info.imageRect.center;
			info.imageRect.size = new Vector2(info.effector.data.effectData.s_scale.x * info.baseRect.size.x,
				info.effector.data.effectData.s_scale.y * info.baseRect.size.y);
			info.imageRect.center = center;
		}
	}
	[System.Serializable]
	public class OpTool_scaleY:OpTool
	{
		public override void Init (SWBaseInfo info)
		{
			base.Init (info);
		}
		public override void OnGUITool(SWBaseInfo info,bool isSelected)
		{
			base.OnGUITool (info,isSelected);
			rect = SWCommon.GetRect (info.imageRect.center + new Vector2(0,-info.imageRect.height*0.5f), new Vector2 (boxSize, boxSize));

			mat.SetFloat("r",Mathf.PI* 0.5f);
			Graphics.DrawTexture(rect,SWEditorUI.Texture(SWUITex.effectArrow),mat);

			mat.SetFloat("r",0);
			mat.SetTexture ("_MainTex", SWEditorUI.Texture (SWUITex.effectLine));
			SWDraw.DrawLine (info.imageRect.center, rect.center, Color.white,lineWidth,mat);
		}
		public override void UI2Data(SWBaseInfo info,bool dorecord = true)
		{
			base.UI2Data (info,dorecord);
			if (dorecord) {
				SWUndo.Record (info);
				Vector2 Move = info.mousePosRotated - info.mousePosRotatedLast;
				info.imageRect = info.imageRect.ScaleSizeBy (new Vector2 (1f, 1 - Move.y / info.imageRect.height));
			}
			info.effector.data.effectData.s_scale = 
				new Vector2 (info.imageRect.size.x / info.baseRect.size.x, info.imageRect.size.y / info.baseRect.size.y);
		}

		public override void Data2UI (SWBaseInfo info,bool dorecord = true)
		{
			base.Data2UI (info);
			if(dorecord)
				SWUndo.Record (info);
			var center = info.imageRect.center;
			info.imageRect.size = new Vector2(info.effector.data.effectData.s_scale.x * info.baseRect.size.x,
				info.effector.data.effectData.s_scale.y * info.baseRect.size.y);
			info.imageRect.center = center;
		}
	}
	[System.Serializable]
	public class OpTool_pos:OpTool
	{
		public override void Init (SWBaseInfo info)
		{
			base.Init (info);
		}
		public override void OnGUITool(SWBaseInfo info,bool isSelected)
		{
			base.OnGUITool (info,isSelected);
			var item = GUI.matrix;
			rect = SWCommon.GetRect (info.imageRect.center,new Vector2(centerSize,centerSize));
			Graphics.DrawTexture(rect,SWEditorUI.Texture(SWUITex.effectPos),mat);
		}
		public override void UI2Data(SWBaseInfo info,bool dorecord = true)  
		{
			base.UI2Data (info,dorecord);
			if (dorecord) {
				SWUndo.Record (info);
				info.imageRect.center += info.movement;
				info.imageRect.center = info.movement;
			}


			var BL_Base = info.baseRect.center;
			var BL_Img = info.imageRect.center;
			var v = BL_Img - BL_Base;
			v = new Vector2 (v.x, -v.y);
			info.effector.data.effectData.t_startMove = new Vector2 (v.x / info.baseRect.size.x, v.y / info.baseRect.size.y);
		}

		public override void Data2UI (SWBaseInfo info,bool dorecord = true)
		{
			base.Data2UI (info);
			if(dorecord)
				SWUndo.Record (info);
			var v = new Vector2 (	info.effector.data.effectData.t_startMove.x * info.baseRect.size.x, 	info.effector.data.effectData.t_startMove.y * info.baseRect.size.y);
			v = new Vector2 (v.x, -v.y);
			var BL_Base = info.baseRect.center;
			info.imageRect.center = v + BL_Base;
		}
	}

	[System.Serializable]
	public class OpTool_angle:OpTool
	{
		public float roundRad;
		public override void Init (SWBaseInfo info)
		{
			base.Init (info);
		}
		public override void OnGUITool(SWBaseInfo info,bool isSelected)
		{
			base.OnGUITool (info,isSelected);
			roundRad = Mathf.Max (info.imageRect.size.x, info.imageRect.size.y) * 0.6f;
			rect = SWCommon.GetRect (info.imageRect.center + new Vector2 (-roundRad, 0),new Vector2(boxSize,boxSize));
			Rect roundRect = SWCommon.GetRect (info.imageRect.center, new Vector2(roundRad * 2.04f,roundRad * 2.04f));
			Graphics.DrawTexture(roundRect,SWEditorUI.Texture(SWUITex.effectRound),mat);
		}
		public override void UI2Data(SWBaseInfo info,bool dorecord = true)
		{
			base.UI2Data (info,dorecord);
			if (dorecord) {
				SWUndo.Record (info);
				var vFrom = info.mousePosLast - info.imageRect.center;
				var vTo = info.mousePos - info.imageRect.center;

				var q = Quaternion.FromToRotation (vFrom, vTo);
				float v = q.eulerAngles.z;
				if (v > 180)
					v -= 360;
				info.angle += v;

				//Debug.Log (string.Format ("last pos:{0} pos:{1} ro:{2} angle:{3}", info.mousePosRotatedLast,info.mousePosRotated,v, info.angle));
				if (OpTool_Move.Instance != null) {
					Matrix4x4 m = Matrix4x4.TRS (Vector3.zero, Quaternion.Euler (new Vector3 (0, 0, -v)), Vector3.one);
					OpTool_Move.Instance.ArrowOff = m.MultiplyVector (OpTool_Move.Instance.ArrowOff);
				}
			}
			info.effector.data.effectData.r_angle = -info.angle * Mathf.Deg2Rad;
		}

		public override void Data2UI (SWBaseInfo info,bool dorecord = true)
		{
			base.Data2UI (info);
			if(dorecord)
				SWUndo.Record (info);
			info.angle = -info.effector.data.effectData.r_angle * Mathf.Rad2Deg;
		}
	} 

	[System.Serializable]
	public class OpTool_Move:OpTool
	{
		public static OpTool_Move Instance;
		[SerializeField]
		public Vector2 ArrowOff;
		[SerializeField]
		float factor;
		public override void Init (SWBaseInfo info)
		{
			base.Init (info);

		}
		public override void OnGUITool(SWBaseInfo info,bool isSelected)
		{
			Instance = this;
			base.OnGUITool (info,isSelected);
			rect = SWCommon.GetRect (info.imageRect.center + ArrowOff,new Vector2(boxSize,boxSize));




			mat.SetFloat ("r", -SWCommon.Vector2Angle(ArrowOff));
			if(ArrowOff.magnitude>8f)
				Graphics.DrawTexture(rect,SWEditorUI.Texture(SWUITex.effectArrow),mat);
			Graphics.DrawTexture(SWCommon.GetRect (info.imageRect.center,new Vector2(centerSize,centerSize)),
				SWEditorUI.Texture(SWUITex.effectCenter),mat);

			mat.SetFloat ("r", 0f);
			mat.SetTexture ("_MainTex", SWEditorUI.Texture (SWUITex.effectLine));
			SWDraw.DrawLine (info.imageRect.center, rect.center, Color.white,lineWidth,mat);
		}
		public override void UI2Data(SWBaseInfo info,bool dorecord = true)
		{
			base.UI2Data (info,dorecord);
			if (dorecord) {
				SWUndo.Record (this);
				ArrowOff = info.mousePosRotated - info.imageRect.center;

				if (Event.current.shift) {
					if (Mathf.Abs(ArrowOff.x) > Mathf.Abs(ArrowOff.y))
						ArrowOff = new Vector2 (ArrowOff.x, 0);
					else
						ArrowOff = new Vector2 (0,ArrowOff.y);
				}
			}

			info.effector.data.effectData.t_speed = new Vector2(ArrowOff.x * factor,-ArrowOff.y* factor);
			Matrix4x4 m = Matrix4x4.TRS(Vector3.zero,Quaternion.Euler(new Vector3(0,0,-info.angle)),Vector3.one);
			info.effector.data.effectData.t_speed = m.MultiplyVector (info.effector.data.effectData.t_speed);
		}
		public override void Data2UI (SWBaseInfo info,bool dorecord = true)
		{
			base.Data2UI (info);
			if(dorecord)
				SWUndo.Record (this);
			factor = 1f / info.baseRect.size.x; 
			ArrowOff = new Vector2(info.effector.data.effectData.t_speed.x / factor,-info.effector.data.effectData.t_speed.y / factor);
			Matrix4x4 m2 = Matrix4x4.TRS(Vector3.zero,Quaternion.Euler(new Vector3(0,0,-info.angle)),Vector3.one);
			ArrowOff = m2.MultiplyVector (ArrowOff);
		}
	}
	[System.Serializable]
	public class OpTool_Spin:OpTool
	{
		[SerializeField]
		Vector2 startPos;
		[SerializeField]
		Vector2 offset;
		[SerializeField]
		float factor;
		public override void Init (SWBaseInfo info)
		{
			base.Init (info);

		}
		public override void OnGUITool(SWBaseInfo info,bool isSelected)
		{
			base.OnGUITool (info,isSelected);
			float roundRad = Mathf.Max (info.imageRect.size.x, info.imageRect.size.y) * 0.6f;
			startPos = info.imageRect.center + new Vector2 (0, -roundRad+ 5);
			rect = SWCommon.GetRect (startPos+offset,new Vector2(boxSize,boxSize));


			//bg circle
			var c = mat.GetColor("_Color");
			mat.SetColor ("_Color",new Color32 (128, 128, 128, 255));
			Rect roundRect = SWCommon.GetRect (info.imageRect.center, new Vector2(roundRad * 2,roundRad * 2));
			Graphics.DrawTexture(roundRect,SWEditorUI.Texture(SWUITex.effectRound),mat);
			mat.SetColor ("_Color",c);

			mat.SetFloat ("r", -SWCommon.Vector2Angle(offset));
			if(offset.magnitude>8f)
				Graphics.DrawTexture(rect,SWEditorUI.Texture(SWUITex.effectArrow),mat);
			Graphics.DrawTexture(SWCommon.GetRect (startPos,new Vector2(centerSize,centerSize)),
				SWEditorUI.Texture(SWUITex.effectCenter),mat);

			mat.SetFloat ("r", 0f);
			mat.SetTexture ("_MainTex", SWEditorUI.Texture (SWUITex.effectLine));
			SWDraw.DrawLine (startPos, startPos+offset, Color.white,lineWidth,mat);
		}
		public override void UI2Data(SWBaseInfo info,bool dorecord = true)
		{
			base.UI2Data (info,dorecord);
			if (dorecord) {
				SWUndo.Record (this);
				Vector2 Move = info.mousePosRotated - info.mousePosRotatedLast;
				offset += new Vector2 (Move.x, 0);
			}
			info.effector.data.effectData.r_speed = -offset.x * factor;
		}
		public override void Data2UI (SWBaseInfo info,bool dorecord = true)
		{
			base.Data2UI (info);
			if(dorecord)
				SWUndo.Record (this);
			factor = 1f / info.baseRect.size.x * Mathf.PI * 2; 
			offset.x = -info.effector.data.effectData.r_speed / factor;
		}
	}
	[System.Serializable]
	public class OpTool_Radial:OpTool
	{
		[SerializeField]
		Vector2 startPos;
		[SerializeField]
		Vector2 offset;
		[SerializeField]
		float factor;
		public override void Init (SWBaseInfo info)
		{
			factor = 1f / info.baseRect.size.x; 
			base.Init (info);
		}
		public override void OnGUITool(SWBaseInfo info,bool isSelected)
		{
			base.OnGUITool (info,isSelected);
			float roundRad = Mathf.Max (info.imageRect.size.x, info.imageRect.size.y) * 0.6f;
			startPos = info.imageRect.center + new Vector2 (0, -roundRad + 5);
			rect = SWCommon.GetRect (startPos+offset,new Vector2(boxSize,boxSize));

			//bg circle
			var c = mat.GetColor("_Color");
			mat.SetColor ("_Color",new Color32 (128, 128, 128, 255));
			Rect roundRect = SWCommon.GetRect (info.imageRect.center, new Vector2(roundRad * 2,roundRad * 2));
			Graphics.DrawTexture(roundRect,SWEditorUI.Texture(SWUITex.effectRound),mat);
			mat.SetColor ("_Color",c);



			mat.SetFloat ("r", -SWCommon.Vector2Angle(offset));
			if(offset.magnitude>8f)
				Graphics.DrawTexture(rect,SWEditorUI.Texture(SWUITex.effectArrow),mat);
			Graphics.DrawTexture(SWCommon.GetRect (startPos,new Vector2(centerSize,centerSize)),
				SWEditorUI.Texture(SWUITex.effectCenter),mat);


			mat.SetFloat ("r", 0f);
			mat.SetTexture ("_MainTex", SWEditorUI.Texture (SWUITex.effectLine));
			SWDraw.DrawLine (startPos, startPos+offset, Color.white,lineWidth,mat);
		}
		public override void UI2Data(SWBaseInfo info,bool dorecord = true)
		{
			base.UI2Data (info,dorecord);

			if (dorecord) {
				SWUndo.Record (this);
				Vector2 Move = info.mousePosRotated - info.mousePosRotatedLast;
				offset += new Vector2 (0, Move.y);
			}
			info.effector.data.effectData.s_speed = new Vector2(-offset.y * factor,-offset.y * factor);
		}
		public override void Data2UI (SWBaseInfo info,bool dorecord = true)
		{
			base.Data2UI (info);
			if(dorecord)
				SWUndo.Record (this);
			offset = new Vector2(0,- info.effector.data.effectData.s_speed.x / factor);
		}
	}

	[System.Serializable]
	public class SWWindowEffect:SWWindowLayoutV{
		protected SWDataNode data
		{
			get{ return info.effector.data;}
		}
		[SerializeField]
		protected SWBaseInfo info;
		[SerializeField]
		protected SWEffectWindowEditMode mode;
		[SerializeField]
		public Dictionary<SWEffectWindowOp,OpTool> ops = new Dictionary<SWEffectWindowOp, OpTool>();
		[SerializeField]
		protected List<SWEffectWindowOp> showOps = new List<SWEffectWindowOp>();
		[SerializeField]    
		protected SWEffectWindowOp op;
		/// <summary>
		/// Display white or dark tiled background
		/// </summary>
		[SerializeField]    
		protected bool brightBackground;

		protected override void SerializedInit ()
		{
			base.SerializedInit ();

			if(slotBox_left!=null)
				slotBox_left.Init (OnSelect);
		}





		public override void OnAfterDeserialize ()
		{
			base.OnAfterDeserialize ();
		}

		public override void Update ()
		{
			if (!CanUpdate ())
				return;
			base.Update ();

			//[Main window close]    or    [related node deleted] ===>  close this window
			if (SWWindowMain.Instance == null || !SWWindowMain.Instance.NodeAll().ContainsKey(info.effector.data.id)) {
				Close ();  
				return;
			}
			SWWindowMain.Instance.NodeAll() [info.effector.data.id] = info.effector;
		}

		public void Init(SWNodeEffector e)
		{
			info = ScriptableObject.CreateInstance<SWBaseInfo> ();
			info.effector = e;
			info.window = this;
			SetLayerMask (info.effector);

			ePopup_texShowChn = new SWEnumPopup (new string[]{"All", "R", "G", "B", "A"},(int)texShowChannel,null,
				delegate(int index)
				{
					SWUndo.Record(this);
					texShowChannel = (SWTexShowChannel)index;
					TexShowChnEnumSet ();
				}
			);
		}
		public override void InitOnce ()
		{
			base.InitOnce ();
		}

		public override void InitUI ()
		{
			base.InitUI ();

			//info.baseRect = new Rect (0, 0, 512, 512);
			zScaleMin = 0.6f;
			zScaleMax = 3f;
			float size = 512;
			info.baseRect = new Rect(al_rectMain.width*0.5f-size*0.5f,al_rectMain.height*0.5f-size*0.5f,size,size);

			mapTL = new Vector2 (-mapFloatSmall+xHalf, -mapFloatSmall+yHalf);
			mapBR = new Vector2 (mapFloatSmall+xHalf, mapFloatSmall+yHalf);
			mapSize = mapBR - mapTL;

			OpsInit ();
			InitSlotNox ();
		}

		protected void OpsInit()
		{
			if (ops == null || ops.Count == 0) {
				ops.Clear ();
				ops.Add (SWEffectWindowOp.scaleX, ScriptableObject.CreateInstance<OpTool_scaleX> ());
				ops.Add (SWEffectWindowOp.scaleY, ScriptableObject.CreateInstance<OpTool_scaleY> ());
				ops.Add (SWEffectWindowOp.pos, ScriptableObject.CreateInstance<OpTool_pos> ());
				ops.Add (SWEffectWindowOp.angle, ScriptableObject.CreateInstance<OpTool_angle> ());
				ops.Add (SWEffectWindowOp.Move, ScriptableObject.CreateInstance<OpTool_Move> ());
				ops.Add (SWEffectWindowOp.Radial, ScriptableObject.CreateInstance<OpTool_Radial> ());
				ops.Add (SWEffectWindowOp.Spin, ScriptableObject.CreateInstance<OpTool_Spin> ());
				OpsInitExtra ();
				foreach (var item in ops) {
					item.Value.Init (info);
				}
			}
		}

		protected virtual void OpsInitExtra()
		{

		}

		private void InitSlotNox()
		{
			List<SWSlot> slotsNodebox = new List<SWSlot> ();
			slotsNodebox.Add(new SWSlot("Basic",SWTipsText.Effect_t_Basic,null,KeyCode.B));
			slotsNodebox.Add(new SWSlot("Move",SWTipsText.Effect_t_Move,null,KeyCode.M));
			slotsNodebox.Add(new SWSlot("Spin",SWTipsText.Effect_t_Spin,null,KeyCode.S));
			slotsNodebox.Add(new SWSlot("Radial",SWTipsText.Effect_t_Radial,null,KeyCode.R));
			AddSlotNox (slotsNodebox);
			slotBox_left = ScriptableObject.CreateInstance<SWSlotBox_Select> ();
			slotBox_left.InitSlot(this,
				new Rect(0,	al_topHeight,	al_leftWidth,	position.height-al_spacing-al_topHeight),
				slotsNodebox,OnSelect,new Vector2(al_leftWidth,al_leftWidth*0.6f));
		}

		public virtual void AddSlotNox(List<SWSlot> slotsNodebox)
		{
		}



		protected void OnSelect(SWSlot slot,Vector2 mp)
		{
		}

		public override void OnGUITop ()
		{
			base.OnGUITop ();
			ShowLayerMask (info.effector);
		}

		public override void OnGUIBot ()
		{
			base.OnGUIBot ();
//			//performance impact
//			mode = (SWEffectWindowEditMode)System.Enum.Parse(
//				typeof(SWEffectWindowEditMode), slotBox_left.selectSlot.name);
			mode = (SWEffectWindowEditMode)slotBox_left.selection;

			showOps.Clear ();
			if (mode == SWEffectWindowEditMode.Basic) {
				showOps.Add (SWEffectWindowOp.scaleX);
				showOps.Add (SWEffectWindowOp.scaleY);
				showOps.Add (SWEffectWindowOp.pos);
				showOps.Add (SWEffectWindowOp.angle);
			}
			if (mode == SWEffectWindowEditMode.Move) {
				showOps.Add (SWEffectWindowOp.Move);
			}
			if (mode == SWEffectWindowEditMode.Radial) {
				showOps.Add (SWEffectWindowOp.Radial);
			}
			if (mode == SWEffectWindowEditMode.Spin) {
				showOps.Add (SWEffectWindowOp.Spin);
			}
			if (mode == SWEffectWindowEditMode.UV) {
				OpsUV ();
			}
		}

		public override void GUIEnd ()
		{
			base.GUIEnd ();
			if(data.effectData.animationSheetUse)
				RepaintGetDirty ();
		}

		public virtual void OpsUV()
		{

		}

		public override void Clean ()
		{
			base.Clean ();
		}



		public override void DrawTop ()
		{
			base.DrawTop ();
			DrawTop (data.effectData,data.effectDataUV);

			if (mode == SWEffectWindowEditMode.Move) {
				Factor_Pick ();
			}
			if (mode == SWEffectWindowEditMode.Spin) {
				Factor_Pick ();
			}
			if (mode == SWEffectWindowEditMode.Radial) {
				Factor_Pick ();
			}
		}


		public override void DrawMainBot ()
		{
			base.DrawMainBot ();
			if(brightBackground)
				SWEditorTools.DrawTiledTexture(al_rectMain, SWEditorTools.backdropTextureBright);
			else
				SWEditorTools.DrawTiledTexture(al_rectMain, SWEditorTools.backdropTexture);
		}
		public override void DrawMainInside1 ()
		{
			base.DrawMainInside1 ();
			DrawMain ();
		}
		public override void DrawMainTop ()
		{
			base.DrawMainTop ();

		}

		public static Matrix4x4 matrixSpinBefore;
		public static Matrix4x4 matrixSpinAfter;
		void DrawMain()
		{
			
			if (SWCommon.GetMouseUp (1) && InMap ()) {
				float dis = Vector2.Distance (mousePosDown, mousePos);
				if(dis<1f)
					brightBackground = !brightBackground;
			}

			OpsInit ();
			//			if(BottomTexture()!=null)
			//				GUI.DrawTexture (info.baseRect, BottomTexture(), ScaleMode.StretchToFill);
			ShowBG_Textures();	


			var p = new Vector3 (al_rectMainZoom.x + info.imageRect.center.x, al_rectMainZoom.y+info.imageRect.center.y, 0);
			matrixSpinBefore = GUI.matrix;
			Matrix4x4 tt = Matrix4x4.TRS (p, Quaternion.identity, Vector3.one);
			Matrix4x4 rr = Matrix4x4.TRS(Vector3.zero,Quaternion.Euler(0,0,info.angle),Vector3.one);
			matrixSpinAfter = GUI.matrix * tt*rr* tt.inverse;
			GUI.matrix = matrixSpinAfter;
			info.mousePosRotated = Event.current.mousePosition;
			//		Debug.Log ("mousePosition: "+Event.current.mousePosition);
			OnGUIImage ();
			GUI.matrix = matrixSpinBefore;

			DrawFrame (info.baseRect);

			Set_Material (OtherTexMat);
			Set_Material (MainTexMat);
			Set_MaterialLoop (MainTexMat,data);
			Set_MaterialTextureSheet (MainTexMat,info.effector);
		}

		void ShowBG_Textures()
		{
			foreach (var tempNode in SWWindowMain.Instance.nodes) {
				if (tempNode.data.id == info.effector.data.id)
					continue;
				var tex = tempNode.Texture;
				if (tempNode is SWNodeMask)
					tex = ((SWNodeMask)tempNode).texMask.Texture;
				if (tex == null)
					continue;
				if (!info.effector.data.layerMask.Has (tempNode.data.id))
					continue;

				var dt = tempNode.data;
				Material matBot = new Material (SWEditorUI.GetShader ("RectTRS"));
				Set_Material (matBot, -dt.effectData.t_startMove, dt.effectData.r_angle, dt.effectData.s_scale);
				Set_MaterialLoop (matBot,dt);
				Graphics.DrawTexture (info.baseRect, tex, matBot);
			}
		}


		protected virtual Texture2D BottomTexture()
		{
			return null;
		}


		void DrawMainTex()
		{
			//Half of the extra grids
			int exHalf = 5;
			float widthAbs = Mathf.Abs(info.imageRect.width);
			float heightAbs = Mathf.Abs(info.imageRect.height);
			float widthSign = Mathf.Sign(info.imageRect.width);
			float heightSign = Mathf.Sign(info.imageRect.height);
			Graphics.DrawTexture (new Rect (
				info.imageRect.x - exHalf*widthAbs,
				info.imageRect.y -  exHalf*heightAbs,
				(1+exHalf*2) * widthAbs, 
				(1+exHalf*2)*heightAbs),

				info.effector.texture,

				new Rect (-exHalf*widthSign,
					-exHalf*heightSign, 
					(1+exHalf*2)*widthSign, 
					(1+exHalf*2)*heightSign), 
				0, 0, 0, 0,
				MainTexMat);
		}
		void OnGUIImage()
		{
			DrawMainTex ();
			info.mousePos = mousePos;
			foreach (var item in showOps) {
				ops[item].OnGUITool (info,item == op);
			}

			if (SWCommon.GetMouseDown (0) && InMap()) {
				op = SWEffectWindowOp.none;

				List<SWEffectWindowOp> _opsIsPressed = new List<SWEffectWindowOp> ();
				foreach (var item in showOps) {
					if (ops[item] is OpTool_angle) {
						OpTool_angle en = (OpTool_angle)ops[item];
						float dis = Vector2.Distance (info.mousePos, info.imageRect.center);
						float checkRange = Mathf.Max(en.roundRad*0.02f,1);
						if (Mathf.Abs (dis - en.roundRad) < checkRange) {
							op = item;
							RepaintGetDirty ();
						}
						continue;
					}
					if (ops[item].rect.Contains (info.mousePosRotated)) {
						_opsIsPressed.Add (item);
					}
				}

				//use other tool rather then angle ring, while overlap
				if (_opsIsPressed.Count == 1) {
					op = _opsIsPressed [0];
					RepaintGetDirty ();
				}
				else if (_opsIsPressed.Count > 1) {
					foreach(var oopp in _opsIsPressed)
					{
						if (oopp == SWEffectWindowOp.angle)
							continue;
						op = oopp;
						RepaintGetDirty ();
						break;
					}
				}


				info.mousePosRotatedLast = info.mousePosRotated;
				info.mousePosLast = info.mousePos;
			}
			if (SWCommon.GetMouse (0)) {
				if (ops.ContainsKey (op)) {
					info.movement = mousePos;
					ops [op].UI2Data (info);

					if (ops [op].repaintDirty) {
						ops [op].repaintDirty = false;
						RepaintGetDirty ();
					}
				}
				info.mousePosRotatedLast = info.mousePosRotated;
				info.mousePosLast = info.mousePos;
			} else {

			}
			if (SWCommon.GetMouseUp (0)) {
				op = SWEffectWindowOp.none;
				info.mousePosRotatedLast = info.mousePosRotated;

				RepaintGetDirty ();
			}
		}

		#region right
		protected override void DrawRightUp()
		{
			DrawBG (rightUpRect);

			GUILayout.BeginHorizontal ();
			DrawModuleTitle ("Loop");
			var tmp0 = EditorGUILayout.Toggle(data.effectData.useLoop);
			if (data.effectData.useLoop != tmp0) {
				SWUndo.Record (info.effector);
				data.effectData.useLoop = tmp0;
			} 
			GUILayout.EndHorizontal ();
			Tooltip_Rec (SWTipsText.Right_LoopModule,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));
			
			if (data.effectData.useLoop) {
				var temp = (SWLoopMode)UI_PopEnum ("X Loop", data.effectData.loopX, true);
				if (data.effectData.loopX != temp) {
					SWUndo.Record (info.effector);
					data.effectData.loopX = temp;
				}
				Tooltip_Rec (SWTipsText.Right_LoopX,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

				UI_Float ("X Gap", ref data.effectData.gapX, null, false, false, true);
				Tooltip_Rec (SWTipsText.Right_GapX,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));
			
				temp = (SWLoopMode)UI_PopEnum ("Y Loop", data.effectData.loopY, true);
				if (data.effectData.loopY != temp) {
					SWUndo.Record (info.effector);
					data.effectData.loopY = temp;
				}
				Tooltip_Rec (SWTipsText.Right_LoopY,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

				UI_Float ("Y Gap", ref data.effectData.gapY, null, false, false, true);
				Tooltip_Rec (SWTipsText.Right_GapY,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));
			}
			GUILayout.Space (rightupSpacing);
			DrawExtra ();
			GUILayout.Space (rightupSpacing);
			DrawAnimationSheet ();

//			//No space 
//			GUILayout.Space (rightupSpacing);
//			Factor_CustomParamCreation ();
		}

		void DrawAnimationSheet()
		{
			EditorGUIUtility.labelWidth = 100;

			GUILayout.BeginHorizontal ();
			DrawModuleTitle ("AnimationSheet");
			data.effectData.animationSheetUse = EditorGUILayout.Toggle (data.effectData.animationSheetUse,
				GUILayout.Width(SWGlobalSettings.FieldWidth-20));
			GUILayout.EndHorizontal ();
			//GUILayout.Space (SWGlobalSettings.UIGap);
			Tooltip_Rec (SWTipsText.Right_AnimationSheetModule,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));
			if (!data.effectData.animationSheetUse) {
				return;
			}

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Tile X",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight), GUILayout.Width(SWGlobalSettings.LabelWidthLong));
			data.effectData.animationSheetCountX = EditorGUILayout.IntField (data.effectData.animationSheetCountX,
				GUILayout.Width(SWGlobalSettings.FieldWidth));
			GUILayout.EndHorizontal ();
			//GUILayout.Space (SWGlobalSettings.UIGap);
			if (data.effectData.animationSheetCountX <= 0)
				data.effectData.animationSheetCountX = 1;
			Tooltip_Rec (SWTipsText.Right_AnimationSheetTileX,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Tile Y",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight), GUILayout.Width(SWGlobalSettings.LabelWidthLong));
			data.effectData.animationSheetCountY = EditorGUILayout.IntField (data.effectData.animationSheetCountY,
				GUILayout.Width(SWGlobalSettings.FieldWidth));
			GUILayout.EndHorizontal ();
			//GUILayout.Space (SWGlobalSettings.UIGap);
			if (data.effectData.animationSheetCountY <= 0)
				data.effectData.animationSheetCountY = 1;
			Tooltip_Rec (SWTipsText.Right_AnimationSheetTileY,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Loop",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight), GUILayout.Width(SWGlobalSettings.LabelWidthLong));
			data.effectData.animationSheetLoop = EditorGUILayout.Toggle (data.effectData.animationSheetLoop,
				GUILayout.Width(SWGlobalSettings.FieldWidth));
			GUILayout.EndHorizontal ();
			//GUILayout.Space (SWGlobalSettings.UIGap);
			Tooltip_Rec (SWTipsText.Right_AnimationSheetLoop,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Single Row",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight), GUILayout.Width(SWGlobalSettings.LabelWidthLong));
			data.effectData.animationSheetSingleRow = EditorGUILayout.Toggle (data.effectData.animationSheetSingleRow,
				GUILayout.Width(SWGlobalSettings.FieldWidth));
			GUILayout.EndHorizontal ();
			//GUILayout.Space (SWGlobalSettings.UIGap);
			Tooltip_Rec (SWTipsText.Right_AnimationSheetSingleRow,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			if (data.effectData.animationSheetSingleRow) {
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Row",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight), GUILayout.Width(SWGlobalSettings.LabelWidthLong));
				data.effectData.animationSheetSingleRowIndex = EditorGUILayout.IntField ( data.effectData.animationSheetSingleRowIndex,GUILayout.Width (SWGlobalSettings.FieldWidth));
				GUILayout.EndHorizontal ();
				//GUILayout.Space (SWGlobalSettings.UIGap);
				Tooltip_Rec (SWTipsText.Right_AnimationSheetRow,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));
			}

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Start Frame",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight), GUILayout.Width(SWGlobalSettings.LabelWidthLong));
			data.effectData.animationSheetStartFrame = EditorGUILayout.IntField (data.effectData.animationSheetStartFrame,
				GUILayout.Width(SWGlobalSettings.FieldWidth));
			GUILayout.EndHorizontal ();
			//GUILayout.Space (SWGlobalSettings.UIGap);
			Tooltip_Rec (SWTipsText.Right_AnimationSheetStartFrame,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			Tooltip_Rec (SWTipsText.Right_AnimationSheetFactor,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().yMax,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));
			Factor_Pick (ref 	data.effectData.animationSheetFrameFactor, true, "Factor");
		}


		protected virtual void DrawExtra()
		{

		}
		protected void UI_Color(string name,ref Color v,ref bool hdr,System.Action<SWBaseInfo,bool> act = null,bool before = false,bool isRightUp = false)
		{
			if(isRightUp)
				GUILayout.BeginHorizontal ();
			Color temp;


			if (hdr)
				name += " [HDR]";
			GUILayout.Label (name,SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight), GUILayout.Width(SWGlobalSettings.LabelWidthLong));
			SWCommon.HRDColor_Switch (this, ref hdr);
			var vvv = v;
			SWCommon.HRDColor_Field (v, true, hdr, SWGlobalSettings.FieldWidth, delegate(Color c) {
				if (before) {
					if(act!=null)
						act (info,true);
				}
				vvv = c;
				if (!before) {
					if(act!=null)
						act (info,true);
				}
				SWProperties.SetColor (data, vvv);
			}
			);
			v = vvv;
			if(isRightUp)
				GUILayout.EndHorizontal ();
		}

		protected void UI_Float(string name,ref float v,
			System.Action<SWBaseInfo,bool> act = null,bool before = false,bool flexible = true,bool isRightUp = false)
		{
			if(isRightUp)
				GUILayout.BeginHorizontal ();
			var temp = SWEditorUI.FloatField (name,v,flexible);
			if (temp != v) {
				if (before) {
					if(act!=null)
						act (info,true);
				}
				v = temp;
				if (!before) {
					if(act!=null)
						act (info,true);
				}
			}
			if(isRightUp)
				GUILayout.EndHorizontal ();
			//GUILayout.Space (SWGlobalSettings.UIGap);
		}
		protected void UI_Vector2(string name,ref Vector2 v,System.Action<SWBaseInfo,bool> act = null,bool before = false,
			float labelWidth = -1,bool isRightUp = false)
		{
			if (labelWidth < 0)
				labelWidth = SWGlobalSettings.LabelWidth;
			if(isRightUp)
				GUILayout.BeginHorizontal ();
			var temp = SWEditorUI.Vector2Field(name,v);

			if (temp != v) {
				if (before) {
					if(act!=null)
						act (info,true);
				}
				v = temp;
				if (!before) {
					if(act!=null)
						act (info,true);
				}
			}
			//GUILayout.Space (SWGlobalSettings.UIGap);
			if(isRightUp)
				GUILayout.EndHorizontal ();
		}
		public static System.Enum UI_PopEnum(string name, System.Enum v,bool isRightUp = false)
		{
			if(isRightUp)
				GUILayout.BeginHorizontal ();
			GUILayout.Label (name, SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width (SWGlobalSettings.LabelWidthLong));
			var temp = EditorGUILayout.EnumPopup ("",(System.Enum)v,GUILayout.Width (SWGlobalSettings.FieldWidth));
			if(isRightUp)
				GUILayout.EndHorizontal ();
			//GUILayout.Space (SWGlobalSettings.UIGap);
			return temp;
		}

		void DrawTop(EffectData data,EffectDataUV dataUV)
		{
			if (mode == SWEffectWindowEditMode.Basic) {
				UI_Vector2 ("Position:", ref data.t_startMove,ops [SWEffectWindowOp.pos].Data2UI);
				Tooltip_Rec (SWTipsText.Effect_Position);

				GUILayout.Space (al_spacingBig);
				UI_Float ("Rotation:", ref data.r_angle,ops [SWEffectWindowOp.angle].Data2UI);
				Tooltip_Rec (SWTipsText.Effect_Rotation,TopElementRect(225,348));

				GUILayout.Space (al_spacingBig);
				UI_Vector2 ("Scale:", ref data.s_scale,ops [SWEffectWindowOp.scaleX].Data2UI);
				Tooltip_Rec (SWTipsText.Effect_Scale);
			}
			else if (mode == SWEffectWindowEditMode.UV) {
				DrawTopUV ();
			}
			else if (mode == SWEffectWindowEditMode.Move) {
				UI_Vector2 ("Move:", ref data.t_speed,ops [SWEffectWindowOp.Move].Data2UI);
				Tooltip_Rec (SWTipsText.Effect_Move);
			}
			else if (mode == SWEffectWindowEditMode.Spin) {
				UI_Float ("Spin:", ref data.r_speed,ops [SWEffectWindowOp.Spin].Data2UI);
				Tooltip_Rec (SWTipsText.Effect_Spin,TopElementRect(5,100));
			}
			else if (mode == SWEffectWindowEditMode.Radial) {
				UI_Vector2 ("Radial:", ref data.s_speed,ops [SWEffectWindowOp.Radial].Data2UI);
				Tooltip_Rec (SWTipsText.Effect_Radial);
			}
		}

		protected virtual void DrawTopUV()
		{

		}
		#endregion

		#region param
		void Factor_Pick()
		{
			GUILayout.Label ("x", SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight));
			GUILayout.Space (8);
			if (mode == SWEffectWindowEditMode.Move) {
				Factor_Pick (ref data.effectData.t_Param);
			}
			if (mode == SWEffectWindowEditMode.Spin) {
				Factor_Pick(ref data.effectData.r_Param);
			}
			if (mode == SWEffectWindowEditMode.Radial) {
				Factor_Pick(ref data.effectData.s_Param);
			}
		}

		protected void Factor_Pick(ref string param,bool isRightUp = false,string label = "Factor")
		{
			if(isRightUp)
				Factor_Pick (ref param, PickParamType.right, label, info.effector);
			else
				Factor_Pick (ref param, PickParamType.top, label, info.effector);
		}

		#endregion
		public override void KeyCmd_HotkeyDown (KeyCode code)
		{
			base.KeyCmd_HotkeyDown (code);
		}
		public override void KeyCmd_HotkeyUp (KeyCode code)
		{
			base.KeyCmd_HotkeyUp (code);  
		}

		public override void SetInsideRects ()
		{
			base.SetInsideRects ();
			info.baseRect = SetInsideRect (	info.baseRect);
			info.imageRect = SetInsideRect (info.imageRect);
		}
		public override void KeyCmd_Dragmove (Vector2 delta)
		{
			base.KeyCmd_Dragmove (delta);
			info.baseRect.position += new Vector2 (1f * delta.x, 1f * delta.y);
			info.imageRect.position += new Vector2 (1f * delta.x, 1f * delta.y);
		}


		[SerializeField]
		public SWTexShowChannel texShowChannel;
		[SerializeField]
		private Material mainTexMat;
		public Material MainTexMat
		{
			get{  
				if (mainTexMat == null) {
					mainTexMat = new Material (SWEditorUI.GetShader ("RectRGBA"));
				}
				return mainTexMat;
			}
		}
		[SerializeField]
		private Material otherTexMat;
		public Material OtherTexMat
		{
			get{ 
				if (otherTexMat == null) {
					otherTexMat = new Material (SWEditorUI.GetShader ("Rect"));
				}
				return otherTexMat;
			}
		}

		[SerializeField]
		SWEnumPopup ePopup_texShowChn;

		protected void  TexShowChnEnum()
		{
			Rect rect = new Rect (al_leftWidth+10, al_topHeight+30, 40, 10);
			ePopup_texShowChn.Show (rect);
		}
		private void TexShowChnEnumSet()
		{
			int i = (int)texShowChannel;
			MainTexMat.SetInt ("cmode", i);
		}
	}
}



