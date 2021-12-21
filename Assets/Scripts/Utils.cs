using System;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ScriptUtils {
	private ScriptUtils() {}

	public static bool IsClamped(float number, float value1, float value2) {
		return number >= value1 && number <= value2;
	}

	public static float RestrictedAngle(float angle) {
		if (angle < 0) return angle < -180 ? angle + 360 : angle;
		return angle > 180 ? angle - 360 : angle;
	}
}

[Serializable]
public struct UDictionaryItem<T, U> { public T key; public U value; }
[Serializable]
public class UDictionary<T, U> : List<UDictionaryItem<T, U>> {
	public UDictionary(List<UDictionaryItem<T, U>> list) : base (list) {}

	public Dictionary<T, U> ToDictionary() {
		Dictionary<T, U> dictionnary = new Dictionary<T, U>();
		ForEach((UDictionaryItem<T, U> item) => {
			dictionnary.Add(item.key, item.value);
		});
		return dictionnary;
	}
}

public abstract class GoodBehaviour : MonoBehaviour {
	protected abstract void Initialize(GameObject original);

	public List<Action<GameObject>> afterInstantiationCallbacks = new List<Action<GameObject>>();

	private static void InitializeClone(GameObject original, GameObject clone) {
		clone.GetComponents<GoodBehaviour>().ForEach((GoodBehaviour behaviour) => {
			behaviour.Initialize(original);
		});
		original.GetComponents<GoodBehaviour>().ForEach((GoodBehaviour behaviour) => {
			behaviour.afterInstantiationCallbacks.ForEach((Action<GameObject> callback) => {
				callback(clone);
			});
		});
	}

	public static GameObject Instantiate(GameObject original) {
		GameObject clone = MonoBehaviour.Instantiate(original);
		InitializeClone(original, clone);
		return clone;
	}

	public static GameObject Instantiate(GameObject original, Transform parent){
		GameObject clone = MonoBehaviour.Instantiate(original, parent);
		InitializeClone(original, clone);
		return clone;
	}

	public static GameObject Instantiate(GameObject original, Transform parent, bool instantiateInWorldSpace){
		GameObject clone = MonoBehaviour.Instantiate(original, parent, instantiateInWorldSpace);
		InitializeClone(original, clone);
		return clone;
	}

	public static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation){
		GameObject clone = MonoBehaviour.Instantiate(original, position, rotation);
		InitializeClone(original, clone);
		return clone;
	}

	public static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation, Transform parent){
		GameObject clone = MonoBehaviour.Instantiate(original, position, rotation, parent);
		InitializeClone(original, clone);
		return clone;
	}

}
