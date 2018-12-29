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


	public class SWShaderProcessRemap:SWShaderProcessBase{
		public  SWShaderProcessRemap():base()
		{
			type = SWNodeType.remap;
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

		protected override void UVParamInit ()
		{
			uvParam = string.Format ("uv{0}", node.data.iName);

			foreach (var item in childOutputs) {
				foreach (var item2 in item.outputs) {
					if (item2.data.type == SWNodeType.image && item2.data.parent.Contains(node.data.id)) {
						StringAddLine( string.Format("\t\t\t\tfloat2  {0} = {1};    ",uvParam,item2.UVparam)); 
						return;
					}
				}
			}
			StringAddLine( string.Format("\t\t\t\tfloat2  {0} = i._uv_STD;",uvParam)); 
		}  

		public override SWOutput Process(SWNodeBase _node)
		{
			node = _node;
			Child_Process ();
			CommentHead ();

			UVParamInit ();
			//StringAddLine( string.Format("\t\t\t\tfloat2  {0} = {1};    ",uvParam,childOutputs[0].outputs[0].UVparam));

			StringAddLine( string.Format("\t\t\t\tfloat {0}A = tex2D(_{1},{0}).b*tex2D(_{1},{0}).a;",
				uvParam, node.TextureShaderName()));

			StringAddLine( string.Format("\t\t\t\tfloat4 color{0} = tex2D(_{2},{1});    ",
				node.data.iName,uvParam, node.TextureShaderName()));
			StringAddLine( string.Format("\t\t\t\tif(color{0}.b >= 0.5)",node.data.iName));
			StringAddLine( string.Format("\t\t\t\t\tcolor{0} = float4(0,color{0}.gba);",node.data.iName));


			SWOutputSub sub = new SWOutputSub ();
			SWOutput result = new SWOutput ();
			result.outputs.Add (sub);
			sub.processor = this;
			sub.type = SWDataType._Remap;
			sub.param = string.Format("color{0}.rg",node.data.iName);
			sub.op = SWOutputOP.add;
			sub.opFactor =string.Format("color{0}.a",node.data.iName);
			return result;
		}
	}

	public class SWShaderProcessReceiveRemap:SWShaderProcessReceiveBase{
		public SWShaderProcessReceiveRemap():base()
		{
			type = SWNodeType.remap;
		}
		public override void ProcessOutputSingle (SWShaderProcessBase processor, SWOutputSub item, string keyword = "")
		{
			base.ProcessOutputSingle (processor, item, keyword);
			string uvParam = string.Format ("uv{0}", processor.node.data.iName);
			processor.StringAddLine( string.Format ("\t\t\t\t{0} = {1};", uvParam, item.param));
		}
	}
}