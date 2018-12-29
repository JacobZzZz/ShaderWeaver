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


	public class SWShaderProcessColor:SWShaderProcessImage{
		public  SWShaderProcessColor():base()
		{
			type = SWNodeType.color;
			receiveOutputTypes.Add (SWNodeType.alpha);
			receiveOutputTypes.Add (SWNodeType.refract);
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

		public override SWOutput Process (SWNodeBase _node)
		{
			node = _node;
			Child_Process ();
			CommentHead ();

			StringAddLine (string.Format ("\t\t\t\tfloat4 color{0} = _Color{0};", node.data.iName));
			GoOutput (SWNodeType.alpha);



			SWOutputSub sub = new SWOutputSub ();
			sub.processor = this;
			sub.param = string.Format ("color{0}",node.data.iName);

			sub.depth = node.data.depth;
			sub.op = node.data.effectDataColor.op;
			sub.opFactor =string.Format("{0}*({1})",sub.opFactor,node.data.effectDataColor.param);
			result.outputs.Add (sub);
			return result;
		}
	}


	public class SWShaderProcessReceiveColor:SWShaderProcessReceiveBase{
		public SWShaderProcessReceiveColor():base()
		{
			type = SWNodeType.color;
		}


		/// <summary>
		/// Add:Keep alpha,only add rgb
		/// Mul:Simple multiple color
		/// Lerp:
		/// </summary>
		public override void ProcessOutputSingle (SWShaderProcessBase processor, SWOutputSub item, string keyword = "")
		{
			base.ProcessOutputSingle (processor, item, keyword);
			if (keyword=="first") {
				//processor.StringAddLine (string.Format ("\t\t\t\tresult = {0}*{1};", item.param, item.opFactor));
				processor.StringAddLine (string.Format ("\t\t\t\tresult = float4({0}.rgb,{0}.a*{1});", item.param, item.opFactor));
				return;
			}

			if (item.op == SWOutputOP.blend) {
				processor.StringAddLine (string.Format ("\t\t\t\tresult = lerp(result,float4({0}.rgb,1),clamp({0}.a*{1},0,1));    ",
					item.param, item.opFactor));
			} else if (item.op == SWOutputOP.blendInner) {
				processor.StringAddLine (string.Format ("\t\t\t\tresult = lerp(result,float4({0}.rgb,rootTexColor.a),clamp({0}.a*{1},0,1));    ",
					item.param, item.opFactor));
			} else if (item.op == SWOutputOP.add) {
				processor.StringAddLine (string.Format ("\t\t\t\tresult = result+float4({0}.rgb*{0}.a*{1},{0}.a*{1});", item.param, item.opFactor));
			} else if (item.op == SWOutputOP.addInner) {
				processor.StringAddLine (string.Format ("\t\t\t\tresult = result+float4({0}.rgb*{0}.a*{1},{0}.a*{1}*(rootTexColor.a - result.a));", item.param, item.opFactor));
			} else if (item.op == SWOutputOP.mul) {
				processor.StringAddLine (string.Format ("result = result *lerp(float4(1,1,1,1),{0},{0}.a*{1});", item.param, item.opFactor));
			} else if (item.op == SWOutputOP.mulIntersect) {
				processor.StringAddLine (string.Format ("\t\t\t\tresult = result*{0}*{1};", item.param, item.opFactor));
			} else if (item.op == SWOutputOP.mulRGB) {
				processor.StringAddLine (string.Format ("\t\t\t\tresult = float4(result.rgb*lerp(float3(1,1,1),{0}.rgb,{1}),result.a);", item.param, item.opFactor));
			} else {
				string func = string.Format ("Blend{0}", item.op.ToString());
				processor.StringAddLine (string.Format ("\t\t\t\tresult3 = {2}({0}.rgb,{1}.rgb);","result",item.param,func));
				processor.StringAddLine (string.Format ("\t\t\t\tminA = min(result.a,{0}.a);",item.param));
				processor.StringAddLine (string.Format ("\t\t\t\tresult3 = result.rgb*(result.a-minA)+{0}.rgb*({0}.a-minA)+minA*result3;",item.param));
				processor.StringAddLine (string.Format ("\t\t\t\tresult2 = float4(result3,result.a+{0}.a*(1-result.a));",item.param));
				processor.StringAddLine (string.Format ("\t\t\t\tresult = lerp(result,result2,{0});",item.opFactor));
			}
		}
	}
}