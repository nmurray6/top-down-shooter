using UnityEngine;
using System.Collections;

public class TestingScript : MonoBehaviour {
	
	Animation anim;
	int x = 0;
	public float spd = 1;
	
	// Use this for initialization
	void Start () {
		anim = transform.GetComponent<Animation>();
		
	}
	
	// Update is called once per frame
	void Update () {
		anim["Walk1"].speed = spd;
		anim.Play ("Walk1");
	}
}
