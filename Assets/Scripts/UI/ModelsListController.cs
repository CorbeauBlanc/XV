using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

public class ModelsListController
{
	private List<string> modelsNames = new List<string>();
	private ListView modelsList;
	private VisualTreeAsset modelsListEntry;
	private VisualElement previewContainer;

	public void InitializeModelsList(VisualElement root, VisualTreeAsset listEntry) {
		previewContainer = root.Q<VisualElement>("PreviewContainer");

		modelsList = root.Q<ListView>("ModelsList");
		modelsListEntry = listEntry;

		Directory.GetFiles(ObjectFactory.assetBundlesPath, "*.manifest").ForEach((string file) => {
			modelsNames.Add(Path.GetFileName(file).Replace(".manifest", ""));
		});

		modelsList.makeItem = () => {
			TemplateContainer newEntry = (new ModelsListEntryController()).Initialize(modelsListEntry.Instantiate());

			return newEntry;
		};
		modelsList.bindItem = (VisualElement item, int index) => {
			(item.userData as ModelsListEntryController).SetListTitle(modelsNames[index]);
		};
		modelsList.itemsSource = modelsNames;
		modelsList.onSelectionChange += OnSelectionChange;
	}

	private void OnSelectionChange(IEnumerable<object> selectedModel) {
		(previewContainer.userData as PreviewContainerController).SetModelToPreview(selectedModel.First() as string);
	}
}
