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


	public class SWShaderProcessCoord:SWShaderProcessBase{
		public  SWShaderProcessCoord():base()
		{
			type = SWNodeType.coord;
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
			//CommentHead ();

			SWOutputSub sub = new SWOutputSub ();
			sub.type = SWDataType._UV;
			sub.processor = this;
			if (node.data.coordMode == SWCoordMode.Default)
				sub.param = "i._uv_MainTex";
			else if(node.data.coordMode == SWCoordMode.Sprite)
				sub.param = "i._uv_STD";
			sub.uvOp = node.data.effectDataUV.op;
			sub.opFactor =string.Format("{0}*({1})",sub.opFactor, node.data.effectDataUV.param);

			SWOutput sw = new SWOutput ();
			sw.outputs.Add (sub);
			return sw;
		}
	}



	public class SWShaderProcessReceiveCoord:SWShaderProcessReceiveBase{
		public SWShaderProcessReceiveCoord():base()
		{
			type = SWNodeType.coord;
		}

		public override void ProcessOutputSingle (SWShaderProcessBase processor, SWOutputSub item, string keyword = "")
		{
			base.ProcessOutputSingle (processor, item, keyword);
			processor.StringAddLine (string.Format ("\t\t\t\t{0} = {1};", processor.uvParam, item.param));
		}
	}
}