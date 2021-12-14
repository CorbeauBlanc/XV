using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

public class PreviewContainerController
{
	private Transform modelAnchor;
	private VisualElement previewImage;
	private GameObject currentModel;

	public void InitializePreviewImageContainer(VisualElement root, Transform previewAnchor) {
		modelAnchor = previewAnchor;
		root.Q<VisualElement>("PreviewContainer").userData = this;
		previewImage = root.Q<VisualElement>("PreviewImage");
	}

	public void SetModelToPreview(string name) {
		previewImage.style.display = StyleKeyword.Initial;
		GameObject model = ObjectFactory.instance.LoadAsset(name);
		model.GetComponentsInChildren<MeshFilter>().ForEach((MeshFilter component) => {
			component.gameObject.layer = LayerMask.NameToLayer("UI");
		});
		Object.Destroy(currentModel);
		currentModel = ObjectFactory.instance.NewObjectOnPosition(model, modelAnchor.position);
	}
}
