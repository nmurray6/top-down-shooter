using UnityEngine;
using System.Collections;

public class FlashLight : MonoBehaviour {
	
	new Light light;
	Transform myTransform;
	
	public float scale = 1;

	// Use this for initialization
	void Start () {
		myTransform = transform;
		
		GameObject obj = new GameObject();
		obj.AddComponent<Light>();
		light = obj.GetComponent<Light>();
		light.type = LightType.Spot;
		light.range = 20;
		light.transform.position = transform.position + transform.forward * .2f;
		light.transform.rotation = myTransform.rotation;
		light.gameObject.name = "Flashlight";
	}
	
	// Update is called once per frame
	void Update () {
		light.transform.position = transform.position + transform.forward * .2f;
		light.transform.rotation = transform.rotation;
		
		Time.timeScale = scale;
	}
}
