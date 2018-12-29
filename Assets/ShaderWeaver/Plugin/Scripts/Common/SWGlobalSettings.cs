//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	/// <summary>
	/// Global settings.
	/// </summary>
	public class SWGlobalSettings{
		/// <summary>
		/// Version Number	120 = 1.2.0
		/// </summary>
		public static readonly int version = 130;
		/// <summary>
		/// Has Space
		/// </summary>
		public static readonly string ProductTitle = "Shader Weaver";
		/// <summary>
		/// No Space
		/// </summary>
		public static readonly string ProductName = "ShaderWeaver";
		public static string AssetsFullPath
		{
			get{return Application.dataPath;}
		}
		public static string ShaderID
		{
			get{return ProductName+"Data";}
		}
		public static readonly float WindowStartHeight = 21;
		public static readonly float LabelWidthLong = 75;
		public static readonly float LabelWidth = 60;
		public static readonly float LabelWidthShort = 30;
		public static readonly float FieldWidth = 100;
		public static readonly float UIGap = 5;
		public static readonly float UISpacing = 20;
		public static readonly float al_topHeightMain = 63;
		public static readonly float SliderWidth = 200;
		public static readonly float Vector2Width = 250;
		public static readonly float FloatWidth = 250;
		public static readonly int TexDefaultSize = 512;
	}
}
