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


	public class SWShaderProcessRetro:SWShaderProcessBase{
		public  SWShaderProcessRetro():base()
		{
			type = SWNodeType.retro;
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

		public override SWOutput Process (SWNodeBase _node)
		{
			node = _node;
			SWOutput sw = new SWOutput ();
			SWOutputSub sub = new SWOutputSub ();
			sub.type = SWDataType._UV;
			sub.processor = this;
			sub.opFactor = string.Format ("({0}*({1})*0.2)", node.data.retro,node.data.retroParam);
			sw.outputs.Add (sub);
			return sw;
		}
	}

	public class SWShaderProcessReceiveRetro:SWShaderProcessReceiveBase{
		public SWShaderProcessReceiveRetro():base()
		{
			type = SWNodeType.retro;
		}

		public override void ProcessOutputSingle (SWShaderProcessBase processor, SWOutputSub item, string keyword = "")
		{
			base.ProcessOutputSingle (processor, item, keyword);
			string uvParam = string.Format ("uv{0}", processor.node.data.iName);

			string param = string.Format ("retroFactor{0}{1}", processor.node.data.iName, item.node.data.iName);
			processor.StringAddLine(string.Format("\t\t\t\tfloat {0} = {1}*max(rect{2}.z,rect{2}.w);",param,item.opFactor,processor.node.data.iName));
			processor.StringAddLine (string.Format ("\t\t\t\t{0} = Retro({0},{1});", uvParam,param));
		}
	}
}