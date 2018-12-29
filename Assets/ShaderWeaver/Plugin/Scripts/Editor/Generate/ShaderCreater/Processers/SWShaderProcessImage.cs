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


	public class SWShaderProcessImage:SWShaderProcessBase{
		public  SWShaderProcessImage():base()
		{
			type = SWNodeType.image;
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

			receiveOutputTypes.Add (SWNodeType.code);
		}

		public override void ProcessSub (SWOutputSub sub)
		{
			base.ProcessSub (sub);
			StringAddLine( string.Format("\t\t\t\tcolor{0} = color{0}*_Color{0};",node.data.iName));
			sub.type = SWDataType._Color;
			sub.param = string.Format ("color{0}", node.data.iName);
			sub.op = node.data.effectDataColor.op;
			sub.opFactor =string.Format("{0}*({1})",sub.opFactor,node.data.effectDataColor.param);
			foreach(var outp in childOutputs)
				foreach (var item in outp.outputs) {
					if (item.type == SWDataType._Remap) {
						StringAddLine( string.Format ("\t\t\t\tcolor{0} = float4(color{0}.rgb,color{0}.a*{1});", node.data.iName,item.opFactor));
					}
				}

			ProcessNormalMap ();
		} 

		#region Sprite Light Normal mapping
		void ProcessNormalMap()
		{
			if (node.UseNormalMap()) {
				StringAddLine( string.Format ("\t\t\t\tfloat3 normal{0} = UnpackNormal(tex2D(_{1},{2}));", node.data.iName,node.TextureShaderName_NormalMap(),uvParam));
				SWOutputSub sub = new SWOutputSub ();
				sub.processor = this;
				sub.type = SWDataType._Normal;
				sub.param =string.Format ("normal{0}", node.data.iName);
				sub.opFactor = string.Format("{0}*{1}*{2}*color{3}.a",sub.opFactor,node.data.nmi,node.data.nmf,node.data.iName);
				result.outputs.Add (sub);
			}	
		}
		#endregion
	}
}