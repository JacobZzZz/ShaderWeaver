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

	/// <summary>
	/// Save as Json
	/// </summary>
	[Serializable]
	public class SWData_Codes
	{
		public List<SWDataCode> codes = new List<SWDataCode> ();
		public List<string> CodeOfCate(CodeParamType type)
		{
			List<string> list = new List<string> ();
			foreach (var item in codes) {
				if (item.output.type == type)
					list.Add (item.name);
			}
			return list;
		}
		public SWDataCode CodeOfName(string name)
		{
			foreach (var item in codes) {
				if (item.name == name)
					return item;
			}
			return null;
		}
	}
	public class SWDataManager_Code
	{
		public static void Save(string swFolder,SWData_Codes data)
		{
			//Delete all .swcode files
			string folder = CodeFolder (swFolder);
			DirectoryInfo d = new DirectoryInfo (folder);
			foreach (var item in d.GetFiles()) {
				if (item.FullName.EndsWith (".swcode")) {
					File.Delete (item.FullName);
				}
			}

			//Create all .swcode files
			foreach (var item in data.codes) {
				string path = folder + item.name + ".swcode";
				SaveSub (path, item);
			}
		}
		private static void SaveSub(string path,SWDataCode data)
		{
			string txt = JsonUtility.ToJson(data);
			File.WriteAllText (path, txt);
		}
		public static SWData_Codes Load(string swFolder)
		{
			string folder = CodeFolder (swFolder);
			SWData_Codes codes = new SWData_Codes ();
			DirectoryInfo d = new DirectoryInfo (folder);
			foreach (var item in d.GetFiles()) {
				if (item.FullName.EndsWith (".swcode")) {
					SWDataCode code = LoadSub (item.FullName);
					codes.codes.Add (code);
				}
			}
			return codes;
		}
		private static SWDataCode LoadSub(string fullPath)
		{
			string jsonTxt = File.ReadAllText (fullPath);
			return JsonUtility.FromJson<SWDataCode> (jsonTxt);
		}
		private static string CodeFolder(string swFolder)
		{
			string str = SWGlobalSettings.AssetsFullPath;
			return str.Substring (0, str.Length - 6) + swFolder.Substring(0,swFolder.Length-7) + "/Codes/";
		}
	}
}