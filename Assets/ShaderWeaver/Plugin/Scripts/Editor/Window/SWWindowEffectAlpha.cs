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
	public class SWWindowEffectAlpha : SWWindowEffect {
		public static SWWindowEffectAlpha Instance;
		[SerializeField]
		SWEnumPopup ePopup_Chn;

		public  static void ShowEditor(SWNodeEffector e) {  
			if (Instance != null)
				Instance.Close ();
			var window = EditorWindow.GetWindow<SWWindowEffectAlpha> (true,"Alpha");
			window.Init (e);
			window.InitOnce ();
		} 

		public override void InitOnce ()
		{
			base.InitOnce ();
			ePopup_Chn = new SWEnumPopup (new string[]{ "R", "G", "B", "A" },(int)data.effectData.pop_channel,null,
				delegate(int index){
					data.effectData.pop_channel = (SWChannel)index;
				});
		}
		public override void Update ()
		{
			Instance = this;
			base.Update ();
		}

		protected override void DrawExtra ()
		{
			base.DrawExtra ();
			DrawModuleTitle ("Alpha");
			Tooltip_Rec (SWTipsText.Right_AlphaModule,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Final",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(SWGlobalSettings.LabelWidthLong));
			data.effectData.pop_final = EditorGUILayout.Toggle(data.effectData.pop_final, GUILayout.Width(SWGlobalSettings.FieldWidth));
			GUILayout.EndHorizontal ();
			Tooltip_Rec (SWTipsText.Right_AlphaFinal,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

	
			GUILayout.BeginHorizontal ();
			float wid = SWGlobalSettings.LabelWidthLong + SWGlobalSettings.FieldWidth - 8;
			wid *= 0.25f;
			GUILayout.Label ("Min",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(wid));
			data.effectData.pop_min = EditorGUILayout.FloatField(data.effectData.pop_min,GUILayout.Width(wid));
			GUILayout.Label ("Max",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(wid));
			data.effectData.pop_max = EditorGUILayout.FloatField(data.effectData.pop_max,GUILayout.Width(wid));
			GUILayout.EndHorizontal ();
			Tooltip_Rec (SWTipsText.Right_AlphaMinMax,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			ePopup_Chn.Show(SWGlobalSettings.FieldWidth,"Channel",SWGlobalSettings.LabelWidthLong);
			Tooltip_Rec (SWTipsText.Right_AlphaChannel,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));


			UI_Float ("Start", ref data.effectData.pop_startValue,null,false,false,true);
			Tooltip_Rec (SWTipsText.Right_AlphaStart,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));


			UI_Float ("Spd", ref data.effectData.pop_speed,null,false,false,true);
			Tooltip_Rec (SWTipsText.Right_AlphaSpeed,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			Tooltip_Rec (SWTipsText.Right_AlphaSpeedFactor,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().yMax,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));
			Factor_Pick(ref data.effectData.pop_Param,true,"Spd Factor");

			Tooltip_Rec (SWTipsText.Right_BlendFactor,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().yMax,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));
			Factor_Pick (ref data.effectDataColor.param,true,"Blend Factor");
		}

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
