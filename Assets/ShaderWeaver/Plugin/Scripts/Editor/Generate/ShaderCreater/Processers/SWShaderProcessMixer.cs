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


	public class SWShaderProcessMixer:SWShaderProcessBase{
		public  SWShaderProcessMixer():base()
		{
			type = SWNodeType.mixer;
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

		public override bool ProcessCondition ()
		{
			return node.HasChild ();
		}

		public override SWOutput Process(SWNodeBase _node)
		{
			node = _node;
			Child_Process ();
			CommentHead ();

			SWOutput result = new SWOutput ();
			//step 0:Get Direct Child
			string directChildID="";
			for (int i = 0; i < node.data.children.Count; i++) {
				int port = node.data.childrenPort [i];
				if (port == 0) {
					directChildID = node.data.children [i];
					break;
				}
			}


			//step 1: from child node, get alpha
			int MaxCount = SWNodeMixer.Gradient_MaxFrameCount ();
			string alphaParam = string.Format ("mixer{0}", node.data.iName);
			StringAddLine (string.Format ("\t\t\t\tfloat {0};",alphaParam));
				
			foreach (var op in childOutputs) {
				foreach (var item in op.outputs) {
					if (directChildID == item.data.id) {
						if(item.data.type == SWNodeType.alpha)
							StringAddLine (string.Format ("\t\t\t\t{0} = clamp({1}*{2},{3},{4});",
								alphaParam,item.param,item.opFactor,item.data.effectData.pop_min,item.data.effectData.pop_max));
						else
							StringAddLine (string.Format ("\t\t\t\t{0} = ({1}).a*{2};",alphaParam,item.param,item.opFactor));
						break;
					}
				}
			}
			StringAddLine (string.Format ("\t\t\t\t{0} = clamp({0},0,1);",alphaParam));


			//step 2:keyframe calculation
			for (int i = 0; i < node.data.gradients.Count; i++) {
				string graParam = string.Format ("gra{0}_{1}",node.data.iName,i);
				var frames = node.data.gradients [i].frames;
				if (frames.Count == 0) {
					StringAddLine (string.Format ("\t\t\t\tfloat {0} = 0;",graParam));
				} else {
					string strList = (string.Format ("\t\t\t\tfloat {0}ListTime[{1}] = {{", graParam, MaxCount));
					for (int j = 0; j < MaxCount; j++) {
						if (j < frames.Count)
							strList += ("" + node.data.gradients [i].frames [j].time);
						else
							strList += ("-1");
						if (j != MaxCount - 1)
							strList += (",");
					}
					strList += ("};");
					StringAddLine (strList);


					strList = (string.Format ("\t\t\t\tfloat {0}ListValue[{1}] = {{", graParam, MaxCount));
					for (int j = 0; j < MaxCount; j++) {
						if (j < frames.Count)
							strList += ("" + node.data.gradients [i].frames [j].value);
						else
							strList += ("-1");
						if (j != MaxCount - 1)
							strList += (",");
					}
					strList += ("};");
					StringAddLine (strList);

					StringAddLine (string.Format ("\t\t\t\tfloat {0} = GradientEvaluate({0}ListTime,{0}ListValue,{1},{2});", graParam, frames.Count, alphaParam));
				}
			}

			PortBelong ();
			//step 3:pass to parent
			foreach (var op in childOutputs) {
				foreach (var item in op.outputs) {
					int port = portBelongs [item.node.data.id];
					if (port != 0) {
						string graParam = string.Format ("gra{0}_{1}", node.data.iName, (port-1));
						item.opFactor = string.Format ("{0}*{1}", item.opFactor, graParam);
						result.outputs.Add (item);
					}
				}
			}
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
