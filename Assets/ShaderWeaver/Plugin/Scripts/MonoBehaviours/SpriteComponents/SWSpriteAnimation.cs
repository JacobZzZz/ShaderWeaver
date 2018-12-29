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
	/// Set uv rect for sprite animation.
	/// </summary>
	[AddComponentMenu("Shader Weaver/Sprite Animation")]
	[RequireComponent(typeof(SpriteRenderer))]
	[ExecuteInEditMode]
	public class SWSpriteAnimation : SWSpriteComponent {
		protected override void Awake ()
		{
			base.Awake ();
			sr.sharedMaterial.SetInt ("_useSpriteAnimation", 1);
		} 
		protected override void OnWillRenderObject ()
		{
			base.OnWillRenderObject ();
			Vector4 rect = CalAnimationSheetCurrentRect (sr.sprite);
			sr.sharedMaterial.SetVector ("_AnimatedRect", rect);
		}

		protected Vector4 CalAnimationSheetCurrentRect(Sprite sprite)
		{
			float x = sprite.rect.x / (float)sprite.texture.width;
			float y = sprite.rect.y / (float)sprite.texture.height;
			float width = sprite.rect.width / sprite.texture.width;
			float height = sprite.rect.height / sprite.texture.height;
			return  new Vector4 (x, y, width, height);
		}
	}
}