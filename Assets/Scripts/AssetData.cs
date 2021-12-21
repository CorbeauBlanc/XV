using System.Collections.Generic;
using UnityEngine;

public enum Anchor {
	FRONT,
	BACK,
	LEFT,
	RIGHT,
	MIDDLE,
	NONE,
}

public class AssetData : GoodBehaviour
{
	private static string[] anchorNames = new string[] { "Anchor Front", "Anchor Back", "Anchor Left", "Anchor Right", "Anchor True Middle", "" };

	public static string GetAnchorName(Anchor anchor) {
		return anchorNames[(int)anchor];
	}

	// Start is called before the first frame update
	void Start() {
		if (instantiationAnchor != "") {
			Transform anchor = anchorsList[instantiationAnchor];
			transform.localPosition -= anchor.localPosition;
			transform.localRotation *= anchor.localRotation;
		}
	}

	protected override void Initialize(GameObject original) {

	}

	[SerializeField]
	private List<UDictionaryItem<string, Transform>> anchors = new List<UDictionaryItem<string, Transform>>();
	private Dictionary<string, Transform> anchorsList;

	public Transform GetAnchorTransform(string name) {
		if (anchorsList == null) anchorsList = new UDictionary<string, Transform>(anchors).ToDictionary();
		return anchorsList[name];
	}

	public Transform GetAnchorTransform(Anchor anchor) {
		if (anchorsList == null) anchorsList = new UDictionary<string, Transform>(anchors).ToDictionary();
		return anchorsList[GetAnchorName(anchor)];
	}

	[HideInInspector]
	public string instantiationAnchor = "";
}
