using UnityEngine;
using System.Collections;

public class AutoTransparent : MonoBehaviour {
	
	Material[] oldMaterials;
	Material[] currentMaterials;
	public float currentTransparency;
	public float targetTransparency = .2f;
	const float fallOff = .1f;
	
	void Start () {
		currentTransparency = targetTransparency;
		oldMaterials = GetComponent<Renderer>().materials;
		currentMaterials = new Material[oldMaterials.Length];
		
		int count;
		for(count = 0; count < oldMaterials.Length; count++){
			currentMaterials[count] = new Material(oldMaterials[count]);
			currentMaterials[count].shader = Shader.Find("Transparent/Diffuse");
		}
		
		GetComponent<Renderer>().materials = currentMaterials;
	}
	
	void Update () {
		if(currentTransparency < 1.0f){
			ChangeTransparency();
		}
		else{
			resetMaterials();
		}
		currentTransparency += ((1.0f-targetTransparency)*Time.deltaTime) / fallOff;
	}
	
	void ChangeTransparency(){
		Color c;
		foreach(Material mat in currentMaterials){
			c = mat.color;
			c.a = currentTransparency;
			mat.color = c;
		}
	}
	
	void resetMaterials(){
		GetComponent<Renderer>().materials = oldMaterials;
		Destroy(this);
	}
}
