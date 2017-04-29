using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour {

	[SerializeField] private Transform Target;
	//[SerializeField] private Transform SecondTarget;
	[SerializeField] private RotateSettings RotSettings;
	[SerializeField] private Transform Racketa;
	private bool IsMouseDown;
	private Vector2 MousePosOnStart;
 	
	// Update is called once per frame
	void Update () 
	{
		Target.position = Racketa.position;

		float x = CrossPlatformInputManager.GetAxis("Mouse X");
		float y = CrossPlatformInputManager.GetAxis("Mouse Y");

		if (Input.GetMouseButtonDown (1))
			MousePosOnStart = new Vector2 (x,y);
		else 
			if (Input.GetMouseButton (1))
			{
				x -= MousePosOnStart.x;
				y -= MousePosOnStart.y;
				float horRot = x * RotSettings.RotateSpeed_x  + Target.localEulerAngles.y;

				float vertRot = -y * RotSettings.RotateSpeed_y + Target.localEulerAngles.x ;
				float vertForClamp = (vertRot + 90.0f) % 360.0f;
				vertForClamp = Mathf.Clamp (vertForClamp, RotSettings.MinVertAngle, RotSettings.MaxVertAngle);		

				vertRot = vertForClamp - 90.0f;
				Quaternion target = Quaternion.Euler (vertRot,horRot, Target.localEulerAngles.z );
				
				Target.localRotation = Quaternion.Slerp (Target.localRotation, target, RotSettings.RotateSpeed);
	
				transform.LookAt (Target);
			}
	}

	[System.Serializable]
	private class RotateSettings 
	{
		public float MinVertAngle;
		public float MaxVertAngle;
		public float RotateSpeed_x;
		public float RotateSpeed_y;

		public float RotateSpeed;


	}
}
