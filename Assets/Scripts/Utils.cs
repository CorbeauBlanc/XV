using System;
using System.Collections.Generic;

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
