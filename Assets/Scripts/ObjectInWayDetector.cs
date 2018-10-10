using UnityEngine;
using System.Collections;

public class ObjectInWayDetector : MonoBehaviour {
	
	Transform myTransform;
	Transform target;

	void Start () {
		myTransform = transform;
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update () {
		Vector3 screenPosition = myTransform.GetComponent<Camera>().WorldToScreenPoint(target.position);
		Ray ray = myTransform.GetComponent<Camera>().ScreenPointToRay(screenPosition);
		
		RaycastHit[] hits = Physics.RaycastAll(ray, Vector3.Distance(myTransform.position, target.position), (1<<9));
		foreach(RaycastHit hit in hits){
			setTransparent(hit.transform);
		}
	}
	
	void setChildrenTransparent(Transform[] children){
		foreach(Transform child in children){
			Renderer R = child.GetComponent<Renderer>();
			if(R == null){}
			else{
				AutoTransparent AT = R.GetComponent<AutoTransparent>();
				if(AT == null)
					AT = R.gameObject.AddComponent<AutoTransparent>();
				else
					AT.currentTransparency = AT.targetTransparency;
			}
		}
	}
	
	void setTransparent(Transform obj){
		Renderer R = obj.GetComponent<Renderer>();
		if(R == null){
			if(obj.childCount > 0){
				setChildrenTransparent(obj.GetComponentsInChildren<Transform>());
			}
		}
		else{
			if(obj.parent != null){
				setTransparent(obj.parent);
				return;
			}
			AutoTransparent AT = R.GetComponent<AutoTransparent>();
			if(AT == null)
				AT = R.gameObject.AddComponent<AutoTransparent>();
			else
				AT.currentTransparency = AT.targetTransparency;
			
			if(obj.childCount > 0){
				setChildrenTransparent(obj.GetComponentsInChildren<Transform>());
			}
		}
	}
}
