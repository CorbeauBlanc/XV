
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ObjectFactory : MonoBehaviour
{
	[SerializeField] private GameObject template;

	void Start() {}

	public void NewObjectOnPosition(Vector3 pos) {
		Instantiate(template, pos, Quaternion.identity);
	}

	public void NewObjectOnPosition(Vector3 pos, Quaternion rot) {
		Instantiate(template, pos, rot);
	}
}
