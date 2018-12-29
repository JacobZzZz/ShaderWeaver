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
	/// For reflection, calculate reflection bottomline
	/// </summary>
	[AddComponentMenu("Shader Weaver/Sprite Reflection")]
	[RequireComponent(typeof(SpriteRenderer))]
	[ExecuteInEditMode]
	public class SWSpriteReflection : SWSpriteComponent {
		public Camera cam;
		[Range(0.1f,3f)]
		public float height=1;

		protected override void Awake ()
		{
			base.Awake ();
			cam = Camera.main;
		} 

		protected override void Update ()
		{
			base.Update ();
			var screenPoss = SpriteScreenUVs (sr, cam);
			float spHeight = screenPoss [0].y;
			sr.sharedMaterial.SetFloat ("_ReflectionLine", spHeight);
			sr.sharedMaterial.SetFloat ("_ReflectionHeight", height);
		}


		/// <summary>
		/// TL TR BL BR
		/// </summary>
		protected List<Vector2> SpriteScreenUVs(SpriteRenderer sr,Camera cam)
		{
			var bounds = sr.sprite.bounds;
			Vector3 localTL = new Vector3 (-bounds.size.x*0.5f, bounds.size.y*0.5f, 0);
			Vector3 localTR = new Vector3 (bounds.size.x*0.5f, bounds.size.y*0.5f, 0);
			Vector3 localBL = new Vector3 (-bounds.size.x*0.5f, -bounds.size.y*0.5f, 0);
			Vector3 localBR = new Vector3 (bounds.size.x*0.5f, -bounds.size.y*0.5f, 0);
			List<Vector2> vs = new List<Vector2> ();
			vs.Add(Local2ScreenUV(localTL,sr.transform,cam));
			vs.Add(Local2ScreenUV(localTR,sr.transform,cam));
			vs.Add(Local2ScreenUV(localBL,sr.transform,cam));
			vs.Add(Local2ScreenUV(localBR,sr.transform,cam));
			return vs;
		}

		protected Vector2 Local2ScreenUV(Vector3 localPos,Transform tran,Camera cam)
		{
			float screenWidth = Screen.width;
			float screenHeight = Screen.height;
			Vector3 worldPos = tran.localToWorldMatrix.MultiplyPoint (localPos);
			Vector3 screenPos = cam.WorldToScreenPoint (worldPos);
			Vector2 screenUV = new Vector2 (screenPos.x / screenWidth, screenPos.y / screenHeight);
			return screenUV;
		}
	}
}
	