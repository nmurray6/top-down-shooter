using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {

	bool check = true;
	bool inRange = false;
	int shoppingDistance = 5;
	float distance;
	Vector3 screenPosition = Vector3.zero;
	
	Transform myTransform, player;
	
	void Start () {
		myTransform = transform;
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	
	void Update () {
		if(check)
			StartCoroutine(checkDistance());
		
		if(inRange)
			screenPosition = Camera.main.WorldToScreenPoint(myTransform.position);
	}
	
	void OnGUI(){
		if(inRange)
			GUI.Label(new Rect(screenPosition.x - 45, Screen.height - screenPosition.y + 55, 150, 20), "Press 'F' to shop.");
	}
	
	IEnumerator checkDistance(){
		check = false;
		if(distance <= shoppingDistance){
			inRange = true;
			check = true;
			distance = Vector3.Distance(myTransform.position, player.position);
		}
		
		else if(distance > shoppingDistance && distance < 15){
			inRange = false;
			check = true;
			distance = Vector3.Distance(myTransform.position, player.position);
		}
		else if(distance >= 15 && distance <= 25){
			inRange = false;
			yield return new WaitForSeconds(1);
			check = true;
			distance = Vector3.Distance(myTransform.position, player.position);
		}
		else if(distance >= 25 && distance <  100){
			inRange = false;
			yield return new WaitForSeconds(2);
			check = true;
			distance = Vector3.Distance(myTransform.position, player.position);
		}
		else if(distance >= 100){
			inRange = false;
			yield return new WaitForSeconds(8);
			check = true;
			distance = Vector3.Distance(myTransform.position, player.position);
		}
	}
	
}
