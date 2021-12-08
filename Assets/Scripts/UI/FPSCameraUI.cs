using UnityEngine;
using UnityEngine.UIElements;

public class FPSCameraUI : MonoBehaviour
{
	[SerializeField]
	private VisualTreeAsset modelsListEntry;

	void OnEnable() {
		UIDocument uiDocument = GetComponent<UIDocument>();

		(new ModelsListController()).InitializeModelsList(uiDocument.rootVisualElement, modelsListEntry);
	}
}
