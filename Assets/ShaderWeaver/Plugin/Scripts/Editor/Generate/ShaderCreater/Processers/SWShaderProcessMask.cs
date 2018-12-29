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


	public class SWShaderProcessMask:SWShaderProcessBase{
		public  SWShaderProcessMask():base()
		{
			type = SWNodeType.mask;
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

		public override SWOutput Process(SWNodeBase _node)
		{
			node = _node;
			Child_Process ();

			SWOutput result = new SWOutput ();
			foreach (var item in childOutputs) {
				result.outputs.AddRange (item.outputs);
			}

			foreach (var item in result.outputs) {
				item.opFactor =string.Format("{0}*color_{1}.{2}",item.opFactor,  node.TextureShaderName(),node.data.maskChannel.ToString());
			}
			return result;
		}
	}
}