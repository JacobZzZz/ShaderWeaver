-------------------------------------------------------
        Shader Weaver - Easy and funny to create
             Copyright Reserved by Jackie Lo
                    Version 1.3.4
-------------------------------------------------------

Thank you for purchasing Shader Weaver!

If you have any questions, suggestions, comments or feature requests,
please email JackieLo@aliyun.com


---------------------------------------
 Support, documentation, and tutorials
---------------------------------------
Home:
www.shaderweaver.com

Tutorials:
www.shaderweaver.com/tutorials.html


--------------------
Description
--------------------
Shader Weaver is a node-based shader creation tool for Unity 2D,
giving you the artistic freedom to enhance Sprites/UI in a visual and intuitive way.
Distinctive nodes and workflow makes it easy to create impressive 2d effects and save huge time.
Use handles/gizmos to make effects rather than input tedious numbers.
Draw masks and create remap textures with convenient tools inside Shader Weaver.Graphics tablet is supported.
Support Unity 5 and 2017.


--------------------
Features
--------------------
-Growing Samples
A pack of sample effects including shaders and textures to study and use freely.

-Intuitive interface 
Clean and intuitive user interface. 

-Mask Texture Creation
Draw masks to divide areas for individual sub-effects.

-UV Distortion
A visual way to distort uv coordinates.

-UV Remapping
A unique way to make path along effects and object surrounding effects.

-Simple Operation
Use handles/gizmos like what you are used to do.

-Preview
Nice width/height corresponding preview.

-Hot keys
Comfortable hot keys speed up editing and drawing.

-Undo/Redo
Fully support Undo/Redo on nodes,numbers and textures.

-Play Mode
Edit and update in play mode.

-Copy Paste
Support copy and paste. Reuse nodes from other Shader Weaver project.

-Depth
Depth Sorting.

-Visual Modes
View textures' individual rgba channel and choose what to see by setting layermask.

-Sync
No extra files to sync over version control system. All Shader Weaver data are stored in .shader files.  


--------------------
 Quick Start
--------------------
(1)Open editor		: Window -> Shader Weaver
(2)Place nodes 		: Drag nodes from left dock to main canvas
(3)Connect nodes	: Data flows from left to right, drag wires from node ports to create connections.
(4)Edit				: Edit (assign textures, draw masks, create remap texture...)
(5)Save				: Click Save button on the top dock,and outcome will show up on the top-right corner.

--------------------
For NGUI
--------------------
(1)Create NGUI's 'UI2D Sprite' 	: NGUI -> Create -> Unity 2D Sprite		
(2)Create Shader Weaver project and set Shader Type to 'UI2D Sprite(NGUI)'. Save after editing.
(3)Assign the material to 'UI2D Sprite'.
Done! Both Soft Clip and Texture Mask are supported. 
When main shader is created, extra shaders are also created under the same folder to handle NGUI clipping.
Make sure all shaders are included in the build. For example, you can simply save your project under Resources folder. 

--------------------
 Spaces
--------------------
Effect Space: 
The whole effect space, showing as yellow square wireframe in editing window.

Node Space: 
Node's texture coordinate space

Screen Space:
The whole screen. Refract node and reflect node are under screen space. 
For UIText, root node is in font atlas space.For all other nodes, effect space is Screen Space. 

--------------------
 Intro to nodes
--------------------
There are 4 kinds of nodes in Shader Weaver currently.
Blue nodes output color.
Orange nodes output UV.
Red nodes output alpha.
Green nodes are nodes used for blending and code

UV node and alpha node behave in effect space independently, if you want them to follow a node like color node,just set same position/rotation/scale/move...

------------
Blue nodes
------------
Root:	
This is the main node.Assign base texture here.

Color:
A solid color.

Image:
Show textures. Depth is for display order, highest depth is shown on the top.

Dummy:
A copy of Root node. 

Refract:
Make refraction effects.Interact with background graphics behind.Use child UV nodes to distort background.
Here are some example effects, ground glass, water fall.Support all shader types.
Be aware that only camera with 'Skybox' clear flags has default background graphics.

Reflect:
Make reflection effects.Interact with background graphics behind.Use child UV nodes to distort background.
Here are some example effects, water, wet floor.
For Sprite, assign material and add ->Component ->Shader Weaver ->Sprite Reflection.
For Default/UI/Text, set material's Reflection Line /  Reflection Height manually.
Be aware that only camera with 'Skybox' clear flags has default background graphics.

------------
Orange nodes
------------
UV:
Use rgba channel of this node'texture to relocate parent's uv coordinates. Use mainly for distortion.Here are some example effects,
floating flags,flowing water. 

Coord:
Offer original UV coordinates. Usually, it is used by Code Node.
Coord has 2 sources:
(1)'Sprite' offers coordinates for sampling sprite texture.
(2)'Normal' offers coordinates for sampling other textures.

Remap:
Supply UV coordinates to the parent node. Drag mode is to lay parental effects on one side of the shape.
Line mode is to make parental effects follow the path you created. In the texture we made,
Red supply  the horizontal coordinates to parent nodes,Green supply  the vertical coordinates.
Axis x:R[0,1]=U[0,1]	
Axis y:G[0,1]=V[0,1]
If you want to use custom texture for remapping,just press '+' button on the remap node and assign texture.
Sometimes choose a lower size for remap texture will make it look smoother. 128x128 and 256x256 are recommended.

Blur:
Make parent nodes blurry.

Retro:
Pixelate parent nodes.

------------
Red nodes
------------
Alpha:
Use one of the rgba channels in this node's texture to do detailed alpha animation for parent nodes.
It offers a multiplier to (1)color node which use it (2)final graphic.
multiplier = textureSampledColor.channel + start +spd*spdFactor  (clamped in [min,max])

------------
Green nodes
------------
Mask:
Draw mask to masking sub-effects.Mask node's child node effects will ONLY show in the masked area.
If you want to use custom grayscale texture for masking,just press '+' button on the mask node and assign texture.

Mixer:
A mixer node has a source node(left) and some child nodes(bottom).
Source node offers alpha for blending. Source node can be any node who outputs color or alpha.
Child nodes' output are blended by mixer node.Left click on mixer node's child port, then we can set blend area in [min,max]. 
Blend area controls how child nodes is blended.

Code:
Use templates or write code. Once a code node is selected, it's settings are in the right panel.
Codes are stored in 'ShaderWeaver/Codes' folder with '.swcode' extension.
(1) How to save: codes will be automaticly saved when a project is saved or updated.
(2) How to import/export: copy '.swcode' files and paste it into the folder.

--------------------
Normal Maps
--------------------
Normal maps are only available when Sprite Light is on.
Steps:
1. Set Shader type to 'Sprite'.
2. Set Sprite Light to a light model rather than 'no'.
3. Set normal map in the window of Image/Dummy node.
Reminder:
1. Normal maps use the same UV of the node texture, so uv effects such as blur and retro will affect normal as well. Looping also works for normal maps.
2. Normal maps can use with mask node and mixer node.
3. How to make blank area? 	Normal map will be multiplied by node texture's alpha, so node texture's transparent area will not affect normal. Or use mask/mixer node.
4. Only use a normal map for Sprite it self? 	Exclude the root node and set the normal map for a dummy node.

--------------------
Q&A
--------------------
How to support Sprite Animation:
Add ->Component ->Shader Weaver ->Sprite Animation to Sprite Render.

How to use Texture Sprite Sheet for nodes:
Open Node Editing Window, press '+' button on the bottom-right corner. Click 'Animation Sheet' toggle.
Set 'Tile X','Tile Y'...
If you wanna use single texture sprite sheet to draw a character multiple times,you need to use Shader Weaver's loop 
rather than use texture's import setting - warp mode 

How to enable HDR color:
1. Edit->Project Settings->Quality  Set Anti Aliasing to Disabled
2. In Camera's inspector, enable HDR
3. Set color attribute in Shader Weaver to HDR by right clicking

How to eliminate seams on boarder:
In texture settings,turn off 'Generate Mip Maps' or set 'Filter Mode' to 'Point'. 

Sometimes there are errors when compiling on mac,why?
Unity has its own built-in variables, so be careful of parameter naming.
It is highly recommended that name your parameter beginning with '_'.

--------------------
 Hotkeys
--------------------
[All windows]
Drag canvas:				Alt + Left Mouse Button		/	Right Mouse Button	/	Mouse Wheel	
Scale canvas:				Mouse Wheel	
Open project:				Alt + O
Save project:				Alt + S (before saved)
Update project:				Alt + S (after saved)
Copy:						Ctrl + C
Paste:						Ctrl + V
Undo:						Ctrl + Z
Redo:						Ctrl + Y
Tiled background switch:	Right Click on it  

[Main window]
Break connections:	Alt/Ctrl + Left Mouse Click on node port	/	Right Mouse Click on node port
Delete selection:	Delete(Win/Mac)		/	Backspace(Mac)

[Edit windows]
Along x/y axis:		Shift + operation

[Mask window]
Brush/Eraser size:	'[' and ']' 
Opacity:			'-' and '=' 
Tolerance:			'[' and ']' 
Invert: Ctrl + I

[Remap window - Drag]
Deviation:			'[' and ']' 
Delete all:			Delete(Win/Mac) Backspace(Mac)			

[Remap window - Line]
Size:				'[' and ']' 
Delete all:			Delete(Win/Mac) Backspace(Mac)
Move point:			Ctrl + Left Mouse Button
Delete point:		Ctrl + Right Mouse Button
Clear points:		C

-----------------------------
 Texture Import Settings
-----------------------------
The following settings will be set to texture automatically when it is used in Shader Weaver.
Textures:
Read/Write Enable -> true
Generate Mip Map -> false

Normal Maps:
Texture Type -> Normal map

Sprites:
Mesh Type -> Full Rect


-----------------------------
 Compatible Assets
-----------------------------
NGUI:Next-Gen UI
2D Dynamic Lights and Shadows 
2DDL Pro : 2D Dynamic Lights and Shadows
Light2D - GPU Lighting System