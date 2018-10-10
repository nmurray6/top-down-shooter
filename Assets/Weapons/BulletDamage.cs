using UnityEngine;
using System.Collections;

public class BulletDamage : MonoBehaviour {
	
	Transform myTransform, target;
	Ray ray;
	float damage;
	Vector3 previousParticlePosition;
	float particlePosition = 0, particleChange = 0, particleLength = 0;
	bool hasHit = false;
	public LayerMask mask;
	
	public GameObject bulletHit;
	GameObject ps;
	
	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		particleChange = myTransform.GetComponent<ParticleSystem>().startSpeed * Time.deltaTime;
		particlePosition = particleChange + particleLength;//here
		RaycastHit hit;
		ray.origin = previousParticlePosition;
		if(!hasHit){
			if(Physics.Raycast(ray, out hit, particlePosition, ~mask)){
				target = hit.transform;
				if(target.tag == "Enemy"){
					Stats stat = target.GetComponent<Stats>();
					stat.takeDamage(damage);
					target.GetComponent<CharacterController>().Move(ray.direction * .1f);
				}
				playBulletHit(hit);
				hasHit = true;
				Destroy(myTransform.gameObject);
			}
		}
		previousParticlePosition += (ray.direction * particleChange);
		previousParticlePosition.y = myTransform.position.y;
	}
	
	public void startShot(Weapon currentWeapon, Ray r){
		previousParticlePosition = r.origin;
		ray = r;
		damage = currentWeapon.getDamage();
		myTransform = transform;
		myTransform.GetComponent<ParticleSystem>().startSpeed = currentWeapon.projectileSpeed * 10;//added
		myTransform.GetComponent<ParticleSystem>().startLifetime = currentWeapon.getRange()/myTransform.GetComponent<ParticleSystem>().startSpeed;
		myTransform.GetComponent<ParticleSystem>().Emit(1);
		//To find the front of the front of the particle from the center.
		particleLength = myTransform.GetComponent<ParticleSystem>().startSpeed * .03f * .5f;
		
		Destroy(myTransform.gameObject, currentWeapon.getRange()/myTransform.GetComponent<ParticleSystem>().startSpeed);
	}
	
	void playBulletHit(RaycastHit hit){
		ps = Instantiate(bulletHit, hit.point, myTransform.rotation) as GameObject;
		ps.transform.LookAt(myTransform.position);
		ps.GetComponent<ParticleSystem>().Emit(5);
		Destroy (ps, ps.GetComponent<ParticleSystem>().startLifetime);
	}
}
