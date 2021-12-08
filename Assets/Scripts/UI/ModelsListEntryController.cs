using UnityEngine.UIElements;

public class ModelsListEntryController {
	private Label assetName;

	public void SetVisualElement(VisualElement root) {
		assetName = root.Q<Label>("assetName");
	}

	public void SetListTitle(string title) {
		assetName.text = title;
	}
}
