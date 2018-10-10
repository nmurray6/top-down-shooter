using UnityEngine;
using System.Collections;

public class RandomWeaponCreator {
	
	float levelModifier = .5f;
	float damageModifier = 0;
	float speedModifier = .5f;
	float rofModifier = 5;
	float accuracyModifier = .2f;
	
	public float rifleProbability = .44f;
	public float shotgunProbability = .33f;
	public float explosiveProbability = .23f;
	
	float maxRifleDamage = 100;
	float minRifleDamage = 20;
	
	float maxShotgunDamage = 20;
	float minShotgunDamage = 5;
	
	float maxExplosiveDamage = 150;
	float minExplosiveDamage = 50;
	
	float rifleRange = 20;
	float shotgunRange = 10;
	float explosiveRange = 0;
	
	float worstCaseRifleAccuracy = 30;
	float bestCastRifleAccuracy = 10;
	
	float bestCaseShotgunAccuracy = 20;
	float worstCaseShotgunAccuracy = 60;
	
	float maxRateOfFire = .7f;
	float minRateOfFire = .04f;
	
	float baseRifleMagazineSize = 50;
	float baseShotgunMagazineSize = 10;
	float baseExplosiveMagazineSize = 1;
	
	int maxShotsPerRound = 20;
	int minShotsPerRound = 8; //for shotguns
	
	public RandomWeaponCreator(){}
	
	public void CreateRandomWeapon(int level, Vector3 position){
		string WeaponName = "";
		int weaponType = RandomWeaponType();
		//Debug.Log(weaponType + " Type");
		float damage = RandomWeaponDamage(weaponType, level);
		//Debug.Log(damage + " base damage");
		float speed = RandomSpeed(weaponType);
		//Debug.Log(speed + " speed");
		float range = SetRange(weaponType);
		//Debug.Log(range + " range");
		float initialAccuracy = 0;
		float accuracy = RandomAccuracy(weaponType);
		//Debug.Log(accuracy + " accuracy");
		float shotsPerSecond = RandomRateOfFire(weaponType, damage);
		//Debug.Log(shotsPerSecond + " shotsPS");
		int magazineSize = findMagazineSize(weaponType, shotsPerSecond);
		//Debug.Log(magazineSize + " mag size");
		float reload = findReloadTime(weaponType, magazineSize);
		//Debug.Log(reload + " reload");
		int shotsPerRound = findShotsPerRound(weaponType);
		//Debug.Log(shotsPerRound + " shots per round");
		float increasedSpreadPerShot = findSpreadPerShot(weaponType);
		//Debug.Log(increasedSpreadPerShot + " inc spread");
		float decreasedSpreadPerSecond = 3 * increasedSpreadPerShot;
		damage *= damageModifier;
		//Debug.Log(damageModifier + " dmg mod");
		//Debug.Log(damage + " final damage");
		
		Weapon created = new Weapon(WeaponName, weaponType, damage, range, initialAccuracy, accuracy, shotsPerSecond, magazineSize, reload, shotsPerRound, increasedSpreadPerShot, decreasedSpreadPerSecond, speed);
		
		GameObject groundWeapon = GameObject.CreatePrimitive(PrimitiveType.Cube);
		groundWeapon.name = "Weapon";
		groundWeapon.transform.localScale = new Vector3(.5f, .25f, 1);
		groundWeapon.transform.position = position;
		groundWeapon.AddComponent<ItemOnGround>();
		groundWeapon.GetComponent<ItemOnGround>().setWeapon(created);
		groundWeapon.AddComponent<Rigidbody>();
	}
	
	int RandomWeaponType(){
		float type = Random.value;
		if(type <= rifleProbability)
			return 1;
		
		type -= rifleProbability;
		if(type <= shotgunProbability)
			return 2;
		
		type -= shotgunProbability;
		if(type <= explosiveProbability)
			return 3;
		
		else{
			Debug.Log("Something went wrong in determining TYPE of the random weapon ( RandomWeaponCreator.cs )");
			return 0;
		}
	}
	
	float RandomWeaponDamage(int type, int level){
		float rand = Random.value;
		if(type == 1){
			rand *= (maxRifleDamage - minRifleDamage);
			rand += minRifleDamage;
			return rand*Mathf.Pow((1+levelModifier), level);
		}
		else if(type == 2){
			rand *= (maxShotgunDamage - minShotgunDamage);
			rand += minShotgunDamage;
			return rand*Mathf.Pow((1+levelModifier), level);
		}
		else if(type == 3){
			rand *= (maxExplosiveDamage - minExplosiveDamage);
			rand += minExplosiveDamage;
			return rand*Mathf.Pow((1+levelModifier), level);
		}
		else{
			Debug.Log("Something went wrong in determining DAMAGE of the random weapon ( RandomWeaponCreator.cs )");
			return 0;
		}
	}
	float RandomSpeed(int type){
		float rand = Random.value;
		damageModifier += (rand * speedModifier);
		if(type == 1){
			return (rand * rifleRange -1) + 1;
		}
		
		else if(type == 2){
			return (rand * shotgunRange -1) + 1;
		}
		
		else if(type == 3){
			return (rand * explosiveRange);
		}
		
		else{
			Debug.Log("Something went wrong in determining SPEED of the random weapon ( RandomWeaponCreator.cs )");
			return 0;
		}
	}
	
	float SetRange(int type){
		if(type == 1)
			return rifleRange;
		else if (type == 2)
			return shotgunRange;
		else
			return 0;
	}
	
	float RandomAccuracy(int type){
		float rand = Random.value;
		if(type == 1){
			damageModifier += (rand * accuracyModifier);//low accuracy damage bonus
			rand *= (worstCaseRifleAccuracy - bestCastRifleAccuracy);
			rand += bestCastRifleAccuracy;
			return rand;
		}
		else if( type == 2){
			damageModifier += (rand * accuracyModifier);//low accuracy damage bonus
			rand *= (worstCaseShotgunAccuracy - bestCaseShotgunAccuracy);
			rand += bestCaseShotgunAccuracy;
			return rand;
		}
		else
			return 0;
	}
	
	float RandomRateOfFire(int type, float damage){
		float rof = Random.value;
		damageModifier += (rof * rofModifier);
		
		rof *= (maxRateOfFire - minRateOfFire);
		rof += minRateOfFire;
		rof = 1/rof;
		return rof;
	}
	
	int findMagazineSize(int type, float shotsPS){
		int magazineSize = 0;
		if(type == 1){
			magazineSize = (int) (baseRifleMagazineSize * shotsPS / 12);
		}
		else if(type == 2){
			magazineSize = (int)(baseShotgunMagazineSize * shotsPS / 10);
		}
		else if(type == 3){
			magazineSize = (int) (baseExplosiveMagazineSize * shotsPS / 5);
			if(magazineSize == 0)
				magazineSize = 1;
		}
		
		if(magazineSize == 0){
			Debug.Log("Something went wrong in determining MAGAZINE SIZE of the random weapon ( RandomWeaponCreator.cs )");
		}
		
		return magazineSize;
	}
	
	int findShotsPerRound(int type){
		int shots = 0;
		if (type == 1 || type == 3)
			shots = 1;
		else if( type == 2){
			shots = (int) ((Random.value * (maxShotsPerRound - minShotsPerRound)) + minShotsPerRound);
			damageModifier += (10/shots) - 1;
		}
		
		if(shots == 0){
			Debug.Log("Something went wrong in determining SHOTS PER ROUND of the random weapon ( RandomWeaponCreator.cs )");
		}
		
		return shots;
	}
	
	float findSpreadPerShot(int type){
		float spread = 0;
		if(type == 1){
			spread = damageModifier +1;
		}
		return spread;
	}
	
	float findReloadTime(int type, int magSize){
		float reloadTime = 1;
		if (type == 1){
			reloadTime = (float)magSize / 50;
		}
		else if(type == 2){
			reloadTime = (float)magSize / 12;
		}
		else if(type == 3){
			reloadTime = damageModifier / 2;
		}
		return reloadTime;
	}
}
