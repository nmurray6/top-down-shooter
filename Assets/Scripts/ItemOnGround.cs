using UnityEngine;
using System.Collections;

public class ItemOnGround : MonoBehaviour {

	Transform myTransform, player;
	float distance, pickupDistance = 2;
	public bool inRange = false;
	bool check = true;
	public Weapon weapon;
	
	bool inList = false;
	
	void Start () {
		myTransform = transform;
		myTransform.gameObject.layer = 12;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		distance = Vector3.Distance(myTransform.position, player.position);
	}
	
	void Update () {
		if(check && weapon != null)
			StartCoroutine(checkDistance());
		if(distance <= pickupDistance){
			
		}
	}
	
	IEnumerator checkDistance(){
		check = false;
		if(distance <= pickupDistance){
			inRange = true;
			check = true;
			distance = Vector3.Distance(myTransform.position, player.position);
			if(!inList){
				player.GetComponent<GroundItemList>().addWeaponToList(weapon, gameObject);
				inList = true;
			}
		}
		else{
			if(inList){
				player.GetComponent<GroundItemList>().removeWeaponFromList(weapon);
				inList = false;
			}
		}
		
		if(distance > pickupDistance && distance < 15){
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
	
	public void setWeapon(Weapon wep){
		weapon = wep;
	}
}
