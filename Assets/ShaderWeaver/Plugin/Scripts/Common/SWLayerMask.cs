//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[Serializable]
	public class SWLayerMask
	{
		public int mask;
		protected bool Has(int digit)
		{
			return (Get (digit)) == 1;
		}
		protected int Get(int digit)
		{
			return (mask >> digit)&1;
		}

		protected void Set(int digit,bool on)
		{
			if (on) {
				mask = mask | 1 << digit;
			} else {
			//	mask = mask & 1 << digit;
			}
		}

		protected void Remove(int digit)
		{
			int left = mask >> 1 & (-1 << digit);
			int right = mask & (-1 >> (31-digit));
			mask = left | right;
		}
	}

	/// <summary>
	/// Select nodes to show
	/// </summary>
	[Serializable]
	public class SWLayerMaskString:SWLayerMask
	{
		public List<string> strs = new List<string> ();
		public void AddKey(string key)
		{
			if(!strs.Contains(key))
				strs.Add (key);
		}
		public void RemoveKey(string key)
		{
			int digit = strs.IndexOf (key);
			Remove (digit);
			strs.Remove (key);
		}

		public bool Has(string key)
		{
			int digit = strs.IndexOf (key);
			return Has (digit);
		}
		public void Set(string key,bool on)
		{
			int digit = strs.IndexOf (key);
			Set (digit, on);
		}
		public void Clear()
		{
			strs.Clear ();
			mask = 0;
		}
	}
}