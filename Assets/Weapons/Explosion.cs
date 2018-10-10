using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	
	ParticleSystem mySystem;
	SphereCollider sphere;
	float damage;
	public float explosionRadius = 2;
	bool damagePhase = true;
	
	public void setup(float dmg, float radius){
		damage = dmg;
		explosionRadius = radius;
	}
	
	void Start(){
		mySystem = transform.GetComponent<ParticleSystem>();
		Destroy(gameObject, mySystem.startLifetime);
	}
	
	void Update(){
		if(damagePhase){
			mySystem.transform.tag = "Untagged";
			dealDamage();
			damagePhase = false;
		}
	}
	
	void dealDamage(){
		Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
		Transform target;
		GameObject helper = new GameObject();
		helper.transform.position = transform.position;
		int i = 0;
		while (i < hits.Length){
			target = hits[i].transform;
			if(target.tag == "Enemy"){
				helper.transform.LookAt(target);
				Stats stat = target.GetComponent<Stats>();
				stat.takeDamage(damage);
				target.GetComponent<CharacterController>().Move(helper.transform.forward);
			}
			i++;
		}
		Destroy(helper);
	}
}
