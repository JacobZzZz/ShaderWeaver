// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//ShaderWeaverData{"shaderQueue":3000,"rt":1,"shaderQueueOffset":0,"shaderType":0,"spriteLightType":0,"shaderModel":0,"shaderBlend":0,"excludeRoot":false,"version":130,"pixelPerUnit":0.0,"spriteRect":{"serializedVersion":"2","x":0.0,"y":0.0,"width":1.0,"height":1.0},"title":"ninja","materialGUID":"1da2a3950503d1e458682e34631bd55d","masksGUID":["81155dd81ebf3c249aa5e62ee3ab96cb"],"paramList":[],"nodes":[{"useNormal":false,"id":"0ce4b059_1bb9_4f7a_b568_ea496290c0f4","name":"ROOT","depth":1,"type":0,"parentPortNumber":0,"parent":[],"parentPort":[],"childPortNumber":1,"children":["c0544044_0353_4612_b997_7d538aea6ebc"],"childrenPort":[0],"textureExGUID":"","textureGUID":"5362bf34910d5ba488510da8be0447d3","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":1297.0,"y":357.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":0.0,"y":0.0},"r_angle":0.0,"s_scale":{"x":1.0,"y":1.0},"t_speed":{"x":0.0,"y":0.0},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0],"inputType":[0,1,3],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":0,"strs":[]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"c0544044_0353_4612_b997_7d538aea6ebc","name":"mask1","depth":1,"type":1,"parentPortNumber":1,"parent":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4"],"parentPort":[0],"childPortNumber":1,"children":["b4d02c7e_a559_4d99_84fe_5b3b560a4af3"],"childrenPort":[0],"textureExGUID":"","textureGUID":"81155dd81ebf3c249aa5e62ee3ab96cb","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":1074.0,"y":358.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":0.0,"y":0.0},"r_angle":0.0,"s_scale":{"x":1.0,"y":1.0},"t_speed":{"x":0.0,"y":0.0},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0,1,3],"inputType":[0,1,3],"dirty":false,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":1,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","b4d02c7e_a559_4d99_84fe_5b3b560a4af3","41c415a1_de67_4452_a27e_d8b210d7f019","2a26c9e3_ba73_407c_a9b2_06ba459afa1c","1cee45cb_5509_4e95_a01d_11b915a6cb66","aa69aaff_298d_485b_a5eb_501fbbb16667","5817aa94_f9c1_422e_801d_19e9dbcbe9f8","669652b9_4a45_4a77_89ce_08edd03a9c3d","76f84b7a_5473_4cdc_b2e6_5e2aac6d2115","623110b0_8125_46fa_82db_cff30d5e8b44","58ada36f_41b9_4a3c_9d4e_4a585c9a59f7","cd010c71_c046_4f66_a68d_5cc4b0387e8a","98090fd0_1717_4249_b7a1_38a13877e7b9"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"b4d02c7e_a559_4d99_84fe_5b3b560a4af3","name":"image2","depth":1,"type":13,"parentPortNumber":1,"parent":["c0544044_0353_4612_b997_7d538aea6ebc"],"parentPort":[0],"childPortNumber":1,"children":["41c415a1_de67_4452_a27e_d8b210d7f019","2a26c9e3_ba73_407c_a9b2_06ba459afa1c"],"childrenPort":[0,0],"textureExGUID":"","textureGUID":"7e23983ed169cf3468ba138638c79233","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":858.0,"y":359.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":-0.015625,"y":0.2109375},"r_angle":0.0,"s_scale":{"x":0.7269287109375,"y":0.3963494896888733},"t_speed":{"x":0.0,"y":0.0},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":1,"gapX":0.0,"loopY":1,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0],"inputType":[1,3,0],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":3,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","c0544044_0353_4612_b997_7d538aea6ebc","41c415a1_de67_4452_a27e_d8b210d7f019","2a26c9e3_ba73_407c_a9b2_06ba459afa1c"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"41c415a1_de67_4452_a27e_d8b210d7f019","name":"image3","depth":15,"type":13,"parentPortNumber":1,"parent":["b4d02c7e_a559_4d99_84fe_5b3b560a4af3"],"parentPort":[0],"childPortNumber":1,"children":["aa69aaff_298d_485b_a5eb_501fbbb16667","76f84b7a_5473_4cdc_b2e6_5e2aac6d2115","58ada36f_41b9_4a3c_9d4e_4a585c9a59f7"],"childrenPort":[0,0,0],"textureExGUID":"","textureGUID":"16b1d99fea2ba394e89f2edf72cff5c6","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":633.0,"y":359.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":-0.138671875,"y":0.12890625},"r_angle":0.019958004355430604,"s_scale":{"x":0.8427830934524536,"y":0.699999988079071},"t_speed":{"x":0.0,"y":0.0},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":1,"gapX":0.0,"loopY":1,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0],"inputType":[1,3,0],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":-1,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","c0544044_0353_4612_b997_7d538aea6ebc","b4d02c7e_a559_4d99_84fe_5b3b560a4af3","2a26c9e3_ba73_407c_a9b2_06ba459afa1c"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"2a26c9e3_ba73_407c_a9b2_06ba459afa1c","name":"image4","depth":3,"type":13,"parentPortNumber":1,"parent":["b4d02c7e_a559_4d99_84fe_5b3b560a4af3"],"parentPort":[0],"childPortNumber":1,"children":["669652b9_4a45_4a77_89ce_08edd03a9c3d","623110b0_8125_46fa_82db_cff30d5e8b44"],"childrenPort":[0,0],"textureExGUID":"","textureGUID":"266d6819da5430d4681eeb25959b6661","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":633.0,"y":514.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":0.00390625,"y":0.19921875},"r_angle":0.9445533156394959,"s_scale":{"x":0.2199999988079071,"y":0.4000000059604645},"t_speed":{"x":0.0,"y":0.0},"r_speed":-1.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":1,"gapX":0.0,"loopY":1,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":3,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0],"inputType":[1,3,0],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":-1,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","c0544044_0353_4612_b997_7d538aea6ebc","b4d02c7e_a559_4d99_84fe_5b3b560a4af3","41c415a1_de67_4452_a27e_d8b210d7f019","1cee45cb_5509_4e95_a01d_11b915a6cb66","aa69aaff_298d_485b_a5eb_501fbbb16667","5817aa94_f9c1_422e_801d_19e9dbcbe9f8","669652b9_4a45_4a77_89ce_08edd03a9c3d"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"1cee45cb_5509_4e95_a01d_11b915a6cb66","name":"uv5","depth":1,"type":4,"parentPortNumber":1,"parent":["5817aa94_f9c1_422e_801d_19e9dbcbe9f8"],"parentPort":[0],"childPortNumber":1,"children":[],"childrenPort":[],"textureExGUID":"","textureGUID":"fa3108da2fe38a748bfce58b4c9b5410","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":115.0,"y":356.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":2.384185791015625e-7,"y":-1.1920928955078126e-7},"r_angle":0.0,"s_scale":{"x":2.0,"y":2.0},"t_speed":{"x":-0.4765627384185791,"y":0.14257824420928956},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(3)","amountR":{"x":-0.0292971134185791,"y":-0.03124988079071045},"amountG":{"x":-0.0253908634185791,"y":0.005859494209289551},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[1],"inputType":[1],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":15,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","c0544044_0353_4612_b997_7d538aea6ebc","b4d02c7e_a559_4d99_84fe_5b3b560a4af3","41c415a1_de67_4452_a27e_d8b210d7f019","2a26c9e3_ba73_407c_a9b2_06ba459afa1c","aa69aaff_298d_485b_a5eb_501fbbb16667","5817aa94_f9c1_422e_801d_19e9dbcbe9f8","669652b9_4a45_4a77_89ce_08edd03a9c3d","76f84b7a_5473_4cdc_b2e6_5e2aac6d2115","623110b0_8125_46fa_82db_cff30d5e8b44","58ada36f_41b9_4a3c_9d4e_4a585c9a59f7","cd010c71_c046_4f66_a68d_5cc4b0387e8a","98090fd0_1717_4249_b7a1_38a13877e7b9"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"aa69aaff_298d_485b_a5eb_501fbbb16667","name":"image6","depth":5,"type":13,"parentPortNumber":1,"parent":["41c415a1_de67_4452_a27e_d8b210d7f019"],"parentPort":[0],"childPortNumber":1,"children":["5817aa94_f9c1_422e_801d_19e9dbcbe9f8"],"childrenPort":[0],"textureExGUID":"","textureGUID":"20f9e73d426eb6d4a887a0a829fd8f42","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":454.0,"y":358.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":-0.1875,"y":0.298828125},"r_angle":0.0,"s_scale":{"x":0.27812498807907107,"y":0.15000000596046449},"t_speed":{"x":0.0,"y":0.0},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":1,"gapX":0.0,"loopY":1,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0],"inputType":[1,3,0],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":1039,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","c0544044_0353_4612_b997_7d538aea6ebc","b4d02c7e_a559_4d99_84fe_5b3b560a4af3","41c415a1_de67_4452_a27e_d8b210d7f019","2a26c9e3_ba73_407c_a9b2_06ba459afa1c","1cee45cb_5509_4e95_a01d_11b915a6cb66","5817aa94_f9c1_422e_801d_19e9dbcbe9f8","669652b9_4a45_4a77_89ce_08edd03a9c3d","76f84b7a_5473_4cdc_b2e6_5e2aac6d2115","623110b0_8125_46fa_82db_cff30d5e8b44","58ada36f_41b9_4a3c_9d4e_4a585c9a59f7","cd010c71_c046_4f66_a68d_5cc4b0387e8a","98090fd0_1717_4249_b7a1_38a13877e7b9"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"5817aa94_f9c1_422e_801d_19e9dbcbe9f8","name":"mask7","depth":1,"type":1,"parentPortNumber":1,"parent":["aa69aaff_298d_485b_a5eb_501fbbb16667"],"parentPort":[0],"childPortNumber":1,"children":["1cee45cb_5509_4e95_a01d_11b915a6cb66"],"childrenPort":[0],"textureExGUID":"","textureGUID":"81155dd81ebf3c249aa5e62ee3ab96cb","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":285.0,"y":356.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":0.0,"y":0.0},"r_angle":0.0,"s_scale":{"x":1.0,"y":1.0},"t_speed":{"x":0.0,"y":0.0},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":1,"outputType":[0,1,3],"inputType":[0,1,3],"dirty":false,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":79,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","c0544044_0353_4612_b997_7d538aea6ebc","b4d02c7e_a559_4d99_84fe_5b3b560a4af3","41c415a1_de67_4452_a27e_d8b210d7f019","2a26c9e3_ba73_407c_a9b2_06ba459afa1c","1cee45cb_5509_4e95_a01d_11b915a6cb66","aa69aaff_298d_485b_a5eb_501fbbb16667","669652b9_4a45_4a77_89ce_08edd03a9c3d","76f84b7a_5473_4cdc_b2e6_5e2aac6d2115","c21d1386_b394_4078_8fda_2cb050bd8514","623110b0_8125_46fa_82db_cff30d5e8b44","58ada36f_41b9_4a3c_9d4e_4a585c9a59f7"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"669652b9_4a45_4a77_89ce_08edd03a9c3d","name":"image8","depth":11,"type":13,"parentPortNumber":1,"parent":["2a26c9e3_ba73_407c_a9b2_06ba459afa1c"],"parentPort":[0],"childPortNumber":1,"children":[],"childrenPort":[],"textureExGUID":"","textureGUID":"7f8256eb3b3def74ab2c2538bc660d16","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":455.0,"y":515.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":0.003906369209289551,"y":0.20703125},"r_angle":0.9930525422096252,"s_scale":{"x":0.4399999976158142,"y":0.800000011920929},"t_speed":{"x":0.0,"y":0.0},"r_speed":-2.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":1,"gapX":0.0,"loopY":1,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":3,"param":"(abs(_Time.y%0.4 - 0.2)*5)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0],"inputType":[1,3,0],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":23,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","c0544044_0353_4612_b997_7d538aea6ebc","b4d02c7e_a559_4d99_84fe_5b3b560a4af3","41c415a1_de67_4452_a27e_d8b210d7f019","2a26c9e3_ba73_407c_a9b2_06ba459afa1c","1cee45cb_5509_4e95_a01d_11b915a6cb66","aa69aaff_298d_485b_a5eb_501fbbb16667","5817aa94_f9c1_422e_801d_19e9dbcbe9f8","76f84b7a_5473_4cdc_b2e6_5e2aac6d2115","623110b0_8125_46fa_82db_cff30d5e8b44","58ada36f_41b9_4a3c_9d4e_4a585c9a59f7","cd010c71_c046_4f66_a68d_5cc4b0387e8a","98090fd0_1717_4249_b7a1_38a13877e7b9"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"76f84b7a_5473_4cdc_b2e6_5e2aac6d2115","name":"uv9","depth":1,"type":4,"parentPortNumber":1,"parent":["41c415a1_de67_4452_a27e_d8b210d7f019"],"parentPort":[0],"childPortNumber":1,"children":[],"childrenPort":[],"textureExGUID":"","textureGUID":"fa3108da2fe38a748bfce58b4c9b5410","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":454.0,"y":29.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":0.0,"y":0.0},"r_angle":0.0,"s_scale":{"x":1.0,"y":1.0},"t_speed":{"x":0.0,"y":0.443359375},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.009999999776482582},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[1],"inputType":[1],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":15,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","c0544044_0353_4612_b997_7d538aea6ebc","b4d02c7e_a559_4d99_84fe_5b3b560a4af3","41c415a1_de67_4452_a27e_d8b210d7f019","2a26c9e3_ba73_407c_a9b2_06ba459afa1c","1cee45cb_5509_4e95_a01d_11b915a6cb66","aa69aaff_298d_485b_a5eb_501fbbb16667","5817aa94_f9c1_422e_801d_19e9dbcbe9f8","669652b9_4a45_4a77_89ce_08edd03a9c3d"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"623110b0_8125_46fa_82db_cff30d5e8b44","name":"image11","depth":20,"type":13,"parentPortNumber":1,"parent":["2a26c9e3_ba73_407c_a9b2_06ba459afa1c"],"parentPort":[0],"childPortNumber":1,"children":[],"childrenPort":[],"textureExGUID":"","textureGUID":"a4015787bde2e1f40a6ef6875a85e45c","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":457.0,"y":680.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":1.1920928955078126e-7,"y":-0.0000025033950805664064},"r_angle":-2.090440511703491,"s_scale":{"x":0.6032040119171143,"y":3.047151565551758},"t_speed":{"x":-0.7639935612678528,"y":0.5332266092300415},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":0.6176470518112183,"b":0.6176470518112183,"a":1.0},"op":3,"param":"(0.2)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0],"inputType":[1,3,0],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":31,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","c0544044_0353_4612_b997_7d538aea6ebc","b4d02c7e_a559_4d99_84fe_5b3b560a4af3","41c415a1_de67_4452_a27e_d8b210d7f019","2a26c9e3_ba73_407c_a9b2_06ba459afa1c","1cee45cb_5509_4e95_a01d_11b915a6cb66","aa69aaff_298d_485b_a5eb_501fbbb16667","5817aa94_f9c1_422e_801d_19e9dbcbe9f8","669652b9_4a45_4a77_89ce_08edd03a9c3d","76f84b7a_5473_4cdc_b2e6_5e2aac6d2115","c21d1386_b394_4078_8fda_2cb050bd8514"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"58ada36f_41b9_4a3c_9d4e_4a585c9a59f7","name":"image12","depth":4,"type":13,"parentPortNumber":1,"parent":["41c415a1_de67_4452_a27e_d8b210d7f019"],"parentPort":[0],"childPortNumber":1,"children":["98090fd0_1717_4249_b7a1_38a13877e7b9"],"childrenPort":[0],"textureExGUID":"","textureGUID":"cb6e677abba83ee4699dbc9751b3e65b","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":451.0,"y":177.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":-0.15625,"y":0.310546875},"r_angle":0.0,"s_scale":{"x":0.20000000298023225,"y":0.15000000596046449},"t_speed":{"x":0.0,"y":0.0},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":1,"gapX":0.0,"loopY":1,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[0],"inputType":[1,3,0],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":79,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","c0544044_0353_4612_b997_7d538aea6ebc","b4d02c7e_a559_4d99_84fe_5b3b560a4af3","41c415a1_de67_4452_a27e_d8b210d7f019","2a26c9e3_ba73_407c_a9b2_06ba459afa1c","1cee45cb_5509_4e95_a01d_11b915a6cb66","aa69aaff_298d_485b_a5eb_501fbbb16667","5817aa94_f9c1_422e_801d_19e9dbcbe9f8","669652b9_4a45_4a77_89ce_08edd03a9c3d","76f84b7a_5473_4cdc_b2e6_5e2aac6d2115","623110b0_8125_46fa_82db_cff30d5e8b44","cd010c71_c046_4f66_a68d_5cc4b0387e8a","98090fd0_1717_4249_b7a1_38a13877e7b9"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"cd010c71_c046_4f66_a68d_5cc4b0387e8a","name":"uv5copy","depth":1,"type":4,"parentPortNumber":1,"parent":["98090fd0_1717_4249_b7a1_38a13877e7b9"],"parentPort":[0],"childPortNumber":1,"children":[],"childrenPort":[],"textureExGUID":"","textureGUID":"fa3108da2fe38a748bfce58b4c9b5410","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":112.0,"y":177.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":2.384185791015625e-7,"y":-1.1920928955078126e-7},"r_angle":0.0,"s_scale":{"x":2.0,"y":2.0},"t_speed":{"x":-0.4843752384185791,"y":0.14453136920928956},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"(0.5+_Time.y)","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(3)","amountR":{"x":0.0312497615814209,"y":0.03710949420928955},"amountG":{"x":-0.013672113418579102,"y":0.01757824420928955},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":0,"outputType":[1],"inputType":[1],"dirty":true,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":14351,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","c0544044_0353_4612_b997_7d538aea6ebc","b4d02c7e_a559_4d99_84fe_5b3b560a4af3","41c415a1_de67_4452_a27e_d8b210d7f019","2a26c9e3_ba73_407c_a9b2_06ba459afa1c","1cee45cb_5509_4e95_a01d_11b915a6cb66","aa69aaff_298d_485b_a5eb_501fbbb16667","5817aa94_f9c1_422e_801d_19e9dbcbe9f8","669652b9_4a45_4a77_89ce_08edd03a9c3d","76f84b7a_5473_4cdc_b2e6_5e2aac6d2115","623110b0_8125_46fa_82db_cff30d5e8b44","58ada36f_41b9_4a3c_9d4e_4a585c9a59f7","98090fd0_1717_4249_b7a1_38a13877e7b9"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1},{"useNormal":false,"id":"98090fd0_1717_4249_b7a1_38a13877e7b9","name":"mask14","depth":1,"type":1,"parentPortNumber":1,"parent":["58ada36f_41b9_4a3c_9d4e_4a585c9a59f7"],"parentPort":[0],"childPortNumber":1,"children":["cd010c71_c046_4f66_a68d_5cc4b0387e8a"],"childrenPort":[0],"textureExGUID":"","textureGUID":"81155dd81ebf3c249aa5e62ee3ab96cb","useGray":false,"useCustomTexture":false,"textureGUIDGray":"","spriteGUID":"","spriteName":"","rect":{"serializedVersion":"2","x":280.0,"y":176.0,"width":100.0,"height":130.0},"effectData":{"t_startMove":{"x":0.0,"y":0.0},"r_angle":0.0,"s_scale":{"x":1.0,"y":1.0},"t_speed":{"x":0.0,"y":0.0},"r_speed":0.0,"s_speed":{"x":0.0,"y":0.0},"t_Param":"_Time.y","r_Param":"_Time.y","s_Param":"_Time.y","pop_final":false,"pop_min":0.0,"pop_max":1.0,"pop_startValue":0.0,"pop_speed":0.0,"pop_Param":"(1)","pop_channel":3,"useLoop":false,"loopX":0,"gapX":0.0,"loopY":0,"gapY":0.0,"animationSheetUse":false,"animationSheetCountX":1,"animationSheetCountY":1,"animationSheetLoop":true,"animationSheetSingleRow":false,"animationSheetSingleRowIndex":0,"animationSheetStartFrame":0,"animationSheetFrameFactor":"_Time.y"},"effectDataColor":{"hdr":false,"color":{"r":1.0,"g":1.0,"b":1.0,"a":1.0},"op":0,"param":"(1)"},"effectDataUV":{"op":0,"param":"(1)","amountR":{"x":0.0,"y":0.0},"amountG":{"x":0.0,"y":0.0},"amountB":{"x":0.0,"y":0.0},"amountA":{"x":0.0,"y":0.0}},"maskChannel":2,"outputType":[0,1,3],"inputType":[0,1,3],"dirty":false,"remap":{"x":0.0,"y":0.05000000074505806},"layerMask":{"mask":4111,"strs":["0ce4b059_1bb9_4f7a_b568_ea496290c0f4","c0544044_0353_4612_b997_7d538aea6ebc","b4d02c7e_a559_4d99_84fe_5b3b560a4af3","41c415a1_de67_4452_a27e_d8b210d7f019","2a26c9e3_ba73_407c_a9b2_06ba459afa1c","1cee45cb_5509_4e95_a01d_11b915a6cb66","aa69aaff_298d_485b_a5eb_501fbbb16667","5817aa94_f9c1_422e_801d_19e9dbcbe9f8","669652b9_4a45_4a77_89ce_08edd03a9c3d","76f84b7a_5473_4cdc_b2e6_5e2aac6d2115","c21d1386_b394_4078_8fda_2cb050bd8514","623110b0_8125_46fa_82db_cff30d5e8b44","58ada36f_41b9_4a3c_9d4e_4a585c9a59f7","cd010c71_c046_4f66_a68d_5cc4b0387e8a"]},"blurX":0.0,"blurY":0.0,"blurXParam":"(1)","blurYParam":"(1)","retro":0.0,"retroParam":"(1)","gradients":[],"tst":0.0,"tstParam":"(1)","tstRad":0.0,"tstRadParam":"(1)","tstPos":{"x":0.0,"y":0.0},"pinch":0.0,"pinchParam":"(1)","fishEye":0.0,"fishEyeParam":"(1)","codeType":0,"code":"","codeParams":[],"coordMode":0,"reso":1}],"clipValue":0.5,"fallback":"Standard","sn":"","pum":true,"ps":1.0,"psm":2.0}
Shader "Shader Weaver/ninja"{   
	Properties {   
		_Color ("Color", Color) = (1,1,1,1)
		_Color_ROOT ("Color ROOT", Color) = (1,1,1,1)
		_Color_image2 ("Color image2", Color) = (1,1,1,1)
		_Color_image3 ("Color image3", Color) = (1,1,1,1)
		_Color_image4 ("Color image4", Color) = (1,1,1,1)
		_Color_image6 ("Color image6", Color) = (1,1,1,1)
		_Color_image8 ("Color image8", Color) = (1,1,1,1)
		_Color_image11 ("Color image11", Color) = (1,0.6176471,0.6176471,1)
		_Color_image12 ("Color image12", Color) = (1,1,1,1)
		_MainTex ("_MainTex", 2D) = "white" { }
		_ninja_mask0 ("_ninja_mask0", 2D) = "white" { }
		_ninjaBg ("_ninjaBg", 2D) = "white" { }
		_ninja ("_ninja", 2D) = "white" { }
		_ninjaWeapon ("_ninjaWeapon", 2D) = "white" { }
		_wave ("_wave", 2D) = "white" { }
		_ninja2 ("_ninja2", 2D) = "white" { }
		_shockwave ("_shockwave", 2D) = "white" { }
		_dots ("_dots", 2D) = "white" { }
		_ninja3 ("_ninja3", 2D) = "white" { }
	}   
	SubShader {   
		Tags {
			"Queue"="Transparent"
			"RenderType"="Transparent"
		}
		pass {   
			Blend SrcAlpha  OneMinusSrcAlpha   
			CGPROGRAM  
			#pragma vertex vert   
			#pragma fragment frag   
			#include "UnityCG.cginc"   
			fixed4 _Color;
			float4 _Color_ROOT;
			float4 _Color_image2;
			float4 _Color_image3;
			float4 _Color_image4;
			float4 _Color_image6;
			float4 _Color_image8;
			float4 _Color_image11;
			float4 _Color_image12;
			float4 _MainTex_ST;
			sampler2D _MainTex;   
			sampler2D _ninja_mask0;   
			sampler2D _ninjaBg;   
			sampler2D _ninja;   
			sampler2D _ninjaWeapon;   
			sampler2D _wave;   
			sampler2D _ninja2;   
			sampler2D _shockwave;   
			sampler2D _dots;   
			sampler2D _ninja3;   
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
				OUT.pos = UnityObjectToClipPos(IN.vertex);
				OUT.color = IN.color * _Color;
				OUT._uv_MainTex = TRANSFORM_TEX(IN.texcoord,_MainTex);
				OUT._uv_STD = TRANSFORM_TEX(IN.texcoord,_MainTex);  
				return OUT;
			}   
			float4 frag (v2f i) : COLOR {
				float4 color_ninja_mask0 = tex2D(_ninja_mask0,i._uv_STD);    
				float4 result = float4(0,0,0,0);
				float4 result2 = float4(0,0,0,0);
				float3 result3 = float3(0,0,0);
				float minA = 0;


				//====================================
				//============ uv5 ============   
				float2  uv_uv5 = i._uv_STD;
				float2 center_uv5 = float2(0.5,0.5);    
				uv_uv5 = uv_uv5-center_uv5;    
				uv_uv5 = uv_uv5+fixed2(-2.384186E-07,1.192093E-07);    
				uv_uv5 = uv_uv5+fixed2(0.4765627,-0.1425782)*(_Time.y);    
				uv_uv5 = UV_RotateAround(fixed2(0,0),uv_uv5,0);    
				uv_uv5 = uv_uv5/fixed2(2,2);    
				float2 dir_uv5 = uv_uv5/length(uv_uv5);    
				uv_uv5 = uv_uv5-dir_uv5*fixed2(0,0)*(_Time.y);    
				uv_uv5 = UV_RotateAround(fixed2(0,0),uv_uv5,0*(_Time.y));    
				uv_uv5 = uv_uv5+center_uv5;    
				float4 rect_uv5 =  float4(1,1,1,1);
				float4 color_uv5 = tex2D(_wave,uv_uv5);    
				uv_uv5 = -(color_uv5.r*fixed2(-0.02929711,-0.03124988) + color_uv5.g*fixed2(-0.02539086,0.005859494) + color_uv5.b*fixed2(0,0) +  color_uv5.a*fixed2(0,0));    


				//====================================
				//============ image6 ============   
				float2  uv_image6 = i._uv_STD;
				float2 center_image6 = float2(0.5,0.5);    
				uv_image6 = uv_image6-center_image6;    
				uv_image6 = uv_image6+fixed2(0.1875,-0.2988281);    
				uv_image6 = uv_image6+fixed2(0,0)*(_Time.y);    
				uv_image6 = UV_RotateAround(fixed2(0,0),uv_image6,0);    
				uv_image6 = uv_image6/fixed2(0.278125,0.15);    
				float2 dir_image6 = uv_image6/length(uv_image6);    
				uv_image6 = uv_image6-dir_image6*fixed2(0,0)*(_Time.y);    
				uv_image6 = UV_RotateAround(fixed2(0,0),uv_image6,0*(_Time.y));    
				uv_image6 = uv_image6+center_image6;    
				uv_image6 = uv_image6 + uv_uv5*1*((3))*color_ninja_mask0.g;
				float4 rect_image6 =  float4(1,1,1,1);
				float4 color_image6 = tex2D(_ninja2,uv_image6);    
				color_image6 = color_image6*_Color_image6;


				//====================================
				//============ uv9 ============   
				float2  uv_uv9 = i._uv_STD;
				float2 center_uv9 = float2(0.5,0.5);    
				uv_uv9 = uv_uv9-center_uv9;    
				uv_uv9 = uv_uv9+fixed2(0,0);    
				uv_uv9 = uv_uv9+fixed2(0,-0.4433594)*(_Time.y);    
				uv_uv9 = UV_RotateAround(fixed2(0,0),uv_uv9,0);    
				uv_uv9 = uv_uv9/fixed2(1,1);    
				float2 dir_uv9 = uv_uv9/length(uv_uv9);    
				uv_uv9 = uv_uv9-dir_uv9*fixed2(0,0)*(_Time.y);    
				uv_uv9 = UV_RotateAround(fixed2(0,0),uv_uv9,0*(_Time.y));    
				uv_uv9 = uv_uv9+center_uv9;    
				float4 rect_uv9 =  float4(1,1,1,1);
				float4 color_uv9 = tex2D(_wave,uv_uv9);    
				uv_uv9 = -(color_uv9.r*fixed2(0,0.01) + color_uv9.g*fixed2(0,0) + color_uv9.b*fixed2(0,0) +  color_uv9.a*fixed2(0,0));    


				//====================================
				//============ uv5copy ============   
				float2  uv_uv5copy = i._uv_STD;
				float2 center_uv5copy = float2(0.5,0.5);    
				uv_uv5copy = uv_uv5copy-center_uv5copy;    
				uv_uv5copy = uv_uv5copy+fixed2(-2.384186E-07,1.192093E-07);    
				uv_uv5copy = uv_uv5copy+fixed2(0.4843752,-0.1445314)*((0.5+_Time.y));    
				uv_uv5copy = UV_RotateAround(fixed2(0,0),uv_uv5copy,0);    
				uv_uv5copy = uv_uv5copy/fixed2(2,2);    
				float2 dir_uv5copy = uv_uv5copy/length(uv_uv5copy);    
				uv_uv5copy = uv_uv5copy-dir_uv5copy*fixed2(0,0)*(_Time.y);    
				uv_uv5copy = UV_RotateAround(fixed2(0,0),uv_uv5copy,0*(_Time.y));    
				uv_uv5copy = uv_uv5copy+center_uv5copy;    
				float4 rect_uv5copy =  float4(1,1,1,1);
				float4 color_uv5copy = tex2D(_wave,uv_uv5copy);    
				uv_uv5copy = -(color_uv5copy.r*fixed2(0.03124976,0.03710949) + color_uv5copy.g*fixed2(-0.01367211,0.01757824) + color_uv5copy.b*fixed2(0,0) +  color_uv5copy.a*fixed2(0,0));    


				//====================================
				//============ image12 ============   
				float2  uv_image12 = i._uv_STD;
				float2 center_image12 = float2(0.5,0.5);    
				uv_image12 = uv_image12-center_image12;    
				uv_image12 = uv_image12+fixed2(0.15625,-0.3105469);    
				uv_image12 = uv_image12+fixed2(0,0)*(_Time.y);    
				uv_image12 = UV_RotateAround(fixed2(0,0),uv_image12,0);    
				uv_image12 = uv_image12/fixed2(0.2,0.15);    
				float2 dir_image12 = uv_image12/length(uv_image12);    
				uv_image12 = uv_image12-dir_image12*fixed2(0,0)*(_Time.y);    
				uv_image12 = UV_RotateAround(fixed2(0,0),uv_image12,0*(_Time.y));    
				uv_image12 = uv_image12+center_image12;    
				uv_image12 = uv_image12 + uv_uv5copy*1*((3))*color_ninja_mask0.b;
				float4 rect_image12 =  float4(1,1,1,1);
				float4 color_image12 = tex2D(_ninja3,uv_image12);    
				color_image12 = color_image12*_Color_image12;


				//====================================
				//============ image3 ============   
				float2  uv_image3 = i._uv_STD;
				float2 center_image3 = float2(0.5,0.5);    
				uv_image3 = uv_image3-center_image3;    
				uv_image3 = uv_image3+fixed2(0.1386719,-0.1289063);    
				uv_image3 = uv_image3+fixed2(0,0)*(_Time.y);    
				uv_image3 = UV_RotateAround(fixed2(0,0),uv_image3,0.019958);    
				uv_image3 = uv_image3/fixed2(0.8427831,0.7);    
				float2 dir_image3 = uv_image3/length(uv_image3);    
				uv_image3 = uv_image3-dir_image3*fixed2(0,0)*(_Time.y);    
				uv_image3 = UV_RotateAround(fixed2(0,0),uv_image3,0*(_Time.y));    
				uv_image3 = uv_image3+center_image3;    
				uv_image3 = uv_image3 + uv_uv9*1*((1));
				float4 rect_image3 =  float4(1,1,1,1);
				float4 color_image3 = tex2D(_ninja,uv_image3);    
				color_image3 = color_image3*_Color_image3;


				//====================================
				//============ image8 ============   
				float2  uv_image8 = i._uv_STD;
				float2 center_image8 = float2(0.5,0.5);    
				uv_image8 = uv_image8-center_image8;    
				uv_image8 = uv_image8+fixed2(-0.003906369,-0.2070313);    
				uv_image8 = uv_image8+fixed2(0,0)*(_Time.y);    
				uv_image8 = UV_RotateAround(fixed2(0,0),uv_image8,0.9930525);    
				uv_image8 = uv_image8/fixed2(0.44,0.8);    
				float2 dir_image8 = uv_image8/length(uv_image8);    
				uv_image8 = uv_image8-dir_image8*fixed2(0,0)*(_Time.y);    
				uv_image8 = UV_RotateAround(fixed2(0,0),uv_image8,-2*(_Time.y));    
				uv_image8 = uv_image8+center_image8;    
				float4 rect_image8 =  float4(1,1,1,1);
				float4 color_image8 = tex2D(_shockwave,uv_image8);    
				color_image8 = color_image8*_Color_image8;


				//====================================
				//============ image11 ============   
				float2  uv_image11 = i._uv_STD;
				float2 center_image11 = float2(0.5,0.5);    
				uv_image11 = uv_image11-center_image11;    
				uv_image11 = uv_image11+fixed2(-1.192093E-07,2.503395E-06);    
				uv_image11 = uv_image11+fixed2(0.7639936,-0.5332266)*(_Time.y);    
				uv_image11 = UV_RotateAround(fixed2(0,0),uv_image11,-2.090441);    
				uv_image11 = uv_image11/fixed2(0.603204,3.047152);    
				float2 dir_image11 = uv_image11/length(uv_image11);    
				uv_image11 = uv_image11-dir_image11*fixed2(0,0)*(_Time.y);    
				uv_image11 = UV_RotateAround(fixed2(0,0),uv_image11,0*(_Time.y));    
				uv_image11 = uv_image11+center_image11;    
				float4 rect_image11 =  float4(1,1,1,1);
				float4 color_image11 = tex2D(_dots,uv_image11);    
				color_image11 = color_image11*_Color_image11;


				//====================================
				//============ image4 ============   
				float2  uv_image4 = i._uv_STD;
				float2 center_image4 = float2(0.5,0.5);    
				uv_image4 = uv_image4-center_image4;    
				uv_image4 = uv_image4+fixed2(-0.00390625,-0.1992188);    
				uv_image4 = uv_image4+fixed2(0,0)*(_Time.y);    
				uv_image4 = UV_RotateAround(fixed2(0,0),uv_image4,0.9445533);    
				uv_image4 = uv_image4/fixed2(0.22,0.4);    
				float2 dir_image4 = uv_image4/length(uv_image4);    
				uv_image4 = uv_image4-dir_image4*fixed2(0,0)*(_Time.y);    
				uv_image4 = UV_RotateAround(fixed2(0,0),uv_image4,-1*(_Time.y));    
				uv_image4 = uv_image4+center_image4;    
				float4 rect_image4 =  float4(1,1,1,1);
				float4 color_image4 = tex2D(_ninjaWeapon,uv_image4);    
				color_image4 = color_image4*_Color_image4;


				//====================================
				//============ image2 ============   
				float2  uv_image2 = i._uv_STD;
				float2 center_image2 = float2(0.5,0.5);    
				uv_image2 = uv_image2-center_image2;    
				uv_image2 = uv_image2+fixed2(0.015625,-0.2109375);    
				uv_image2 = uv_image2+fixed2(0,0)*(_Time.y);    
				uv_image2 = UV_RotateAround(fixed2(0,0),uv_image2,0);    
				uv_image2 = uv_image2/fixed2(0.7269287,0.3963495);    
				float2 dir_image2 = uv_image2/length(uv_image2);    
				uv_image2 = uv_image2-dir_image2*fixed2(0,0)*(_Time.y);    
				uv_image2 = UV_RotateAround(fixed2(0,0),uv_image2,0*(_Time.y));    
				uv_image2 = uv_image2+center_image2;    
				float4 rect_image2 =  float4(1,1,1,1);
				float4 color_image2 = tex2D(_ninjaBg,uv_image2);    
				color_image2 = color_image2*_Color_image2;


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
				float4 rect_ROOT =  float4(1,1,1,1);
				float4 color_ROOT = tex2D(_MainTex,uv_ROOT);    
				float4 rootTexColor = color_ROOT;
				color_ROOT = color_ROOT*_Color_ROOT;
				result = float4(color_ROOT.rgb,color_ROOT.a*1);
				result = lerp(result,float4(color_image2.rgb,1),clamp(color_image2.a*1*((1))*color_ninja_mask0.r,0,1));    
				result = result+float4(color_image4.rgb*color_image4.a*1*((1))*color_ninja_mask0.r,color_image4.a*1*((1))*color_ninja_mask0.r*(rootTexColor.a - result.a));
				result = lerp(result,float4(color_image12.rgb,1),clamp(color_image12.a*1*((1))*color_ninja_mask0.r,0,1));    
				result = lerp(result,float4(color_image6.rgb,1),clamp(color_image6.a*1*((1))*color_ninja_mask0.r,0,1));    
				result = result+float4(color_image8.rgb*color_image8.a*1*((abs(_Time.y%0.4 - 0.2)*5))*color_ninja_mask0.r,color_image8.a*1*((abs(_Time.y%0.4 - 0.2)*5))*color_ninja_mask0.r*(rootTexColor.a - result.a));
				result = lerp(result,float4(color_image3.rgb,1),clamp(color_image3.a*1*((1))*color_ninja_mask0.r,0,1));    
				result = result+float4(color_image11.rgb*color_image11.a*1*((0.2))*color_ninja_mask0.r,color_image11.a*1*((0.2))*color_ninja_mask0.r*(rootTexColor.a - result.a));
				result = result*i.color;
				clip(result.a - 0.5);
				return result;
			}  
			ENDCG
		}
	}
	fallback "Standard"
}
