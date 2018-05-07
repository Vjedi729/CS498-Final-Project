using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour {

	private float eatDistance = 0.175f;
	private GameObject eyeCenter;

	// Use this for initialization
	void Start () {
		eyeCenter = GameObject.Find ("CenterEyeAnchor");
	}
	
	// Update is called once per frame
	void Update () {
		if ((this.gameObject.transform.position - eyeCenter.transform.position).magnitude < eatDistance) {
			// Are we being grabbed right now? (Check both kinds)
			if (this.gameObject.GetComponent<OVRGrabbable>() != null && this.gameObject.GetComponent<OVRGrabbable> ().isGrabbed ) {
				GameObject.Destroy (this.gameObject);
			}

			if (this.gameObject.GetComponent<SpringGrabbable>() != null && this.gameObject.GetComponent<SpringGrabbable> ().isGrabbed ) {
				GameObject.Destroy (this.gameObject);
			}
		}
	}
}
