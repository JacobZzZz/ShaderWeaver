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

	[Serializable]
	public enum SWCoordMode
	{
		Default,
		Sprite
	}
	[Serializable]
	public enum SWTexResolution
	{
		_128x128=0,
		_256x256=1,
		_512x512=2,
		_1024x1024=3,
	}

	[Serializable]
	public enum SWLoopMode
	{
		loop=0,
		single=1
	}



	[Serializable]
	public enum SWChannel
	{
		r,
		g,
		b,
		a
	}

	[Serializable]
	public enum SWOutputOP
	{
		blend = 0,
		blendInner = 1,
		add = 2,
		addInner=3,
		mul =4,
		mulIntersect=5,
		mulRGB=6,

		Darken=20,
		ColorBurn=21,
		LinearBurn=22,
		DarkerColor=23,
		Lighten=24,
		Screen=25,
		ColorDodge=26,
		LinearDodge=27,
		LighterColor=28,
		Overlay=29,
		SoftLight=30,
		HardLight=31,
		VividLight=32,
		LinearLight=33,
		PinLight=34,
		HardMix=35,
		Difference=36,
		Exclusion=37,
		Subtract=38,
		Divide=39,
		Hue=40,
		Saturation=41,
		Color=42,
		Luminosity=43
	}

	[Serializable]
	public enum SWUVop
	{
		add,
		lerp,
		replace
	}

	[Serializable]
	public enum SWParamType
	{
		RANGE,
		FLOAT
	}

	[Serializable]
	public class SWParam
	{
		public SWParamType type;
		public string name;
		public float min = 0;
		public float max = 1;
		public float defaultValue;
		public SWParam()
		{

		}
		public SWParam(string _name)
		{
			name = _name;
		}
	}

	[Serializable]
	public class SWCodeParamUse
	{
		/// <summary>
		/// Name
		/// </summary>
		public string n;
		/// <summary>
		/// Value
		/// </summary>
		public string v;
		/// <summary>
		/// FloatV
		/// </summary>
		public float fv;
	}


	[Serializable]
	public enum SWNodeType
	{
		root,
		mask,
		remap,
		color,
		uv,
		alpha,
		blur,
		retro,
		coord,
		dummy,
		refract,
		reflect,
		mixer,
		image,
		code
	}

	//Invisible in the editor
	[Serializable]
	public enum SWDataType
	{
		_Color,
		_UV,
		_Remap,
		_Alpha,
		_Normal
	}

	[Serializable]
	public class EffectData
	{
		public Vector2 t_startMove = Vector2.zero;
		public float r_angle = 0;
		public Vector2 s_scale = new Vector2(1f,1f);

		public Vector2 t_speed = Vector2.zero;
		public float r_speed = 0;
		public Vector2 s_speed = Vector2.zero;

		public string t_Param = "_Time.y";
		public string r_Param = "_Time.y";
		public string s_Param = "_Time.y";

		#region use for Alpha/Mixer Node 

		/// <summary>
		/// Alpha apply only to one node or apply to final graphic
		/// </summary>
		public bool pop_final;
		public float pop_min = 0;
		public float pop_max = 1;
		public float pop_startValue = 0;
		public float pop_speed = 0;
		public string pop_Param = "(1)";
		public SWChannel pop_channel = SWChannel.a;
		#endregion

		public bool useLoop = true;
		public SWLoopMode loopX = SWLoopMode.loop;
		public float gapX;
		public SWLoopMode loopY = SWLoopMode.loop;
		public float gapY;

		#region texture sheet animation
		public bool animationSheetUse;
		public int animationSheetCountX = 1;
		public int animationSheetCountY = 1;
		public bool animationSheetLoop = true;
		public bool animationSheetSingleRow;
		public int animationSheetSingleRowIndex;
		public int animationSheetStartFrame;
		public string animationSheetFrameFactor = "_Time.y";
		#endregion
	} 

	/// <summary>
	/// Use by nodes: Color Refraction Reflection
	/// </summary>
	[Serializable]
	public class EffectDataColor
	{
		public bool hdr;
		public Color color = Color.white;
		public SWOutputOP op;
		/// <summary>
		/// WRONG NAMING.It is opFactor
		/// For op, lerp/add factor
		/// </summary>
		public string param="(1)";
	}

	/// <summary>
	/// Use by UV node
	/// </summary>
	[Serializable]
	public class EffectDataUV
	{
		public SWUVop op;
		/// <summary>
		/// WRONG NAMING.It is opFactor
		/// For op, lerp/add factor
		/// </summary>
		public string param="(1)";

		public Vector2 amountR;
		public Vector2 amountG;
		public Vector2 amountB;
		public Vector2 amountA;
	}


	/// <summary>
	/// Data for each node
	/// </summary>
	[Serializable]
	public class SWDataNode{
		//iNormal
		public bool useNormal;
		public string id="";
		public string name="";
		public string iName
		{
			get{ 
				return "_" + name;
			}
		}

		public int depth = 1;
		public SWNodeType type;

		/// <summary>
		/// Right port count
		/// </summary>
		public int parentPortNumber = 1;
		/// <summary>
		/// All parents
		/// </summary>
		public List<string> parent = new List<string> ();
		/// <summary>
		/// Right port link to parent
		/// </summary>
		public List<int> parentPort = new List<int> ();

		/// <summary>
		/// Left port count
		/// 
		/// For Mixer node
		/// port 0: source port
		/// port 1-5:Child port
		/// </summary>
		public int childPortNumber = 1;
		/// <summary>
		/// All children
		/// </summary>
		public List<string> children = new List<string>();
		/// <summary>
		/// Left port link to child
		/// </summary>
		public List<int> childrenPort = new List<int> ();



		public string textureExGUID="";
		public string textureGUID="";
		/// <summary>
		/// Use for Mask Node, custom
		/// </summary>
		public bool useGray;
		/// <summary>
		/// Use for Remap Node, custom
		/// </summary>
		public bool useCustomTexture;
		public string textureGUIDGray="";
		public string spriteGUID="";
		public string spriteName="";
		public Rect rect;
		public EffectData effectData = new EffectData();

		/// <summary>
		/// Use by nodes: Color Refraction Reflection
		/// </summary>
		public EffectDataColor effectDataColor = new EffectDataColor();
		public EffectDataUV effectDataUV = new EffectDataUV();
		public SWChannel maskChannel;
		public List<SWDataType> outputType = new List<SWDataType> ();
		public List<SWDataType> inputType = new List<SWDataType>();

		public bool dirty = true;
		public SWLayerMaskString layerMask = new SWLayerMaskString();

		#region new effect
		public float blurX;
		public float blurY;
		public string blurXParam="(1)";
		public string blurYParam="(1)";

		public float retro;
		public string retroParam="(1)";

		public List<SWGradient> gradients = new List<SWGradient>();
		#endregion

		#region new effect
		#region code
		public CodeParamType codeType;
		public string code = "";
		public List<SWCodeParamUse> codeParams = new List<SWCodeParamUse> ();
		public SWCodeParamUse GetCodeParamUse(string name)
		{
			foreach (var item in codeParams) {
				if (item.n == name)
					return item;
			}
			SWCodeParamUse newitem = new SWCodeParamUse();
			newitem.n = name;
			codeParams.Add (newitem);
			return newitem;
		}
		#endregion

		public SWCoordMode coordMode; 
		public SWTexResolution reso = SWTexResolution._256x256; 
		public static List<int> resoList
		{
			get{
				return  new List<int> (){128, 256, 512, 1024 };
			}
		}
		public int resolution
		{
			get{ 
				int size = resoList [(int)reso];
				return size;
			}
		}
			
		#region Sprite Light Normal mapping
		/// <summary>
		/// [Sprite Light]	Support normal map or not
		/// </summary>
		public bool nm = false;
		/// <summary>
		/// [Sprite Light]	Normal map importance(0~1).Use for normal map blending, ignore it when you use single normal map.
		/// </summary>
		public float nmi=1;
		/// <summary>
		/// [Sprite Light]	factor, use for normal animation
		/// </summary>
		public string nmf = "(1)";
		/// <summary>
		/// [Sprite Light]	Normal map guid
		/// </summary>
		public string nmid="";
		#endregion
		#endregion

		#region remap
		/// <summary>
		/// Remap Data
		/// </summary>
		public SWDataNodeRemap rd = new SWDataNodeRemap();
		#endregion

		public SWDataNode(SWNodeType _type)
		{
			type = _type;
			AssingNewID ();

			if (type == SWNodeType.root || type == SWNodeType.dummy)
				effectData.useLoop = false;
		}

		public SWDataType GetCodeType()
		{
			if (codeType == CodeParamType.Color)
				return SWDataType._Color;
			if (codeType == CodeParamType.UV)
				return SWDataType._UV;
			return SWDataType._Alpha;
		}
		public void AssingNewID()
		{
			id = SWDataManager.NewGUID ();
		}
		public string ChildOfPort(int p)
		{
			int id = childrenPort.IndexOf (p);
			return children [id];
		}
		public string ParentOfPort(int p)
		{
			int id = parentPort.IndexOf (p);
			return parent [id];
		}
	}
}
