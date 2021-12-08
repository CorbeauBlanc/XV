using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

public class ModelsListController
{
	private List<string> modelsNames = new List<string>();

	public void InitializeModelsList(VisualElement root, VisualTreeAsset listEntry) {
		Directory.GetFiles(ObjectFactory.assetBundlesPath, "*.manifest").ForEach(delegate (string file) {
			modelsNames.Add(file.Replace(".manifest", ""));
		});
	}
}
