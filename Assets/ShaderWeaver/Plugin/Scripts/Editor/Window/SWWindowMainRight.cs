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

	public partial class SWWindowMain:SWWindowLayoutV{
		protected readonly float leftWidth = 90;
		[SerializeField]
		SWEnumPopup ePopup_blendMode;
		[SerializeField]
		SWEnumPopup ePopup_renderOrder;
		[SerializeField]
		SWEnumPopup ePopup_renderType;

		protected void InitRightUp()
		{
			ePopup_blendMode = new SWEnumPopup (new string[]{"Off","Blend", "Add", "Mul"},(int)(data.shaderBlend+1),null,
				delegate(int index)
				{
					SWUndo.Record (this);
					data.shaderBlend = (SWShaderBlend)(index-1); 
				}
			);
			ePopup_renderOrder = new SWEnumPopup (typeof(SWShaderQueue),(int)data.shaderQueue,true,null,
				delegate(int index)
				{
					SWUndo.Record (this);
					data.shaderQueue = (SWShaderQueue)index; 
				}
			);

			ePopup_renderType = new SWEnumPopup (typeof(SWRenderType),(int)data.rt,false,null,
				delegate(int index)
				{
					SWUndo.Record (this);
					data.rt = (SWRenderType)index; 
				}
			);
		}

		protected override void DrawRightUp()
		{
			DrawBG (rightUpRect);
			//GUILayout.Space (position.height * 0.3f);

			try
			{
				if(IsSelectSingleCode())
					DrawRightUp_Code ();
				else
					DrawRightUp_Settings ();
			}
			catch(System.Exception e) {

			}
		}

		bool IsSelectSingleCode()
		{
			if (selection.Count == 1) {
				var node = NodeAll() [selection [0]];
				if (node.data.type == SWNodeType.code) {
					nodeCode = (SWNodeCode)node;
					return true;
				}
			}
			return false;
		}

		void DrawRightUp_Settings()
		{
			GUILayout.Space (5);
			GUILayout.BeginHorizontal ();
			GUILayout.Space (5);
			DrawModuleTitle ("Settings");
			Tooltip_Rec (SWTipsText.Settings_Module);
			GUILayout.EndHorizontal ();
			GUILayout.Space (10);

			GUILayout.BeginHorizontal (GUILayout.Width(170));
			newName = EditorGUILayout.TextField (newName,GUILayout.Width(96));
			if (GUILayout.Button ("Rename"))
				PressRename ();
			GUILayout.EndHorizontal ();

			GUILayout.Space (10);
		
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Shader Name:",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(leftWidth));
			var tmpName = EditorGUILayout.TextField(data.sn,GUILayout.Width(rightupWidth-10-leftWidth));
			if (data.sn != tmpName) {
				SWUndo.Record (this);
				data.sn = tmpName; 
			}
			GUILayout.EndHorizontal ();
			SWTooltip.Rec (this,SWTipsText.Settings_ShaderName);


			string[] strs = { "Default", "Sprite", "UI", "Text", "UI2D Sprite (NGUI)" };
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Shader Type:",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(leftWidth));
			var tmpType = (SWShaderType)EditorGUILayout.Popup((int)data.shaderType,strs,GUILayout.Width(rightupWidth-10-leftWidth));
			if (data.shaderType != tmpType) {
				SWUndo.Record (this);
				data.shaderType = tmpType; 
				if (tmpType == SWShaderType.normal)
					nRoot.texture = nRoot.texForNormal;
				else 
					nRoot.texture = nRoot.texForSprite;
			}
			GUILayout.EndHorizontal ();
			SWTooltip.Rec (this,SWTipsText.Settings_Type);

			ChooseSpriteLightType ();
			ChooseShaderModel ();


			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Exclude Root:",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(leftWidth));
			var tmp0 = EditorGUILayout.Toggle(data.excludeRoot,GUILayout.Width(rightupWidth-10-leftWidth));
			if (data.excludeRoot != tmp0) {
				SWUndo.Record (this);
				data.excludeRoot = tmp0; 
			}
			GUILayout.EndHorizontal ();
			SWTooltip.Rec (this,SWTipsText.Settings_ExcludeRoot);

			ePopup_blendMode.Show (rightupWidth - 10 - leftWidth, "Blend Mode:", leftWidth);
			SWTooltip.Rec (this,SWTipsText.Settings_Blend);


			GUILayout.BeginHorizontal ();
			ePopup_renderOrder.Show (170 - leftWidth, "Render Order:", leftWidth, false);
			GUILayout.Label ("+",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(14));
			var tmp4 = EditorGUILayout.IntField (data.shaderQueueOffset,GUILayout.Width(30));
			if (tmp4 != data.shaderQueueOffset) {
				SWUndo.Record (this);
				data.shaderQueueOffset = tmp4;
			}
			GUILayout.Label ("= "+
				((int)data.shaderQueue+data.shaderQueueOffset),SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(60));
			GUILayout.EndHorizontal ();
			SWTooltip.Rec (this,SWTipsText.Settings_Queue);

			GUILayout.BeginHorizontal ();
			ePopup_renderType.Show (rightupWidth-10-leftWidth, "Render Type:", leftWidth, false);
			GUILayout.EndHorizontal ();
			SWTooltip.Rec (this,SWTipsText.Settings_RenderType);

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Alpha Clip:",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(leftWidth));
			var tmp5 = EditorGUILayout.FloatField(data.clipValue,GUILayout.Width(rightupWidth-10-leftWidth));
			if (data.clipValue != tmp5) {
				SWUndo.Record (this);
				data.clipValue = tmp5; 
			}
			GUILayout.EndHorizontal ();
			SWTooltip.Rec (this,SWTipsText.Settings_Clip);

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Fallback:",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(leftWidth));
			var fallbackPress = GUILayout.Button(data.fallback,GUILayout.Width(rightupWidth-10-leftWidth));
			var xtype = Event.current.type;
			var xrect = GUILayoutUtility.GetLastRect ();
			if (xtype == EventType.Repaint) {
				fallbackRect = new Rect(xrect.x,xrect.y,xrect.width,xrect.height);
			}
			if (fallbackPress) {
				DisplayShaderContext(fallbackRect);
			}
			GUILayout.EndHorizontal ();
			SWTooltip.Rec (this,SWTipsText.Settings_Fallback);

			GUI.color = Color.gray;
			GUILayout.Label ("_______________________________________",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight));
			GUI.color = Color.white;
			DrawModuleTitle ("Preview");
			SWTooltip.Rec (this,SWTipsText.Settings_ModulePreview);

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Update On Mouse Over",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight));
			data.pum = EditorGUILayout.Toggle (data.pum);
			GUILayout.EndHorizontal ();
			SWTooltip.Rec (this,SWTipsText.Settings_PreviewUpdateOnMouseOver);


			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Size",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(leftWidth+20));
			data.ps = EditorGUILayout.Slider (data.ps, 1, 5,GUILayout.Width(160));
			GUILayout.EndHorizontal ();
			SWTooltip.Rec (this,SWTipsText.Settings_PreviewSize);

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Size(Mouse Over)",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(leftWidth+20));
			data.psm = EditorGUILayout.Slider (data.psm, viewWindow.scale, 5,GUILayout.Width(160));
			GUILayout.EndHorizontal ();
			SWTooltip.Rec (this,SWTipsText.Settings_PreviewSizeMouseOver);

			GUI.color = Color.gray;
			GUILayout.Label ("_______________________________________",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight));
			GUI.color = Color.white;

			Factor_CustomParamCreation ();
		}

		void ChooseSpriteLightType()
		{
			if (data.shaderType == SWShaderType.sprite) {
				string[] strs = { "No", "Diffuse" };

				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Sprite Light:",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(leftWidth));
				var tmpType = (SWSpriteLightType)EditorGUILayout.Popup((int)data.spriteLightType,strs,GUILayout.Width(rightupWidth-10-leftWidth));
				if (data.spriteLightType != tmpType) {
					SWUndo.Record (this);
					data.spriteLightType = tmpType; 
				}
				GUILayout.EndHorizontal ();
				SWTooltip.Rec (this,SWTipsText.Settings_SpriteLight);
			}
		}

		void ChooseShaderModel()
		{
			string[] strs = { "auto","1.0", "2.0","3.0","4.0","5.0" };

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Shader Model:",SWEditorUI.Style_Get(SWCustomStyle.eTxtSmallLight),GUILayout.Width(leftWidth));
			var tmpType = (SWShaderModel)EditorGUILayout.Popup((int)data.shaderModel,strs,GUILayout.Width(rightupWidth-10-leftWidth));
			if (data.shaderModel != tmpType) {
				SWUndo.Record (this);
				data.shaderModel = tmpType; 
			}
			GUILayout.EndHorizontal ();
			SWTooltip.Rec (this,SWTipsText.Settings_ShaderModel);
		}

		#region fallback
		Rect fallbackRect;  
		MenuCommand mc;
		private void DisplayShaderContext(Rect r) {
			try{
				if( mc == null )
				{
					mc = new MenuCommand( this, 0 );
					Shader shader = Shader.Find("Standard");
					Material temp = new Material(shader); 
					UnityEditorInternal.InternalEditorUtility.SetupShaderMenu( temp );
				}
				EditorUtility.DisplayPopupMenu( r, "CONTEXT/ShaderPopup", mc );
			}
			catch(System.Exception e) {
				Debug.Log (e.ToString ());
			}
		}
		private void OnSelectedShaderPopup( string command, Shader shader ) {
			if( shader != null ) {
				data.fallback = shader.name;
				//Debug.Log("Clicked shader: " + shader.name);
			}
		}
		#endregion
	}
}
	