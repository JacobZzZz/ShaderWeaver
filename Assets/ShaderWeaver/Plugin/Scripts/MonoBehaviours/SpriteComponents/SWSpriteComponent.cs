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
	/// Base class of SW component
	/// </summary>
	[ExecuteInEditMode]
	public class SWSpriteComponent : MonoBehaviour {
		protected SpriteRenderer sr;
		protected virtual void Awake()
		{
			sr = GetComponent<SpriteRenderer> ();
		}

		protected virtual void OnWillRenderObject()
		{
		}

		protected virtual void Update()
		{
		}
	}
}