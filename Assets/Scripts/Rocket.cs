using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
	
	float damage = 0;
	public float speed = 50; //units per second
	Vector3 target, start;
	public ParticleSystem explosion;
	GameObject explode;
	float distance, traveled = 0;
	public float explosionRadius = 2;
	
	// Use this for initialization
	void Start () {
		start = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(traveled < distance){
			transform.position += (transform.forward * speed * Time.deltaTime);
			traveled = Vector3.Distance(transform.position, start);
		}
		else{
			createExplosion();
		}
	}
	
	public void setStats(float dmg, Vector3 trgt){
		damage = dmg;
		target = trgt;
		
		start = transform.position;
		distance = Vector3.Distance(start, target);
	}
	
	void OnTriggerEnter(Collider hit){
		if(hit.tag == "Player" || hit.tag == "Rocket Explosion"){
			return;
		}
		else {
			createExplosion();
		}
	}
	
	void createExplosion(){
		Instantiate(explosion, transform.position, new Quaternion(1,1,1,-1));
		explode = GameObject.FindGameObjectWithTag("Rocket Explosion");
		explode.GetComponent<Explosion>().setup(damage, explosionRadius);
		Destroy(transform.gameObject);
	}
}
