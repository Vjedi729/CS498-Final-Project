using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

	private HingeJoint hinge;
	public float rawPosition;
	public float histeresisInDegrees = 20f;
	public bool state;

	// Use this for initialization
	void Start () {
		hinge = GetComponentInChildren<HingeJoint> ();
	}
	
	// Update is called once per frame
	void Update () {
		rawPosition = hinge.angle;
		if (state && rawPosition > histeresisInDegrees) {
			state = !state;
		}
		if (!state && rawPosition < -histeresisInDegrees) {
			state = !state;
		}
		//state = rawPosition > 0;
	}
}
