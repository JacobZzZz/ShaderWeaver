using System;
using UnityEngine;

public class RobotControl : MonoBehaviour
{
	private Robot m_Character;
	private bool m_Jump;


	private void Awake()
	{
		m_Character = GetComponent<Robot>();
	}


	private void Update()
	{
		if (!m_Jump)
		{
			m_Jump = Input.GetKeyDown(KeyCode.Space);
		}
	}


	private void FixedUpdate()
	{
		// Read the inputs.
		bool crouch = Input.GetKey(KeyCode.LeftControl);
		float h = Input.GetAxis("Horizontal");
		// Pass all parameters to the character control script.
		m_Character.Move(h, crouch, m_Jump);
		m_Jump = false;
	}
}

