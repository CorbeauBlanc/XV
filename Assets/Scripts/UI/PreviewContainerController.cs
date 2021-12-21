using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

public class PreviewContainerController
{
	private VisualElement previewImage;
	private Camera previewCamera;
	private GameObject currentModel;
	private float previewCamTan;

	public void InitializePreviewImageContainer(VisualElement root, Camera previewCamera)
	{
		this.previewCamera = previewCamera;
		previewCamTan = Mathf.Tan((previewCamera.fieldOfView / 2) * Mathf.Deg2Rad);
		root.Q<VisualElement>("PreviewContainer").userData = this;
		previewImage = root.Q<VisualElement>("PreviewImage");
	}

	public void SetModelToPreview(string name)
	{
		GameObject model = ObjectFactory.instance.LoadAsset(name);
		AssetData modelData = model.GetComponent<AssetData>();

		previewImage.style.display = StyleKeyword.Initial;
		model.GetComponentsInChildren<MeshFilter>().ForEach((MeshFilter component) =>
		{
			component.gameObject.layer = LayerMask.NameToLayer("UI");
		});
		modelData.afterInstantiationCallbacks.TryAdd("SetupModel", (GameObject clone) =>
		{
			BoxCollider mainHitbox = model.GetComponent<BoxCollider>();
			if (mainHitbox != null)
			{
				float safeDistance = previewCamera.nearClipPlane +
									clone.GetComponent<AssetData>().GetAnchorTransform(Anchor.FRONT).position.z +
									GetMinDistanceHitboxVisibility(mainHitbox);
				clone.transform.Rotate(Vector3.up, -45, Space.World);
				clone.transform.Translate(0, 0, safeDistance, Space.World);
			}
		});
		Object.Destroy(currentModel);
		currentModel = ObjectFactory.instance.NewObjectOnPosition(model, previewCamera.transform, Anchor.MIDDLE);
		modelData.afterInstantiationCallbacks.Remove("SetupModel");
	}

	private float GetMinDistanceHitboxVisibility(BoxCollider hitbox)
	{
		return hitbox.size.magnitude / (2 * previewCamTan);
	}
}
