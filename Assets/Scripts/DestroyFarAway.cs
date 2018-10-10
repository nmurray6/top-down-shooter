using UnityEngine;
using System.Collections;

public class DestroyFarAway : MonoBehaviour {
	
	public Transform target;
	public float maxDistance = 35;
	AIMovement movement;
	MonsterSpawner spawner;
	
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		movement = transform.GetComponent<AIMovement>();
		spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<MonsterSpawner>();
	}
	
	bool outOfRange = false;
	void Update () {
		if(Vector3.Distance(transform.position, target.position) > maxDistance && !outOfRange){
			movement.canMove = false;
			movement.findPaths = false;
			spawner.creatureCount--;
			outOfRange = true;
		}
		if(Vector3.Distance(transform.position, target.position) < maxDistance && outOfRange){
			movement.canMove = true;
			movement.findPaths = true;
			spawner.creatureCount++;
			outOfRange = false;
		}
	}
}
