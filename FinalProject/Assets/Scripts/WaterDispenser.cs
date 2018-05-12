using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDispenser : MonoBehaviour {

	public GameObject triggeringLever;
	public int maxNumberOfItems = 10;
	public Vector3 spawnOffset = new Vector3(0, 0, 0);

	private bool leverState = false;
	private Lever leverComponent;
	private int curNumberOfItems;
	private List<GameObject> itemBuffer;


	// Use this for initialization
	void Start () {
		if (triggeringLever == null) {
			triggeringLever = GameObject.Find ("Lever");
		}
		leverComponent = triggeringLever.GetComponent<Lever> ();
		leverState = leverComponent.state;
		curNumberOfItems = 0;
		itemBuffer = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool newState = leverComponent.state;
		if (newState != leverState) {
			leverState = newState;

			if (curNumberOfItems >= maxNumberOfItems) {
				SanityChecker.removeFromSanityChecker (itemBuffer [0]);
				GameObject.Destroy (itemBuffer [0]);
				itemBuffer.RemoveAt (0);
			}
				
			GameObject waterBottle = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/WaterBottleFull"));
			waterBottle.transform.position = this.transform.position + spawnOffset;
			waterBottle.transform.rotation = this.transform.rotation * waterBottle.transform.rotation;

			SanityChecker.addToSanityChecker (waterBottle);

			itemBuffer.Add (waterBottle);
			curNumberOfItems++;
		}
	}
}
