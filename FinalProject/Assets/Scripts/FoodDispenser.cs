using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDispenser : MonoBehaviour {

	public GameObject triggeringLever;
	public int maxNumberOfItems = 10;
	public Vector3 spawnOffset = new Vector3(0, 0, 0);

	private ResourceManager rm;
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
		rm = GameObject.FindObjectOfType<ResourceManager> ();
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

			// Try to consume the resources and spawn in an object
			if (rm.loseResource ("Food", 5)) {
				GameObject apple = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/Apple"));
				apple.transform.position = this.transform.position + spawnOffset;
				apple.transform.rotation = this.transform.rotation * apple.transform.rotation;

				SanityChecker.addToSanityChecker (apple);

				itemBuffer.Add (apple);
				curNumberOfItems++;
			}

		}
	}
}
