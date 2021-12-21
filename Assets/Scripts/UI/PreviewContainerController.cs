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
		GameObject model = ObjectFactory.instance.LoadAsset(name);
		AssetData modelData = model.GetComponent<AssetData>();

		previewImage.style.display = StyleKeyword.Initial;
		model.GetComponentsInChildren<MeshFilter>().ForEach((MeshFilter component) => {
			component.gameObject.layer = LayerMask.NameToLayer("UI");
		});
		modelData.afterInstantiationCallbacks.Add((GameObject clone) => {
			BoxCollider mainHitbox = model.GetComponent<BoxCollider>();
			if (mainHitbox != null) {
				float halfSize = mainHitbox.size.magnitude / 2;
				float safeDistance = clone.GetComponent<AssetData>().GetAnchorTransform(Anchor.MIDDLE).position.z - halfSize;
				Debug.Log(halfSize);
				Debug.Log(safeDistance);
				clone.transform.Translate(0, 0, safeDistance, Space.World);
			} else
				Debug.Log("NULL");
		});
		Object.Destroy(currentModel);
		currentModel = ObjectFactory.instance.NewObjectOnPosition(model, modelAnchor, Anchor.MIDDLE);
	}
}
