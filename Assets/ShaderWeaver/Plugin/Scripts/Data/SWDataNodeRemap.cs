//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using System;



	[Serializable]
	public enum DrawRemapMode
	{
		dir=0, 
		line
	}


	#region dir
	[Serializable]
	public class SWDataNodeRemapDir{
		public Vector2 v = new Vector2 (0f, 0.05f);

		/// <summary>
		/// precise
		/// </summary>
		public bool pre;

		/// <summary>
		/// pixelBack
		/// </summary>
		public int pb;
	}
	#endregion

	#region line
	[Serializable]
	public class RemapWayPointData{
		public Vector2 uv;
	}

	[Serializable]
	public class SWDataNodeRemapLine{
		/// <summary>
		/// stitch
		/// </summary>
		public bool st;

		/// <summary>
		/// brushSize
		/// </summary>
		public int bs = 30;

		public List<RemapWayPointData> pts = new List<RemapWayPointData> ();
	}
	#endregion

	[Serializable]
	public class SWDataNodeRemap{
		public DrawRemapMode mode;
		public SWDataNodeRemapDir d = new SWDataNodeRemapDir();
		public SWDataNodeRemapLine l = new SWDataNodeRemapLine ();
	}
}