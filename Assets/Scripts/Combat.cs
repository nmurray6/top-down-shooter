using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {
	
	public Transform myTransform, target;
	public float shotsPerSecond, shootTime, shootTimer = 0, currentAccuracy = 0, switchWeaponsTimer = 0;
	public Weapon currentWeapon;
	public bool changedWeapons = false;
	public bool reloading = false;
	public bool canShoot = true;
	public float reloadTimer = 0;
	public GameObject bullet;
	GameObject bullet1;
	public GameObject rocket;
	
	Inventory inventory;

	// Use this for initialization
	void Start () {
		myTransform = transform;
		inventory = myTransform.GetComponent<Inventory>();
		currentWeapon = inventory.inventory[inventory.currentlyActive];
		//Incase this is started before the inventory contents are created
		if(currentWeapon != null){
			currentAccuracy = currentWeapon.getInitialAccuracy();
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Incase this is started before the inventory contents are created
		if(currentWeapon == null){
			currentWeapon = inventory.inventory[inventory.currentlyActive];
			currentAccuracy = currentWeapon.getInitialAccuracy();
		}
			
		
		if(changedWeapons){
			currentAccuracy = currentWeapon.getInitialAccuracy();
			switchWeaponsTimer = 1;
			changedWeapons = false;
		}
		if(Input.GetAxis("Fire1") != 0 && !reloading && canShoot && switchWeaponsTimer == 0){
			if(currentWeapon.getWeaponType() == 1){
				shootSingleShot ();
				removeABullet();
				canShoot = false;
			}
			else if (currentWeapon.getWeaponType() == 2){
				shootMultiShot();
				removeABullet();
				canShoot = false;
			}
			else if(currentWeapon.getWeaponType() == 3){
				shootARocket();
				removeABullet();
				canShoot = false;
			}
		}
		
		if(Input.GetKeyDown(KeyCode.R) && currentWeapon.getBulletsInClip() < currentWeapon.getMagazineSize()){
			reload();
		}
	}
	
	void FixedUpdate(){
		
		if(shootTimer > 0){
			shootTimer -= Time.fixedDeltaTime;
		}
		else
			canShoot = true;
		
		if(currentWeapon != null)
			if(currentAccuracy > currentWeapon.getInitialAccuracy()){
				currentAccuracy -= (Time.fixedDeltaTime * currentWeapon.getDecreasedSpreadPerSecond());
			}
		
		if(reloading){
			reload();
		}
		
		if(switchWeaponsTimer > 0)
			switchWeaponsTimer -= Time.fixedDeltaTime;
		else
			switchWeaponsTimer = 0;
		
	}
	
	void shootSingleShot(){
		shootTimer += currentWeapon.getShootTime();
		Ray ray = new Ray();
		ray.origin = myTransform.position;
		
		float randomValue = Random.value;
		
		//Debugging
		Debug.DrawLine(myTransform.position, 
				new Vector3((currentWeapon.getRange() * Mathf.Sin(((myTransform.rotation.eulerAngles.y - (currentAccuracy/2)) + currentAccuracy * randomValue) * Mathf.Deg2Rad)),
				0,
				(currentWeapon.getRange() * Mathf.Cos(((myTransform.rotation.eulerAngles.y - (currentAccuracy/2)) + currentAccuracy * randomValue) * Mathf.Deg2Rad)))
				+ myTransform.position);
		//End debugging
		
		
		Vector3 Direction = new Vector3((Mathf.Sin(((myTransform.rotation.eulerAngles.y - (currentAccuracy/2)) + currentAccuracy * randomValue) * Mathf.Deg2Rad)),
				0,
				(Mathf.Cos(((myTransform.rotation.eulerAngles.y - (currentAccuracy/2)) + currentAccuracy * randomValue) * Mathf.Deg2Rad)));
		
		ray.direction = Direction;
		
		if(currentAccuracy <= currentWeapon.getAccuracy())
			currentAccuracy += currentWeapon.getIncreasedSpreadPerShot();
		else
			currentAccuracy = currentWeapon.getAccuracy();

		//Particle Effects
		
		ShootABullet(ray);
		
		/*bullet1 = Instantiate(bullet, myTransform.position+ (myTransform.forward), myTransform.rotation) as GameObject;
		bullet1.transform.LookAt(myTransform.position + Direction * 10);
		bullet1.particleSystem.startLifetime = currentWeapon.getRange()/bullet1.particleSystem.startSpeed;
		bullet1.particleSystem.Emit(1);
		Destroy(bullet1, currentWeapon.getRange()/bullet1.particleSystem.startSpeed);
		
		
		if(Physics.Raycast(ray, out hit, currentWeapon.getRange())){
			target = hit.transform;
			if(target.tag == "Enemy"){
				Stats stat = target.GetComponent<Stats>();
				stat.takeDamage(currentWeapon.getDamage());
			}
		}*/
	}
	
	void shootMultiShot(){
		shootTimer += currentWeapon.getShootTime();
		Ray ray = new Ray();
		ray.origin = myTransform.position;
		ray.direction = myTransform.forward;
		
		float randomValue;
	/*	//For debugging
		for(int i = 0; i < currentWeapon.getShotsPerRound(); i++){
			randomValue = Random.value;
			Debug.DrawLine(myTransform.position, 
				new Vector3((currentWeapon.getRange() * Mathf.Sin(((myTransform.rotation.eulerAngles.y - (currentWeapon.getAccuracy()/2)) + currentWeapon.getAccuracy() * randomValue) * Mathf.Deg2Rad)),
				0,
				(currentWeapon.getRange() * Mathf.Cos(((myTransform.rotation.eulerAngles.y - (currentWeapon.getAccuracy()/2)) + currentWeapon.getAccuracy() * randomValue) * Mathf.Deg2Rad)))
				+ myTransform.position);
				
		}//End debugging */
		
		for(int i = 0; i < currentWeapon.getShotsPerRound(); i++){
			randomValue = Random.value;
			Vector3 Direction = new Vector3((Mathf.Sin(((myTransform.rotation.eulerAngles.y - (currentWeapon.getAccuracy()/2)) + currentWeapon.getAccuracy() * randomValue) * Mathf.Deg2Rad)),
				0,
				(Mathf.Cos(((myTransform.rotation.eulerAngles.y - (currentWeapon.getAccuracy()/2)) + currentWeapon.getAccuracy() * randomValue) * Mathf.Deg2Rad)));
			
			ray.direction = Direction;
			
			ShootABullet(ray);
			
			/*bullet1 = Instantiate(bullet, myTransform.position+ (myTransform.forward * 1.5f), myTransform.rotation) as GameObject;
			bullet1.transform.LookAt(myTransform.position + Direction * 10);
			bullet1.particleSystem.startLifetime = currentWeapon.getRange()/bullet1.particleSystem.startSpeed;
			bullet1.particleSystem.Emit(1);
			Destroy(bullet1, currentWeapon.getRange()/bullet1.particleSystem.startSpeed);
			
			if(Physics.Raycast(ray, out hit, currentWeapon.getRange())){
				target = hit.transform;
				Debug.Log(hit.distance);
				if(target.tag == "Enemy"){
					Stats stat = target.GetComponent<Stats>();
					stat.takeDamage(currentWeapon.getDamage());
				}
			}*/
		}
	}
	
	void removeABullet(){
		currentWeapon.setBulletsInClip(currentWeapon.getBulletsInClip() - 1);
		if(currentWeapon.getBulletsInClip() <= 0){
			reloading = true;
		}
	}
	
	void reload(){
		if(reloadTimer == 0){
				//Play animation
		}
		if(reloadTimer < currentWeapon.getReloadTime()){
			reloadTimer += Time.fixedDeltaTime;
		}
		else{
			reloading = false;
			reloadTimer = 0;
			currentWeapon.setBulletsInClip(currentWeapon.getMagazineSize());
		}
	}
	
	void ShootABullet(Ray ray){
		bullet1 = Instantiate(bullet, myTransform.position+ (myTransform.forward), myTransform.rotation) as GameObject;
		bullet1.transform.LookAt(myTransform.position + ray.direction * 10);
		
		bullet1.GetComponent<BulletDamage>().startShot(currentWeapon, ray);
	}
	
	void shootARocket(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, 1<<8)){
			Vector3 trgt = hit.point;
			shootTimer += currentWeapon.getShootTime();
			GameObject projectile = Instantiate(rocket, myTransform.position + myTransform.forward, myTransform.rotation) as GameObject;
			projectile.GetComponent<Rocket>().setStats(currentWeapon.getDamage(), trgt);
		}
	}
}
