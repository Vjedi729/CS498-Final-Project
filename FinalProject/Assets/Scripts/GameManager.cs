using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Set tracking so that zero is the floor, rather than eye level.
		OVRManager.instance.trackingOriginType = OVRManager.TrackingOrigin.FloorLevel;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
