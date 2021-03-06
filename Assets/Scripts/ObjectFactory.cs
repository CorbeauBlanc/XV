
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
	public static ObjectFactory instance {get; private set;}
	public static string assetBundlesPath = Path.Combine(Application.streamingAssetsPath, "ObjectsModels");

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
	}

	public GameObject NewObjectOnPosition(string name, Vector3 pos, Anchor objAnchor = Anchor.NONE) {
		GameObject obj = LoadAsset(name);
		return NewObjectOnPosition(obj, pos, objAnchor);
	}

	public GameObject NewObjectOnPosition(string name, Transform crd, Anchor objAnchor = Anchor.NONE) {
		GameObject obj = LoadAsset(name);
		return NewObjectOnPosition(obj, crd, objAnchor);
	}

	public GameObject NewObjectOnPosition(GameObject obj, Vector3 pos, Anchor objAnchor = Anchor.NONE) {
		obj.GetComponent<AssetData>().instantiationAnchor = AssetData.GetAnchorName(objAnchor);
		return GoodBehaviour.Instantiate(obj, pos, Quaternion.identity);
	}

	public GameObject NewObjectOnPosition(GameObject obj, Transform crd, Anchor objAnchor = Anchor.NONE) {
		obj.GetComponent<AssetData>().instantiationAnchor = AssetData.GetAnchorName(objAnchor);
		return GoodBehaviour.Instantiate(obj, crd);
	}
}
