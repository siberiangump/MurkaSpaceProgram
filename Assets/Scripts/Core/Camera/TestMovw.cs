using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovw : MonoBehaviour
{
	[SerializeField] private float Speed;
	[SerializeField] private ObjectFreeLookCamera ObjCamera;


	private void Start ()
	{
		//SetTarget ();	
	}

	void Update () 
	{
		if (Input.GetKey (KeyCode.W))
			transform.position= new Vector3 (transform.position.x, transform.position.y + Speed, transform.position.z);
		if (Input.GetKey (KeyCode.S))
			transform.position= new Vector3 (transform.position.x, transform.position.y - Speed, transform.position.z);


		if (Input.GetKey (KeyCode.A))
			transform.position= new Vector3 (transform.position.x- Speed, transform.position.y , transform.position.z);
		if (Input.GetKey (KeyCode.D))
			transform.position= new Vector3 (transform.position.x+ Speed, transform.position.y , transform.position.z);
	}

	[ContextMenu ("SetTarget")]
	public void SetTarget () 
	{
		ObjCamera.SetTarget (transform);
	}
}
