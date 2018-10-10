//var walkAnim:AnimationClip;
var walkAnimSpeed:float;
var runAnimSpeed:float;
var strafeAnimSpeed:float;
var jumpAnimSpeed:float;
var jumpDelay:float;
var walkSpeed:float;
var runSpeed:float;
var strafeSpeed:float;
var turnSpeed:float;
var pushForce:float;
var jumpForce:float;
var gravity:float;

private var charController:CharacterController;
private var walking:boolean;
private var running:boolean;
private var strafing:boolean;
private var jumping:boolean;
private var preparingJump:boolean = false;

function Start(){
	charController = GetComponent(CharacterController);
	Screen.lockCursor = true;
	//Debug.Log(charController.isGrounded);
}

function Update () {
	if(Input.GetAxis("Vertical") > 0){
		Debug.Log("fwd");
		
		if(Input.GetButton("Shift")){ //run
			running = true;
			animation["run"].speed = runAnimSpeed;
			animation.CrossFade("run");
			charController.Move(transform.TransformDirection(Vector3.forward * runSpeed * Time.deltaTime));
		}
		
		else{ //walk
			walking = true;
			animation["walk"].speed = walkAnimSpeed;
			animation.CrossFade("walk");
			charController.Move(transform.TransformDirection(Vector3.forward * walkSpeed * Time.deltaTime));
		}
	}
	else if(Input.GetAxis("Vertical") < 0){
		Debug.Log("bwd");
		
		if(Input.GetButton("Shift")){ //run
			running = true;
			animation["run"].speed = -runAnimSpeed;
			animation.CrossFade("run");
			charController.Move(transform.TransformDirection(-Vector3.forward * runSpeed * Time.deltaTime));
		}
		else{ //walk
			walking = true;
			animation["walk"].speed = -walkAnimSpeed;
			animation.CrossFade("walk");
			charController.Move(transform.TransformDirection(-Vector3.forward * walkSpeed * Time.deltaTime));
		}
	}
	else{
		walking = false;
		running = false;
	}
	
	if(Input.GetAxis("Horizontal") > 0){
		strafing = true;
		animation["strafe_R"].speed = strafeAnimSpeed;
		animation.CrossFade("strafe_R");
		
		charController.Move(transform.TransformDirection(Vector3.right * strafeSpeed * Time.deltaTime));
	}
	else if(Input.GetAxis("Horizontal") < 0){
		strafing = true;
		animation["strafe_L"].speed = -strafeAnimSpeed;
		animation.CrossFade("strafe_L");
		
		charController.Move(transform.TransformDirection(-Vector3.right * strafeSpeed * Time.deltaTime));
	}
	else{
		strafing = false;
	}
	
	if(Input.GetButtonDown("Jump") && !jumping){
		if(!preparingJump){
			jump();
			preparingJump = true;
		}
	}
	
	if(jumping || preparingJump){
		animation.CrossFade("jump");
	}
	
	if(jumping || !charController.isGrounded){
		charController.Move(Vector3(0, gravity, 0));
		gravity -= 1 * Time.deltaTime;
		
		if(charController.isGrounded){
			jumping = false;
			gravity = 0;
		}
	}
	
	if(!walking && !running && !strafing && !jumping && !preparingJump){
		animation.CrossFade("idle");
	}
	
//	if(Input.GetAxis("Horizontal") > 0){
//		transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
//	}
//	else if(Input.GetAxis("Horizontal") < 0){
//		transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
//	}

	transform.Rotate(0, Input.GetAxisRaw("Mouse X") * turnSpeed * Time.deltaTime, 0);
	
}

function jump(){
	animation["jump"].speed = jumpAnimSpeed;
//	animation["jump"].time = 0;
	animation.CrossFade("jump");
	
	yield WaitForSeconds(jumpDelay);
	
	gravity = jumpForce;
	preparingJump = false;
	jumping = true;
}

function OnControllerColliderHit(hit:ControllerColliderHit){
	//Debug.Log(hit);
	if(hit.rigidbody){
		hit.rigidbody.velocity = Vector3(hit.moveDirection.x * walkSpeed, hit.moveDirection.y, hit.moveDirection.z * walkSpeed);
		//hit.rigidbody.AddForce(transform.TransformDirection(Vector3.forward * pushForce * Time.deltaTime));
		Debug.Log("Push!");
	}
}