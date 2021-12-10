using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

public class ModelsListController
{
	private List<string> modelsNames = new List<string>();
	private ListView modelsList;
	private VisualTreeAsset modelsListEntry;

	public void InitializeModelsList(VisualElement root, VisualTreeAsset listEntry) {
		modelsList = root.Q<ListView>("ModelsList");
		modelsListEntry = listEntry;

		Directory.GetFiles(ObjectFactory.assetBundlesPath, "*.manifest").ForEach(delegate (string file) {
			modelsNames.Add(Path.GetFileName(file).Replace(".manifest", ""));
		});

		modelsList.makeItem = delegate () {
			TemplateContainer newEntry = (new ModelsListEntryController()).Initialize(modelsListEntry.Instantiate());

			return newEntry;
		};
		modelsList.bindItem = delegate (VisualElement item, int index) {
			(item.userData as ModelsListEntryController).SetListTitle(modelsNames[index]);
		};
		modelsList.itemsSource = modelsNames;
	}
}
