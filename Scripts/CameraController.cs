using UnityEngine;
using System.Collections;

public class CarCamera : MonoBehaviour {
	public GameObject car;
	public float distancia = 6.4f;
	public float height = 1.4f;
	public float rotationDamping = 3.0f;
	public float heightDamping = 2.0f;
	public float zoomRacio = 0.5f;
	public float DefaultFOV = 60f;
	private Vector3 rotationVector;
	Rigidbody rb;
	Camera camera;

	void Start () {
		rb = car.GetComponent<Rigidbody>();
		camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void LateUpdate () {
		float wantedAngle = rotationVector.y;//car.transform.eulerAngles.y;
		float wantedHeight = car.transform.position.y + height;
		float myAngel = transform.eulerAngles.y;
		float myHeight = transform.position.y;
		myAngel = Mathf.LerpAngle(myAngel,wantedAngle,rotationDamping*Time.deltaTime);
		myHeight = Mathf.Lerp(myHeight,wantedHeight,heightDamping*Time.deltaTime);
		Quaternion currentRotation = Quaternion.Euler(0,myAngel,0);
		transform.position = car.transform.position;
		transform.position -= currentRotation*Vector3.forward*distancia;
		transform.position = new Vector3 (transform.position.x, myHeight, transform.position.z);
		transform.LookAt(car.transform);
	}
	void FixedUpdate (){
		Vector3 localVilocity = car.transform.InverseTransformDirection(rb.velocity);
		if (localVilocity.z<-1.5f){
			rotationVector.y = car.transform.eulerAngles.y + 180;
		}
		else {
			rotationVector.y = car.transform.eulerAngles.y;
		}
		float acc = rb.velocity.magnitude;
		camera.fieldOfView = DefaultFOV + acc*zoomRacio;
	}
}﻿