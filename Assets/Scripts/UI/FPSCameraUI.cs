using UnityEngine;
using UnityEngine.UIElements;

public class FPSCameraUI : MonoBehaviour
{
	[SerializeField]
	private VisualTreeAsset modelsListEntry;

	[SerializeField]
	private Camera previewCamera;

	void OnEnable() {
		UIDocument uiDocument = GetComponent<UIDocument>();

		(new PreviewContainerController()).InitializePreviewImageContainer(uiDocument.rootVisualElement, previewCamera);
		(new ModelsListController()).InitializeModelsList(uiDocument.rootVisualElement, modelsListEntry);
	}
}
