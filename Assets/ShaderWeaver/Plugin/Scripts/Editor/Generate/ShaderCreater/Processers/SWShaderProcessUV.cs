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


	public class SWShaderProcessUV:SWShaderProcessBase{
		public  SWShaderProcessUV():base()
		{
			type = SWNodeType.uv;
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
			StringAddLine( string.Format ("\t\t\t\t{0} = -(color{1}.r*{2} + color{1}.g*{3} + color{1}.b*{4} +  color{1}.a*{5});    ", 
				uvParam, node.data.iName,
				ToShader (node.data.effectDataUV.amountR),ToShader (node.data.effectDataUV.amountG),ToShader (node.data.effectDataUV.amountB),ToShader (node.data.effectDataUV.amountA)));
			sub.type = SWDataType._UV;
			sub.param = string.Format ("uv{0}", node.data.iName);
			sub.opFactor =string.Format("{0}*({1})",sub.opFactor, node.data.effectDataUV.param);
			sub.uvOp = node.data.effectDataUV.op;
		}

	}


	public class SWShaderProcessReceiveUV:SWShaderProcessReceiveBase{
		public SWShaderProcessReceiveUV():base()
		{
			type = SWNodeType.uv;
		}

		public override void ProcessOutputSingle (SWShaderProcessBase processor, SWOutputSub item, string keyword = "")
		{
			base.ProcessOutputSingle (processor, item, keyword);
			if (keyword == "add") {
				//UV加上去，一个影像
				if (item.type == SWDataType._UV && item.uvOp == SWUVop.add) {
					processor.StringAddLine (string.Format ("\t\t\t\t{0} = {0} + {1}*{2};", processor.uvParam, item.param, item.opFactor));
				}
			} else if (keyword == "lerp") {
				//原影像和 UV后影像叠加
				if (item.type == SWDataType._UV && item.uvOp == SWUVop.lerp) {
					//iNormal
					processor.TextureSample (string.Format ("color{0}_{1}", processor.node.data.iName, item.param),
						true, processor.node.TextureShaderName (), string.Format("{0}+{1}",processor.uvParam,item.param,processor.node.data.useNormal));
					processor.StringAddLine( string.Format("\t\t\t\tcolor{0} = lerp(color{0},color{0}_{1},clamp({2},0,1));    ",processor.node.data.iName,item.param,item.opFactor));
				}
			} else if (keyword == "replace") {
				//UV加上去，一个影像
				if (item.type == SWDataType._UV && item.uvOp == SWUVop.replace) {
					processor.StringAddLine (string.Format ("\t\t\t\t{0} = lerp({0},{1},{2});", processor.uvParam, item.param, item.opFactor));
				}
			}
		}
	}
}