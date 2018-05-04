using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGlobalScale : MonoBehaviour {

	public Vector3 globalScale;
	public Transform transform;

	public bool scaleX, scaleY, scaleZ;

	private bool scaled = false;

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
		Vector3 oldScale = transform.localScale;

		transform.localScale = Vector3.one;
		transform.localScale = new Vector3 (
			(scaleX ? globalScale.x/transform.lossyScale.x : oldScale.x), 
			(scaleY ? globalScale.y/transform.lossyScale.y : oldScale.y), 
			(scaleZ ? globalScale.z/transform.lossyScale.z : oldScale.z)
		);
	}

	public void setScale(Vector3 newScale){
		globalScale = newScale;
		resetScale ();
	}
}
