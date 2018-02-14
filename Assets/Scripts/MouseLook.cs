using System;
using UnityEngine;

[Serializable]
public class MouseLook :MonoBehaviour
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90f;
    public float MaximumX = 90f;
    public bool smooth;
    public float smoothTime = 5f;

    private Quaternion m_CameraTargetRot;
    private bool m_cursorIsLocked = true;

	public void Awake()
    {
		m_CameraTargetRot = transform.localRotation;
    }


	public void Update()
    {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

		// What if I comment this ...
		m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);
		m_CameraTargetRot *= Quaternion.Euler (0f, yRot, 0f);

		// ... and uncomment this?
		//m_CameraTargetRot *= Quaternion.Euler (-xRot, yRot, 0f);
		Quaternion rot;
        if(smooth)
        {
			rot=Quaternion.Slerp (transform.localRotation, m_CameraTargetRot,
				smoothTime * Time.deltaTime);
        }
        else
        {
            rot = m_CameraTargetRot;
        }
		rot = Quaternion.Euler (rot.eulerAngles.x, rot.eulerAngles.y, 0);
		transform.localRotation = rot;
    }
}