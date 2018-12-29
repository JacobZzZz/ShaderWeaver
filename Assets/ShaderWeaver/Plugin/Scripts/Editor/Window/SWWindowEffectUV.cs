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
	public class SWOpTool_UVBase:OpTool
	{
		[SerializeField]
		public Vector2 ArrowOff;
		[SerializeField]
		protected float factor;
		protected Vector2 center;

		protected Vector2 offset = new Vector2 (0, -20f);
		protected string title;


		public override void Init (SWBaseInfo info)
		{
			base.Init (info);

		}
		public override void OnGUITool (SWBaseInfo info,bool isSelected)
		{
			center = info.imageRect.center +offset;
			base.OnGUITool (info,isSelected);
			rect = SWCommon.GetRect (center + ArrowOff,new Vector2(boxSize,boxSize));


			mat.SetFloat ("r", -SWCommon.Vector2Angle(ArrowOff));
			if(ArrowOff.magnitude>8f)
				Graphics.DrawTexture(rect,SWEditorUI.Texture(SWUITex.effectArrow),mat);
			Graphics.DrawTexture(SWCommon.GetRect (center,new Vector2(centerSize,centerSize)),
				SWEditorUI.Texture(SWUITex.effectCenter),mat);



			mat.SetFloat ("r", 0f);
			mat.SetTexture ("_MainTex", SWEditorUI.Texture (SWUITex.effectLine));
			SWDraw.DrawLine (center, rect.center, Color.white,lineWidth,mat);

			float dis = ArrowOff.magnitude;
			dis = dis - 3;
			dis = Mathf.Clamp (dis, 0, float.MaxValue);
			Vector2 v = center + ArrowOff.normalized * dis;
			v = center;
			GUI.color = Color.black;
			GUI.Label ( SWCommon.GetRect (v,new Vector2(15,15)), title);
			GUI.color = Color.white;
		}
		public override void UI2Data(SWBaseInfo info,bool dorecord = true)
		{
			base.UI2Data (info,dorecord);
			if (dorecord) {
				SWUndo.Record (this);
				ArrowOff = info.mousePosRotated - center;

				if (Event.current.shift) {
					if (Mathf.Abs(ArrowOff.x) > Mathf.Abs(ArrowOff.y))
						ArrowOff = new Vector2 (ArrowOff.x, 0);
					else
						ArrowOff = new Vector2 (0,ArrowOff.y);
				}
			}
				
			var a = new Vector2(ArrowOff.x * factor,-ArrowOff.y* factor);
			Matrix4x4 m = Matrix4x4.TRS(Vector3.zero,Quaternion.Euler(new Vector3(0,0,-info.angle)),Vector3.one);
			a = m.MultiplyVector (a);
			SetValue(info,a);
		}
		public override void Data2UI (SWBaseInfo info,bool dorecord = true)
		{
			base.Data2UI (info);
			if(dorecord)
				SWUndo.Record (this);
			factor = 1f / info.baseRect.size.x; 

			Vector2 v = FromValue (info);
			ArrowOff = new Vector2(v.x / factor,-v.y / factor);
			Matrix4x4 m2 = Matrix4x4.TRS(Vector3.zero,Quaternion.Euler(new Vector3(0,0,-info.angle)),Vector3.one);
			ArrowOff = m2.MultiplyVector (ArrowOff);
		}

		protected virtual void SetValue(SWBaseInfo info,Vector2 v)
		{
			
		}
		protected virtual Vector2 FromValue(SWBaseInfo info)
		{
			return Vector2.zero;
		}
	}

	[System.Serializable]
	public class SWOpTool_UVR:SWOpTool_UVBase
	{
		public override void Init (SWBaseInfo info)
		{
			base.Init (info);
			offset = new Vector2 (-20, 0f);
			title = "R";
		}
		protected override void SetValue (SWBaseInfo info, Vector2 v)
		{
			base.SetValue (info, v);
			info.effector.data.effectDataUV.amountR = v;
		}
		protected override Vector2 FromValue (SWBaseInfo info)
		{
			return info.effector.data.effectDataUV.amountR;
		}
	}

	[System.Serializable]
	public class SWOpTool_UVG:SWOpTool_UVBase
	{
		public override void Init (SWBaseInfo info)
		{
			base.Init (info);
			offset = new Vector2 (0, -20f);
			title = "G";
		}
		protected override void SetValue (SWBaseInfo info, Vector2 v)
		{
			base.SetValue (info, v);
			info.effector.data.effectDataUV.amountG = v;
		}
		protected override Vector2 FromValue (SWBaseInfo info)
		{
			return info.effector.data.effectDataUV.amountG;
		}
	}


	[System.Serializable]
	public class SWOpTool_UVB:SWOpTool_UVBase
	{
		public override void Init (SWBaseInfo info)
		{
			base.Init (info);
			offset = new Vector2 (20, 0f);
			title = "B";
		}
		protected override void SetValue (SWBaseInfo info, Vector2 v)
		{
			base.SetValue (info, v);
			info.effector.data.effectDataUV.amountB = v;
		}
		protected override Vector2 FromValue (SWBaseInfo info)
		{
			return info.effector.data.effectDataUV.amountB;
		}
	}

	[System.Serializable]
	public class SWOpTool_UVA:SWOpTool_UVBase
	{
		public override void Init (SWBaseInfo info)
		{
			base.Init (info);
			offset = new Vector2 (0f, 20f);
			title = "A";
		}
		protected override void SetValue (SWBaseInfo info, Vector2 v)
		{
			base.SetValue (info, v);
			info.effector.data.effectDataUV.amountA = v;
		}
		protected override Vector2 FromValue (SWBaseInfo info)
		{
			return info.effector.data.effectDataUV.amountA;
		}
	}



	[System.Serializable]
	public enum SWTexShowChannel
	{
		all = 0,
		r,
		g,
		b,
		a
	}

	/// <summary>
	/// UV Window:edit UV node
	/// </summary>
	[System.Serializable]
	public class SWWindowEffectUV : SWWindowEffect {
		public static SWWindowEffectUV Instance;
		public  static void ShowEditor(SWNodeEffector e) {  
			if (Instance != null)
				Instance.Close ();
			var window =EditorWindow.GetWindow<SWWindowEffectUV> (true,"UV");
			window.Init (e);
			window.InitOnce ();
		} 

		public override void Update ()
		{
			Instance = this;
			base.Update ();
		}

		public override void AddSlotNox (List<SWSlot> slotsNodebox)
		{
			slotsNodebox.Add(new SWSlot("UV",SWTipsText.Effect_t_UV,null,KeyCode.U));
		}

		#region ops
		protected override void OpsInitExtra ()
		{
			base.OpsInitExtra ();
			ops.Add (SWEffectWindowOp.UVR, ScriptableObject.CreateInstance<SWOpTool_UVR> ());
			ops.Add (SWEffectWindowOp.UVG, ScriptableObject.CreateInstance<SWOpTool_UVG> ());
			ops.Add (SWEffectWindowOp.UVB, ScriptableObject.CreateInstance<SWOpTool_UVB> ());
			ops.Add (SWEffectWindowOp.UVA, ScriptableObject.CreateInstance<SWOpTool_UVA> ());
		}

		public override void OpsUV ()
		{
			base.OpsUV ();
			showOps.Add (SWEffectWindowOp.UVR);
			showOps.Add (SWEffectWindowOp.UVG);
			showOps.Add (SWEffectWindowOp.UVB);
			showOps.Add (SWEffectWindowOp.UVA);
		}

		protected override void DrawTopUV ()
		{
			EffectDataUV dataUV = data.effectDataUV;
			UI_Vector2 ("R:", ref dataUV.amountR,ops [SWEffectWindowOp.UVR].Data2UI,false,20);
			Tooltip_Rec (SWTipsText.Effect_R);
			GUILayout.Space (al_spacingBig);
			UI_Vector2 ("G:", ref dataUV.amountG,ops [SWEffectWindowOp.UVG].Data2UI,false,20);
			Tooltip_Rec (SWTipsText.Effect_G);
			GUILayout.Space (al_spacingBig);
			UI_Vector2 ("B:", ref dataUV.amountB,ops [SWEffectWindowOp.UVB].Data2UI,false,20);
			Tooltip_Rec (SWTipsText.Effect_B);
			GUILayout.Space (al_spacingBig);
			UI_Vector2 ("A:", ref dataUV.amountA,ops [SWEffectWindowOp.UVA].Data2UI,false,20);
			Tooltip_Rec (SWTipsText.Effect_A);
		}



		protected override void DrawExtra ()
		{
			base.DrawExtra ();

			DrawModuleTitle ("Blend");
			Tooltip_Rec (SWTipsText.Right_BlendModule,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			GUILayout.BeginHorizontal ();
			EffectDataUV dataUV = data.effectDataUV;
			var temp = (SWUVop)UI_PopEnum ("Blend Op", dataUV.op,true);
			if (dataUV.op != temp) {
				SWUndo.Record(info.effector);
				dataUV.op = temp;
			}
			GUILayout.EndHorizontal ();
			Tooltip_Rec (SWTipsText.Right_BlendOp,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			Tooltip_Rec (SWTipsText.Right_BlendFactor,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().yMax,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));
			Factor_Pick (ref dataUV.param,true,"Blend Factor");
		}
		#endregion

		protected override Texture2D BottomTexture ()
		{
			if (!info.effector.HasParent ())
				return null;
			return info.effector.GetParentTexture ();
		} 

		public override void OnGUITop ()
		{
			base.OnGUITop ();
			TexShowChnEnum ();
		}
	}
}
