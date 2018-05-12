using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirRecycler : MonoBehaviour {

	bool hasUpdated = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.childCount == 5 && !hasUpdated) {
			hasUpdated = true;

			GetComponent<MeshCollider> ().convex = true;
			GetComponent<Rigidbody> ().isKinematic = false;
			GetComponent<Rigidbody> ().useGravity = true;
			this.gameObject.AddComponent<OVRGrabbable> ();
			OVRGrabbable ovr = GetComponent<OVRGrabbable> ();
			ovr.enabled = true;

			Collider[] cols = new Collider[] {GetComponent<MeshCollider> ()};

			ovr.setColliders (cols);
		}
	}
}
