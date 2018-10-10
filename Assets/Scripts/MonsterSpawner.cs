using UnityEngine;
using System.Collections;
using Pathfinding;

public class MonsterSpawner : MonoBehaviour {
	
	public Transform player;
	Transform myTransform;
	public GameObject zombie;
	public float spawnRate = 1;
	public int creatureCount = 0;
	public int maxCreatureCount = 10;
	public float minDistance = 10, maxDistance = 30;
	int x, y;
	Vector3 center;
	//GridGraph gridGraph;
	
	public Transform target;
	
	// Use this for initialization
	void Start () {
		myTransform = transform;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		x = target.GetComponent<AstarPath>().astarData.gridGraph.width;
		y = target.GetComponent<AstarPath>().astarData.gridGraph.depth;
		center = target.GetComponent<AstarPath>().astarData.gridGraph.center;
		center.x -= x/2;
		center.z -= y/2;
	}
	
	// Update is called once per frame
	void Update () {
		myTransform.position = player.transform.position;
		if(creatureCount < maxCreatureCount){
			
			tryToSpawnMonster();
			
			/*Vector3 pos = new Vector3(Random.value * x, 0, Random.value * y);
			pos += center;
			Node node = AstarPath.active.GetNearest(pos).node;
			if(node.walkable){
				pos = AstarPath.active.GetNearest(pos).clampedPosition;
				GameObject mob = Instantiate(zombie) as GameObject;
				pos = AstarPath.active.GetNearest(pos).clampedPosition;
				pos.y += zombie.transform.localScale.y + .1f;
				mob.transform.position = pos;
				creatureCount ++;
			}
		*/	
		}
	}
	
	void tryToSpawnMonster(){
		myTransform.rotation = new Quaternion(0, (Random.value * 2) -1, 0 , (Random.value * 2) - 1);
		Vector3 pos = myTransform.forward * ((Random.value * (maxDistance - minDistance)) + minDistance) + myTransform.position;
		
		Node node = AstarPath.active.GetNearest(pos).node;
			if(node.walkable){
				pos = AstarPath.active.GetNearest(pos).clampedPosition;
				GameObject mob = Instantiate(zombie) as GameObject;
				pos = AstarPath.active.GetNearest(pos).clampedPosition;
				pos.y += zombie.transform.localScale.y + zombie.GetComponent<CharacterController>().height * zombie.transform.localScale.y /2 + .1f;
				mob.transform.position = pos;
				creatureCount ++;
			}
	}
	
}
