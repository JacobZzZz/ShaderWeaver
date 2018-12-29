using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWSimpleCameraFollow : MonoBehaviour {
	public Transform target;
	Vector3 startPos;
	Vector3 targetPos;
	void Awake()
	{
		startPos = transform.position;
	}

	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x,startPos.y,startPos.z),1*Time.deltaTime);
	}
}
