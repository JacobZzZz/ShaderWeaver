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
	using System.Text.RegularExpressions;

	public class SWShaderProcessCode:SWShaderProcessBase{
		public  SWShaderProcessCode():base()
		{
			type = SWNodeType.code;
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
			//1: init
			node = _node;
			Child_Process ();
			CommentHead ();

			//2: port belong
			PortBelong ();
			SWOutputSub[] subs = new SWOutputSub[node.data.childPortNumber];
			foreach (var op in childOutputs) {
				foreach (var item in op.outputs) {
					int port = portBelongs [item.node.data.id];
					subs [port] = item;
				}
			}

			//3: param
			var dataCode = SWWindowMain.Instance.dataCode.CodeOfName (node.data.code);

			string param = string.Format ("v{0}", node.data.iName);

			if(dataCode.output.GetType() == SWDataType._Color)
				StringAddLine (string.Format ("\t\t\t\tfloat4 {0} = float4(0,0,0,0);",  param));
			else if(dataCode.output.GetType() == SWDataType._UV)
				StringAddLine (string.Format ("\t\t\t\tfloat2 {0} = float2(0,0);", param));
			else if(dataCode.output.GetType() == SWDataType._Alpha)
				StringAddLine (string.Format ("\t\t\t\tfloat {0} = 0;",param));

			List<string> list = new List<string> ();
			int portIndex = 0;
			for (int i = 0; i < dataCode.inputs.Count; i++) {
				var item = dataCode.inputs [i];
				string str = "";
				if (item.IsParam ()) {
					var paramUse = node.data.GetCodeParamUse (item.name);
					if (item.type == CodeParamType.CustomParam)
						str = paramUse.v;
					else
						str = string.Format("{0}_{1}",node.data.iName,paramUse.n);
				} else {
					str = subs [portIndex].param;
					portIndex++;
				}
				list.Add (str);
			}

			if (dataCode.IsFunction ()) {
				string func = dataCode.name + "(";
				for (int i = 0; i < dataCode.inputs.Count; i++) {
					if (i != 0)
						func += ",";
					func += list[i];
				}
				func += ")";

				StringAddLine (string.Format ("\t\t\t\t{0} = {1};", param, func));
			} else {
				string content = "\t\t\t\t"+dataCode.code;
				content = content.Replace ("\n", "\n\t\t\t\t");
				for (int i = 0; i < dataCode.inputs.Count; i++) {
					content = SWRegex.ReplaceWord (content, dataCode.inputs[i].name, list[i]);
				}
				content = SWRegex.ReplaceWord (content, dataCode.output.name, param);
				StringAddLine (content);
			}



			SWOutputSub sub = new SWOutputSub ();
			sub.processor = this;

			sub.type = node.data.GetCodeType();
			//sub.type = window.data.CodeOfName(node.data.code).output.type;
			sub.param = param;

			if (sub.type == SWDataType._Color) {
				sub.data = new SWDataNode (SWNodeType.color);
				sub.depth = node.data.depth;
				sub.op = node.data.effectDataColor.op;
				sub.opFactor =string.Format("{0}*({1})",sub.opFactor,node.data.effectDataColor.param);
			}
			else if (sub.type == SWDataType._UV) {
				sub.data = new SWDataNode (SWNodeType.uv);
				sub.opFactor =string.Format("{0}*({1})",sub.opFactor, node.data.effectDataUV.param);
				sub.uvOp = node.data.effectDataUV.op;
			}
			else if (sub.type == SWDataType._Alpha) {
				sub.data = new SWDataNode (SWNodeType.alpha);
				sub.op = node.data.effectDataColor.op;
				sub.opFactor =string.Format("{0}*({1})",sub.opFactor,node.data.effectDataColor.param);
			}

			SWOutput result = new SWOutput ();
			result.outputs.Add (sub);
			return result;
		}

		Dictionary<string,int> portBelongs = new Dictionary<string, int> ();
		void PortBelong()
		{
			for (int i = 0; i < node.data.children.Count; i++) {
				PortTraverse (node.data.children [i], node.data.childrenPort [i]);
			}
		}

		void PortTraverse(string node,int id)
		{
			if (!portBelongs.ContainsKey (node)) {
				portBelongs.Add (node, id);
			}
			foreach (var item in SWWindowMain.Instance.NodeAll()[node].data.children) {
				PortTraverse (item, id);
			}
		}
	}
}