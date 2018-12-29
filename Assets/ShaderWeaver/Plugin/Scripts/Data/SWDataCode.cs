//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using System;

	public enum CodeParamType
	{
		Color=0,
		UV=1,
		Alpha=2,
		Float=3,
		Range=4,
		CustomParam=5
	}

	[Serializable]
	public class CodeParam
	{
		public string name;
		public CodeParamType type;
		/// <summary>
		/// Default Value
		/// </summary>
		public float v;
		public float min;
		public float max;

		public CodeParam Clone()
		{
			CodeParam c = new CodeParam (name,type);
			c.name = name;
			c.type = type;
			c.v = v;
			c.min = min;
			c.max = max;
			return c;
		}

		public CodeParam(string _name,CodeParamType _type)
		{
			name = _name;
			type = _type;
		}

		public new SWDataType GetType()
		{
			if (type == CodeParamType.Color)
				return SWDataType._Color;
			if (type == CodeParamType.UV)
				return SWDataType._UV;
			return SWDataType._Alpha;
		}

		public bool IsParam()
		{
			if (type == CodeParamType.Color)
				return false;
			if (type == CodeParamType.UV)
				return false;
			if (type == CodeParamType.Alpha)
				return false;
			return true;
		}

		public bool IsProperty()
		{
			return type == CodeParamType.Float || type == CodeParamType.Range;
		}
	}
	[Serializable]
	public class SWDataCode
	{
		public string name;
		public string description="";
		public CodeParam output;
		public List<CodeParam> inputs;
		public string code;

		public SWDataCode(string _name,CodeParamType _outType)
		{
			name = _name;
			description = "";
			output = new CodeParam("o",_outType);
			inputs = new List<CodeParam> ();
			code = "";
		}

		public SWDataCode Clone(string newName)
		{
			SWDataCode item = new SWDataCode (newName, output.type);
			item.name = newName;
			item.description = description;
			item.output = output.Clone ();
			foreach (var input in inputs) {
				item.inputs.Add(input.Clone());
			}
			item.code = code;
			return item;
		}

		/// <summary>
		/// Params from nodes
		/// </summary>
		/// <returns>The inputs.</returns>
		public  List<CodeParam> RealInputs()
		{
			List<CodeParam> list = new List<CodeParam> ();
			foreach (var item in inputs) {
				if (item.type == CodeParamType.Color ||
				   item.type == CodeParamType.UV ||
				   item.type == CodeParamType.Alpha)
					list.Add (item);
			}
			return list;
		}

		public void AddParam()
		{
			CodeParam p = new CodeParam (NextParam (),CodeParamType.Color);
			inputs.Add (p);
		}

		public string NextParam()
		{
			for(char c='a';c<='z';c++)
			{
				string str = "" + c;
				if (!ContainParam (str)) {
					return str;
				}
			} 
			return SWDataManager.NewGUID ();
		}

		public bool ContainParam(string name)
		{
			if (output.name == name)
				return true;
			foreach (var item in inputs) {
				if (item.name == name)
					return true;
			}
			return false;
		}

		public CodeParam GetParam(string name)
		{
			if (output.name == name)
				return output;
			foreach (var item in inputs) {
				if (item.name == name)
					return item;
			}
			return null;
		}

		public bool IsFunction()
		{
			return code.Contains ("return");
		}
	}
}