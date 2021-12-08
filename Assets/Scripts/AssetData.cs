using System;
using System.Collections.Generic;
using UnityEngine;

public class AssetData : MonoBehaviour
{
	// Start is called before the first frame update
	void Start() {
		anchorsList = new UDictionary<string, Transform>(anchors).ToDictionary();
	}

	[SerializeField]
	private List<UDictionaryItem<string, Transform>> anchors = new List<UDictionaryItem<string, Transform>>();
	public Dictionary<string, Transform> anchorsList;
}
