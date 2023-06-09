using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour {
	private bool  doMovement         = true;
	public  float panSpeed           = 30;
	public  float panBorderThickness = 10;
	public  float scrollSpeed        = 5;
	public  float minY               = 10;
	public  float maxY               = 80;
	void Update() {
		if (GameManager.GameIsOver) {
			enabled = false;
			return;
		}
		
		
		
		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) {
			transform.Translate(Vector3.forward * (panSpeed * Time.deltaTime), Space.World);
		}
		if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) {
			transform.Translate(Vector3.back * (panSpeed * Time.deltaTime), Space.World);
		}
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) {
			transform.Translate(Vector3.right * (panSpeed * Time.deltaTime), Space.World);
		}
		if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) {
			transform.Translate(Vector3.left * (panSpeed * Time.deltaTime), Space.World);
		}

		float   scroll = Input.GetAxis("Mouse ScrollWheel");
		Vector3 pos    = transform.position;
		pos.y              -= scroll * 1000 * scrollSpeed * Time.deltaTime;
		pos.y              =  Math.Clamp(pos.y, minY, maxY); 
		transform.position =  pos;
	}
}