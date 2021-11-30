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
