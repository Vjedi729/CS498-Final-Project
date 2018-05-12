using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouldSanityCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SanityChecker.addToSanityChecker (this.gameObject);
	}
}
