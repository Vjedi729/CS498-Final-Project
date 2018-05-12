using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetJoint : MonoBehaviour {

	public GameObject connectedObj; // The object we're tied to
	private Rigidbody connectedBody;
	private Rigidbody body;

	public float force = 1f; // Force value (1 is a good default)
	private Vector3 forceVector;
	public float minDistance = .1f; // The point where the two objects snap together
	public float maxDistance = 100f; // The point where no force is applied

	public Vector3 attachmentOffset; // Where this object's magnet is
	public Vector3 connectedAttachmentOffset; // Where the connected object's magnet is

	// When the objects snap together, what is the offset of the connected body?
	public Vector3 finalAttachedOffset;

	// Use this for initialization
	void Start () {
		forceVector = new Vector3 (force, force, force);
		connectedBody = connectedObj.GetComponent<Rigidbody> ();
		body = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 thisPosition = body.position;
		//Vector3 thatPosition = connectedBody.position;
		Vector3 thisPosition = body.position + (body.rotation * attachmentOffset);
		Vector3 thatPosition = connectedBody.position + (connectedBody.rotation * connectedAttachmentOffset);
		Vector3 distanceVector = thisPosition - thatPosition;
		float distance = distanceVector.magnitude;
		//Debug.Log (distance);
		if (distance < minDistance) {
			// Set the position and rotation of the connected object to its ideal
			connectedObj.transform.rotation = this.gameObject.transform.rotation;
			connectedObj.transform.position = this.gameObject.transform.position + (this.gameObject.transform.rotation * finalAttachedOffset);

			// Set the connected object as a child of this object
			connectedObj.transform.SetParent (this.transform);
			GameObject.Destroy(connectedObj.GetComponent<OVRGrabbable> ());
			GameObject.Destroy (connectedObj.GetComponent<Rigidbody> ());

			// Destroy this script when done
			GameObject.Destroy (this);
			return;
		}
		if (distance > maxDistance) {
			return;
		}

		float invDistSqr = 1.0f / distanceVector.sqrMagnitude;

		Vector3 finalForceVector = distanceVector;
		finalForceVector.Scale (forceVector);
		finalForceVector.Scale(new Vector3 (invDistSqr, invDistSqr, invDistSqr));

		body.AddForceAtPosition (-finalForceVector, thisPosition);
		connectedBody.AddForceAtPosition (finalForceVector, thatPosition);
	}
}
