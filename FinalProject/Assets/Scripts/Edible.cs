using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour {

	private ResourceManager rm;
	private int value = 5;

	private float eatDistance = 0.175f;
	private GameObject eyeCenter;

	void Start() {
		rm = GameObject.FindObjectOfType<ResourceManager> ();
		rm.loseResource ("Food", value);
	}

	// Use this for initialization
	void Awake () {
		eyeCenter = GameObject.Find ("CenterEyeAnchor");
	}
	
	// Update is called once per frame
	void Update () {
		if ((this.gameObject.transform.position - eyeCenter.transform.position).magnitude < eatDistance) {
			// Are we being grabbed right now? (Check both kinds)
			if (this.gameObject.GetComponent<OVRGrabbable>() != null && this.gameObject.GetComponent<OVRGrabbable> ().isGrabbed ) {
				rm.addResource ("Satiety", value);
				GameObject.Destroy (this.gameObject);
			}

			if (this.gameObject.GetComponent<SpringGrabbable>() != null && this.gameObject.GetComponent<SpringGrabbable> ().isGrabbed ) {
				rm.addResource ("Satiety", value);
				GameObject.Destroy (this.gameObject);
			}
		}
	}
}
