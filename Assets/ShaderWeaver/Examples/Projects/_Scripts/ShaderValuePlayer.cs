using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ShaderValue
{
	public string keyword;
	public float factor;
}

public class ShaderValuePlayer : MonoBehaviour {
	public List<ShaderValue> values;
	private float duration =2f;
	private Material mat;
	private bool visible = true;
	private float speed;

	private void Start()
	{
		speed = 1f / duration;
		var r = GetComponent<Renderer> ();
		if (r != null) {
			mat = r.material;
		}else{
			var image = GetComponent<Image> ();
			if (image != null)
				mat = image.material;
		}
	}
	private void Update()
	{
		if (mat == null)
			return;
		if (Input.GetKeyDown (KeyCode.Return)) {
			if(visible)
				StartCoroutine (Go ());
			else
				StartCoroutine (Back ());
		}
	}
	private IEnumerator Go()
	{
		float pcg = 0;
		while (pcg < 1) {
			pcg += speed*Time.deltaTime;
			SetValue(pcg);
			yield return new WaitForEndOfFrame ();
		}
		pcg = 1;
		SetValue(pcg);
		visible = false;
	}
	private IEnumerator Back()
	{
		float pcg = 1;
		while (pcg >0) {
			pcg -= speed*Time.deltaTime;
			SetValue(pcg);
			yield return new WaitForEndOfFrame ();
		}
		pcg = 0;
		SetValue(pcg);
		visible = true;
	}
	private void SetValue(float v)
	{
		foreach (var value in values) {
			mat.SetFloat (value.keyword,v*value.factor);
		}
	}
}
