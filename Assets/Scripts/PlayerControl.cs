using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	CharacterController controller;
	Camera playerCamera;
	float speed = 10f;
	Ray ray;
	RaycastHit hit;
	Vector3 target;
	GameObject helper;
	
	public Texture2D crosshair;
	
	// Use this for initialization
	void Start () {
		helper = new GameObject("Controller helper");
		controller = transform.GetComponent<CharacterController>();
		playerCamera = Camera.main;
		UpdateHelper();
		
		Cursor.SetCursor(crosshair, Vector2.zero, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHelper();
		//Gravity
		controller.Move(-transform.up);
		
		//Raycast to get the position of the mouse on the ground, then have the character look at it.
		ray = playerCamera.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, 1<<8)){
			target = hit.point;
		}
		
		transform.LookAt(target);
		transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
		
		//Moves the character left and right.
		if(Input.GetAxis("Horizontal") != 0){
			controller.Move(helper.transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);	
		}
		//Moves the character up and down.
		if(Input.GetAxis("Vertical") != 0) {
			controller.Move ((helper.transform.forward) * Input.GetAxis("Vertical") * speed * Time.deltaTime);
		}
	}
	
	//Use this function every time the camera has changed rotation.
	void UpdateHelper(){
		helper.transform.rotation = new Quaternion(0, playerCamera.transform.rotation.y, 0, playerCamera.transform.rotation.w);
	}
}
