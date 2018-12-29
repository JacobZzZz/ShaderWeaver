//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;

	/// <summary>
	/// Create Shader
	/// </summary>
	public partial class SWShaderCreaterBase{
		public static SWShaderCreaterBase Instance; 
		protected SWWindowMain window;
		protected string txt;

		//True: UI UIText
		public bool IsTextureSampleAdd;
		//True: Sprite UI
		public bool IsSprite;
		public string postFix_fileName="";
		public string postFix_shaderName="";

		public SWShaderCreaterBase(SWWindowMain _window)
		{
			Instance = this;
			window = _window;
		}


		#region Screen nodes:refraction refraction   grab pass and calculate screen pos
		protected void Screen_PropertyField()
		{
			if (HasChildNodeType (SWNodeType.reflect)) {
				StringAddLine ("\t\t_ReflectionLine (\"Reflection Line\", Range(0,1)) = 1");
				StringAddLine ("\t\t_ReflectionHeight (\"Reflection Height\", Range(0.1,3)) = 1");
			}
		}

		protected void Screen_Grab_Pass()
		{
			if (HasChildNodeType (SWNodeType.refract) || HasChildNodeType (SWNodeType.reflect)) {
				StringAddLine ("\t\tGrabPass{ }");
			}
		}
		protected void Screen_Struct_v2f()
		{
			if (HasChildNodeType (SWNodeType.refract) || HasChildNodeType (SWNodeType.reflect)) {
				StringAddV2d ("float2", "_uv_Screen", "TEXCOORD3");
			}
		}
		protected void Screen_PropertyDeclare()
		{
			if (HasChildNodeType (SWNodeType.refract) || HasChildNodeType (SWNodeType.reflect)) {
				StringAddLine ("\t\t\tuniform sampler2D _GrabTexture;");
			}
			if (HasChildNodeType (SWNodeType.reflect)) {
				StringAddLine ("\t\t\tfloat _ReflectionLine;");
				StringAddLine ("\t\t\tfloat _ReflectionHeight;");
			}
		}
		protected void Screen_Functions()
		{
			if (HasChildNodeType (SWNodeType.reflect)) {
				StringAddLine ("\t\t\tfloat2 FlipUV_Y(float2 uv,float midY)\n\t\t\t{\n\t\t\t\tfloat y = uv.y - midY;\n\t\t\t\treturn float2(uv.x,midY - y* 1/_ReflectionHeight);\n\t\t\t}");
			}
		}
		protected void Screen_Vert()
		{
			if (HasChildNodeType (SWNodeType.refract) || HasChildNodeType (SWNodeType.reflect)) {
				if(this is SWShaderCreaterSpriteLight)
					StringAddLine ("\t\t\t\tfloat4 wpos = mul(UNITY_MATRIX_MVP,IN.vertex);");
				else
					StringAddLine ("\t\t\t\tfloat4 wpos = OUT.pos;");
				StringAddLine ("\t\t\t\t#if UNITY_UV_STARTS_AT_TOP");
				StringAddLine ("\t\t\t\t\tfloat grabSign = -_ProjectionParams.x;");
				StringAddLine ("\t\t\t\t#else");
				StringAddLine ("\t\t\t\t\tfloat grabSign = _ProjectionParams.x;");
				StringAddLine ("\t\t\t\t#endif");
				StringAddLine ("\t\t\t\tOUT._uv_Screen = wpos.xy / wpos.w;");
				StringAddLine ("\t\t\t\tOUT._uv_Screen.y *= _ProjectionParams.x;");
				StringAddLine ("\t\t\t\tOUT._uv_Screen = float2(1,grabSign)*OUT._uv_Screen*0.5+0.5;");
			}
		}
		protected void Screen_Fragment()
		{
			if (HasChildNodeType (SWNodeType.refract) || HasChildNodeType (SWNodeType.reflect)) {
				
			}
		}
		#endregion


		private void CreateTitle()
		{
//			if(string.IsNullOrEmpty(window.data.sn))
//				StringAddLine( string.Format("Shader \"{0}/{1}\"{{   ",SWGlobalSettings.ProductTitle,window.data.title));
//			else
//				StringAddLine( string.Format("Shader \"{0}\"{{   ",window.data.sn));


			string title = "";
			if (string.IsNullOrEmpty (window.data.sn))
				title = string.Format ("{0}/{1}", SWGlobalSettings.ProductTitle, window.data.title);
			else
				title = window.data.sn;
			title = TitleProcess (title);
			StringAddLine( string.Format("Shader \"{0}\"{{   ",title));
		}

		protected virtual string TitleProcess(string name)
		{
			return name;
		}

		public string CreateShaderText()
		{
			txt = "";
			CreateTitle ();
		
			StringAddLine( "\tProperties {   ");
			PropertyField();
			StringAddLine( "\t}   ");


			StringAddLine( "\tSubShader {   ");

			StringAddLine( "\t\tTags {");
			Tags ();
			StringAddLine( "\t\t}");
			Screen_Grab_Pass ();
			PassWarp ();
			StringAddLine( "\t}");
			AddFallBack ();
			StringAddLine( "}");
			return txt;
		}

		void AddFallBack()
		{
			string fb = window.data.fallback;
			if(!string.IsNullOrEmpty(fb))
				StringAddLine( string.Format("\tfallback \"{0}\"",fb));
		}


		#region init
		protected virtual void PropertyDeclare()
		{
			StringAddLine( "\t\t\tfixed4 _Color;");
			foreach (var item in window.nodes) {
				PropertyDeclare_Node (item);
			}

			StringAddLine( "\t\t\tfloat4 _MainTex_ST;");
			foreach (var item in window.textures) {
				StringAddLine( string.Format("\t\t\tsampler2D {0};   ",item.Key));
				//StringAddLine( string.Format("\t\t\tfloat4 {0}_ST;   ",item.Key));
			}
			foreach (var item in window.data.paramList) {
				StringAddLine( string.Format("\t\t\tfloat {0}; ",item.name));
			}


			Screen_PropertyDeclare ();

		}

		protected void PropertyDeclare_Node(SWNodeBase node)
		{
			if (!node.BelongRootTree ())
				return;
			if (node.HasColorAttribute()){
				StringAddLine (string.Format ("\t\t\tfloat4 _Color{0};", node.data.iName));
			}

			Property_Code (node, false);
		}
	
		protected virtual void PropertyField()
		{
			StringAddLine (string.Format("\t\t{0}_Color (\"Color\", Color) = ({1},{2},{3},{4})",
				window.nRoot.data.effectDataColor.hdr? "[HDR]":"",
				window.nRoot.data.effectDataColor.color.r,
				window.nRoot.data.effectDataColor.color.g,
				window.nRoot.data.effectDataColor.color.b,
				window.nRoot.data.effectDataColor.color.a
			));

			foreach (var item in window.nodes) {
				PropertyField_Node (item);
			}

			foreach (var item in window.textures) {
				StringAddLine( string.Format("\t\t{0} (\"{1}\", 2D) = \"white\" {{ }}",item.Key,item.Key));
			}

			foreach (var item in window.data.paramList) {
				if(item.type == SWParamType.FLOAT)
					StringAddLine( string.Format("\t\t{0} (\"{0}\", Float) = {1}",item.name,item.defaultValue));
				if(item.type == SWParamType.RANGE)
					StringAddLine( string.Format("\t\t{0} (\"{0}\", Range({1},{2})) = {3}",item.name,item.min,item.max,item.defaultValue));
			}

			Screen_PropertyField ();
		}
	

		protected void PropertyField_Node(SWNodeBase node)
		{
			if (!node.BelongRootTree ())
				return;
			if (node.HasColorAttribute())
			{
				StringAddLine (string.Format("\t\t{0}_Color{1} (\"Color {2}\", Color) = ({3},{4},{5},{6})",
					node.data.effectDataColor.hdr? "[HDR]":"",
					node.data.iName,
					node.data.name,
					node.data.effectDataColor.color.r,
					node.data.effectDataColor.color.g,
					node.data.effectDataColor.color.b,
					node.data.effectDataColor.color.a
				));
			}

			Property_Code (node, true);
		}

		protected void Property_Code(SWNodeBase node,bool isField)
		{
			if (node is SWNodeCode) {
				var data = node.data;
				SWDataCode code = window.dataCode.CodeOfName (data.code);
				foreach (var paramUse in data.codeParams) {
					if (code.ContainParam (paramUse.n)) {
						var param = code.GetParam (paramUse.n);
						if (param != null && param.IsProperty()) {
							if (isField) {
								if(param.type == CodeParamType.Range)
									StringAddLine (string.Format("\t\t{0}_{2} (\"{1}_{2}\", Range({4},{5})) = {3}", 
										node.data.iName,node.data.name, paramUse.n,paramUse.fv,param.min,param.max));
								else
									StringAddLine (string.Format("\t\t{0}_{2} (\"{0}_{2}\", float) = {3}", 
										node.data.iName,node.data.name,paramUse.n,paramUse.fv));
							} else {
								StringAddLine (string.Format ("\t\t\tfloat {0}_{1};", node.data.iName,paramUse.n));
							}
						}
					}
				}
			}
		}

		protected virtual void Pragma()
		{
			StringAddLine( "\t\t\t#pragma vertex vert   ");
			StringAddLine( "\t\t\t#pragma fragment frag   ");
			PragmaModel ();
		}

		protected void PragmaModel()
		{
			if(window.data.shaderModel == SWShaderModel.m10)
				StringAddLine( "\t\t\t#pragma target 1.0   ");
			else if(window.data.shaderModel == SWShaderModel.m20)
				StringAddLine( "\t\t\t#pragma target 2.0   ");
			else if(window.data.shaderModel == SWShaderModel.m30)
				StringAddLine( "\t\t\t#pragma target 3.0   ");
			else if(window.data.shaderModel == SWShaderModel.m40)
				StringAddLine( "\t\t\t#pragma target 4.0   ");
			else if(window.data.shaderModel == SWShaderModel.m50)
				StringAddLine( "\t\t\t#pragma target 5.0   ");
		}

		protected virtual void Tags()
		{
			if(window.data.shaderQueueOffset == 0)
				StringAddLine( string.Format("\t\t\t\"Queue\"=\"{0}\"",window.data.shaderQueue.ToString()));
			else 	if(window.data.shaderQueueOffset > 0)
				StringAddLine( string.Format("\t\t\t\"Queue\"=\"{0}+{1}\"",window.data.shaderQueue.ToString(),window.data.shaderQueueOffset));
			else 	if(window.data.shaderQueueOffset < 0)
				StringAddLine( string.Format("\t\t\t\"Queue\"=\"{0}{1}\"",window.data.shaderQueue.ToString(),window.data.shaderQueueOffset));
		
		
			StringAddLine (string.Format ("\t\t\t\"RenderType\"=\"{0}\"", window.data.rt.ToString ()));
		}

		protected virtual void Ops()
		{
		}
		protected void Blending()
		{
			if(window.data.shaderBlend == SWShaderBlend.off)
				StringAddLine( "\t\t\tBlend Off   ");
			else if(window.data.shaderBlend == SWShaderBlend.blend)
				StringAddLine( "\t\t\tBlend SrcAlpha  OneMinusSrcAlpha   ");
			else if(window.data.shaderBlend == SWShaderBlend.add)
				StringAddLine( "\t\t\tBlend SrcAlpha  One   ");
			else if(window.data.shaderBlend == SWShaderBlend.mul)
				StringAddLine( "\t\t\tBlend zero  SrcColor   ");
		}

		protected virtual void Includes()
		{
			StringAddLine( "\t\t\t#include \"UnityCG.cginc\"   ");
		}

		protected virtual void Struct_Appdata()
		{
			StringAddLine ("\t\t\tstruct appdata_t {");
			StringAddLine ("\t\t\t\tfloat4 vertex   : POSITION;");
			StringAddLine ("\t\t\t\tfloat4 color    : COLOR;");
			StringAddLine ("\t\t\t\tfloat2 texcoord : TEXCOORD0;");
			StringAddLine ("\t\t\t};");
		}


		protected virtual void Struct_v2fWarp()
		{
			StringAddLine( "\t\t\tstruct v2f {   ");
			Struct_v2f ();
			StringAddLine( "\t\t\t};   ");
		}

		/// <summary>
		/// For Sprite Lighting
		/// uv_xx naming will lead to error,so name them starting with _uv_xx
		/// </summary>
		protected virtual void Struct_v2f()
		{
			if(!(this is SWShaderCreaterSpriteLight))
				StringAddV2d ("float4", "pos", "SV_POSITION");

			//COLOR:color COLOR1:rect_Sprite COLOR2:worldPosition//Use for UI Clipping 				
			StringAddV2d ("fixed4", "color", "COLOR");

			//TEXCOORD0:_uv_MainTex	TEXCOORD1:_uv_STD TEXCOORD2:_uv_Screen 
			StringAddV2d ("float2", "_uv_MainTex", "TEXCOORD0");
			StringAddV2d ("float2", "_uv_STD", "TEXCOORD1");
			Screen_Struct_v2f ();
		}



		#endregion

		#region pass
		public virtual void PassWarp()
		{
			StringAddLine( "\t\tpass {   ");
			Pass ();
			StringAddLine( "\t\t}");
		}

		public virtual void Pass()
		{
			Ops ();
			Blending ();
			StringAddLine( "\t\t\tCGPROGRAM  ");
			Pragma ();
			Includes ();
			PropertyDeclare ();
			Struct_Appdata ();

			Struct_v2fWarp ();

			Functions ();

			VertWrap ();
			FragWarp ();

			StringAddLine( "\t\t\tENDCG");
		}
		#endregion

		#region functions
		protected virtual void Functions()
		{
			Functions_BlendOp ();
			StringAddLine ("\t\t\tfloat2 UV_RotateAround(float2 center,float2 uv,float rad)\n\t\t\t{\n\t\t\t\tfloat2 fuv = uv - center;\n\t\t\t\tfloat2x2 ma = float2x2(cos(rad),sin(rad),-sin(rad),cos(rad));\n\t\t\t\tfuv = mul(ma,fuv)+center;\n\t\t\t\treturn fuv;\n\t\t\t}");
			Functions_Blur ();
			Functions_Retro ();
			Functions_AnimationSheet ();
			Screen_Functions ();

			int Gradient_MaxFrameCount = SWNodeMixer.Gradient_MaxFrameCount ();
			if (Gradient_MaxFrameCount > 0) {
				StringAddLine (string.Format ("\t\t\tfloat GradientEvaluate(float _listTime[{0}],float _listValue[{0}],float count,float pcg)", Gradient_MaxFrameCount));
				StringAddLine ("\t\t\t{\n\t\t\t\tif(count==0)\n\t\t\t\t\treturn 0;\n\t\t\t\tif(pcg<_listTime[0])\n\t\t\t\t\treturn 0;\n\t\t\t\tif(pcg>_listTime[count-1])\n\t\t\t\t\treturn 0;\n\n\t\t\t\tfor(int i= 1;i<count;i++)\n\t\t\t\t{\n\t\t\t\t\tif(pcg <= _listTime[i])\n\t\t\t\t\t{\n\t\t\t\t\t\tfloat v1= _listValue[i-1];\n\t\t\t\t\t\tfloat v2= _listValue[i];\n\t\t\t\t\t\tfloat t1= _listTime[i-1];\n\t\t\t\t\t\tfloat t2= _listTime[i];\n\t\t\t\t\t\treturn lerp(v1,v2, (pcg - t1) / (t2-t1));\n\t\t\t\t\t}\n\t\t\t\t}\n\t\t\t\treturn 0;\n\t\t\t}");
			}

			Functions_CustomCodes ();
		}

		protected void Functions_CustomCodes()
		{
			foreach (var item in SWWindowMain.Instance.dataCode.codes) {
				Functions_CustomCode (item);
			}
		}
		protected void Functions_CustomCode(SWDataCode code)
		{
			if (!code.IsFunction())
				return;

			Dictionary<SWDataType,string> dic = new Dictionary<SWDataType, string> ();
			dic.Add (SWDataType._Alpha, "float");
			dic.Add (SWDataType._UV, "float2");
			dic.Add (SWDataType._Color, "float4");

			string param = "";
			for (int i = 0; i < code.inputs.Count; i++) {
				var input = code.inputs [i];
				if(i!=0)
					param+=",";
				param += string.Format (" {0} {1} ", dic [input.GetType()], input.name);
			}

			string content = "\t\t\t\t"+code.code;
			content = content.Replace ("\n", "\n\t\t\t\t");

			string str = string.Format ("\t\t\t{0} {1}({2})\n\t\t\t{{\n{3}\n\t\t\t}}",
				dic [code.output.GetType()], code.name, param, content);
			
			StringAddLine (str);
		}
	

		protected void Functions_Blur()
		{
			//Blur constrains in rect_Sprite
			StringAddLine ("\t\t\tfloat4 Blur(sampler2D sam,float2 _uv,float2 offset,float4 rect,bool isSpriteTex)\n\t\t\t{\n\t\t\t    int num =12;\n\t\t\t\tfloat2 divi[12] = {float2(-0.326212f, -0.40581f),\n\t\t\t\tfloat2(-0.840144f, -0.07358f),\n\t\t\t\tfloat2(-0.695914f, 0.457137f),\n\t\t\t\tfloat2(-0.203345f, 0.620716f),\n\t\t\t\tfloat2(0.96234f, -0.194983f),\n\t\t\t\tfloat2(0.473434f, -0.480026f),\n\t\t\t\tfloat2(0.519456f, 0.767022f),\n\t\t\t\tfloat2(0.185461f, -0.893124f),\n\t\t\t\tfloat2(0.507431f, 0.064425f),\n\t\t\t\tfloat2(0.89642f, 0.412458f),\n\t\t\t\tfloat2(-0.32194f, -0.932615f),\n\t\t\t\tfloat2(-0.791559f, -0.59771f)};\n\t\t\t\tfloat4 col = float4(0,0,0,0);\n\t\t\t\tfor(int i=0;i<num;i++)\n\t\t\t\t{\n\t\t\t\t\tfloat2 uv = _uv+ offset*divi[i];\n\t\t\t\t\tuv = float2(clamp(uv.x,rect.x,rect.x+rect.z),clamp(uv.y,rect.y,rect.y+rect.w));\n\t\t\t\t\tfloat4 c = tex2D(sam,uv);\n\t\t\t\t\tif(isSpriteTex)\n\t\t\t\t\t\tc.rgb*= 0.3 + 0.7*c.a;\n\t\t\t\t\tcol += c;\n\t\t\t\t}\n\t\t\t\tcol /= num;\n\t\t\t\treturn col;\n\t\t\t}");
		}

		protected void Functions_Retro()
		{
			//use fmod, dont use %
			StringAddLine ("\t\t\tfloat2 Retro(float2 uv,float v)\n\t\t\t{\n\t\t\t\tuv = float2(uv.x - fmod(uv.x,v) + v*0.5 ,uv.y - fmod(uv.y,v) + v*0.5);\n\t\t\t\treturn uv;\n\t\t\t}");
		}

		protected void Functions_AnimationSheet()
		{
			StringAddLine ("\t\t\tfloat2 UV_STD2Rect(float2 uv,float4 rect)\n\t\t\t{\n\t\t\t\tuv.x = lerp(rect.x,rect.x+rect.z, uv.x);\n\t\t\t\tuv.y = lerp(rect.y,rect.y+rect.w, uv.y);\n\t\t\t\treturn uv;\n\t\t\t}");
			StringAddLine ("\t\t\tfloat4 AnimationSheet_RectSub(float row,float col,float rowMax,float colMax)\n\t\t\t{\n\t\t\t\tfloat4 w = float4(0,0,0,0);\n\t\t\t\tw.x =  col/colMax;\n\t\t\t\tw.y =  row/rowMax;\n\t\t\t\tw.z =  1/colMax;\n\t\t\t\tw.w =  1/rowMax;\n\t\t\t\treturn w;\n\t\t\t}");
			StringAddLine ("\t\t\tfloat4 AnimationSheet_Rect(int numTilesX,int numTilesY,bool isLoop,bool singleRow,int rowIndex, int startFrame,float factor)\n\t\t\t{\n\t\t\t\tint count = singleRow? numTilesX : numTilesX*numTilesY;\n\t\t\t\tint f = factor;\n\t\t\t\tif(isLoop)\n\t\t\t\t\tf = (startFrame+f)%count;\n\t\t\t\telse\n\t\t\t\t\tf = clamp((startFrame+f),0,count-1);\n\n\t\t\t\tint row = singleRow? rowIndex : (f / numTilesX);\n\t\t\t\trow = numTilesY - 1 - row;\n\t\t\t\tint col = singleRow? f : f % numTilesX;\n\t\t\t\treturn  AnimationSheet_RectSub(row,col,numTilesY,numTilesX);\n\t\t\t}");
		}

		protected void Functions_BlendOp()
		{
			StringAddLine ("\t\t\tfloat BlendAddf(float base,float act){\n\t\t\t\treturn min(base+act, 1.0);\n\t\t\t}\n\n\t\t\tfloat BlendSubstractf(float base,float act)\n\t\t\t{\n\t\t\t\treturn max(base + act - 1.0, 0.0);\n\t\t\t}\n\n\t\t\tfloat BlendLightenf(float base,float act)\n\t\t\t{\n\t\t\t\treturn max(base, act);\n\t\t\t}\n\n\t\t\tfloat BlendDarkenf(float base,float act)\n\t\t\t{\n\t\t\t\treturn min(base,act);\n\t\t\t}\n\n\t\t\tfloat BlendLinearLightf(float base,float act)\n\t\t\t{\n\t\t\t\treturn (act < 0.5 ? BlendSubstractf(base, (2.0 * act)) : BlendAddf(base, (2.0 * (act - 0.5))));\n\t\t\t}\n\n\t\t\tfloat BlendScreenf(float base,float act)\n\t\t\t{\n\t\t\t\treturn (1.0 - ((1.0 - base) * (1.0 - act)));\n\t\t\t}\n\n\t\t\tfloat BlendOverlayf(float base,float act)\n\t\t\t{\n\t\t\t\treturn (base < 0.5 ? (2.0 * base * act) : (1.0 - 2.0 * (1.0 - base) * (1.0 - act)));\n\t\t\t}\n\n\t\t\tfloat BlendSoftLightf(float base,float act)\n\t\t\t{\n\t\t\t\treturn ((act < 0.5) ? (2.0 * base * act + base * base * (1.0 - 2.0 * act)) : (sqrt(base) * (2.0 * act - 1.0) + 2.0 * base * (1.0 - act)));\n\t\t\t}\n\t\t\tfloat BlendColorDodgef(float base,float act)\n\t\t\t{\n\t\t\t\treturn \t((act == 1.0) ? base : min(base / (1.0 - act), 1.0));\n\t\t\t}\n\t\t\tfloat BlendColorBurnf(float base,float act)\n\t\t\t{\n\t\t\t\treturn ((act == 0.0) ? base : max((1.0 - ((1.0 - base) / act)), 0.0));\n\t\t\t}\n\t\t\tfloat BlendVividLightf(float base,float act)\n\t\t\t{\n\t\t\t\treturn ((act < 0.5) ? BlendColorBurnf(base, (2.0 * act)) : BlendColorDodgef(base, (2.0 * (act - 0.5))));\n\t\t\t}\n\t\t\tfloat BlendPinLightf(float base,float act)\n\t\t\t{\n\t\t\t\treturn ((act < 0.5) ? BlendDarkenf(base, (2.0 * act)) : BlendLightenf(base, (2.0 *(act - 0.5))));\n\t\t\t}\n\t\t\tfloat BlendHardMixf(float base,float act)\n\t\t\t{\n\t\t\t\treturn ((BlendVividLightf(base, act) < 0.5) ? 0.0 : 1.0);\n\t\t\t}\n\t\t\tfloat BlendReflectf(float base,float act)\n\t\t\t{\n\t\t\t\treturn ((act == 1.0) ? act : min(base * base / (1.0 - act), 1.0));\n\t\t\t}\n\n\n\t\t\tfloat BlendDarkerColorf(float base,float act)\n\t\t\t{\n\t\t\t\treturn clamp(base-(1-base)*(1-act)/act,0,1);\n\t\t\t}\n\n\n\n\t\t\tfloat3 BlendDarken(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn min(base,act);\n\t\t\t}\n\t\t\tfloat3 BlendColorBurn(float3 base, float3 act) \n\t\t\t{\n\t\t\t\treturn float3(BlendColorBurnf(base.r,act.r),BlendColorBurnf(base.g,act.g),BlendColorBurnf(base.b,act.b));\n\t\t\t}\n\n\t\t\tfloat3 BlendLinearBurn(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn max(base + act - 1,0);\n\t\t\t}\n\n\n\t\t\t//mine\n\t\t\tfloat3 BlendDarkerColor(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn (base.r+base.g+base.b)>(act.r+act.g+act.b)?act:base;\n\t\t\t}\n\n\t\t\tfloat3 BlendLighten(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn max(base, act);\n\t\t\t}\n\t\t\tfloat3 BlendScreen(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn float3(BlendScreenf(base.r,act.r),BlendScreenf(base.g,act.g),BlendScreenf(base.b,act.b));\n\t\t\t}\n\t\t\tfloat3 BlendColorDodge(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn float3(BlendColorDodgef(base.r,act.r),BlendColorDodgef(base.g,act.g),BlendColorDodgef(base.b,act.b));\n\t\t\t}\n\t\t\tfloat3 BlendLinearDodge(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn min(base+act, 1.0);\n\t\t\t}\n\t\n\n\t\t\n\t\t\t\n\t\t\t//mine\n\t\t\tfloat3 BlendLighterColor(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn (base.r+base.g+base.b)>(act.r+act.g+act.b)?base:act;\n\t\t\t}\n\n\t\t\tfloat3 BlendOverlay(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn  float3(BlendOverlayf(base.r,act.r),BlendOverlayf(base.g,act.g),BlendOverlayf(base.b,act.b));\n\t\t\t}\n\t\t\tfloat3 BlendSoftLight(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn float3(BlendSoftLightf(base.r,act.r),BlendSoftLightf(base.g,act.g),BlendSoftLightf(base.b,act.b));\n\t\t\t}\n\t\t\tfloat3 BlendHardLight(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn BlendOverlay(act, base);\n\t\t\t}\n\t\t\tfloat3 BlendVividLight(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn float3(BlendVividLightf(base.r,act.r),BlendVividLightf(base.g,act.g),BlendVividLightf(base.b,act.b));\n\t\t\t}\n\t\t\tfloat3 BlendLinearLight(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn float3(BlendLinearLightf(base.r,act.r),BlendLinearLightf(base.g,act.g),BlendLinearLightf(base.b,act.b));\n\t\t\t}\n\t\t\tfloat3 BlendPinLight(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn float3(BlendPinLightf(base.r,act.r),BlendPinLightf(base.g,act.g),BlendPinLightf(base.b,act.b));\n\t\t\t}\n\t\t\tfloat3 BlendHardMix(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn float3(BlendHardMixf(base.r,act.r),BlendHardMixf(base.g,act.g),BlendHardMixf(base.b,act.b));\n\t\t\t}\n\t\t\tfloat3 BlendDifference(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn abs(base - act);\n\t\t\t}\n\t\t\tfloat3 BlendExclusion(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn (base + act - 2.0 * base * act);\n\t\t\t}\n\t\t\tfloat3 BlendSubtract(float3 base,float3 act)\n\t\t\t{\n\t\t\t\treturn max(base - act, 0.0);\n\t\t\t}");
			StringAddLine ("\t\t\t/*\n\t\t\t** Hue, saturation, luminance\n\t\t\t*/\n\n\t\t\tfloat3 RGBToHSL(float3 color)\n\t\t\t{\n\t\t\t\tfloat3 hsl; // init to 0 to avoid warnings ? (and reverse if + remove first part)\n\t\t\t\t\n\t\t\t\tfloat fmin = min(min(color.r, color.g), color.b);    //Min. value of RGB\n\t\t\t\tfloat fmax = max(max(color.r, color.g), color.b);    //Max. value of RGB\n\t\t\t\tfloat delta = fmax - fmin;             //Delta RGB value\n\n\t\t\t\thsl.z = (fmax + fmin) / 2.0; // Luminance\n\n\t\t\t\tif (delta == 0.0)\t\t//This is a gray, no chroma...\n\t\t\t\t{\n\t\t\t\t\thsl.x = 0.0;\t// Hue\n\t\t\t\t\thsl.y = 0.0;\t// Saturation\n\t\t\t\t}\n\t\t\t\telse                                    //Chromatic data...\n\t\t\t\t{\n\t\t\t\t\tif (hsl.z < 0.5)\n\t\t\t\t\t\thsl.y = delta / (fmax + fmin); // Saturation\n\t\t\t\t\telse\n\t\t\t\t\t\thsl.y = delta / (2.0 - fmax - fmin); // Saturation\n\t\t\t\t\t\n\t\t\t\t\tfloat deltaR = (((fmax - color.r) / 6.0) + (delta / 2.0)) / delta;\n\t\t\t\t\tfloat deltaG = (((fmax - color.g) / 6.0) + (delta / 2.0)) / delta;\n\t\t\t\t\tfloat deltaB = (((fmax - color.b) / 6.0) + (delta / 2.0)) / delta;\n\n\t\t\t\t\tif (color.r == fmax )\n\t\t\t\t\t\thsl.x = deltaB - deltaG; // Hue\n\t\t\t\t\telse if (color.g == fmax)\n\t\t\t\t\t\thsl.x = (1.0 / 3.0) + deltaR - deltaB; // Hue\n\t\t\t\t\telse if (color.b == fmax)\n\t\t\t\t\t\thsl.x = (2.0 / 3.0) + deltaG - deltaR; // Hue\n\n\t\t\t\t\tif (hsl.x < 0.0)\n\t\t\t\t\t\thsl.x += 1.0; // Hue\n\t\t\t\t\telse if (hsl.x > 1.0)\n\t\t\t\t\t\thsl.x -= 1.0; // Hue\n\t\t\t\t}\n\n\t\t\t\treturn hsl;\n\t\t\t}\n\n\t\t\tfloat HueToRGB(float f1, float f2, float hue)\n\t\t\t{\n\t\t\t\tif (hue < 0.0)\n\t\t\t\t\thue += 1.0;\n\t\t\t\telse if (hue > 1.0)\n\t\t\t\t\thue -= 1.0;\n\t\t\t\tfloat res;\n\t\t\t\tif ((6.0 * hue) < 1.0)\n\t\t\t\t\tres = f1 + (f2 - f1) * 6.0 * hue;\n\t\t\t\telse if ((2.0 * hue) < 1.0)\n\t\t\t\t\tres = f2;\n\t\t\t\telse if ((3.0 * hue) < 2.0)\n\t\t\t\t\tres = f1 + (f2 - f1) * ((2.0 / 3.0) - hue) * 6.0;\n\t\t\t\telse\n\t\t\t\t\tres = f1;\n\t\t\t\treturn res;\n\t\t\t}\n\n\t\t\tfloat3 HSLToRGB(float3 hsl)\n\t\t\t{\n\t\t\t\tfloat3 rgb;\n\t\t\t\t\n\t\t\t\tif (hsl.y == 0.0)\n\t\t\t\t\trgb = float3(hsl.z, hsl.z, hsl.z); // Luminance\n\t\t\t\telse\n\t\t\t\t{\n\t\t\t\t\tfloat f2;\n\t\t\t\t\t\n\t\t\t\t\tif (hsl.z < 0.5)\n\t\t\t\t\t\tf2 = hsl.z * (1.0 + hsl.y);\n\t\t\t\t\telse\n\t\t\t\t\t\tf2 = (hsl.z + hsl.y) - (hsl.y * hsl.z);\n\t\t\t\t\t\t\n\t\t\t\t\tfloat f1 = 2.0 * hsl.z - f2;\n\t\t\t\t\t\n\t\t\t\t\trgb.r = HueToRGB(f1, f2, hsl.x + (1.0/3.0));\n\t\t\t\t\trgb.g = HueToRGB(f1, f2, hsl.x);\n\t\t\t\t\trgb.b= HueToRGB(f1, f2, hsl.x - (1.0/3.0));\n\t\t\t\t}\n\t\t\t\t\n\t\t\t\treturn rgb;\n\t\t\t}\n\n\n\t\t\t// Hue Blend mode creates the result color by combining the luminance and saturation of the base color with the hue of the blend color.\n\t\t\tfloat3 BlendHue(float3 base, float3 blend)\n\t\t\t{\n\t\t\t\tfloat3 baseHSL = RGBToHSL(base);\n\t\t\t\treturn HSLToRGB(float3(RGBToHSL(blend).r, baseHSL.g, baseHSL.b));\n\t\t\t}\n\n\t\t\t// Saturation Blend mode creates the result color by combining the luminance and hue of the base color with the saturation of the blend color.\n\t\t\tfloat3 BlendSaturation(float3 base, float3 blend)\n\t\t\t{\n\t\t\t\tfloat3 baseHSL = RGBToHSL(base);\n\t\t\t\treturn HSLToRGB(float3(baseHSL.r, RGBToHSL(blend).g, baseHSL.b));\n\t\t\t}\n\n\t\t\t// Color Mode keeps the brightness of the base color and applies both the hue and saturation of the blend color.\n\t\t\tfloat3 BlendColor(float3 base, float3 blend)\n\t\t\t{\n\t\t\t\tfloat3 blendHSL = RGBToHSL(blend);\n\t\t\t\treturn HSLToRGB(float3(blendHSL.r, blendHSL.g, RGBToHSL(base).b));\n\t\t\t}\n\n\t\t\t// Luminosity Blend mode creates the result color by combining the hue and saturation of the base color with the luminance of the blend color.\n\t\t\tfloat3 BlendLuminosity(float3 base, float3 blend)\n\t\t\t{\n\t\t\t\tfloat3 baseHSL = RGBToHSL(base);\n\t\t\t\treturn HSLToRGB(float3(baseHSL.r, baseHSL.g, RGBToHSL(blend).b));\n\t\t\t}");
			StringAddLine ("");
		}
		#endregion
	
		#region Vert
		protected virtual void VertWrap()
		{
			StringAddLine( "\t\t\tv2f vert (appdata_t IN) {");
			StringAddLine( "\t\t\t\tv2f OUT;   ");
			Vert ();
			StringAddLine( "\t\t\t\treturn OUT;");
			StringAddLine( "\t\t\t}   ");
		}
		protected virtual void Vert()
		{
			StringAddLine( "\t\t\t\tOUT.pos = mul(UNITY_MATRIX_MVP,IN.vertex);");
			StringAddLine ("\t\t\t\tOUT.color = IN.color * _Color;");
			StringAddLine ("\t\t\t\tOUT._uv_MainTex = TRANSFORM_TEX(IN.texcoord,_MainTex);");
			Screen_Vert ();
			Vert_UV_STD ();
		} 

		protected virtual void Vert_UV_STD()
		{
			StringAddLine ("\t\t\t\tOUT._uv_STD = TRANSFORM_TEX(IN.texcoord,_MainTex);  ");
		}
		#endregion

		#region fragment
		protected virtual void FragWarp()
		{
			StringAddLine( "\t\t\tfloat4 frag (v2f i) : COLOR {");
			Frag ();
			StringAddLine("\t\t\t\treturn result;");
			StringAddLine( "\t\t\t}  ");
		}
		protected virtual void Frag()
		{
			Screen_Fragment ();
			foreach (var item in window.textures) {
				if (item.Key.Contains ("mask")) {
					StringAddLine( string.Format("\t\t\t\tfloat4 color{0} = tex2D({0},i._uv_STD);    ",item.Key));
				}
			}
			ProcessAll(window.nRoot);
		} 
		#endregion

		#region node
		public void ProcessAll(  SWNodeBase root)
		{
			StringAddLine( "\t\t\t\tfloat4 result = float4(0,0,0,0);");
			StringAddLine( "\t\t\t\tfloat4 result2 = float4(0,0,0,0);");
			StringAddLine( "\t\t\t\tfloat3 result3 = float3(0,0,0);");
			StringAddLine( "\t\t\t\tfloat minA = 0;");
			Process (root);
			StringAddLine ("\t\t\t\tresult = result*i.color;");


			ProcessExtra (root);


			StringAddLine( string.Format("\t\t\t\tclip(result.a - {0});",
				window.data.clipValue));
			if(window.data.shaderBlend == SWShaderBlend.mul)
				StringAddLine ("\t\t\t\tresult = lerp(half4(1,1,1,1), result, result.a);");
		}

		/// <summary>
		/// Result -> Here -> Clipping 
		/// </summary>
		public virtual void ProcessExtra(SWNodeBase root)
		{
			
		}
		public   SWOutput Process(  SWNodeBase node)
		{
			if (node.shaderOutput != null)
				return node.shaderOutput;

			SWShaderProcessBase processor = CreateProcessor(node.data.type); 
			node.shaderOutput = processor.Process (node);
			return node.shaderOutput;
		}

		public static SWShaderProcessBase CreateProcessor( SWNodeType type)
		{
			SWShaderProcessBase processor = null;

			if (type == SWNodeType.root){
				processor = new SWShaderProcessRoot ();
			}
			else if (type == SWNodeType.mask){
				processor = new SWShaderProcessMask ();
			}
			else if (type == SWNodeType.color){
				processor = new SWShaderProcessColor ();
			}
			else if (type == SWNodeType.image){
				processor = new SWShaderProcessImage ();
			}
			else if (type == SWNodeType.uv){
				processor = new SWShaderProcessUV ();
			}
			else if (type == SWNodeType.alpha){
				processor = new SWShaderProcessAlpha ();
			}
			else if (type == SWNodeType.mixer){
				processor = new SWShaderProcessMixer ();
			}
			else if (type == SWNodeType.remap){
				processor = new SWShaderProcessRemap ();
			}
			else if (type == SWNodeType.blur){
				processor = new SWShaderProcessBlur ();
			}
			else if (type == SWNodeType.retro){
				processor = new SWShaderProcessRetro ();
			}
			else if (type == SWNodeType.refract){
				processor = new SWShaderProcessRefraction ();
			}
			else if (type == SWNodeType.reflect){
				processor = new SWShaderProcessReflection ();
			}

			else if (type == SWNodeType.coord){
				processor = new SWShaderProcessCoord ();
			}
			else if (type == SWNodeType.dummy){
				processor = new SWShaderProcessDummy ();
			}


			else if (type == SWNodeType.code){
				processor = new SWShaderProcessCode ();
			}
			return processor;
		}

		public static SWShaderProcessReceiveBase CreateProcessorReceiver( SWNodeType type)
		{
			SWShaderProcessReceiveBase receiver = null;

			if (type == SWNodeType.root){

			}
			else if (type == SWNodeType.mask){
				
			}
			else if (type == SWNodeType.color){
				receiver = new SWShaderProcessReceiveColor ();
			}
			else if (type == SWNodeType.image){

			}
			else if (type == SWNodeType.uv){
				receiver = new SWShaderProcessReceiveUV ();
			}
			else if (type == SWNodeType.alpha){
				receiver = new SWShaderProcessReceiveAlpha ();
			}
			else if (type == SWNodeType.remap){
				receiver = new SWShaderProcessReceiveRemap ();
			}
			else if (type == SWNodeType.blur){
				receiver = new SWShaderProcessReceiveBlur ();
			}
			else if (type == SWNodeType.retro){
				receiver = new SWShaderProcessReceiveRetro ();
			}


			else if (type == SWNodeType.coord){
				receiver = new SWShaderProcessReceiveCoord ();
			}
			else if (type == SWNodeType.dummy){
			}
			else if (type == SWNodeType.code){
			}
			return receiver;
		}
		#endregion

		#region other
		public bool HasChildNodeType(SWNodeType type)
		{
			foreach (var node in SWWindowMain.Instance.nodes) {
				if (node.BelongRootTree () && node.data.type == type)
					return true;
			}
			return false;
		}

		public void StringAddV2d(string type,string name,string v2f)
		{
			if(this is SWShaderCreaterSpriteLight)
				StringAddLine(string.Format( "\t\t\t\t{0}  {1};",type,name));
			else 
				StringAddLine(string.Format( "\t\t\t\t{0}  {1} : {2};",type,name,v2f));
		}
		public void StringAddLine(string line)
		{
			txt += line;
			txt += "\n";
		}
		#endregion
	}
}