//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using UnityEditor;

	/// <summary>
	/// Create material
	/// </summary>
	public class SWMaterialManager{
		private static SWWindowMain edit;
		public static Material CreateMaterial(SWWindowMain _edit)
		{
			edit = _edit;
			//step:shader
			var shader = CreateStep_Shader ();
			if (edit.data.shaderType == SWShaderType.ngui_ui2dSprite) {
				CreateStep_ShaderNGUI ();
			}

			//step:material
			Material mat = CreateStep_Material (shader);
			return mat;
		}


		#region Shader
		private static Shader CreateStep_Shader()
		{
			SWShaderCreaterBase sc = null;
			if (edit.data.shaderType == SWShaderType.normal) {
				sc = new SWShaderCreaterBase (edit);
			}
			else if (edit.data.shaderType == SWShaderType.ui) {
				sc = new SWShaderCreaterUI (edit);
			}
			else if (edit.data.shaderType == SWShaderType.uiFont) {
				sc = new SWShaderCreaterUIFont (edit);
			}
			else if (edit.data.shaderType == SWShaderType.sprite) {
				if(edit.data.spriteLightType == SWSpriteLightType.no)
					sc = new SWShaderCreaterSprite (edit);
				else if(edit.data.spriteLightType == SWSpriteLightType.diffuse)
					sc = new SWShaderCreaterSpriteLight (edit);
			}
			else if (edit.data.shaderType == SWShaderType.ngui_ui2dSprite) {
				sc = new SWShaderCreaterNGUI (edit);
			}
			return CreateShader (sc);
		}
		private static Shader CreateShader(SWShaderCreaterBase sc)
		{
			foreach (var node in edit.nodes) {
				node.shaderOutput = null;
			}

			//step 1: path
			string path = ShaderFilePath(sc.postFix_fileName);

			//step 2: gen shader text
			string txt = sc.CreateShaderText();
			txt = CorrectLineEnding (txt);

			//step 3: create file
			var shader = CreateShaderFile (path,txt);
			return shader;
		}
		private static string ShaderFilePath(string postfix = "")
		{
			return string.Format ("{0}{1}{2}.shader", edit.folderPath, edit.data.title,postfix);
		}
		private static Shader CreateShaderFile(string path,string txt)
		{
			string fullPath = SWCommon.Path2FullPath (path);
			string adbPath =  SWCommon.Path2AssetDBPath (path);
			//			string guid = AssetDatabase.AssetPathToGUID (adbPath);
			File.WriteAllText(fullPath, txt );    
			AssetDatabase.ImportAsset(adbPath, ImportAssetOptions.ForceUpdate);
			Shader currentShader = AssetDatabase.LoadAssetAtPath<Shader> ( adbPath);
			return currentShader;
		}
		static string CorrectLineEnding(string str)
		{
			return str.Replace("\r\n", "\n");
		}


		#region NGUI
		private static void CreateStep_ShaderNGUI()
		{
			CreateShader (new SWShaderCreaterNGUIClipTex (edit));
			CreateShader (new SWShaderCreaterNGUIClip1 (edit));
			CreateShader (new SWShaderCreaterNGUIClip2 (edit));
			CreateShader (new SWShaderCreaterNGUIClip3 (edit));
		}

	
		#endregion
		#endregion

		#region mat
		private static Material CreateStep_Material(Shader shader)
		{
			string path = "";
			if (edit.newCopy || string.IsNullOrEmpty (edit.data.materialGUID)
				||string.IsNullOrEmpty(AssetDatabase.GUIDToAssetPath (edit.data.materialGUID))) 
				path = string.Format ("{0}{1}.mat", edit.folderPath, edit.data.title);
			else
				path = SWCommon.AssetDBPath2Path(AssetDatabase.GUIDToAssetPath (edit.data.materialGUID));

			Material m = AssetDatabase.LoadAssetAtPath<Material> (SWCommon.Path2AssetDBPath(path));
			if (m == null) {
				m = new Material (shader);
				SetMaterialProp (m, edit);
				m = SWCommon.SaveReload<Material> (m, path);
			} else {
				m.shader = shader;
				SetMaterialProp (m, edit);
			}
			edit.data.materialGUID = SWEditorTools.ObjectToGUID (m);
			return m;
		}
		private static void SetMaterialProp(Material m,SWWindowMain edit)
		{
			foreach (var item in edit.textures) {
				if(item.Value !=null)
					m.SetTexture (item.Key, item.Value);
			}


			foreach (var item in edit.nodes) {
				if (item.HasColorAttribute ()) {
					//					if(item.data.type == SWNodeType.root)
					//						m.SetColor (string.Format ("_Color"),item.data.effectDataColor.color);
					//					else
					//						m.SetColor (string.Format ("_Color{0}", item.data.iName),item.data.effectDataColor.color);
					m.SetColor (string.Format ("_Color{0}", item.data.iName),item.data.effectDataColor.color);
				}
			}
		}
		#endregion
	}
}