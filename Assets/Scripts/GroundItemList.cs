using UnityEngine;
using System.Collections;

public class GroundItemList : MonoBehaviour {

	WeaponList list = new WeaponList();
	Node currentNode = null;
	
	Vector3 screenPosition = Vector3.zero;
	
	void Update(){
		if(list.head == null)
			currentNode = null;
		if(currentNode == null)
			if(list.head != null)
				currentNode = list.head;
		
		if(currentNode != null)
			screenPosition = Camera.main.WorldToScreenPoint(currentNode.model.transform.position);
		
		if(Input.GetKeyDown(KeyCode.F)){
			if(currentNode != null){
				GameObject groundWeapon = GameObject.CreatePrimitive(PrimitiveType.Cube);
				groundWeapon.name = "Weapon";
				groundWeapon.transform.localScale = new Vector3(.5f, .25f, 1);
				groundWeapon.transform.position = transform.position;
				groundWeapon.AddComponent<ItemOnGround>();
				Inventory inv = transform.GetComponent<Inventory>();
				groundWeapon.GetComponent<ItemOnGround>().setWeapon(inv.inventory[inv.currentlyActive]);
				groundWeapon.AddComponent<Rigidbody>();
				
				transform.GetComponent<Inventory>().pickUpItem(currentNode.weapon);
				Destroy(currentNode.model);
				removeWeaponFromList(currentNode.weapon);
				currentNode = list.head;
			}
		}
		if(Input.GetKeyDown(KeyCode.Tab)){
			if(currentNode != null){
				if(currentNode.next == null){
					currentNode = list.head;
				}
				else
					currentNode = currentNode.next;
			}
		}
	}
	
	public void addWeaponToList(Weapon weapon, GameObject model){
		list.addWeapon(weapon, model);
	}
	
	public void removeWeaponFromList(Weapon weapon){
		if(currentNode.weapon == weapon)
			currentNode = currentNode.next;
		list.removeWeapon(weapon);
	}
	
	
	
	void OnGUI(){
		if(currentNode != null){
			GUI.Box (new Rect(screenPosition.x, Screen.height - screenPosition.y, 150, 100), currentNode.weapon.WeaponName);
			GUI.Label(new Rect(screenPosition.x + 5, Screen.height - screenPosition.y + 20, 100, 20), "Damage:");
			GUI.Label(new Rect(screenPosition.x + 75, Screen.height - screenPosition.y + 20, 50,20), currentNode.weapon.damage.ToString());
			GUI.Label(new Rect(screenPosition.x + 5, Screen.height - screenPosition.y + 40, 100, 20), "Speed:");
			GUI.Label(new Rect(screenPosition.x + 75, Screen.height - screenPosition.y + 40, 50,20), currentNode.weapon.projectileSpeed.ToString());
		}
	}
	
	
	
	
	
	
	
	
	
	public class WeaponList{
		public Node head = null;
		Node last = null;
		
		public WeaponList(){}
		
		public void addWeapon(Weapon weapon, GameObject model){
			if(head == null){
				head = new Node(weapon, model);
				last = head;
			}
			else{
				last.next = new Node(weapon, model);
				last = last.next;
			}
		}
		public void removeNode(Node node){
			if(node != null){
				if(head != null){
					if(head == node && head.next != null){
						head = head.next;
					}
					else if(head == node && head.next == null){
						head = null;
						last = null;
					}
					else{
						Node ptr = head;
						Node ptr2 = head.next;
						while(ptr2 != null && ptr2 != node){
							ptr = ptr.next;
							ptr2 = ptr.next;
						}
					
						if(ptr2 != null){
							ptr2 = ptr2.next;
							ptr.next = ptr2;
							if(ptr2 == null){
								last = ptr;
							}
						}	
					}
				}
			}
		}
		
		public void removeWeapon(Weapon weapon){
			Node ptr = head;
			if(head.weapon == weapon){
				removeNode(ptr);
			}
			else{
				ptr = ptr.next;
				while(ptr != null && ptr.weapon != weapon){
					ptr = ptr.next;
				}
				removeNode(ptr);
			}
		}
	}
		
		
	public class Node {
		public Weapon weapon;
		public GameObject model;
		public Node next;
		public Node(Weapon wep, GameObject mod){
			weapon = wep;
			model = mod;
		}
	}
		
	
	
	
}


