using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	public GameObject target;
	Transform myTransform;
	Vector3 desiredPosition = Vector3.zero;
	Vector3 changePosition, currentPosition;
	float sensitivity = 3;
	float xPosition = -5, yPosition = 10, zPosition = 0;
	float scrollSpeed = 20f;
	float velX = 0, velZ = 0;
	float radius;
	float degree = 180;
	int maxRadius = 12, minRadius = 2;
	
	// Use this for initialization
	void Start () {
		//Pull in the camera and the object that the camera is following.
		target = GameObject.FindGameObjectWithTag("Player");
		myTransform = transform;
		//Find the radius and the Y value.
		//radius = Mathf.Sqrt((xPosition * xPosition) + (zPosition * zPosition));
		radius = maxRadius;
		yPosition = .2f*(radius*radius);
		xPosition = -Mathf.Sqrt(5 * yPosition);
		//Setup the starting point for the camera.
		myTransform.position = new Vector3(target.transform.position.x + xPosition, target.transform.position.y + yPosition, target.transform.position.z + zPosition);
		myTransform.LookAt(target.transform);
		//Set up the point that the camera will try to get to, defaulted onto the starting position of the camera.
		desiredPosition = myTransform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//Vector which will hold the X and Z changes when zooming in or out.
		changePosition = Vector3.zero;
		//Updates the vector which holds the current position, used for manipulation.
		currentPosition = myTransform.position - target.transform.position;
		//Middle mouse button
		if(Input.GetAxis("Fire3") != 0){
			if(Input.GetAxis("Mouse X") != 0){
				//Changes the degree.
				degree += Input.GetAxis("Mouse X") * sensitivity;
				//Keeps degree between 0 and 360, to prevent any potential complications, ie out of bounds.
				checkDegree();
				//Circle function to find the new X and Z values.
				float newX = radius * Mathf.Cos(degree * Mathf.Deg2Rad);
				float newZ = radius * Mathf.Sin(degree * Mathf.Deg2Rad);
				//Sets the new X and Z values into the desired position vector.
				desiredPosition.x = newX;
				desiredPosition.z = newZ;
			}
		}
		//Mouse wheel
		if(Input.GetAxis("Mouse ScrollWheel") != 0){
			//Gets the change asked for in this frame.
			changePosition += (myTransform.forward * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed);
			//Adds the change to the desired position.
			desiredPosition = myTransform.position - target.transform.position + changePosition;
		}
		
		//Puts some calculations into a function that not always run to potentially be more effecient.
		if(Mathf.Abs(desiredPosition.x - currentPosition.x) >= .05f || Mathf.Abs(desiredPosition.z - currentPosition.z) >= .05f){
			changeXYZ();
		}
		
		//Update the position and rotation of the camera.
		myTransform.position = new Vector3(target.transform.position.x + xPosition, target.transform.position.y + yPosition, target.transform.position.z + zPosition);
		myTransform.LookAt(target.transform);
	}
	
	//Changes the X, Y, and Z coordinates of the camera.
	void changeXYZ(){
		float z;
		float x;
		//Runs this if the middle mouse button is used (for rotations).
		if(Input.GetAxis("Fire3") !=0){
			x = desiredPosition.x;
			z = desiredPosition.z;
		}
		//Otherwise does this
		else{
			x = Mathf.SmoothDamp(xPosition, desiredPosition.x, ref velX, .1f);
			z = Mathf.SmoothDamp(zPosition, desiredPosition.z, ref velZ, .1f);
		}
		//Finds the radius.
		float r = Mathf.Sqrt((x*x)+(z*z));
		//Checks if the radius is in bounds and sets the values.
		if(checkRadius(r)){
			xPosition = x;
			zPosition = z;
			radius = r;
			yPosition = .2f*(radius*radius);
		}
		else{
			//Radius was out of bounds so it is ignored and the positions are reset to the last known.
			desiredPosition = myTransform.position - target.transform.position;
		}
	}
	
	//Checks the radius in bounds.
	bool checkRadius(float Radius){
		if(Radius >= minRadius && Radius <= maxRadius)
			return true;
		else
			return false;
	}
	
	//Keeps degree between 0 and 360.
	void checkDegree(){
		if(degree >= 360)
			degree -= 360;
		else if(degree < 0)
			degree += 360;
	}
}
