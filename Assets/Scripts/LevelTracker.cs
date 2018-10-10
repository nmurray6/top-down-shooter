using UnityEngine;
using System.Collections;

public class LevelTracker : MonoBehaviour {
	
	public int Level;
	int startingMonsterCount = 25;
	float levelModifier = .25f;
	public float monstersKilled = 0;
	public float killsNeededToLevelUp = 5;
	
	void Start () {
		Level = 0;
		monstersKilled = 0;
		killsNeededToLevelUp = killsToLevelUp();
	}
	
	void Update () {
		if(monstersKilled >= killsNeededToLevelUp){
			monstersKilled -= killsNeededToLevelUp;
			Level++;
			killsNeededToLevelUp = killsToLevelUp();
		}
	}
	
	float killsToLevelUp(){
		return startingMonsterCount * Mathf.Pow((1+levelModifier), Level);
	}
}
