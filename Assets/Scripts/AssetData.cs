using System;
using System.Collections.Generic;
using UnityEngine;

public class AssetData : GoodBehaviour
{
	// Start is called before the first frame update
	protected override void BeforeStart() {
		anchorsList = new UDictionary<string, Transform>(anchors).ToDictionary();
		if (instantiationAnchor != "") {
			Transform anchor = anchorsList[instantiationAnchor];
			transform.localPosition -= anchor.localPosition;
			transform.localRotation *= anchor.localRotation;
		}
	}

	[SerializeField]
	private List<UDictionaryItem<string, Transform>> anchors = new List<UDictionaryItem<string, Transform>>();
	public Dictionary<string, Transform> anchorsList {get; private set;}

	[HideInInspector]
	public string instantiationAnchor = "";
}
