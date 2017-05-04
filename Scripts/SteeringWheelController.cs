using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SteeringWheelController : MonoBehaviour {

	public int turnSpeed;
	public GameObject wheel;
	private float rotations = 0;
	private float steering;
	private float h;
	Quaternion defaultRotation;
	[SerializeField] private float m_MaximumSteerAngle;

	void Start()
	{
		defaultRotation = wheel.transform.localRotation;
	}
	void FixedUpdate(){
		h = CrossPlatformInputManager.GetAxis("Horizontal");
	}

	void Update()
	{
		steering = Mathf.Clamp(h, -1, 1);
		//Debug.Log (steering);
		//Debug.Log (-Vector3.up * Time.deltaTime * turnSpeed);
		//Debug.Log (Vector3.up * Time.deltaTime * turnSpeed);
		if(Input.GetKey (KeyCode.A))
		{

			wheel.transform.Rotate(-Vector3.up * Time.deltaTime * turnSpeed);
			rotations -= turnSpeed;
		}

		else if(Input.GetKey (KeyCode.D))
		{

			wheel.transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
			rotations += turnSpeed;
		}

		else    //rotate to default
		{
			if (rotations < -1)
			{
				wheel.transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
				rotations += turnSpeed;
			}
			else if (rotations > 1)
			{
				wheel.transform.Rotate(-Vector3.up * Time.deltaTime * turnSpeed);
				rotations -= turnSpeed;
			}
			else if (rotations != 0)
			{
				wheel.transform.localRotation = defaultRotation;
				rotations = 0;
			}
		}
	}

}
