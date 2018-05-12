using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxDistanceSpring : MonoBehaviour {

	public GameObject connectedBody;
	private Rigidbody connectedRB;

	private FixedJoint fj;
	private SpringJoint sj;
	public float maxDistance = 0.25f;
	public float minDistance = 0.05f;

	private bool isFixed = false;

	// Use this for initialization
	void Start () {
		//sj = GetComponent<SpringJoint> ();
		connectedRB = connectedBody.GetComponent<Rigidbody>();

		float distance = (transform.position - connectedBody.transform.position).magnitude;
		if (distance < minDistance) {
			isFixed = true;
			this.gameObject.AddComponent<FixedJoint> ();
			fj = GetComponent<FixedJoint> ();
			fj.connectedBody = connectedRB;
		} else {
			this.gameObject.AddComponent<SpringJoint> ();
			sj = GetComponent<SpringJoint> ();
			sj.connectedBody = connectedRB;

			sj.anchor = new Vector3 (0.25f, 0, 0);
			sj.autoConfigureConnectedAnchor = false;
			sj.connectedAnchor = new Vector3 (1f, 0, 0);
			sj.spring = 500f;
			sj.enableCollision = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		float distance = (transform.position - sj.connectedBody.transform.position).magnitude;
		Debug.Log (distance);

		if (distance > maxDistance) {
			sj.minDistance = 1000f;
		} else {
			sj.minDistance = 0f;
			if (distance < minDistance) {
				//GameObject.Destroy (sj);
				this.gameObject.AddComponent<FixedJoint> ();
				fj = GetComponent<FixedJoint> ();
				fj.connectedBody = sj.connectedBody;

				GameObject.Destroy (sj);
			}
		}
	}
}
