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
	using System;

	[System.Serializable]
	public enum SWRenderType
	{
		Opaque,
		Transparent,
		TransparentCutout,
		Background,
		Overlay,
		TreeOpaque,
		TreeTransparentCutout,
		Grass,
		GrassBillboard
	}

	[System.Serializable]
	public enum SWShaderQueue
	{
		Background = 1000,
		Geometry = 2000,
		AlphaTest = 2450,
		Transparent = 3000,
		Overlay = 4000
	}

	[System.Serializable]
	public enum SWShaderType
	{
		normal = 0,
		sprite = 1,
		ui = 2,
		uiFont =3,
		ngui_ui2dSprite=4
	}

	[System.Serializable]
	public enum SWShaderModel
	{
		auto=0,
		m10=1,
		m20=2,
		m30=3,
		m40=4,
		m50=5
	}

	[System.Serializable]
	public enum SWSpriteLightType
	{
		no = 0,
		diffuse = 1
	}

	[Serializable]
	public enum SWShaderBlend
	{
		off=-1,
		blend=0,
		add=1,
		mul=2,
	}

	/// <summary>
	/// Save as Json
	/// </summary>
	[Serializable]
	public class SWData
	{
		public SWShaderQueue shaderQueue = SWShaderQueue.Transparent;
		/// <summary>
		/// RenderType
		/// </summary>
		public SWRenderType rt = SWRenderType.Transparent;
		public int shaderQueueOffset;
		public SWShaderType shaderType;
		public SWSpriteLightType spriteLightType;
		public SWShaderModel shaderModel;
		public SWShaderBlend shaderBlend = SWShaderBlend.blend;
		public bool excludeRoot;
		public int version = 1;
		public float pixelPerUnit;
		public Rect spriteRect;
		public string title = "effect1";
		public string materialGUID;
		public List<string> masksGUID = new List<string> ();

		public List<SWParam> paramList = new List<SWParam>();
		public List<SWDataNode> nodes = new List<SWDataNode>();
		public float clipValue;
		public string fallback = "Standard";
		/// <summary>
		/// shader name when choose in Material
		/// eg:ShaderWeaver/aaa/bbb
		/// </summary>
		public string sn;
		/// <summary>
		/// Preview Update On Mouse Over
		/// </summary>
		public bool pum = true;
		/// <summary>
		/// Preview Size
		/// </summary>
		public float ps = 1;
		/// <summary>
		/// Preview Size(Mouse Over)
		/// </summary>
		public float psm = 2;

		public SWData()
		{

		}

		public SWDataNode FindNode(string id)
		{
			foreach (var item in nodes) {
				if (item.id == id)
					return item;
			}
			return null;
		}

		public bool ContainParam(SWParam param)
		{
			foreach (var item in paramList) {
				if (item.name == param.name)
					return true;
			}	
			return false;
		}
	}

	/// <summary>
	/// Write all data into shader file as Json
	/// </summary>
	public class SWDataManager{
		public static bool IsSWShader(string fullPath)
		{
			return File.ReadAllText (fullPath).Contains ("ShaderWeaverData");
		}

		public static void Save(string path,SWData data)
		{
			string s = JsonUtility.ToJson (data);
			s = "//"+ SWGlobalSettings.ShaderID + s + "\n";
			s += File.ReadAllText (path);
			File.WriteAllText (path, s);
		}

		public static SWData Load(string path)
		{
			string jsonTxt = GetShaderJson (File.ReadAllLines (path));
			if (!string.IsNullOrEmpty (jsonTxt)) {
				SWData e = JsonUtility.FromJson<SWData> (jsonTxt);
				VersionUpdate (e);
				return e;
			}
			return null;
		}

		private static string GetShaderJson(string[] lines)
		{
			foreach (var line in lines) {
				if (line.Contains (SWGlobalSettings.ShaderID)) {
					var cArray = line.ToCharArray ();
					for (int i = 0; i < cArray.Length; i++) {
						if (cArray [i] == '{') {
							return line.Substring (i);
						}
					}
				}
			}
			return "";
		}

		public static string DataToText(List<SWDataNode> nodes)
		{
			SWData data = new SWData ();
			data.nodes = nodes;
			string s = JsonUtility.ToJson (data);
			return s;
		}

		public static List<SWDataNode> TextToData(string text)
		{
			SWData e = JsonUtility.FromJson<SWData> (text);
			return e.nodes;
		}

		public static bool StringIsData(string txt)
		{
			return txt.Contains ("shaderQueue");
		}

		public static void VersionUpdate(SWData data)
		{
			if (data.version >= SWGlobalSettings.version)
				return;
			if (data.version < 120)
				VersionUpdate_120 (data);
			if (data.version < 121)
				VersionUpdate_121 (data);
			FixParams (data);

			if (data.version < 130)
				VersionUpdate_130 (data);
			data.version = SWGlobalSettings.version;
		}

		protected static void VersionUpdate_120(SWData data)
		{
			//new blend options
			foreach (var item in data.nodes) {
				if (item.effectDataColor.op == SWOutputOP.blendInner) {
					item.effectDataColor.op = SWOutputOP.addInner;
				}
				if (item.effectDataColor.op == SWOutputOP.add) {
					item.effectDataColor.op = SWOutputOP.mul;
				}
			}
			//Add masks GUID
			List<string> masksGUID = new List<string>();
			foreach (var item in data.nodes) {
				if (item.type == SWNodeType.mask) {
					if (!masksGUID.Contains (item.textureGUID)) {
						masksGUID.Add (item.textureGUID);
					}
				}
			}
			data.masksGUID = masksGUID;
		}
		protected static void VersionUpdate_121(SWData data)
		{
			foreach (var node in data.nodes) {
				//color node now name as image node
				if (node.type == SWNodeType.color) {
					node.name = node.name.Replace ("color", "image");
					node.type = SWNodeType.image;
				}

				//new ports
				foreach (var item in node.parent) {
					node.parentPort.Add (0);
				}
				foreach (var item in node.children) {
					node.childrenPort.Add (0);
				}
			}
		}

		protected static void VersionUpdate_130(SWData data)
		{
			List<SWDataNode> mixerNodes = new List<SWDataNode> ();
			foreach (var node in data.nodes) {
				node.dirty = true;
				if (node.type == SWNodeType.mixer) {
					mixerNodes.Add (node);
				}
			}

			foreach (var node in mixerNodes) {
				for (int i = 0; i < node.children.Count; i++) {
					node.childrenPort [i]++;
				}



				string s = JsonUtility.ToJson (node);
				var childNode = JsonUtility.FromJson<SWDataNode> (s);
				node.childPortNumber++;
				childNode.AssingNewID ();
				childNode.childPortNumber = 1;
				childNode.children.Clear ();
				childNode.childrenPort.Clear ();
				childNode.parent.Clear ();
				childNode.parentPort.Clear ();
				childNode.rect = new Rect (childNode.rect.x-100, childNode.rect.y, childNode.rect.width, childNode.rect.height);
				childNode.name = "alpha"+NewGUID ();
				childNode.type = SWNodeType.alpha;
				AddConnection (childNode,0,node, 0);
				data.nodes.Add (childNode);
			}
		}



		public static void FixParams(SWData data)
		{
			foreach (var item in data.nodes) {
				FixParam (data,ref item.effectData.t_Param);
				FixParam (data,ref item.effectData.r_Param);
				FixParam (data,ref item.effectData.s_Param);
				FixParam (data,ref item.effectData.pop_Param);
				FixParam (data,ref item.effectDataUV.param);
				FixParam (data,ref item.effectDataColor.param);
			}
		}
		public static void FixParam(SWData data,ref string param)
		{
			//1 _TimeAll->_Time      _TimeMod->1
			param = param.Replace ("_TimeAll", "_Time");
			param = param.Replace ("_TimeMod", "1");

			//_Time.y*2 ->(_Time.y*2)
			if (!ParamIsPredefined (data, param)) {
				if (param [0] != '(' || param [param.Length - 1] != ')') {
					param = string.Format ("({0})",param);
				}
			}
		}

		public static bool ParamIsPredefined(SWData data,string param)
		{
			if (param == "_Time.y")
				return true;
			foreach (var item in data.paramList) {
				if (item.name == param)
					return true;
			}
			return false;
		}

		public static string NewGUID()
		{
			System.Guid guid =	System.Guid.NewGuid ();
			string str = guid.ToString ();
			str = str.Replace ('-', '_');
			return str;
		}

		public static void AddConnection(SWDataNode leftNode,int leftPort,SWDataNode rightNode,int rightPort)
		{
			if (leftNode.parent.Contains (rightNode.id)) {
				int index = leftNode.parent.IndexOf (rightNode.id);
				leftNode.parentPort [index] = leftPort;
			} else {
				leftNode.parent.Add(rightNode.id);
				leftNode.parentPort.Add (leftPort);
			}


			if (rightNode.children.Contains (leftNode.id)) {
				int index = rightNode.children.IndexOf (leftNode.id);
				rightNode.childrenPort [index] = rightPort;
			} else {
				rightNode.children.Add(leftNode.id);
				rightNode.childrenPort.Add (rightPort);
			}
		}

		public static void RemoveConnection(SWDataNode parent,SWDataNode child)
		{
			int id = parent.children.IndexOf (child.id);
			parent.children.RemoveAt (id);
			parent.childrenPort.RemoveAt (id);

			id = child.parent.IndexOf (parent.id);
			child.parent.RemoveAt (id);
			child.parentPort.RemoveAt (id);
		}
	}
}
