using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionToRoom : MonoBehaviour {

	public Vector3 inRoomPosition;
	public Vector3 offset;

	public AutomaticRoomScaler roomScaler;

	#if UNITY_EDITOR
	public bool resetPos;
	#endif

	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR
		Debug.LogFormat("Registering for Room Done: {0}", gameObject.name);
		#endif
		roomScaler.roomDone.Register (resetPosition);
	}

	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		// Code for allowing testing of values for position/offset
		if (resetPos) {
			resetPosition ();
			resetPos = false;
		}
		#endif

		//Debug.Log (transform.position);
	}

	void resetPosition(){
		#if UNITY_EDITOR
		Debug.LogFormat("Setting Position To Room: {0}", gameObject.name);
		#endif

		transform.position = roomScaler.getPosInRoom (inRoomPosition) + offset;
	}
}
