using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirRecycler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.childCount == 4) {
			Debug.Log ("Have 4 kids now");
			GetComponent<MeshCollider> ().convex = true;
			GetComponent<Rigidbody> ().isKinematic = false;
			//this.gameObject.AddComponent<OVRGrabbable> ();
		}
	}
}
