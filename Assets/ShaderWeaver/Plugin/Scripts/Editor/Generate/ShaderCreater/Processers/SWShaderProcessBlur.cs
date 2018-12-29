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


	public class SWShaderProcessBlur:SWShaderProcessBase{
		public  SWShaderProcessBlur():base()
		{
			type = SWNodeType.blur;
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
			sub.opFactor = string.Format ("float2( {0}*{1}*0.1 ,{2}*{3}*0.1)", node.data.blurX, node.data.blurXParam, node.data.blurY, node.data.blurYParam);
			sw.outputs.Add (sub);
			return sw;
		}
	}


	public class SWShaderProcessReceiveBlur:SWShaderProcessReceiveBase{
		public SWShaderProcessReceiveBlur():base()
		{
			type = SWNodeType.blur;
		}

		public void Blur(SWShaderProcessBase processor,SWOutputSub sub,string colorName,string texName,string uv)
		{
			//			processor.StringAddLine (string.Format ("\t\t\t\t{1} = Blur(_{2},{3},{4}*rect{0}.zw);", 
			//				processor.node.data.iName,colorName,texName,uv,sub.opFactor));

			//Blur constrains in rect_Sprite
			if (processor.NeedRectSprite ()) {
				processor.StringAddLine (string.Format ("\t\t\t\t{1} = Blur(_{2},{3},{4}*rect{0}.zw,i.rect_Sprite,{5});", 
					processor.node.data.iName,
					colorName,
					texName,
					uv,
					sub.opFactor,
					processor.IsSpriteType_SamplingSprite()?"true":"false"
				));
			} else {
				processor.StringAddLine (string.Format ("\t\t\t\t{1} = Blur(_{2},{3},{4}*rect{0}.zw,float4(0,0,1,1),false);", 
					processor.node.data.iName,colorName,texName,uv,sub.opFactor));
			}
		}
	}
}