using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDispenser : MonoBehaviour {

	public GameObject triggeringLever;

	private bool leverState = false;
	private Lever leverComponent;

	// Use this for initialization
	void Start () {
		if (triggeringLever == null) {
			triggeringLever = GameObject.Find ("Lever");
		}
		leverComponent = triggeringLever.GetComponent<Lever> ();
		leverState = leverComponent.state;
	}
	
	// Update is called once per frame
	void Update () {
		bool newState = leverComponent.state;
		if (newState != leverState) {
			leverState = newState;
			GameObject waterBottle = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/WaterBottle"));
			waterBottle.transform.position = this.transform.position + new Vector3(0, 0.3f, 0);
			waterBottle.transform.rotation = this.transform.rotation * waterBottle.transform.rotation;
		}
	}
}
