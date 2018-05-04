using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGlobalScale : MonoBehaviour {

	public Vector3 globalScale;
	public Transform transform;

	bool scaled = false;

	// Use this for initialization
	void Start () {
		resetScale ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!scaled) {
			resetScale ();
		}
	}

	public void resetScale (){
		transform.localScale = Vector3.one;
		transform.localScale = new Vector3 (
			globalScale.x/transform.lossyScale.x, 
			globalScale.y/transform.lossyScale.y, 
			globalScale.z/transform.lossyScale.z
		);
	}

	public void setScale(Vector3 newScale){
		globalScale = newScale;
		resetScale ();
	}
}
