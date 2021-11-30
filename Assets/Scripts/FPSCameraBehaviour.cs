using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FPSCameraBehaviour : MonoBehaviour
{
	[SerializeField] private Transform verticalRig;
	[SerializeField] private Camera fpsCamera;

	private float virtualScreenDistance;

	// Start is called before the first frame update
	void Start()
	{
		virtualScreenDistance = (Screen.height / 2) / Mathf.Tan((fpsCamera.fieldOfView / 2) * Mathf.Deg2Rad);
	}

	private void UpdateCameraOrientation() {
		if (!Input.GetMouseButton(1)) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			return;
		}

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
		float mY = -Input.GetAxis("Mouse Y");
		float xRot = ScriptUtils.RestrictedAngle(verticalRig.localEulerAngles.x);
		verticalRig.Rotate(ScriptUtils.IsClamped(mY + xRot, -90, 90) ? mY : 0, 0, 0);
	}

	private void UpdateCameraPosition() {
		transform.Translate(0, 0, Input.GetAxis("Vertical"));
		transform.Translate(Input.GetAxis("Horizontal"), 0, 0);
		if (Input.GetKey("left ctrl")) transform.Translate(0, -1, 0);
		if (Input.GetKey("space")) transform.Translate(0, 1, 0);
	}

	private RaycastHit GetMousePointingTarget() {
		RaycastHit hit;
		Vector3 rayDirection = new Vector3(
			(Input.mousePosition.x - Screen.width / 2) - transform.position.x,
			(Input.mousePosition.y - Screen.height / 2) - transform.position.y,
			virtualScreenDistance);
		rayDirection = transform.localRotation * rayDirection;
		rayDirection = verticalRig.localRotation * rayDirection;
		Physics.Raycast(transform.position, rayDirection, out hit, fpsCamera.farClipPlane);
		return hit;
	}

	// Update is called once per frame
	void Update()
	{
		UpdateCameraOrientation();
		UpdateCameraPosition();
	}
}
