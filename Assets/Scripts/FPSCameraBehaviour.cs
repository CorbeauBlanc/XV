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
	[SerializeField] private float movementSpeed = 1;
	[SerializeField] private float mouseSensitivity = 1;

	[SerializeField] private ObjectFactory factory;

	// Start is called before the first frame update
	void Start()
	{
	}

	private void UpdateCameraOrientation() {
		if (!Input.GetMouseButton(1)) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			return;
		}

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0);
		float mY = -Input.GetAxis("Mouse Y") * mouseSensitivity;
		float xRot = ScriptUtils.RestrictedAngle(verticalRig.localEulerAngles.x);
		verticalRig.Rotate(ScriptUtils.IsClamped(mY + xRot, -90, 90) ? mY : 0, 0, 0);
	}

	private void UpdateCameraPosition() {
		transform.Translate(0, 0, Input.GetAxis("Vertical") * movementSpeed);
		transform.Translate(Input.GetAxis("Horizontal") * movementSpeed, 0, 0);
		if (Input.GetKey("left ctrl")) transform.Translate(0, -1 * movementSpeed, 0);
		if (Input.GetKey("space")) transform.Translate(0, 1 * movementSpeed, 0);
	}

	private RaycastHit GetMousePointingTarget() {
		RaycastHit hit;
		Physics.Raycast(fpsCamera.ScreenPointToRay(Input.mousePosition), out hit, fpsCamera.farClipPlane, LayerMask.GetMask("Default"));
		return hit;
	}

	private void HandleClicEvent() {
		RaycastHit hit = GetMousePointingTarget();
		if (Input.GetMouseButtonUp(0) && hit.collider != null)
			factory.NewObjectOnPosition("1m3_crate", hit.point);
	}

	// Update is called once per frame
	void Update()
	{
		UpdateCameraOrientation();
		UpdateCameraPosition();
		HandleClicEvent();
	}
}
