//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine.UI;

	/// <summary>
	/// Attach to the preview prefab
	/// </summary>
	[System.Serializable]
	[ExecuteInEditMode]
	public class SWPreview : MonoBehaviour {
		[SerializeField]
		public Canvas canvas;
		[SerializeField]
		public Camera cam;
		[SerializeField]
		public List<GameObject> objs;
		[SerializeField]
		public MeshRenderer rNormal;
		[SerializeField]
		public SpriteRenderer rSprite;
		[SerializeField]
		public Image uiImage;
		[SerializeField]
		public Text uiText;


		public void Init(Vector3 pos)
		{
			transform.position = pos;
			//cam.backgroundColor = new Color(0,0,0,0);
			cam.backgroundColor = new Color(0.2f,0.2f,0.2f,0.2f);
			cam.enabled = false;
			cam.targetTexture = new RenderTexture (512, 512, 24, RenderTextureFormat.ARGB32);
		}

		public void SetMaterial(Material mat,SWData data,Sprite sp,float mulX,float mulY)
		{
			SWShaderType type = data.shaderType;

			for (int i = 0; i < objs.Count; i++) {
				objs [i].SetActive (false);
			}
			int index = (int)type;
			objs [index].SetActive (true);

			if (type == SWShaderType.normal) {
				rNormal.sharedMaterial = mat;
			}
			else if (type == SWShaderType.ui) {
				uiImage.material = mat;
				uiImage.sprite = sp;
			}
			else if (type == SWShaderType.uiFont) {
				uiText.material = mat;
			}
			else if (type == SWShaderType.sprite) {
				rSprite.sharedMaterial = mat;
				rSprite.sprite = sp;

				if (sp != null) {
					float xUnits = sp.rect.width / data.pixelPerUnit *mulX;
					float yUnits = sp.rect.height / data.pixelPerUnit*mulY;
					rSprite.transform.localScale = new Vector3 (1 / xUnits, 1 / yUnits, 1);
				}
			}
			else if (type == SWShaderType.ngui_ui2dSprite) {
				uiImage.material = mat;
				uiImage.sprite = sp;
			}
		}



		public void Destory()
		{
			
		}

		public void Update()
		{
			for(int i=0;i<canvas.transform.childCount;i++)
			{
				var child = canvas.transform.GetChild (i);
				if (child != uiImage.transform && child != uiText.transform) {
					var cs = FindObjectsOfType<Canvas> ();
					Canvas can = null;
					foreach (var  item in cs) {
						if (item != canvas) {
							can = item;
						}
					}
					if (can == null) {
						GameObject obj = new GameObject ("Canvas");
						can = obj.AddComponent<Canvas> ();
						can.renderMode = RenderMode.ScreenSpaceOverlay;
						obj.AddComponent<CanvasScaler>();
						obj.AddComponent<GraphicRaycaster> ();
					}
					child.parent = can.transform;
					child.GetComponent<RectTransform> ().localPosition = Vector3.zero;
					var es = FindObjectOfType<UnityEngine.EventSystems.EventSystem> ();
					if (es == null) {
						GameObject obj = new GameObject ("EventSystem");
						es = obj.AddComponent<UnityEngine.EventSystems.EventSystem> ();
						obj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule> ();
					}
				}
			}
		}
	}
}