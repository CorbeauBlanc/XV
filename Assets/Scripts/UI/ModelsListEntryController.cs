using UnityEngine.UIElements;

public class ModelsListEntryController {
	private Label assetName;

	public TemplateContainer Initialize(TemplateContainer root) {
		root.userData = this;
		assetName = root.Q<Label>("AssetName");
		return root;
	}

	public void SetListTitle(string title) {
		assetName.text = title;
	}
}
