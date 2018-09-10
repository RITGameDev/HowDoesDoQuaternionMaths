using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloaterRotater : MonoBehaviour {
	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	public float ZSensitivity = 2f;
	public float MinimumX = -90f;
	public float MaximumX = 90f;
	public bool smooth;
	public float smoothTime = 5f;

	private Quaternion m_rot;
	private bool m_cursorIsLocked = true;

	public void Awake()
	{
		m_rot = transform.localRotation;
	}


	public void Update()
	{
		float yRot = Input.GetAxis("Horizontal") * XSensitivity;
		float xRot = Input.GetAxis("Vertical") * YSensitivity;
		float zRot = Input.GetAxis("Other") * ZSensitivity;

		// What if I comment this ... (NOTE: You can also rewrite all three lines to be AngleAxis)
		m_rot = Quaternion.identity;
		m_rot *= Quaternion.Euler (xRot, 0f, 0f);
		m_rot *= Quaternion.Euler (0f, yRot, 0f);
		m_rot *= Quaternion.Euler (0f, 0f, zRot);

		// ... and uncomment this?
		//m_CameraTargetRot *= Quaternion.Euler (-xRot, yRot, 0f);
		Quaternion rot;
		if(smooth)
		{
			rot=Quaternion.Slerp (transform.localRotation, m_rot,
				smoothTime * Time.deltaTime);
		}
		else
		{
			rot = m_rot;
		}

		//rot = Quaternion.Euler (rot.eulerAngles.x, rot.eulerAngles.y, 0);
		transform.localRotation *= rot;
	}
}
