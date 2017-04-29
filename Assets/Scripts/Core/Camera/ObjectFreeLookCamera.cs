using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFreeLookCamera : FreeLookCam 
{
	[SerializeField] ZoomSettings ZoomSetting = new ZoomSettings ();

	private bool StopRotation = false;
	private Vector3 MousePos;
	private Camera RocketCam;

	protected override void Update ()
	{
		base.Update ();

		Zoom ();

		if (Input.GetMouseButtonDown (2))
			MousePos = Input.mousePosition;

		if (Input.GetMouseButton (2)) 
		{
			StopRotation = true;
			float x = CrossPlatformInputManager.GetAxis("Mouse X");
			float y = CrossPlatformInputManager.GetAxis("Mouse Y");
			//float moveVert = x * 
			float yDiff =  Input.mousePosition.y - MousePos.y;
			float xDiff =  Input.mousePosition.x - MousePos.x;
			RocketCam = m_Cam.GetComponent <Camera> ();
			Vector3 targetWorldPos =  new Vector3(xDiff,yDiff,0);
			Vector3 targetPos = m_Pivot.InverseTransformPoint(new Vector3(targetWorldPos.x, targetWorldPos.y, m_Pivot.position.z));
			m_Pivot.localPosition = Vector3.Lerp (m_Pivot.localPosition, targetPos, ZoomSetting.AxisMoveSpeed);


			//m_Pivot.localPosition = new Vector3 (m_Pivot.localPosition.x, vertMovement , m_Pivot.localPosition.z);

		}else 
		{
			StopRotation = false;
		}
	}

	protected override void HandleRotationMovement ()
	{
		if (StopRotation)
			return;
		base.HandleRotationMovement ();
	}

	protected override void FollowTarget(float deltaTime)
	{
		if (m_Target == null) return;
		// Move the rig towards target position.
		transform.position = m_Target.position; //Vector3.Lerp(transform.position, m_Target.position, deltaTime*m_MoveSpeed);
	}

	private void Zoom () 
	{
		float targetZoom =m_Pivot.localPosition.z + CrossPlatformInputManager.GetAxis("Mouse ScrollWheel");
		float zoom = Mathf.Lerp (m_Pivot.localPosition.z, targetZoom, ZoomSetting.ZoomSpeed);	
		zoom = Mathf.Clamp (zoom, ZoomSetting.Max, ZoomSetting.Min);
		m_Pivot.localPosition = new Vector3 (m_Pivot.localPosition.x, m_Pivot.localPosition.y,zoom );
	}

	[System.Serializable]
	private class ZoomSettings 
	{
		public float Min;
		public float Max;
		public float ZoomSpeed;
		public float AxisMoveSpeed;

		public ZoomSettings ()
		{
			Min = -1.0f;
			Max = -10.0f;
		}
	}

}
