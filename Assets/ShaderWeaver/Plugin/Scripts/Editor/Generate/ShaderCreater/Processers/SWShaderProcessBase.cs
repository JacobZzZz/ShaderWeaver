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
	using System;

	public class SWOutputSub
	{
		public SWShaderProcessBase _processor;
		public SWShaderProcessBase processor
		{
			get{ 
				return _processor;
			}
			set{ 
				_processor = value;
				data = _processor.node.data;
				node = _processor.node;
			}
		}
		public SWNodeBase node;
		public SWDataNode data;
		public int depth;
		public SWDataType type;

		/// <summary>
		/// eg. _color_0 sampled from texture
		/// </summary>
		public string param = "1";

		/// <summary>
		/// Add Lerp Mul, assgin by user
		/// </summary>
		public SWOutputOP op;
		public SWUVop uvOp;
		/// <summary>
		/// assgin by user, and multiplied by mask
		/// </summary>
		public string opFactor = "1";
		/// <summary>
		/// Only use for remap
		/// </summary>
		public string UVparam = "1";
	}
	public class SWOutput
	{
		public List<SWOutputSub> outputs = new List<SWOutputSub>();
	}


	public class SWShaderProcessBase{
		public SWShaderCreaterBase creator
		{
			get{ return SWShaderCreaterBase.Instance;}
		}
		public SWWindowMain window
		{
			get{ return SWWindowMain.Instance;}
		}
		public SWNodeType type;
		public SWNodeBase node;
		/// <summary>
		/// All child outputs: Color UV Alpha.....
		/// </summary>
		public List<SWOutput> childOutputs = new List<SWOutput> ();
		public List<SWNodeType> receiveOutputTypes = new List<SWNodeType> ();
		public string uvParam;
		/// <summary>
		/// Only pass color type to parent
		/// </summary>
		public SWOutput result = new SWOutput ();
		public bool ignoreChildOutput = false;

		public SWShaderProcessBase()
		{
			
		}

		protected virtual void UVParamInit()
		{
			uvParam = string.Format ("uv{0}", node.data.iName);
			StringAddLine( string.Format("\t\t\t\tfloat2  {0} = i._uv_STD;",uvParam));
		}

		public void TextureSample(string colorName,bool declard,string texName,string uv,bool isNormal = false)
		{
			if (Condition(SWNodeType.blur)) {
				if (declard)
					StringAddLine (string.Format ("\t\t\t\tfloat4 {0} = float4(1,1,1,1);", colorName));

				SWShaderProcessReceiveBlur receiveBlur = new SWShaderProcessReceiveBlur ();
				foreach (var outp in childOutputs) {
					foreach (var item in outp.outputs) {
						if (item.data.type == SWNodeType.blur) {
							receiveBlur.Blur (this, item, colorName, texName, uv);
						}
					}
				}
			} else {
				StringAddLine (string.Format ("\t\t\t\t{0}{1} = tex2D(_{2},{3});    ",
					declard?"float4 ":"",colorName, texName , uv));

//				//No need here, only need for blur
//				if (IsSpriteType_SamplingSprite ()) {
//					StringAddLine (string.Format ("\t\t\t\t{0}.rgb*={0}.a;",colorName));
//				}

				//iNormal
				if(isNormal)
					StringAddLine (string.Format ("\t\t\t\t{0} = float4(UnpackNormal({0}),1);",colorName));
			}
		}

		protected void CommentHead()
		{
			StringAddLine( "");
			StringAddLine( "");
			StringAddLine( "\t\t\t\t//====================================");
			StringAddLine( string.Format("\t\t\t\t//============ {0} ============   ",node.data.name));
		}

		public virtual bool ProcessCondition()
		{
			return true;
		}

		public virtual SWOutput Process(SWNodeBase _node)
		{
			node = _node;
			Child_Process ();
			if (!ProcessCondition ())
				return result;
			CommentHead ();

			UVParamInit ();
			GoOutput (SWNodeType.coord);
			GoOutput (SWNodeType.remap);
		
			TRS ();
			GoOutput (SWNodeType.uv, "replace");
			GoOutput (SWNodeType.uv, "add");

			FinalUVStage ();
			//iNormal
			TextureSample (string.Format ("color{0}", node.data.iName), true, node.TextureShaderName (), uvParam,node.data.useNormal);
			GoOutput (SWNodeType.uv, "lerp");
			AfterAllTextureSample ();


			GoOutput (SWNodeType.alpha);
			SWOutputSub sub = new SWOutputSub ();
			ProcessSub (sub);
			return result;
		}


		/// <summary>
		/// Process UV before tex2D
		/// </summary>
		public virtual void FinalUVStage()
		{
			LoopProcess_BeforeSample();
			FinalUV_2Rect ();
			GoOutput (SWNodeType.retro);
		}

		/// <summary>
		/// STD UV to uv in SpriteRect/AnimationSheet Rect
		/// </summary>
		public virtual void FinalUV_2Rect()
		{
			if (node.data.effectData.animationSheetUse) {
				var ed = node.data.effectData;
				StringAddLine (string.Format ("\t\t\t\tfloat4 rect{0} =  AnimationSheet_Rect({1},{2},{3},{4},{5},{6},{7});",
					node.data.iName,
					ed.animationSheetCountX,
					ed.animationSheetCountY,
					ed.animationSheetLoop.ToString ().ToLower (),
					node.data.effectData.animationSheetSingleRow.ToString ().ToLower (),
					node.data.effectData.animationSheetSingleRowIndex,
					node.data.effectData.animationSheetStartFrame,
					node.data.effectData.animationSheetFrameFactor));
				StringAddLine (string.Format ("\t\t\t\t{0} = UV_STD2Rect({0},rect{1});", uvParam, node.data.iName));
			} else if (NeedRectSprite()) {
				StringAddLine (string.Format ("\t\t\t\tfloat4 rect{0} =  i.rect_Sprite;",node.data.iName));
				StringAddLine (string.Format ("\t\t\t\t{0} = UV_STD2Rect({0},rect{1});", uvParam, node.data.iName));
			} else {
				StringAddLine (string.Format ("\t\t\t\tfloat4 rect{0} =  float4(1,1,1,1);",node.data.iName));
			}
		}
		public virtual void AfterAllTextureSample()
		{
			LoopProcess_AfterSample ();
		}

		public virtual void ProcessSub(SWOutputSub sub)
		{			
			sub.processor = this;
			result.outputs.Add (sub);
			sub.depth = node.data.depth;
			sub.UVparam = string.Format ("uv{0}", node.data.iName);
		}

		protected void GoOutput(SWNodeType type,string keyword="")
		{
			if (ignoreChildOutput)
				return;
			if (!Condition (type))
				return;
			if(receiveOutputTypes.Contains(type))
				SWShaderCreaterBase.CreateProcessorReceiver(type).ProcessOutput (this,keyword);
		}

		/// <summary>
		/// Node has child of type
		/// </summary>
		public virtual bool Condition(SWNodeType type)
		{
			foreach (var item in childOutputs) {
				foreach (var sub in item.outputs) {
					if (sub.data != null && sub.data.type == type)
						return true;
				}
			}
			return false;
		}


		#region common
		protected void Child_Process()
		{
			var tempNodeAll = window.NodeAll ();
			foreach (var item in node.data.children) {
				var child = tempNodeAll[item];
				if (!child.BelongRootTree ())
					continue;
				SWOutput output = SWShaderCreaterBase.Instance.Process (child);
				childOutputs.Add (output);
			}

			//pass child who output color
			foreach (var item in childOutputs) {
				foreach (var item2 in item.outputs) {
					//Sprite Light Normal mapping
					if(item2.type == SWDataType._Color || item2.type == SWDataType._Normal)
						result.outputs.Add(item2);
				}
			}
		}



		protected void TRS()
		{
			StringAddLine( string.Format("\t\t\t\tfloat2 center{0} = float2(0.5,0.5);    ",node.data.iName));
			StringAddLine( string.Format("\t\t\t\t{0} = {0}-center{1};    ",uvParam,node.data.iName));


			//step 1: t
			StringAddLine( string.Format("\t\t\t\t{0} = {0}+{1};    ",uvParam,ToShader(-node.data.effectData.t_startMove)));
			StringAddLine( string.Format("\t\t\t\t{0} = {0}+{1}*({2});    ",
				uvParam,ToShader(-node.data.effectData.t_speed),node.data.effectData.t_Param));




			//step 2:r
			StringAddLine( string.Format("\t\t\t\t{0} = UV_RotateAround({1},{0},{2});    ",uvParam,ToShader(new Vector2(0,0)),node.data.effectData.r_angle));



			//step 3:s
			StringAddLine( string.Format("\t\t\t\t{0} = {0}/{1};    ",uvParam,ToShader(node.data.effectData.s_scale)));
			StringAddLine( string.Format("\t\t\t\tfloat2 dir{0} = {1}/length({1});    ",node.data.iName,uvParam));
			StringAddLine( string.Format("\t\t\t\t{1} = {1}-dir{0}*{2}*({3});    ",
				node.data.iName,uvParam,ToShader(node.data.effectData.s_speed),node.data.effectData.s_Param));

			//step extra inner rotate
			StringAddLine( string.Format("\t\t\t\t{0} = UV_RotateAround({1},{0},{2}*({3}));    ",
				uvParam,ToShader(new Vector2(0,0)),node.data.effectData.r_speed,node.data.effectData.r_Param));

			StringAddLine( string.Format("\t\t\t\t{0} = {0}+center{1};    ",uvParam,node.data.iName));
		}


		protected void LoopProcess_BeforeSample()
		{
			if (!node.data.effectData.useLoop)
				return;
			StringAddLine (string.Format ("\t\t\t\tfloat2 {0}orgin = {0};", uvParam));
			//use fmod, dont use %
			StringAddLine (string.Format ("\t\t\t\t{0} = float2({0}.x >0 ?fmod({0}.x,1+{1}) : (1+{1}) - fmod(abs({0}.x),1+{1}), {0}.y >0 ?fmod({0}.y,1+{2}): (1+{2}) - fmod(abs({0}.y),1+{2}));", 
				uvParam, node.data.effectData.gapX, node.data.effectData.gapY));


			if (!node.data.effectData.useLoop)
				return;
			StringAddLine (string.Format ("\t\t\t\tbool discard{0} = false;", node.data.iName));

			//For loop's gap area. It can not be >=1,or  it will be conflict with remap
			StringAddLine (string.Format ("\t\t\t\tif({0}.x>1 || {0}.y>1)", uvParam));
			StringAddLine( string.Format ("\t\t\t\t\tdiscard{0} = true;",  node.data.iName));

			if (node.data.effectData.loopX == SWLoopMode.single) {
				StringAddLine( string.Format ("\t\t\t\tif({0}orgin.x<0)", uvParam));
				StringAddLine( string.Format ("\t\t\t\t\tdiscard{0} = true;",  node.data.iName));
				StringAddLine( string.Format ("\t\t\t\tif({0}orgin.x>1)", uvParam));
				StringAddLine( string.Format ("\t\t\t\t\tdiscard{0} = true;",  node.data.iName));
			}
			if (node.data.effectData.loopY == SWLoopMode.single) {
				StringAddLine( string.Format ("\t\t\t\tif({0}orgin.y<0)", uvParam));
				StringAddLine( string.Format ("\t\t\t\t\tdiscard{0} = true;",  node.data.iName));
				StringAddLine( string.Format ("\t\t\t\tif({0}orgin.y>1)", uvParam));
				StringAddLine( string.Format ("\t\t\t\t\tdiscard{0} = true;",  node.data.iName));
			}

		}
		protected void LoopProcess_AfterSample()
		{
			if (!node.data.effectData.useLoop)
				return;
			StringAddLine( string.Format ("\t\t\t\tif(discard{0} == true) color{0} = float4(0,0,0,0);",  node.data.iName));
		}

		protected string ToShader(Color c)
		{
			return string.Format ("fixed4({0},{1},{2},{3})", c.r,c.g,c.b,c.a);
		}
		protected string ToShader(Vector2 v)
		{
			return string.Format ("fixed2({0},{1})", v.x, v.y);
		}

		public void StringAddLine(string line)
		{
			SWShaderCreaterBase.Instance.StringAddLine (line);
		}
		#endregion

		public bool NeedRectSprite()
		{
			return (node.data.type == SWNodeType.root || node.data.type == SWNodeType.dummy)
				&& SWShaderCreaterBase.Instance.IsSprite;
		}
			 
		/// <summary>
		/// *Only use for Blur when Sprite lighting is on*
		/// Blur used for Sprite tex needs c.rgb *= c.a after tex2D,otherwise sprite lighting will 'leak color'
		/// See Blured Robot with lights
		/// </summary>
		public bool IsSpriteType_SamplingSprite()
		{
			return NeedRectSprite() && (SWWindowMain.Instance.data.shaderType == SWShaderType.sprite) &&  
				(SWWindowMain.Instance.data.spriteLightType != SWSpriteLightType.no);
		}
	}


	public class SWShaderProcessReceiveBase{
		public SWNodeType type;
		public virtual void ProcessOutput(SWShaderProcessBase processor,string keyword="")
		{
			foreach (var outp in processor.childOutputs) {
				foreach (var item in outp.outputs) {
					if (item.data.type == type) {
						ProcessOutputSingle (processor, item, keyword);
					}
				}
			}
		}
		public virtual void ProcessOutputSingle(SWShaderProcessBase processor,SWOutputSub item,string keyword="")
		{

		}
	}
}