using UnityEngine;
[AddComponentMenu("Catlike/BezierCurve")]
public class CatlikeBezierCurve : MonoBehaviour {

	public Vector3[] points;
	
	public Vector3 GetPoint (float t) {
		return transform.TransformPoint(CatlikeBezier.GetPoint(points[0], points[1], points[2], points[3], t));
	}
	
	public Vector3 GetVelocity (float t) {
		return transform.TransformPoint(CatlikeBezier.GetFirstDerivative(points[0], points[1], points[2], points[3], t)) - transform.position;
	}
	
	public Vector3 GetDirection (float t) {
		return GetVelocity(t).normalized;
	}
	
	public void Reset () {
		points = new Vector3[] {
			new Vector3(100f, 100f, 0f),
			new Vector3(100f, 500f, 0f),
			new Vector3(600f, 100f, 0f),
			new Vector3(600f, 500f, 0f)
		};
	}
    }