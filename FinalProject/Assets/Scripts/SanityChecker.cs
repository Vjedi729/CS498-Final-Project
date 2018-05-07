using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityChecker : MonoBehaviour {

	private static List<GameObject> gameObjects = new List<GameObject> ();

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < gameObjects.Count; i++) {
			GameObject go = gameObjects [i];
			if (go == null) {
				continue;
			}

			Vector3 pos = go.transform.position;
			Quaternion rot = go.transform.rotation;

			if (float.IsNaN (pos.x) || float.IsNaN (pos.y) || float.IsNaN (pos.z)) {
				Debug.LogWarning ("Object \"" + go.name + "\" had invalid position. Removing.");
				gameObjects.Remove (go);
				GameObject.Destroy (go);
			}

			if (float.IsNaN (rot.x) || float.IsNaN (rot.y) || float.IsNaN (rot.z) || float.IsNaN (rot.w)) {
				Debug.LogWarning ("Object \"" + go.name + "\" had invalid position. Removing.");
				gameObjects.Remove (go);
				GameObject.Destroy (go);
			}
		}
	}

	public static void addToSanityChecker(GameObject obj) {
		gameObjects.Add (obj);
	}

	public static void removeFromSanityChecker(GameObject obj) {
		gameObjects.Remove (obj);
	}
}
