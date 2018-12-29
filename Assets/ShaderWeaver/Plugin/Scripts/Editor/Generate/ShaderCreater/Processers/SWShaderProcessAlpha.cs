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


	public class SWShaderProcessAlpha:SWShaderProcessBase{
		public  SWShaderProcessAlpha():base()
		{
			type = SWNodeType.alpha;
			receiveOutputTypes.Add (SWNodeType.alpha);
			receiveOutputTypes.Add (SWNodeType.blur);
			receiveOutputTypes.Add (SWNodeType.coord);
			receiveOutputTypes.Add (SWNodeType.color);
			receiveOutputTypes.Add (SWNodeType.mask);
			receiveOutputTypes.Add (SWNodeType.dummy);
			receiveOutputTypes.Add (SWNodeType.refract);
			receiveOutputTypes.Add (SWNodeType.reflect);
			receiveOutputTypes.Add (SWNodeType.remap);
			receiveOutputTypes.Add (SWNodeType.retro);
			receiveOutputTypes.Add (SWNodeType.root);
			receiveOutputTypes.Add (SWNodeType.uv);
		}

		public override void ProcessSub (SWOutputSub sub)
		{
			base.ProcessSub (sub);
			string alphaParam = string.Format ("aplha{0}", node.data.iName);
			StringAddLine (string.Format ("\t\t\t\tfloat {0} = {1} +{2}*({3}) + color{4}.{5};",
				alphaParam,
				node.data.effectData.pop_startValue,
				node.data.effectData.pop_speed,
				node.data.effectData.pop_Param,
				node.data.iName,
				node.data.effectData.pop_channel.ToString()));

			sub.type = SWDataType._Alpha;
			sub.param = alphaParam;
			sub.op = node.data.effectDataColor.op;
			sub.opFactor =string.Format("{0}*({1})",sub.opFactor,node.data.effectDataColor.param);
			foreach(var outp in childOutputs)
				foreach (var item in outp.outputs) {
					if (item.type == SWDataType._Remap) {
						StringAddLine( string.Format ("\t\t\t\tcolor{0} = float4(color{0}.rgb,color{0}.a*{1});", node.data.iName,item.opFactor));
					}
				}
		}
	}


	public class SWShaderProcessReceiveAlpha:SWShaderProcessReceiveBase{
		public SWShaderProcessReceiveAlpha():base()
		{
			type = SWNodeType.alpha;
		}

		public override void ProcessOutputSingle (SWShaderProcessBase processor, SWOutputSub item, string keyword = "")
		{
			base.ProcessOutputSingle (processor, item, keyword);
			if (keyword == "final")
				ProcessFinal (processor, item);
			else
				Process (processor, item);
		}
	 
		void Process(SWShaderProcessBase processor,SWOutputSub item)
		{
			if (item.data.effectData.pop_final)
				return;

			string colorParam = string.Format ("color{0}", processor.node.data.iName);
			processor.StringAddLine (string.Format ("\t\t\t\t{0} = float4({0}.rgb,{0}.a* lerp(1,clamp({1}*{2},{3},{4}),{5}));    ",
				colorParam, item.param, item.opFactor, item.data.effectData.pop_min,item.data.effectData.pop_max,item.opFactor));
		}

		void ProcessFinal(SWShaderProcessBase processor,SWOutputSub item)
		{
			if (!item.data.effectData.pop_final)
				return;

			processor.StringAddLine (string.Format ("\t\t\t\t{0} = float4({0}.rgb,{0}.a* lerp(1,clamp({1}*{2},{3},{4}),{5}));    ",
				"result", item.param, item.opFactor, item.data.effectData.pop_min,item.data.effectData.pop_max,item.opFactor));
		}
	}
}