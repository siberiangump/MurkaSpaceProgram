using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CatlikeLine))]
public class CatlikeLineInspector : Editor {

	private void OnSceneGUI () {
		CatlikeLine catlikeLine = target as CatlikeLine;
		Transform handleTransform = catlikeLine.transform;
		Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;
		Vector3 p0 = handleTransform.TransformPoint(catlikeLine.p0);
		Vector3 p1 = handleTransform.TransformPoint(catlikeLine.p1);

		Handles.color = Color.white;
		Handles.DrawLine(p0, p1);
		EditorGUI.BeginChangeCheck();
		p0 = Handles.DoPositionHandle(p0, handleRotation);
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(catlikeLine, "Move Point");
			EditorUtility.SetDirty(catlikeLine);
			catlikeLine.p0 = handleTransform.InverseTransformPoint(p0);
		}
		EditorGUI.BeginChangeCheck();
		p1 = Handles.DoPositionHandle(p1, handleRotation);
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(catlikeLine, "Move Point");
			EditorUtility.SetDirty(catlikeLine);
			catlikeLine.p1 = handleTransform.InverseTransformPoint(p1);
		}
	}
}