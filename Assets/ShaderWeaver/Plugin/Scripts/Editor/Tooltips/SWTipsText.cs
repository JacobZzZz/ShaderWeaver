using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWTipsText{
	public static readonly string Main_Open = "Choose a Shader Weaver Shader to open"; 
	public static readonly string Main_Save = "Save your project"; 
	public static readonly string Main_Update = "Update all modifications and save"; 
	public static readonly string Main_t_Mask = "Draw mask to masking sub-effects"; 
	public static readonly string Main_t_Color = "Show color"; 
	public static readonly string Main_t_Image = "Show textures"; 
	public static readonly string Main_t_UV = "Relocate parent's UV coordinates"; 
	public static readonly string Main_t_Alpha = "Modify parent's Alpha"; 
	public static readonly string Main_t_Remap = "Remap parent's uv coordinate"; 
	public static readonly string Main_t_Blur = "Blur parent nodes"; 
	public static readonly string Main_t_Retro = "Pixelate parent nodes"; 
	public static readonly string Main_t_Refraction = "Refract background"; 
	public static readonly string Main_t_Reflect = "Reflect background"; 

	public static readonly string Main_t_Mixer = "Mix child nodes by spliting source alpha"; 
	public static readonly string Main_t_Coord = "Offer original UV coordinates"; 
	public static readonly string Main_t_Dummy = "A copy of Root node"; 
	public static readonly string Main_t_Code = "Use templates or your own code"; 

	public static readonly string Mask_t_Brush = "Draw masked area"; 
	public static readonly string Mask_t_Eraser = "Erase masked area"; 
	public static readonly string Mask_t_Wand = "Pickup an area"; 
	public static readonly string Mask_t_Dropper = "Select color"; 
	public static readonly string Mask_Size = "Change size"; 
	public static readonly string Mask_Opacity = "Set Opacity"; 
	public static readonly string Mask_Tolerance = "Higher tolerance will cover more area"; 
	public static readonly string Mask_Invert = "Invert masked area"; 
	public static readonly string Mask_TexSize = "Set texture's resolution"; 

	public static readonly string Remap_t_Drag = "Generate remap texture along a direction"; 
	public static readonly string Remap_t_Line = "Generate remap texture by placing way points"; 

	public static readonly string Remap_Drag = "Set direction and amount"; 
	public static readonly string Remap_Deviation = "Set deviation along the direction"; 
	public static readonly string Remap_Precise = "Set quality to high"; 
	public static readonly string Remap_Size = "Set way point size"; 
	public static readonly string Remap_Stitch = "Make a close shape rather a line"; 
	public static readonly string Remap_TexSize = "Set texture's resolution"; 



	public static readonly string Effect_t_Basic = "Set position,rotation,scale"; 
	public static readonly string Effect_t_Move = "Move"; 
	public static readonly string Effect_t_Spin = "Spin"; 
	public static readonly string Effect_t_Radial = "Radial"; 
	public static readonly string Effect_t_UV = "Set UV";

	public static readonly string Effect_Position = "Set texture's default position"; 
	public static readonly string Effect_Rotation = "Rotate around texture center"; 
	public static readonly string Effect_Scale = "Scale texture"; 
	public static readonly string Effect_Move = "Move"; 
	public static readonly string Effect_Spin = "Spin"; 
	public static readonly string Effect_Radial = "Radial"; 
	public static readonly string Effect_R = "R channel"; 
	public static readonly string Effect_G= "G channel"; 
	public static readonly string Effect_B = "B channel"; 
	public static readonly string Effect_A = "A channel"; 
	public static readonly string Effect_BlendFactor = "Blend Factor"; 

	//public static readonly string Right_Clip = "Clip value"; 
	public static readonly string Right_LoopModule= "Loop \n Use loop instead of texture's warp mode"; 
	public static readonly string Right_LoopX= "Loop or not on X"; 
	public static readonly string Right_GapX = "Gap between repeating textures on X"; 
	public static readonly string Right_LoopY = "Loop or not on Y"; 
	public static readonly string Right_GapY = "Gap between repeating textures on Y"; 
	public static readonly string Right_Color = "Color"; 


	public static readonly string Right_AnimationSheetModule= "Animation Sheet \n Animates from left to right,top to bottom."; 
	public static readonly string Right_AnimationSheetTileX= "Defines the tiling of the texture."; 
	public static readonly string Right_AnimationSheetTileY= "Defines the tiling of the texture."; 
	public static readonly string Right_AnimationSheetLoop= "Loop or not"; 
	public static readonly string Right_AnimationSheetSingleRow= "Use single row"; 
	public static readonly string Right_AnimationSheetRow= "The row in the sheet which will be played"; 
	public static readonly string Right_AnimationSheetStartFrame= "Starts on a frame other than 0."; 
	public static readonly string Right_AnimationSheetFactor= "Playing pace."; 

	public static readonly string Right_ImageModule = "Image"; 
	public static readonly string Right_BlendModule = "Blend"; 
	public static readonly string Right_BlendOp = "Blend Operator"; 
	public static readonly string Right_BlendFactor = "Blend Factor"; 
	public static readonly string Right_CustomParam = "Create custom parameters"; 

	#region Sprite Light Normal mapping
	public static readonly string Right_SpriteLight_NormalMapModule = "Normal Map \n Only for sprite lighting."; 
	public static readonly string Right_SpriteLight_NormalMapTex = "Normal map."; 
	public static readonly string Right_SpriteLight_NormalMapImportance = "Use for normal map blending, ignore it when you use single normal map."; 
	public static readonly string Right_SpriteLight_NormalMapFactor = "Factor. Use for normal animation."; 
	#endregion

	public static readonly string Right_AlphaModule = "Alpha \n Use one of the rgba channel in this node'texture to do detailed aplha animation for parent nodes.It offers a multiplier to (1)color node who use it (2)final graphic. multiplier = textureSampledColor.channel + start +spd*spdFactor  (clamped in [min,max])"; 
	public static readonly string Right_AlphaFinal = "On:Apply to final alpha \n Off:Apply to parent nodes who use it"; 
	public static readonly string Right_AlphaMinMax = "Value will be clamped in this range"; 
	public static readonly string Right_AlphaChannel = "Choose a channel for alpha animation"; 
	public static readonly string Right_AlphaStart = "Alpha start value"; 
	public static readonly string Right_AlphaSpeed = "Alpha changing speed"; 
	public static readonly string Right_AlphaSpeedFactor= "Alpha changing speed factor"; 


	public static readonly string Right_MixerModule = "Mixer \n Use one of the rgba channel in this node'texture to do animated blending for child nodes.It produce a value.value = textureSampledColor.channel + start +spd*spdFactor  (clamped in [min,max])Right click on mixer node's left port, then we can set blend area in [min,max]. Blend area controls how child nodes linked to this port is blended."; 
	public static readonly string Right_MixerMinMax = "Value will be clamped in this range"; 
	public static readonly string Right_MixerChannel = "Choose a channel"; 
	public static readonly string Right_MixerStart = "Start value"; 
	public static readonly string Right_MixerSpeed = "Changing speed"; 
	public static readonly string Right_MixerSpeedFactor= "Changing speed factor"; 

	public static readonly string Settings_Module = "Settings";
	public static readonly string Settings_Type = "Default: use for Quad or Plane \n Sprite: use for 2d sprites \n UI: use for UI image \n Text: use for UI text"; 
	public static readonly string Settings_SpriteLight = "Sprite Light Mode";
	public static readonly string Settings_ShaderModel = "Shader Model";

	public static readonly string Settings_ExcludeRoot = "Exclude root(Main texture) or not"; 
	public static readonly string Settings_Blend = "Blend with background \n Lerp: Src OneMinusSrcAlpha \n Add: Src One \n Mul: Multiply"; 
	public static readonly string Settings_Queue = "Background - rendered before any others. \n " +
	                                               "Geometry - this is used for most objects. Opaque geometry uses this queue. \n " +
	                                               "AlphaTest - render alpha-tested objects after all solid ones are drawn. \n " +
	                                               "Transparent - Anything alpha-blended should go here (glass, particle effects). \n " +
												   "Overlay - Anything rendered last should go here (e.g. lens flares).";
	public static readonly string Settings_Clip = "Only render pixels whose alpha is greater than value"; 
	public static readonly string Settings_Fallback = "If shader can not run on the hardware, it will try to use fallback shader"; 
	public static readonly string Settings_RenderType = "RenderType";
	public static readonly string Settings_ShaderName = "Shader name, which is show in material inspector \n If blank, it will be ShaderWeaver/YourProjectName \n Useful for groupping, such as group/subGroup/shaderName";

	public static readonly string Settings_ModulePreview = "Preview";
	public static readonly string Settings_PreviewUpdateOnMouseOver = "Preview Update On Mouse Over \n On: \n Preview will only be updated when mouse is over, which has better editor performance \n Off: \n Preview will be updated all the time.";
	public static readonly string Settings_PreviewSize = "Preview Size";
	public static readonly string Settings_PreviewSizeMouseOver = "Preview Size(Mouse Over)";



	public static readonly string Code_Module = "Code \n Use templates or your own code. Once a code node is selected, it's settings are in the right panel. \n Codes are stored in ShaderWeaver/codes.swcode, which should never be moved or rename. \n Two ways to import codes from other projects: \n (1)Press 'Import' in the right panel and choose .swcode file. \n (2)Copy .swcode from other project and paste it under ShaderWeaver/Codes folder.";
	public static readonly string Code_Category = "Category by output type";
	public static readonly string Code_Code = "Code";
	public static readonly string Code_Duplicate = "Duplicate";
	public static readonly string Code_Delete = "Delete";
	public static readonly string Code_Description = "Describe this code";
	public static readonly string Code_Input = "Input \n Color,UV,Alpha are nodes \n Float,Range can be set by hand";
	public static readonly string Code_Output = "Output";
	public static readonly string Code_Content = "Content \n (1)Contains return: \n Code will be built as a function \n (2)No return: \n Code will be built inside fragment";
}
