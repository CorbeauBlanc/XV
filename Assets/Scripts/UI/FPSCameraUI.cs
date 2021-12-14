using UnityEngine;
using UnityEngine.UIElements;

public class FPSCameraUI : MonoBehaviour
{
	[SerializeField]
	private VisualTreeAsset modelsListEntry;

	[SerializeField]
	private Transform previewAnchor;

	void OnEnable() {
		UIDocument uiDocument = GetComponent<UIDocument>();

		(new PreviewContainerController()).InitializePreviewImageContainer(uiDocument.rootVisualElement, previewAnchor);
		(new ModelsListController()).InitializeModelsList(uiDocument.rootVisualElement, modelsListEntry);
	}
}
