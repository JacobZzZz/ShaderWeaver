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
	public class SWWindowEffectImage : SWWindowEffect {
		public static SWWindowEffectImage Instance;

		public  static void ShowEditor(SWNodeEffector e) { 
			if (Instance != null)
				Instance.Close ();
			var window =EditorWindow.GetWindow<SWWindowEffectImage> (true,"Image");
			window.Init (e);
			window.InitOnce ();
		} 

		public override void Update ()
		{
			Instance = this;
			base.Update ();
		}

		protected override void DrawExtra ()
		{
			base.DrawExtra ();
			DrawModuleTitle ("Image");
			Tooltip_Rec (SWTipsText.Right_ImageModule,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			EffectDataColor _data = data.effectDataColor;
			UI_Color("Color",ref _data.color,ref _data.hdr,delegate(SWBaseInfo arg1, bool arg2) {
				SWUndo.Record(info.effector);
			},true,true);
			Tooltip_Rec (SWTipsText.Right_Color,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));


			GUILayout.BeginHorizontal ();
			var temp = (SWOutputOP)UI_PopEnum ("Blend Op", _data.op,true);
			if (_data.op != temp) {
				SWUndo.Record(info.effector);
				_data.op = temp;
			}
			GUILayout.EndHorizontal ();
			Tooltip_Rec (SWTipsText.Right_BlendOp,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			Tooltip_Rec (SWTipsText.Right_BlendFactor,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().yMax,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));
			Factor_Pick (ref _data.param,true,"Blend Factor");

			DrawExtra_NormalMap ();
		}

		#region Sprite Light Normal mapping
		void DrawExtra_NormalMap()
		{
			if (!SWWindowMain.Instance.SupportNormalMap())
				return;

			//Title and toggle
			GUILayout.Space (rightupSpacing);
			GUILayout.BeginHorizontal ();
			DrawModuleTitle ("Normal Map");
			var tmp0 = EditorGUILayout.Toggle(data.nm);
			if (data.nm != tmp0) {
				SWUndo.Record (info.effector);
				data.nm = tmp0;
			} 
			GUILayout.EndHorizontal ();
			Tooltip_Rec (SWTipsText.Right_SpriteLight_NormalMapModule,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));
			if (data.nm == false)
				return;

			//Normal Tex
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Normal Map",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight), GUILayout.Width(SWGlobalSettings.LabelWidthLong));
			var tex = (Texture2D)EditorGUILayout.ObjectField(info.effector.textureNormalMap,typeof(Texture2D), false,GUILayout.Width (SWGlobalSettings.FieldWidth));
			if (tex != info.effector.textureNormalMap) {
				SWUndo.Record (info.effector);
				info.effector.textureNormalMap = tex;
				if (tex != null) {
					string adbPath = AssetDatabase.GetAssetPath (tex);
					var tImporter = AssetImporter.GetAtPath (adbPath) as TextureImporter;
					if (tImporter.textureType != TextureImporterType.NormalMap) {
						tImporter.textureType = TextureImporterType.NormalMap;
						AssetDatabase.ImportAsset( adbPath);
						AssetDatabase.Refresh ();  
					}
				}
			}
			GUILayout.EndHorizontal ();
			Tooltip_Rec (SWTipsText.Right_SpriteLight_NormalMapTex,new Rect(rightUpRect.x,GUILayoutUtility.GetLastRect ().y,rightUpRect.width,GUILayoutUtility.GetLastRect ().height));

			if (info.effector.UseNormalMap ()) {
				//Importance
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Importance", SWEditorUI.Style_Get (SWCustomStyle.eTxtSmallLight), GUILayout.Width (SWGlobalSettings.LabelWidthLong));
				var nmi = EditorGUILayout.FloatField (data.nmi, GUILayout.Width (SWGlobalSettings.FieldWidth));
				nmi = Mathf.Clamp01 (nmi);
				if (data.nmi != nmi) {
					SWUndo.Record (info.effector);
					data.nmi = nmi;
				}
				GUILayout.EndHorizontal ();
				Tooltip_Rec (SWTipsText.Right_SpriteLight_NormalMapImportance, new Rect (rightUpRect.x, GUILayoutUtility.GetLastRect ().y, rightUpRect.width, GUILayoutUtility.GetLastRect ().height));
		
				//Factor
				Tooltip_Rec (SWTipsText.Right_SpriteLight_NormalMapFactor, new Rect (rightUpRect.x, GUILayoutUtility.GetLastRect ().yMax, rightUpRect.width, GUILayoutUtility.GetLastRect ().height));
				Factor_Pick (ref data.nmf, true, "Factor");
			}
		}
		#endregion

		protected override Texture2D BottomTexture ()
		{
			if (!info.effector.HasParent ())
				return null;
			if (info.effector.GetParentNode () is SWNodeRemap) {
				return SWWindowMain.Instance.nRoot.texture;
			}
			return info.effector.GetParentTexture ();
		} 
	}
}
