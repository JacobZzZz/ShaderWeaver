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
	using System.Text.RegularExpressions;

	public class SWRegex
	{
		public static string ReplaceWord(string str,string from,string to)
		{
			Regex reg = new Regex ( string.Format( @"\b(?<![.]){0}",from));
			str = reg.Replace (str, to);
			return str;
		}

		public static string NameLegal(string name)
		{
			name = Regex.Replace (name, @"[^_a-zA-Z0-9]", "");
			return name;
		}

		public static bool ContainParam(string content,string param)
		{
			return Regex.IsMatch(content,string.Format( @"\b(?<![.]){0}",param));
		}
	}
}