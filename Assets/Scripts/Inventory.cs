using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
	
	public Weapon[] inventory = new Weapon[2];
	public int currentlyActive = 0;
	public int money = 0;
	Combat combat;
	
	// Use this for initialization
	void Start () {
		setupInitialInventory();
		combat = transform.GetComponent<Combat>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			if(inventory[0] != null){
				if(combat.currentWeapon != inventory[0]){
					combat.currentWeapon = inventory[0];
					combat.changedWeapons = true;
					currentlyActive = 0;
				}
			}
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2)){
			if(inventory[1] != null){
				if(combat.currentWeapon != inventory[1]){
					combat.currentWeapon = inventory[1];
					combat.changedWeapons = true;
					currentlyActive = 1;
				}
			}
		}
		/*else if(Input.GetKeyDown(KeyCode.Alpha3)){
			if(inventory[2] != null){
				if(combat.currentWeapon != inventory[2]){
					combat.currentWeapon = inventory[2];
					combat.changedWeapons = true;
				}
			}
		}*/
	}
	
	void setupInitialInventory(){
		Weapon temp = new Weapon();
		temp.CreateStarterWeapon();
		inventory[0] = temp;
		temp = new Weapon();
		//temp.CreateStarterPistol();
		temp.CreateTestRocketLauncher();
		inventory[1] = temp;
	}
	
	public void pickUpItem(Weapon wep){
		inventory[currentlyActive] = wep;
		combat.currentWeapon = inventory[currentlyActive];
		combat.changedWeapons = true;
	}
}
