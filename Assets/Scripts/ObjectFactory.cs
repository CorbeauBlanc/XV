
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
	public static ObjectFactory instance {get; private set;}
	public static string assetBundlesPath = Path.Combine(Application.streamingAssetsPath, "ObjectsModels");

	[SerializeField] private GameObject obj;

	private Dictionary<string, GameObject> assets = new Dictionary<string, GameObject>();

	public GameObject LoadAsset(string name) {
		AssetBundle loadedBundle;

		if (assets.ContainsKey(name)) return assets[name];
		if ((loadedBundle = AssetBundle.LoadFromFile(Path.Combine(assetBundlesPath, name))) == null)
			throw (new Exception("ObjectFactory: LoadAssetBundle: " + name + " could not be loaded"));
		assets.Add(name, loadedBundle.LoadAsset<GameObject>(name));
		return assets[name];
	}

	void Start() {
		ObjectFactory.instance = this;
		try {
			obj = LoadAsset("1m3_crate");
		}
		catch (System.Exception) {
			throw;
		}
	}

	public GameObject NewObjectOnPosition(string name, Vector3 pos, string objAnchor = "") {
		GameObject obj = LoadAsset(name);
		return NewObjectOnPosition(obj, pos, objAnchor);
	}

	public GameObject NewObjectOnPosition(GameObject obj, Vector3 pos, string objAnchor = "") {
		if (objAnchor != "") {
			Transform anchor = obj.GetComponent<AssetData>().anchorsList[objAnchor];
			return Instantiate(obj, pos + anchor.position, anchor.rotation);
		}
		return Instantiate(obj, pos, Quaternion.identity);
	}
}
