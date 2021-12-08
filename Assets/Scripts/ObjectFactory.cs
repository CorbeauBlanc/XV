
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ObjectFactory : MonoBehaviour
{
	public static string assetBundlesPath = Path.Combine(Application.streamingAssetsPath, "ObjectsModels");

	[SerializeField] private GameObject obj;

	private Dictionary<string, GameObject> assets = new Dictionary<string, GameObject>();

	private GameObject LoadAsset(string name) {
		AssetBundle loadedBundle;

		if (assets.ContainsKey(name)) return assets[name];
		if ((loadedBundle = AssetBundle.LoadFromFile(Path.Combine(assetBundlesPath, name))) == null)
			throw (new Exception("ObjectFactory: LoadAssetBundle: " + name + " could not be loaded"));
		assets.Add(name, loadedBundle.LoadAsset<GameObject>(name));
		return assets[name];
	}

	void Start() {
		try {
			obj = LoadAsset("1m3_crate");
		}
		catch (System.Exception) {
			throw;
		}
	}

	public void NewObjectOnPosition(Vector3 pos) {
		Instantiate(obj, pos, Quaternion.identity);
	}

	public void NewObjectOnPosition(Vector3 pos, Quaternion rot) {
		Instantiate(obj, pos, rot);
	}
}
