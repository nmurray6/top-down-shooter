using UnityEngine;
using System.Collections;

public class Weapon {
	//Variables on the weapon
	public string WeaponName = "";
	public int weaponType; // 1 for straight shot, 2 for shotgun, 3 for projectile explosive
	public float damage;
	public float range;
	public float initialAccuracy;//on the first shot within how many degrees will the shot land?
	public float accuracy; //Max spread when full auto.
	public float shotsPerSecond;
	public int magazineSize;
	public int shotsPerRound; // usually 1 unless its a shotgun.
	public float increasedSpreadPerShot;
	public float decreasedSpreadPerSecond; //In terms of angles.
	public float reload;//in terms of seconds
	public int bulletsInClip;
	public float projectileSpeed;
	
	//Variables not on the weapon but effect it
	float shootTime; //Time between each shot
	int invintorySlot;
	GameObject model;//Will be used to hold the model of the weapon
	
	public Weapon(){}
	
	public Weapon(string name, int WeaponType,float DamagePerShot, float Range, float InitialAccuracy, float Accuracy, float ShotsPerSecond, int MagazineSize, float reloadTime, int ShotsPerRound, float incSpreadPerShot, float decSpreadPerSecond, int InvintorySlot){
		WeaponName = name;
		weaponType = WeaponType;
		shotsPerRound = ShotsPerRound;
		initialAccuracy = InitialAccuracy;
		accuracy = Accuracy;
		shotsPerSecond = ShotsPerSecond;
		magazineSize = MagazineSize;
		reload = reloadTime;
		range = Range;
		damage = DamagePerShot;
		increasedSpreadPerShot = incSpreadPerShot;
		decreasedSpreadPerSecond = decSpreadPerSecond;
		
		shootTime = 1/ShotsPerSecond;
		bulletsInClip = magazineSize;
		invintorySlot = InvintorySlot;
	}
	
	public Weapon(string name, int WeaponType,float DamagePerShot, float Range, float InitialAccuracy, float Accuracy, float ShotsPerSecond, int MagazineSize, float reloadTime, int ShotsPerRound, float incSpreadPerShot, float decSpreadPerSecond, float speed){
		WeaponName = name;
		weaponType = WeaponType;
		shotsPerRound = ShotsPerRound;
		initialAccuracy = InitialAccuracy;
		accuracy = Accuracy;
		shotsPerSecond = ShotsPerSecond;
		magazineSize = MagazineSize;
		reload = reloadTime;
		range = Range;
		damage = DamagePerShot;
		increasedSpreadPerShot = incSpreadPerShot;
		decreasedSpreadPerSecond = decSpreadPerSecond;
		projectileSpeed = speed;
		
		shootTime = 1/ShotsPerSecond;
		bulletsInClip = magazineSize;
	}
	
	public void CreateStarterWeapon(){
		WeaponName = "M4";
		weaponType = 1;
		damage = 20;
		range = 20;
		initialAccuracy = 0;
		accuracy = 20;
		shotsPerSecond = 10;
		magazineSize = 30;
		reload = 2;
		shotsPerRound = 1;
		increasedSpreadPerShot = 3;
		decreasedSpreadPerSecond = 20;
		projectileSpeed = 17;
		
		shootTime = 1/shotsPerSecond;
		bulletsInClip = magazineSize;
		invintorySlot = 0;
	}
	
	public void CreateStarterPistol(){
		WeaponName = "Generic Pistol";
		weaponType = 1;
		damage = 35;
		range = 20;
		initialAccuracy = 0;
		accuracy = 20;
		shotsPerSecond = 5;
		magazineSize = 10;
		reload = 1;
		shotsPerRound = 1;
		increasedSpreadPerShot = 3;
		decreasedSpreadPerSecond = 20;
		projectileSpeed = 6;
		
		shootTime = 1/shotsPerSecond;
		bulletsInClip = magazineSize;
		invintorySlot = 1;
	}
	
	public void CreateTestRocketLauncher(){
		WeaponName = "Rocket Launcher";
		weaponType = 3;
		damage = 100;
		range = 20;
		initialAccuracy = 0;
		accuracy = 20;
		shotsPerSecond = 1;
		magazineSize = 10;
		reload = 1;
		shotsPerRound = 1;
		increasedSpreadPerShot = 0;
		decreasedSpreadPerSecond = 0;
		
		shootTime = 1/shotsPerSecond;
		bulletsInClip = magazineSize;
		invintorySlot = 1;
	}
	
	public int getWeaponType(){
		return weaponType;
	}
	
	public int getShotsPerRound(){
		return shotsPerRound;
	}
	
	public float getAccuracy(){
		return accuracy;
	}
	
	public float getShotsPerSecond(){
		return shotsPerSecond;
	}
	
	public int getMagazineSize(){
		return magazineSize;
	}
	
	public float getRange(){
		return range;
	}
	
	public float getDamage(){
		return damage;
	}
	
	public float getShootTime(){
		return shootTime;	
	}
	
	public float getInitialAccuracy(){
		return initialAccuracy;
	}
	public float getIncreasedSpreadPerShot(){
		return increasedSpreadPerShot;
	}
	public float getDecreasedSpreadPerSecond(){
		return decreasedSpreadPerSecond;
	}
	public float getReloadTime(){
		return reload;
	}
	
	public int getBulletsInClip(){
		return bulletsInClip;
	}
	public void setBulletsInClip(int bullets){
		bulletsInClip = bullets;
	}
	public int getInvintorySlot(){
		return invintorySlot;
	}
}
