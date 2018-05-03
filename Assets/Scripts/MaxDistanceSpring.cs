using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxDistanceSpring : MonoBehaviour {

	SpringJoint sj;
	public float maxDistance = 0.25f;

	// Use this for initialization
	void Start () {
		sj = GetComponent<SpringJoint> ();
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position - sj.connectedBody.transform.position).magnitude > maxDistance) {
			sj.minDistance = 1000f;
		} else {
			sj.minDistance = sj.maxDistance = 0f;
		}
	}
}
