// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//ShaderWeaverData{"shaderQueue":3000,"rt":1,"shaderQueueOffset":0,"shaderType":1,"spriteLightType":0,"shaderModel":0,"shaderBlend":0,"excludeRoot":false,"version":130,"pixelPerUnit":200.0,"spriteRect":{"serializedVersion":"2","x":0.0,"y":0.86572265625,"width":0.13427734375,"height":0.13427734375},"title":"dissolve_refract","materialGUID":"abd7c54011f11624baa4b359df14bcdf","masksGUID":[],"paramList":[{"type":0,"name":"_pcg","min":0.0,"max":1.0,"defaultValue":0.0}],"nodes":[{"useNormal":false,"id":"463d1404_76ac_4080_a668_589900cf5c1c","name":"ROOT","depth":1,"type":0,"parentPortNumber":0,"parent":[],"parentPort":[],"childPortNumber":1,"children":["b9fda4a8_4f8c_4cfc_a66e_62e6294d447d","0a7e96b6_96de_4fed_aaf4_b3129ddf77dd"],"childrenPort":[0,0],"textureExGUID":"","textureGUID":"","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"8fb98a6035269e64a998f9b56828fc4f","spriteName":"RobotBoyIdle00","rect":{"serializedVersion":"2","x":921.0,"y":280.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":0.0,"y":0.0},"r_angle":0.0,"s_scale":{"x":1.0,"y":1.0},"t_speed":{"x":0.0,"y":0.0},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":true,"color":{"r":0.75,"g":0.75,"b":0.75,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0],"inputType":[0,1,3],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":0,"strs":[]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"78096084_cb2b_4aec_9b17_625021c7ac33","name":"refract1","depth":1,"type":10,"parentPortNumber":1,"parent":["0a7e96b6_96de_4fed_aaf4_b3129ddf77dd"],"parentPort":[0],"childPortNumber":1,"children":["b9fda4a8_4f8c_4cfc_a66e_62e6294d447d"],"childrenPort":[0],"textureExGUID":"","textureGUID":"","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":278.0,"y":555.0,"width":144.0,"height":130.0},"effectData":{"t_startMove":{"x":0.0,"y":0.0},"r_angle":0.0,"s_scale":{"x":1.0,"y":1.0},"t_speed":{"x":0.0,"y":0.0},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":true,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":1,"param":"(_pcg*0.8)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0],"inputType":[1,3,0],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":0,"strs":[]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"b9fda4a8_4f8c_4cfc_a66e_62e6294d447d","name":"uv2","depth":1,"type":4,"parentPortNumber":1,"parent":["463d1404_76ac_4080_a668_589900cf5c1c","78096084_cb2b_4aec_9b17_625021c7ac33"],"parentPort":[0,0],"childPortNumber":1,"children":[],"childrenPort":[],"textureExGUID":"","textureGUID":"e80c3c84ea861404d8a427db8b7abf04","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":70.0,"y":68.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":0.0,"y":0.0},"r_angle":0.0,"s_scale":{"x":1.0,"y":1.0},"t_speed":{"x":0.0,"y":-0.1171875},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":true,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"_pcg","amountR":{"x":0.0,"y":0.009999999776482582},"amountG":{"x":0.009999999776482582,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[1],"inputType":[1],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":19,"strs":["463d1404_76ac_4080_a668_589900cf5c1c","78096084_cb2b_4aec_9b17_625021c7ac33","f7174d0f_3605_4960_beb5_a2f6d5ccf0e8","3839af3e_6192_4fee_a98c_246d93119f27","0a7e96b6_96de_4fed_aaf4_b3129ddf77dd"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"f7174d0f_3605_4960_beb5_a2f6d5ccf0e8","name":"color3","depth":1,"type":3,"parentPortNumber":1,"parent":["0a7e96b6_96de_4fed_aaf4_b3129ddf77dd"],"parentPort":[0],"childPortNumber":1,"children":[],"childrenPort":[],"textureExGUID":"","textureGUID":"","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":499.0,"y":556.0,"width":144.0,"height":130.0},"effectData":{"t_startMove":{"x":0.0,"y":0.0},"r_angle":0.0,"s_scale":{"x":1.0,"y":1.0},"t_speed":{"x":0.0,"y":0.0},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":true,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":true,"color":{"r":3.0,"g":2.7690000534057619,"b":1.569000005722046,"a":1.0},"op":1,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0],"inputType":[3],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":0,"strs":[]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"3839af3e_6192_4fee_a98c_246d93119f27","name":"color4","depth":1,"type":3,"parentPortNumber":1,"parent":["0a7e96b6_96de_4fed_aaf4_b3129ddf77dd"],"parentPort":[0],"childPortNumber":1,"children":[],"childrenPort":[],"textureExGUID":"","textureGUID":"","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":703.0,"y":557.0,"width":144.0,"height":130.0},"effectData":{"t_startMove":{"x":0.0,"y":0.0},"r_angle":0.0,"s_scale":{"x":1.0,"y":1.0},"t_speed":{"x":0.0,"y":0.0},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":true,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":true,"color":{"r":3.0,"g":0.0,"b":0.0,"a":1.0},"op":1,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0],"inputType":[3],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":0,"strs":[]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"0a7e96b6_96de_4fed_aaf4_b3129ddf77dd","name":"mixer5","depth":1,"type":12,"parentPortNumber":1,"parent":["463d1404_76ac_4080_a668_589900cf5c1c"],"parentPort":[0],"childPortNumber":4,"children":["78096084_cb2b_4aec_9b17_625021c7ac33","f7174d0f_3605_4960_beb5_a2f6d5ccf0e8","3839af3e_6192_4fee_a98c_246d93119f27","e3834cae_fea5_45e3_9048_6d8f504b1f7e"],"childrenPort":[1,2,3,0],"textureExGUID":"","textureGUID":"fa3108da2fe38a748bfce58b4c9b5410","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":551.0,"y":303.0,"width":152.0,"height":80.0},"effectData":{"t_startMove":{"x":0.0,"y":0.0},"r_angle":0.0,"s_scale":{"x":1.0,"y":1.0},"t_speed":{"x":0.0,"y":-0.07421875},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":1.0,"pop_speed":-2.0,"pop_Param":"_pcg","pop_channel":0,"useLoop":true,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0,1,3],"inputType":[0,1,3],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":1,"strs":["463d1404_76ac_4080_a668_589900cf5c1c","78096084_cb2b_4aec_9b17_625021c7ac33","b9fda4a8_4f8c_4cfc_a66e_62e6294d447d","f7174d0f_3605_4960_beb5_a2f6d5ccf0e8","3839af3e_6192_4fee_a98c_246d93119f27"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[{"tex":{"instanceID":-384618},"frames":[{"time":0.0,"value":1.0},{"time":0.20000000298023225,"value":1.0}]},{"tex":{"instanceID":-384620},"frames":[{"time":0.20000000298023225,"value":1.0},{"time":0.30000001192092898,"value":0.0}]},{"tex":{"instanceID":-384622},"frames":[{"time":0.20000000298023225,"value":0.0},{"time":0.30000001192092898,"value":1.0}]}],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"e3834cae_fea5_45e3_9048_6d8f504b1f7e","name":"alpha5","depth":1,"type":5,"parentPortNumber":1,"parent":["0a7e96b6_96de_4fed_aaf4_b3129ddf77dd"],"parentPort":[0],"childPortNumber":1,"children":[],"childrenPort":[],"textureExGUID":"","textureGUID":"fa3108da2fe38a748bfce58b4c9b5410","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":308.0,"y":280.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":0.0,"y":0.0},"r_angle":0.0,"s_scale":{"x":1.0,"y":1.0},"t_speed":{"x":0.0,"y":-0.07421875},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":1.0,"pop_speed":-2.0,"pop_Param":"_pcg","pop_channel":0,"useLoop":true,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[3],"inputType":[1],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":1,"strs":["463d1404_76ac_4080_a668_589900cf5c1c","78096084_cb2b_4aec_9b17_625021c7ac33","b9fda4a8_4f8c_4cfc_a66e_62e6294d447d","f7174d0f_3605_4960_beb5_a2f6d5ccf0e8","3839af3e_6192_4fee_a98c_246d93119f27"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[{"tex":{"instanceID":0},"frames":[{"time":0.0,"value":1.0},{"time":0.20000000298023225,"value":1.0}]},{"tex":{"instanceID":0},"frames":[{"time":0.20000000298023225,"value":1.0},{"time":0.30000001192092898,"value":0.0}]},{"tex":{"instanceID":0},"frames":[{"time":0.20000000298023225,"value":0.0},{"time":0.30000001192092898,"value":1.0}]}],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1}],"clipValue":0.0,"fallback":"Standard","sn":"","pum":true,"ps":1.0,"psm":2.0}
Shader "Shader Weaver/dissolve_refract"{   
	Properties {   
		[HDR]_Color ("Color", Color) = (0.75,0.75,0.75,1)
		[HDR]_Color_ROOT ("Color ROOT", Color) = (0.75,0.75,0.75,1)
		_Color_refract1 ("Color refract1", Color) = (1,1,1,1)
		[HDR]_Color_color3 ("Color color3", Color) = (3,2.769,1.569,1)
		[HDR]_Color_color4 ("Color color4", Color) = (3,0,0,1)
		_MainTex ("_MainTex", 2D) = "white" { }
		_Noise ("_Noise", 2D) = "white" { }
		_wave ("_wave", 2D) = "white" { }
		_pcg ("_pcg", Range(0,1)) = 0
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}   
	SubShader {   
		Tags {
			"Queue"="Transparent"
			"RenderType"="Transparent"
			"IgnoreProjector"="True" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		GrabPass{ }
		pass {   
			Cull Off
			Lighting Off
			ZWrite Off
			Blend SrcAlpha  OneMinusSrcAlpha   
			CGPROGRAM  
			#pragma vertex vert   
			#pragma fragment frag   
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"   
			fixed4 _Color;
			float4 _Color_ROOT;
			float4 _Color_refract1;
			float4 _Color_color3;
			float4 _Color_color4;
			float4 _MainTex_ST;
			sampler2D _MainTex;   
			sampler2D _Noise;   
			sampler2D _wave;   
			float _pcg; 
			uniform sampler2D _GrabTexture;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;
			int _useSpriteAnimation;
			float4 _AnimatedRect;
			struct appdata_t {
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			struct v2f {   
				float4  pos : SV_POSITION;
				fixed4  color : COLOR;
				float2  _uv_MainTex : TEXCOORD0;
				float2  _uv_STD : TEXCOORD1;
				float2  _uv_Screen : TEXCOORD3;
				float4  rect_Sprite : COLOR1;
			};   
			float BlendAddf(float base,float act){
				return min(base+act, 1.0);
			}

			float BlendSubstractf(float base,float act)
			{
				return max(base + act - 1.0, 0.0);
			}

			float BlendLightenf(float base,float act)
			{
				return max(base, act);
			}

			float BlendDarkenf(float base,float act)
			{
				return min(base,act);
			}

			float BlendLinearLightf(float base,float act)
			{
				return (act < 0.5 ? BlendSubstractf(base, (2.0 * act)) : BlendAddf(base, (2.0 * (act - 0.5))));
			}

			float BlendScreenf(float base,float act)
			{
				return (1.0 - ((1.0 - base) * (1.0 - act)));
			}

			float BlendOverlayf(float base,float act)
			{
				return (base < 0.5 ? (2.0 * base * act) : (1.0 - 2.0 * (1.0 - base) * (1.0 - act)));
			}

			float BlendSoftLightf(float base,float act)
			{
				return ((act < 0.5) ? (2.0 * base * act + base * base * (1.0 - 2.0 * act)) : (sqrt(base) * (2.0 * act - 1.0) + 2.0 * base * (1.0 - act)));
			}
			float BlendColorDodgef(float base,float act)
			{
				return 	((act == 1.0) ? base : min(base / (1.0 - act), 1.0));
			}
			float BlendColorBurnf(float base,float act)
			{
				return ((act == 0.0) ? base : max((1.0 - ((1.0 - base) / act)), 0.0));
			}
			float BlendVividLightf(float base,float act)
			{
				return ((act < 0.5) ? BlendColorBurnf(base, (2.0 * act)) : BlendColorDodgef(base, (2.0 * (act - 0.5))));
			}
			float BlendPinLightf(float base,float act)
			{
				return ((act < 0.5) ? BlendDarkenf(base, (2.0 * act)) : BlendLightenf(base, (2.0 *(act - 0.5))));
			}
			float BlendHardMixf(float base,float act)
			{
				return ((BlendVividLightf(base, act) < 0.5) ? 0.0 : 1.0);
			}
			float BlendReflectf(float base,float act)
			{
				return ((act == 1.0) ? act : min(base * base / (1.0 - act), 1.0));
			}


			float BlendDarkerColorf(float base,float act)
			{
				return clamp(base-(1-base)*(1-act)/act,0,1);
			}



			float3 BlendDarken(float3 base,float3 act)
			{
				return min(base,act);
			}
			float3 BlendColorBurn(float3 base, float3 act) 
			{
				return float3(BlendColorBurnf(base.r,act.r),BlendColorBurnf(base.g,act.g),BlendColorBurnf(base.b,act.b));
			}

			float3 BlendLinearBurn(float3 base,float3 act)
			{
				return max(base + act - 1,0);
			}


			//mine
			float3 BlendDarkerColor(float3 base,float3 act)
			{
				return (base.r+base.g+base.b)>(act.r+act.g+act.b)?act:base;
			}

			float3 BlendLighten(float3 base,float3 act)
			{
				return max(base, act);
			}
			float3 BlendScreen(float3 base,float3 act)
			{
				return float3(BlendScreenf(base.r,act.r),BlendScreenf(base.g,act.g),BlendScreenf(base.b,act.b));
			}
			float3 BlendColorDodge(float3 base,float3 act)
			{
				return float3(BlendColorDodgef(base.r,act.r),BlendColorDodgef(base.g,act.g),BlendColorDodgef(base.b,act.b));
			}
			float3 BlendLinearDodge(float3 base,float3 act)
			{
				return min(base+act, 1.0);
			}
	

		
			
			//mine
			float3 BlendLighterColor(float3 base,float3 act)
			{
				return (base.r+base.g+base.b)>(act.r+act.g+act.b)?base:act;
			}

			float3 BlendOverlay(float3 base,float3 act)
			{
				return  float3(BlendOverlayf(base.r,act.r),BlendOverlayf(base.g,act.g),BlendOverlayf(base.b,act.b));
			}
			float3 BlendSoftLight(float3 base,float3 act)
			{
				return float3(BlendSoftLightf(base.r,act.r),BlendSoftLightf(base.g,act.g),BlendSoftLightf(base.b,act.b));
			}
			float3 BlendHardLight(float3 base,float3 act)
			{
				return BlendOverlay(act, base);
			}
			float3 BlendVividLight(float3 base,float3 act)
			{
				return float3(BlendVividLightf(base.r,act.r),BlendVividLightf(base.g,act.g),BlendVividLightf(base.b,act.b));
			}
			float3 BlendLinearLight(float3 base,float3 act)
			{
				return float3(BlendLinearLightf(base.r,act.r),BlendLinearLightf(base.g,act.g),BlendLinearLightf(base.b,act.b));
			}
			float3 BlendPinLight(float3 base,float3 act)
			{
				return float3(BlendPinLightf(base.r,act.r),BlendPinLightf(base.g,act.g),BlendPinLightf(base.b,act.b));
			}
			float3 BlendHardMix(float3 base,float3 act)
			{
				return float3(BlendHardMixf(base.r,act.r),BlendHardMixf(base.g,act.g),BlendHardMixf(base.b,act.b));
			}
			float3 BlendDifference(float3 base,float3 act)
			{
				return abs(base - act);
			}
			float3 BlendExclusion(float3 base,float3 act)
			{
				return (base + act - 2.0 * base * act);
			}
			float3 BlendSubtract(float3 base,float3 act)
			{
				return max(base - act, 0.0);
			}
			/*
			** Hue, saturation, luminance
			*/

			float3 RGBToHSL(float3 color)
			{
				float3 hsl; // init to 0 to avoid warnings ? (and reverse if + remove first part)
				
				float fmin = min(min(color.r, color.g), color.b);    //Min. value of RGB
				float fmax = max(max(color.r, color.g), color.b);    //Max. value of RGB
				float delta = fmax - fmin;             //Delta RGB value

				hsl.z = (fmax + fmin) / 2.0; // Luminance

				if (delta == 0.0)		//This is a gray, no chroma...
				{
					hsl.x = 0.0;	// Hue
					hsl.y = 0.0;	// Saturation
				}
				else                                    //Chromatic data...
				{
					if (hsl.z < 0.5)
						hsl.y = delta / (fmax + fmin); // Saturation
					else
						hsl.y = delta / (2.0 - fmax - fmin); // Saturation
					
					float deltaR = (((fmax - color.r) / 6.0) + (delta / 2.0)) / delta;
					float deltaG = (((fmax - color.g) / 6.0) + (delta / 2.0)) / delta;
					float deltaB = (((fmax - color.b) / 6.0) + (delta / 2.0)) / delta;

					if (color.r == fmax )
						hsl.x = deltaB - deltaG; // Hue
					else if (color.g == fmax)
						hsl.x = (1.0 / 3.0) + deltaR - deltaB; // Hue
					else if (color.b == fmax)
						hsl.x = (2.0 / 3.0) + deltaG - deltaR; // Hue

					if (hsl.x < 0.0)
						hsl.x += 1.0; // Hue
					else if (hsl.x > 1.0)
						hsl.x -= 1.0; // Hue
				}

				return hsl;
			}

			float HueToRGB(float f1, float f2, float hue)
			{
				if (hue < 0.0)
					hue += 1.0;
				else if (hue > 1.0)
					hue -= 1.0;
				float res;
				if ((6.0 * hue) < 1.0)
					res = f1 + (f2 - f1) * 6.0 * hue;
				else if ((2.0 * hue) < 1.0)
					res = f2;
				else if ((3.0 * hue) < 2.0)
					res = f1 + (f2 - f1) * ((2.0 / 3.0) - hue) * 6.0;
				else
					res = f1;
				return res;
			}

			float3 HSLToRGB(float3 hsl)
			{
				float3 rgb;
				
				if (hsl.y == 0.0)
					rgb = float3(hsl.z, hsl.z, hsl.z); // Luminance
				else
				{
					float f2;
					
					if (hsl.z < 0.5)
						f2 = hsl.z * (1.0 + hsl.y);
					else
						f2 = (hsl.z + hsl.y) - (hsl.y * hsl.z);
						
					float f1 = 2.0 * hsl.z - f2;
					
					rgb.r = HueToRGB(f1, f2, hsl.x + (1.0/3.0));
					rgb.g = HueToRGB(f1, f2, hsl.x);
					rgb.b= HueToRGB(f1, f2, hsl.x - (1.0/3.0));
				}
				
				return rgb;
			}


			// Hue Blend mode creates the result color by combining the luminance and saturation of the base color with the hue of the blend color.
			float3 BlendHue(float3 base, float3 blend)
			{
				float3 baseHSL = RGBToHSL(base);
				return HSLToRGB(float3(RGBToHSL(blend).r, baseHSL.g, baseHSL.b));
			}

			// Saturation Blend mode creates the result color by combining the luminance and hue of the base color with the saturation of the blend color.
			float3 BlendSaturation(float3 base, float3 blend)
			{
				float3 baseHSL = RGBToHSL(base);
				return HSLToRGB(float3(baseHSL.r, RGBToHSL(blend).g, baseHSL.b));
			}

			// Color Mode keeps the brightness of the base color and applies both the hue and saturation of the blend color.
			float3 BlendColor(float3 base, float3 blend)
			{
				float3 blendHSL = RGBToHSL(blend);
				return HSLToRGB(float3(blendHSL.r, blendHSL.g, RGBToHSL(base).b));
			}

			// Luminosity Blend mode creates the result color by combining the hue and saturation of the base color with the luminance of the blend color.
			float3 BlendLuminosity(float3 base, float3 blend)
			{
				float3 baseHSL = RGBToHSL(base);
				return HSLToRGB(float3(baseHSL.r, baseHSL.g, RGBToHSL(blend).b));
			}

			float2 UV_RotateAround(float2 center,float2 uv,float rad)
			{
				float2 fuv = uv - center;
				float2x2 ma = float2x2(cos(rad),sin(rad),-sin(rad),cos(rad));
				fuv = mul(ma,fuv)+center;
				return fuv;
			}
			float4 Blur(sampler2D sam,float2 _uv,float2 offset,float4 rect,bool isSpriteTex)
			{
			    int num =12;
				float2 divi[12] = {float2(-0.326212f, -0.40581f),

				float2(-0.840144f, -0.07358f),

				float2(-0.695914f, 0.457137f),

				float2(-0.203345f, 0.620716f),

				float2(0.96234f, -0.194983f),

				float2(0.473434f, -0.480026f),

				float2(0.519456f, 0.767022f),

				float2(0.185461f, -0.893124f),

				float2(0.507431f, 0.064425f),

				float2(0.89642f, 0.412458f),

				float2(-0.32194f, -0.932615f),

				float2(-0.791559f, -0.59771f)};
				float4 col = float4(0,0,0,0);



				for(int i=0;i<num;i++)
				{
					float2 uv = _uv+ offset*divi[i];
					uv = float2(clamp(uv.x,rect.x,rect.x+rect.z),clamp(uv.y,rect.y,rect.y+rect.w));
					float4 c = tex2D(sam,uv);
					if(isSpriteTex)
						c.rgb*=c.a;
					col += c;
				}
				col /= num;
				return col;
			}
			float2 Retro(float2 uv,float v)
			{
				uv = float2(uv.x - fmod(uv.x,v) + v*0.5 ,uv.y - fmod(uv.y,v) + v*0.5);
				return uv;
			}
			float2 UV_STD2Rect(float2 uv,float4 rect)
			{
				uv.x = lerp(rect.x,rect.x+rect.z, uv.x);
				uv.y = lerp(rect.y,rect.y+rect.w, uv.y);
				return uv;
			}
			float4 AnimationSheet_RectSub(float row,float col,float rowMax,float colMax)
			{
				float4 w = float4(0,0,0,0);
				w.x =  col/colMax;
				w.y =  row/rowMax;
				w.z =  1/colMax;
				w.w =  1/rowMax;
				return w;
			}
			float4 AnimationSheet_Rect(int numTilesX,int numTilesY,bool isLoop,bool singleRow,int rowIndex, int startFrame,float factor)
			{
				int count = singleRow? numTilesX : numTilesX*numTilesY;
				int f = factor;
				if(isLoop)
					f = (startFrame+f)%count;
				else
					f = clamp((startFrame+f),0,count-1);

				int row = singleRow? rowIndex : (f / numTilesX);
				row = numTilesY - 1 - row;
				int col = singleRow? f : f % numTilesX;
				return  AnimationSheet_RectSub(row,col,numTilesY,numTilesX);
			}
			float GradientEvaluate(float _listTime[2],float _listValue[2],float count,float pcg)
			{
				if(count==0)
					return 0;
				if(pcg<_listTime[0])
					return 0;
				if(pcg>_listTime[count-1])
					return 0;

				for(int i= 1;i<count;i++)
				{
					if(pcg <= _listTime[i])
					{
						float v1= _listValue[i-1];
						float v2= _listValue[i];
						float t1= _listTime[i-1];
						float t2= _listTime[i];
						return lerp(v1,v2, (pcg - t1) / (t2-t1));
					}
				}
				return 0;
			}
			float4 Contrast( float4 color , float4 blurred , float intensity , float threshold )
			{
				half4 difference = color - blurred;
				half4 signs = sign (difference);
				
				half4 enhancement = saturate (abs(difference) - threshold) * signs * 1.0/(1.0-threshold);
				color += enhancement * intensity;
				
				return color;
			}
			float2 FishEye( float2 uv , float size )
			{
				float2 m = float2(0.5, 0.5);
				float2 d = uv - m;
				float r = sqrt(dot(d, d));
				float amount = (2.0 * 3.141592653 / (2.0 * sqrt(dot(m, m)))) * (size*0.5+0.0001);
				float bind = sqrt(dot(m, m));
				uv = m + normalize(d) * tan(r * amount) * bind/ tan(bind * amount);
				return uv;
			}
			float4 Grayscale( float4 color , float rate )
			{
				fixed gray = Luminance(color.rgb);
				return lerp(color, float4(gray,gray,gray,color.a),rate);
			}
			float4 OldPhoto( float4 color , float rate )
			{
				// get intensity value (Y part of YIQ color space)
				fixed Y = dot (fixed3(0.299, 0.587, 0.114), color.rgb);
				
				// Convert to Sepia Tone by adding constant
				fixed4 sepiaConvert = float4 (0.191, -0.054, -0.221, 0.0);
				fixed4 output = sepiaConvert + Y;
				output.a = color.a;
				return lerp(color,output,rate);
			}
			float OneMinus( float a )
			{
				return 1-a;
			}
			float2 Pinch( float2 uv , float size )
			{
				float2 m = float2(0.5, 0.5);
				float2 d = uv - m;
				float r = sqrt(dot(d, d));
				float amount = (2.0 * 3.141592653 / (2.0 * sqrt(dot(m, m)))) * (-size+0.001);
				float bind = 0.5;
				uv = m + normalize(d) * atan(r * -amount * 16.0) * bind / atan(-amount * bind * 16.0);
				return uv;
			}
			float2 Ramp( float4 Color )
			{
				return float2(sqrt(Color.a),0.5);
			}
			float2 Twirl( float2 uv , float value , float posx , float posy , float radius )
			{
				value = value / (180/3.141592653);
				uv -= float2(posx,posy);
				float2 distortedOffset = UV_RotateAround(float2(0,0),uv,value);
				float2 tmp = uv / radius;
				float t = min (1, length(tmp));
				uv = lerp (distortedOffset, uv, t);
				uv += float2(posx,posy);
				return uv;
			}
			float2 Vortex( float2 uv , float value , float posx , float posy , float radius )
			{
				value = value / (180/3.141592653);
				uv -= float2(posx,posy);
				float angle = 1.0 - length(uv / radius);
				angle = max (0, angle);
				angle = angle * angle * value;
				float cosLength, sinLength;
				sincos (angle, sinLength, cosLength);
				
				float2 _uv;
				_uv.x = cosLength * uv[0] - sinLength * uv[1];
				_uv.y = sinLength * uv[0] + cosLength * uv[1];
				_uv += float2(posx,posy);
				return _uv;
			}
			v2f vert (appdata_t IN) {
				v2f OUT;   
				if(_useSpriteAnimation==1)
					OUT.rect_Sprite = _AnimatedRect;
				else
				OUT.rect_Sprite = float4(0,0.8657227,0.1342773,0.1342773);
				OUT.pos = UnityObjectToClipPos(IN.vertex);   
				OUT.color = IN.color * _Color;
				OUT._uv_MainTex = TRANSFORM_TEX(IN.texcoord,_MainTex);
				float4 wpos = OUT.pos;
				#if UNITY_UV_STARTS_AT_TOP
					float grabSign = -_ProjectionParams.x;
				#else
					float grabSign = _ProjectionParams.x;
				#endif
				OUT._uv_Screen = wpos.xy / wpos.w;
				OUT._uv_Screen.y *= _ProjectionParams.x;
				OUT._uv_Screen = float2(1,grabSign)*OUT._uv_Screen*0.5+0.5;
				OUT._uv_STD = float2((IN.texcoord.x - OUT.rect_Sprite.x)/OUT.rect_Sprite.z,(IN.texcoord.y - OUT.rect_Sprite.y)/OUT.rect_Sprite.w);
				OUT._uv_STD = TRANSFORM_TEX(OUT._uv_STD,_MainTex);  
				#ifdef PIXELSNAP_ON
				OUT.pos = UnityPixelSnap (OUT.pos);
				#endif
				return OUT;
			}   
			float4 frag (v2f i) : COLOR {
				float4 result = float4(0,0,0,0);
				float4 result2 = float4(0,0,0,0);
				float3 result3 = float3(0,0,0);
				float minA = 0;


				//====================================
				//============ uv2 ============   
				float2  uv_uv2 = i._uv_STD;
				float2 center_uv2 = float2(0.5,0.5);    
				uv_uv2 = uv_uv2-center_uv2;    
				uv_uv2 = uv_uv2+fixed2(0,0);    
				uv_uv2 = uv_uv2+fixed2(0,0.1171875)*(_Time.y);    
				uv_uv2 = UV_RotateAround(fixed2(0,0),uv_uv2,0);    
				uv_uv2 = uv_uv2/fixed2(1,1);    
				float2 dir_uv2 = uv_uv2/length(uv_uv2);    
				uv_uv2 = uv_uv2-dir_uv2*fixed2(0,0)*(_Time.y);    
				uv_uv2 = UV_RotateAround(fixed2(0,0),uv_uv2,0*(_Time.y));    
				uv_uv2 = uv_uv2+center_uv2;    
				float2 uv_uv2orgin = uv_uv2;
				uv_uv2 = float2(uv_uv2.x >0 ?fmod(uv_uv2.x,1+0) : (1+0) - fmod(abs(uv_uv2.x),1+0), uv_uv2.y >0 ?fmod(uv_uv2.y,1+0): (1+0) - fmod(abs(uv_uv2.y),1+0));
				bool discard_uv2 = false;
				if(uv_uv2.x>1 || uv_uv2.y>1)
					discard_uv2 = true;
				float4 rect_uv2 =  float4(1,1,1,1);
				float4 color_uv2 = tex2D(_Noise,uv_uv2);    
				if(discard_uv2 == true) color_uv2 = float4(0,0,0,0);
				uv_uv2 = -(color_uv2.r*fixed2(0,0.01) + color_uv2.g*fixed2(0.01,0) + color_uv2.b*fixed2(0,0) +  color_uv2.a*fixed2(0,0));    


				//====================================
				//============ refract1 ============   
				float2  uv_refract1 = i._uv_Screen;
				float2 center_refract1 = float2(0.5,0.5);    
				uv_refract1 = uv_refract1-center_refract1;    
				uv_refract1 = uv_refract1+fixed2(0,0);    
				uv_refract1 = uv_refract1+fixed2(0,0)*(_Time.y);    
				uv_refract1 = UV_RotateAround(fixed2(0,0),uv_refract1,0);    
				uv_refract1 = uv_refract1/fixed2(1,1);    
				float2 dir_refract1 = uv_refract1/length(uv_refract1);    
				uv_refract1 = uv_refract1-dir_refract1*fixed2(0,0)*(_Time.y);    
				uv_refract1 = UV_RotateAround(fixed2(0,0),uv_refract1,0*(_Time.y));    
				uv_refract1 = uv_refract1+center_refract1;    
				uv_refract1 = uv_refract1 + uv_uv2*1*(_pcg);
				float2 uv_refract1orgin = uv_refract1;
				uv_refract1 = float2(uv_refract1.x >0 ?fmod(uv_refract1.x,1+0) : (1+0) - fmod(abs(uv_refract1.x),1+0), uv_refract1.y >0 ?fmod(uv_refract1.y,1+0): (1+0) - fmod(abs(uv_refract1.y),1+0));
				bool discard_refract1 = false;
				if(uv_refract1.x>1 || uv_refract1.y>1)
					discard_refract1 = true;
				float4 rect_refract1 =  float4(1,1,1,1);
				float4 color_refract1 = tex2D(_GrabTexture,uv_refract1);    
				if(discard_refract1 == true) color_refract1 = float4(0,0,0,0);
				color_refract1 = color_refract1*_Color_refract1;


				//====================================
				//============ color3 ============   
				float4 color_color3 = _Color_color3;


				//====================================
				//============ color4 ============   
				float4 color_color4 = _Color_color4;


				//====================================
				//============ alpha5 ============   
				float2  uv_alpha5 = i._uv_STD;
				float2 center_alpha5 = float2(0.5,0.5);    
				uv_alpha5 = uv_alpha5-center_alpha5;    
				uv_alpha5 = uv_alpha5+fixed2(0,0);    
				uv_alpha5 = uv_alpha5+fixed2(0,0.07421875)*(_Time.y);    
				uv_alpha5 = UV_RotateAround(fixed2(0,0),uv_alpha5,0);    
				uv_alpha5 = uv_alpha5/fixed2(1,1);    
				float2 dir_alpha5 = uv_alpha5/length(uv_alpha5);    
				uv_alpha5 = uv_alpha5-dir_alpha5*fixed2(0,0)*(_Time.y);    
				uv_alpha5 = UV_RotateAround(fixed2(0,0),uv_alpha5,0*(_Time.y));    
				uv_alpha5 = uv_alpha5+center_alpha5;    
				float2 uv_alpha5orgin = uv_alpha5;
				uv_alpha5 = float2(uv_alpha5.x >0 ?fmod(uv_alpha5.x,1+0) : (1+0) - fmod(abs(uv_alpha5.x),1+0), uv_alpha5.y >0 ?fmod(uv_alpha5.y,1+0): (1+0) - fmod(abs(uv_alpha5.y),1+0));
				bool discard_alpha5 = false;
				if(uv_alpha5.x>1 || uv_alpha5.y>1)
					discard_alpha5 = true;
				float4 rect_alpha5 =  float4(1,1,1,1);
				float4 color_alpha5 = tex2D(_wave,uv_alpha5);    
				if(discard_alpha5 == true) color_alpha5 = float4(0,0,0,0);
				float aplha_alpha5 = 1 +-2*(_pcg) + color_alpha5.r;


				//====================================
				//============ mixer5 ============   
				float mixer_mixer5;
				mixer_mixer5 = clamp(aplha_alpha5*1*((1)),0,1);
				mixer_mixer5 = clamp(mixer_mixer5,0,1);
				float gra_mixer5_0ListTime[2] = {0,0.2};
				float gra_mixer5_0ListValue[2] = {1,1};
				float gra_mixer5_0 = GradientEvaluate(gra_mixer5_0ListTime,gra_mixer5_0ListValue,2,mixer_mixer5);
				float gra_mixer5_1ListTime[2] = {0.2,0.3};
				float gra_mixer5_1ListValue[2] = {1,0};
				float gra_mixer5_1 = GradientEvaluate(gra_mixer5_1ListTime,gra_mixer5_1ListValue,2,mixer_mixer5);
				float gra_mixer5_2ListTime[2] = {0.2,0.3};
				float gra_mixer5_2ListValue[2] = {0,1};
				float gra_mixer5_2 = GradientEvaluate(gra_mixer5_2ListTime,gra_mixer5_2ListValue,2,mixer_mixer5);


				//====================================
				//============ ROOT ============   
				float2  uv_ROOT = i._uv_STD;
				float2 center_ROOT = float2(0.5,0.5);    
				uv_ROOT = uv_ROOT-center_ROOT;    
				uv_ROOT = uv_ROOT+fixed2(0,0);    
				uv_ROOT = uv_ROOT+fixed2(0,0)*(_Time.y);    
				uv_ROOT = UV_RotateAround(fixed2(0,0),uv_ROOT,0);    
				uv_ROOT = uv_ROOT/fixed2(1,1);    
				float2 dir_ROOT = uv_ROOT/length(uv_ROOT);    
				uv_ROOT = uv_ROOT-dir_ROOT*fixed2(0,0)*(_Time.y);    
				uv_ROOT = UV_RotateAround(fixed2(0,0),uv_ROOT,0*(_Time.y));    
				uv_ROOT = uv_ROOT+center_ROOT;    
				uv_ROOT = uv_ROOT + uv_uv2*1*(_pcg);
				float4 rect_ROOT =  i.rect_Sprite;
				uv_ROOT = UV_STD2Rect(uv_ROOT,rect_ROOT);
				float4 color_ROOT = tex2D(_MainTex,uv_ROOT);    
				color_ROOT.rgb*=color_ROOT.a;
				float4 rootTexColor = color_ROOT;
				color_ROOT = color_ROOT*_Color_ROOT;
				result = float4(color_ROOT.rgb,color_ROOT.a*1);
				result = lerp(result,float4(color_color4.rgb,rootTexColor.a),clamp(color_color4.a*1*((1))*gra_mixer5_2,0,1));    
				result = lerp(result,float4(color_color3.rgb,rootTexColor.a),clamp(color_color3.a*1*((1))*gra_mixer5_1,0,1));    
				result = lerp(result,float4(color_refract1.rgb,rootTexColor.a),clamp(color_refract1.a*1*((_pcg*0.8))*gra_mixer5_0,0,1));    
				result = result*i.color;
				#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
				result.a = tex2D (_AlphaTex, uv).r;
				#endif 
				clip(result.a - 0);
				return result;
			}  
			ENDCG
		}
	}
	fallback "Standard"
}
