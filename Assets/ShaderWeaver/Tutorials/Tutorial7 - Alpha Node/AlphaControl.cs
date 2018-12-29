using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaControl : MonoBehaviour {
	Material mat;

	void Start () {
		mat = GetComponent<MeshRenderer> ().material;
		StartCoroutine (AlphaAnim ());
	}

	IEnumerator AlphaAnim()
	{
		float alpha = 0;
		while (alpha <= 1) {
			SetValue (alpha);
			alpha += Time.deltaTime*0.5f;
			yield return new WaitForEndOfFrame();
		}
		alpha = 1;
		SetValue (alpha);
	}

	private void SetValue(float alpha)
	{
		mat.SetFloat ("_p", alpha);
	}
}
