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
	using System.Reflection;
	using System;

	public enum SWColorPl
	{
		blue,
		green,
		red,
		yellow,
		light,
		dark
	}


	public enum SWCustomStyle
	{
		none=-1,
		eTooltip=0,
		eTool0=1,
		eTool1=2,
		eTool2=3,
		eTool3=4,
		eToolDown0=5,
		eToolDown1=6,
		eToolDown2=7,
		eToolDown3=8,
		eToolDrag0=9,
		eToolDrag1=10,
		eToolDrag2=11,
		eToolDrag3=12,
		eLine=13,
		eNode0=14,
		eNode1=15,
		eNode2=16,
		eNode3=17,
		eNodeSelected=18,
		eNodeTitle=19,
		eTxtLight=20,
		eCodeLeft=21,
		eCodeRight=22,
		eCode=23,
		eTxtSmallLight=24,
		eTxtDark=25,
		eTxtSmallDark=26,
		eTxtRoot=27,
		eSelectRect=28,
		eImageFrame=29,
		// To Add
	}

	public enum SWUITex
	{
		logo,
		cursor,
		cursorCenter,
		cursorWand,
		cursorDropper,
		create,
		open,
		save,
		update,
		updateGray,
		canvasBG,
		canvasFG,
		uibg,
		effectRound,
		effectArrow,
		effectCenter,
		effectLine,
		effectPos,
		effectRight1,
		effectRight2,
		// To Add
	}

	/// <summary>
	/// Manage EditorUI
	/// </summary>
	public class SWEditorUI{
		#region Material
		private static Dictionary<string,Material> matDic = new Dictionary<string, Material>();
		public static Material GetMaterial(string name)
		{
			if (!matDic.ContainsKey (name)) {
				string path = SWCommon.ProductFolder()+"/Materials/";
				var item = AssetDatabase.LoadAssetAtPath<Material> (path + name+".mat");
				if(item!=null)
					matDic.Add (name, item);
			}
			return matDic[name];
		}

		private static Dictionary<string,Shader> shaderDic = new Dictionary<string, Shader>();
		public static Shader GetShader(string name)
		{
			if (!shaderDic.ContainsKey (name)) {
				string path = SWCommon.ProductFolder()+"/Shaders/";
				var item = AssetDatabase.LoadAssetAtPath<Shader> (path + name+".shader");
				if(item!=null)
					shaderDic.Add (name, item);
			}
			return shaderDic[name];
		}
		#endregion

//		private static Dictionary<SWColorPl,Color> ColorDic = new Dictionary<SWColorPl, Color>(){
//			//{SWColorPl.blue,new Color (0.557f, 0.871f, 0.796f, 1f)},
//			{SWColorPl.green,new Color (0.1f,0.9f,0.1f,1f)},
//			//{SWColorPl.red,new Color (1f, 0.253f, 0.253f, 1f)},
//			//{SWColorPl.yellow,new Color (1f, 0.508f, 0.0f, 1f)},
//			{SWColorPl.light,new Color (0.9f, 0.9f, 0.9f, 1f)},
//			//{SWColorPl.dark,new Color (0.365f, 0.365f, 0.365f, 1f)}
//
//			// To Add
//		};
		private static Dictionary<SWColorPl,Color32> ColorDic = new Dictionary<SWColorPl, Color32>(){
			//{SWColorPl.blue,new Color (0.557f, 0.871f, 0.796f, 1f)},
			{SWColorPl.green,new Color32 (25,230,25,255)},
			//{SWColorPl.red,new Color (1f, 0.253f, 0.253f, 1f)},
			//{SWColorPl.yellow,new Color (1f, 0.508f, 0.0f, 1f)},
			{SWColorPl.light,new Color32 (210,210,210,255)},
			//{SWColorPl.dark,new Color32 (111,111,111,255)}
			// To Add
		};

		public static Color ColorPalette(SWColorPl e)
		{
			return ColorDic [e];
		}
		#region textures
		private static Dictionary<SWUITex,Texture2D> texDic = new Dictionary<SWUITex, Texture2D>();
		private static Dictionary<SWUITex,string> texPathDic = new Dictionary<SWUITex, string>(){
			{SWUITex.logo,"logo.png"},
			{SWUITex.cursor,"cursor.png"},
			{SWUITex.cursorCenter,"cursorCenter.png"},
			{SWUITex.cursorWand,"cursorWand.png"},
			{SWUITex.cursorDropper,"cursorDropper.png"},
			{SWUITex.create,"New.png"},
			{SWUITex.open,"Open.png"},
			{SWUITex.save,"Save.png"},
			{SWUITex.update,"Update.png"},
			{SWUITex.updateGray,"UpdateGray.png"},
			{SWUITex.canvasBG,"canvasBG.png"},
			{SWUITex.canvasFG,"canvasFG.png"},

			{SWUITex.uibg,"uibg.png"},
			{SWUITex.effectArrow,"effectArrow.png"},
			{SWUITex.effectCenter,"effectCenter.png"},
			{SWUITex.effectLine,"effectLine.png"},
			{SWUITex.effectRound,"effectRound.png"},
			{SWUITex.effectPos,"effectPos.png"},
			{SWUITex.effectRight1,"right1.png"},
			{SWUITex.effectRight2,"right2.png"}
			// To Add
		};
			
		public static Texture2D Texture(SWUITex e)
		{
			if (!texDic.ContainsKey (e)) {
				string path = SWCommon.ProductFolder()+"/UI/";
				var item = AssetDatabase.LoadAssetAtPath<Texture2D> (path + texPathDic[e]);
				if(item!=null)
					texDic.Add (e, item);
			}
			return texDic[e];
		}
		#endregion

		#region styles
		protected static GUISkin mainSkin;
		public static GUISkin MainSkin
		{
			get{ 
				if (mainSkin == null) {
					mainSkin = AssetDatabase.LoadAssetAtPath<GUISkin>( SWCommon.ProductFolder()+"/Skin/MainSkin.guiskin" );
				}
				return mainSkin;
			}
		}
	
		protected static GUIStyle styleTxtSmallLight;
		public static GUIStyle Style_Get(SWCustomStyle _style)
		{
			if (_style == SWCustomStyle.eTxtSmallLight) {
				if (styleTxtSmallLight == null) {
					styleTxtSmallLight = new GUIStyle( GUI.skin.label);
					styleTxtSmallLight.normal.textColor= new Color(0.7058824f,0.7058824f,0.7058824f,1f);
					styleTxtSmallLight.stretchWidth = false;
					styleTxtSmallLight.stretchHeight = false;
				}
				return styleTxtSmallLight;
			}

			GUIStyle style = null; 
			int id = (int)_style;
			if (id == -1) 
				style = GUIStyle.none;
			else
				style = MainSkin.customStyles [id];
			return style;
		}
			
		#endregion

		public static Vector2 Vector2Field(string txt,Vector2 v)
		{
			GUILayout.Label (txt,Style_Get(SWCustomStyle.eTxtSmallLight));
			GUILayout.Label ("X",Style_Get(SWCustomStyle.eTxtSmallLight));
			float x = EditorGUILayout.FloatField ( v.x,GUILayout.Width(50));
			GUILayout.Label ("Y",Style_Get(SWCustomStyle.eTxtSmallLight));
			float y = EditorGUILayout.FloatField ( v.y,GUILayout.Width(50));
			return new Vector2 (x, y);
		}

		public static float FloatField(string txt,float v,bool flexible = true)
		{
			if (flexible) {
				GUILayout.Label (txt, Style_Get (SWCustomStyle.eTxtSmallLight));
				var t = EditorGUILayout.FloatField (v, GUILayout.Width (50));
				return t;
			} else {
				GUILayout.Label (txt, Style_Get (SWCustomStyle.eTxtSmallLight),
					GUILayout.Width (SWGlobalSettings.LabelWidthLong));
				var t = EditorGUILayout.FloatField (v, GUILayout.Width (SWGlobalSettings.FieldWidth));
				return t;
			}
		}
	}
}