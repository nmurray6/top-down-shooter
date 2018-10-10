using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	bool alive = true;
	
	public float currentHealth, maxHealth;
	public float speed;
	public float damage;
	public float defence;
	
	float levelModifier = .1f;
	float startHealth = 100;
	
	void Start(){
		if(transform.tag == "Player"){
			maxHealth = startHealth;
			currentHealth = maxHealth;
		}
		else{
			int level = GameObject.FindGameObjectWithTag("Spawner").GetComponent<LevelTracker>().Level;
			maxHealth = (int) (startHealth * Mathf.Pow((1+levelModifier), level));
			currentHealth = maxHealth;
		
			float speed;
			if(level != 0){
				speed = 1 + Mathf.Log(level);
			}
			else{
				speed = 1;
			}
			transform.GetComponent<AIMovement>().speed = speed;
		}
	}
	
	public void takeDamage(float change){
		currentHealth -= change;
		if(currentHealth <= 0)
			death();
	}
	
	private void death(){
		if(alive){
			//Destroy(transform.gameObject);
			Animation anim = transform.GetComponent<Animation>();
			float f = Random.value;
			if(f < .5f)
				anim.CrossFade("Death2", .1f);
			else if(f >= .5f)
				anim.CrossFade ("Death1", .1f);
			Destroy (GetComponent<AIComponentList>());
			Destroy (GetComponent<CharacterController>());
			Destroy (GetComponent<SimpleSmoothModifier>());
			Destroy(GetComponent<Seeker>());
			Destroy(GetComponent<AIMovement>());
			Destroy(GetComponent<FunnelModifier>());
			Destroy(GetComponent<Stats>());
			Destroy(GetComponent<DestroyFarAway>());
			Destroy (GetComponent<TestingScript>());

			
			if(transform.tag == "Enemy"){
				LevelTracker tracker = GameObject.FindGameObjectWithTag("Spawner").GetComponent<LevelTracker>();
				tracker.monstersKilled++;
				GameObject.FindGameObjectWithTag("Spawner").GetComponent<MonsterSpawner>().creatureCount--;
				float rand = Random.value;
				if(rand <= .02f){
					RandomWeaponCreator creator = new RandomWeaponCreator();
					creator.CreateRandomWeapon(tracker.Level, transform.position);
				}
			}
			alive = false;
		}
	}
	
	public void heal(float change){
		currentHealth += change;
		if(currentHealth > maxHealth)
			currentHealth = maxHealth;
	}
	
	public float getDefence(){
		return defence;
	}
}
