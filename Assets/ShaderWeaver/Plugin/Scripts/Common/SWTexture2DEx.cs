//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System;

	/// <summary>
	/// A warper for texture2d
	/// </summary>
	[Serializable]
	public class SWTexture2DEx {
		#region get set
		protected Texture2D texture;
		public Texture2D Texture
		{
			get
			{
				if (texture == null) {
					texture = new Texture2D (sf_width, sf_height,sf_format,sf_mipmap,sf_linear);
					texture.SetPixels (sf_colorBuffer);
					texture.Apply();
				}
				return texture;
			}
			set
			{
				CreateFromTex (value);
			}
		}

//		public bool alphaIsTransparency
//		{
//			get { return Texture.alphaIsTransparency;}
//			set{ Texture.alphaIsTransparency = value;}
//		}

		public FilterMode filterMode
		{
			get { return Texture.filterMode;}
			set{ Texture.filterMode = value;}
		}

		public int width
		{
			get { return Texture.width;}
		}

		public int height
		{
			get { return Texture.height;}
		}

		public TextureFormat format
		{
			get { return Texture.format;}
		}


		public bool IsNull
		{
			get { return sf_width == 0; }
		}
		#endregion

		[SerializeField]
		protected int sf_width;
		[SerializeField]
		protected int sf_height;
		[SerializeField]
		TextureFormat sf_format;
		[SerializeField]
		protected bool sf_mipmap;
		[SerializeField]
		protected bool sf_linear;
		[SerializeField]
		protected Color[] sf_colorBuffer;


		public SWTexture2DEx(int _width,int _height,TextureFormat _format = TextureFormat.ARGB32,bool _mipmap = false,bool _linear = true)
		{
			sf_width = _width;
			sf_height = _height;
			sf_format = _format;
			sf_mipmap = _mipmap;
			sf_linear = _linear;

			texture = new Texture2D (sf_width, sf_height,sf_format,sf_mipmap,sf_linear);
			sf_colorBuffer = texture.GetPixels ();
		}
		public SWTexture2DEx(Texture2D tex)
		{
			CreateFromTex (tex);
		}

		protected void CreateFromTex(Texture2D tex)
		{
			sf_width = tex.width;
			sf_height = tex.height;
			sf_format = tex.format;
			sf_mipmap = false;
			sf_linear = true;

			texture = tex;
			sf_colorBuffer = texture.GetPixels ();
		}



		public void SetPixels(Color[] cs)
		{
			sf_colorBuffer = cs;
			Texture.SetPixels (cs);
		}

		public Color[] GetPixels()
		{
			return Texture.GetPixels ();
		}

		public void Apply()
		{
			Texture.Apply ();
		}

		public byte[] EncodeToPNG()
		{
			return Texture.EncodeToPNG ();
		}

		public byte[] EncodeToJPG()
		{
			return Texture.EncodeToJPG ();
		}
	}
}