using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AutomaticRoomScaler : MonoBehaviour {

	public GameObject wallXp, wallXn, wallZp, wallZn, floor, ceiling;

	public const float wallThickness = 0.1f;
	public const float yThickness = 1.0f;

	public const float wallHeight = 2.4384f; // = 8ft
	public const float defZ = 1.524f; // = 5 ft
	public const float defX = 2.1336f; // = 7 ft

	public float wallToSpaceBuffer;

	private Vector3 roomDim;
	public Announcer roomDone = new Announcer();

	// Use this for initialization
	void Start () {
		//gameObject.transform.position = OVRManager.tracker.GetPose (0).position;
		OVRManager.instance.trackingOriginType = OVRManager.TrackingOrigin.FloorLevel;

		//Debug.Log (transform.position);

		if (OVRManager.boundary.GetConfigured ()) {
			roomDim = OVRManager.boundary.GetDimensions (OVRBoundary.BoundaryType.PlayArea);
			roomDim.x -= 2*wallToSpaceBuffer;
			roomDim.z -= 2*wallToSpaceBuffer;
			roomDim.y = wallHeight;
		} else {
			roomDim = new Vector3 (defX, wallHeight, defZ);
		}

		wallXp.transform.localScale = wallXn.transform.localScale = getXscale ();
		wallZp.transform.localScale = wallZn.transform.localScale = getZscale ();
		ceiling.transform.localScale = floor.transform.localScale = getYscale ();

		wallXp.transform.position = getXpos (false);
		wallXn.transform.position = getXpos (true);;

		wallZp.transform.position = getZpos (false);
		wallZn.transform.position = getZpos (true);

		ceiling.transform.position = new Vector3 (0, wallHeight + (yThickness / 2), 0);
		floor.transform.position = new Vector3(0, -(yThickness/2), 0);

		roomDone.Announce ();
	}

	// Update is called once per frame
	void Update () {

	}

	Vector3 getXscale(){
		return new Vector3 (wallThickness, wallHeight /*+ 2*wallThickness*/, roomDim.z  /*+ 2*wallThickness*/);
	}

	Vector3 getZscale(){
		return new Vector3 (roomDim.x /*+ 2*wallThickness*/, wallHeight /*+ 2*wallThickness*/, wallThickness);
	}

	Vector3 getYscale(){
		return new Vector3 (roomDim.x /*+ 2*wallThickness*/, yThickness, roomDim.z /*+ 2*wallThickness*/);
	}

	Vector3 getZpos(bool negative){
		if (negative) {
			return new Vector3 (0, (wallHeight /*+ wallThickness*/)/2, -(roomDim.z + wallThickness) / 2);
		}
		return new Vector3 (0, (wallHeight /*+ wallThickness*/)/2, (roomDim.z + wallThickness) / 2);
	}

	Vector3 getXpos(bool negative){
		if (negative) {
			return new Vector3 ( -(roomDim.x + wallThickness) / 2, (wallHeight /*+ wallThickness*/) / 2, 0);
		}
		return new Vector3 ((roomDim.x + wallThickness) / 2, (wallHeight /*+ wallThickness*/) / 2, 0);
	}

	/*
	 * Gets a position in the scaled room based on a Vector 3 where:
	 * X is between -1 and 1, with 0 being the center of the room
	 * Z is between -1 and 1, with 0 being the center of the room
	 * Y is between 0 and 1, with 0 being the floor
	 */
	public Vector3 getPosInRoom(Vector3 inRoomLocation){
		return new Vector3 (inRoomLocation.x * roomDim.x / 2, inRoomLocation.y * roomDim.y, inRoomLocation.z * roomDim.z / 2);
	}
		
}