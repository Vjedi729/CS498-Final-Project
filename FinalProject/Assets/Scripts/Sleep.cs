using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sleep : MonoBehaviour {

	private ResourceManager rm;
	private Collider sleepCollider;
	private Image cover;

	void Awake () {
		sleepCollider = gameObject.GetComponent<Collider> ();
		cover = GameObject.Find ("CenterEyeAnchor").GetComponentInChildren<Image> ();
		sleepCollider.enabled = false;
		Invoke ("turnOnSleepCollider", 2.0f);
		cover.CrossFadeAlpha (0, 2.0f, false);
	}

	// Use this for initialization
	void Start () {
		rm = GameObject.FindObjectOfType<ResourceManager> ();
	}

	void turnOnSleepCollider ()
	{
		sleepCollider.enabled = true;
	}
	
	void OnTriggerEnter (Collider coll) 
	{
		//be sure to make goToSleep public in the resouce manager
		if (coll.gameObject.CompareTag ("MainCamera")) 
		{
			//rm.goToSleep ();
			//StartCoroutine(fadeToBlack());
			cover.CrossFadeAlpha (1, 1.0f, false);
		}
			
	}

	void OnTriggerExit(Collider coll) {

		if (coll.gameObject.CompareTag ("MainCamera")) {
			cover.CrossFadeAlpha (0, 2.0f, false);
		}
	}

	IEnumerator fadeToBlack ()
	{
		cover.CrossFadeAlpha (1, 1.0f, false);
		yield return new WaitForSeconds (1.0f);
		cover.CrossFadeAlpha (0, 2.0f, false);
		yield return null;
	}
}
