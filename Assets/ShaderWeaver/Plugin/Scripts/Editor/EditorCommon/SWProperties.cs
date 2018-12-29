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
	public class SWProperties
	{
		private static Material material
		{
			get{ 
				return SWWindowMain.Instance.viewWindow.material;
			}
		}


		public static void SetColor(SWDataNode data,Color c)
		{
			string name = string.Format ("_Color{0}",data.iName);
			SetColor (name, c);
		}
		public static void SetColor(string name,Color c)
		{
			if(material!=null)
				material.SetColor (name, c);
			SWWindowMain.Instance.viewWindow.needRender = true;
		}

		public static void SetParam(SWDataNode data,SWCodeParamUse p)
		{
			string name = string.Format ("{0}_{1}",data.iName,p.n);
			SetFloat (name, p.fv);
		}
		public static void SetFloat(string name,float v)
		{
			if(material!=null)
				material.SetFloat (name, v);
			SWWindowMain.Instance.viewWindow.needRender = true;
		}
	}
}