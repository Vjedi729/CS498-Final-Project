using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollider : MonoBehaviour {

	private float histeresis = 0.2f;
	private float lastPressTime = 0f;

	private UIManager manager;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("GameManager").GetComponent<UIManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter() {
		if (Time.fixedTime - lastPressTime > histeresis) {
			switch (gameObject.name) {
			case "IncreaseQuantity": 
				manager.increaseBtn.onClick.Invoke ();
				lastPressTime = Time.fixedTime;
				break;
			case "DecreaseQuantity":
				manager.decreaseBtn.onClick.Invoke ();
				lastPressTime = Time.fixedTime;
				break;
			case "PreviousPlot":
				manager.prevPlotBtn.onClick.Invoke ();
				lastPressTime = Time.fixedTime;
				break;
			case "NextPlot":
				manager.nextPlotBtn.onClick.Invoke ();
				lastPressTime = Time.fixedTime;
				break;
			case "ContextualBtn":
				manager.contextBtn.onClick.Invoke ();
				lastPressTime = Time.fixedTime;
				break;
			case "Next Page":
				manager.nextResourceGroupBtn.onClick.Invoke ();
				lastPressTime = Time.fixedTime;
				break;
			case "Previous Page":
				manager.prevResourceGroupBtn.onClick.Invoke ();
				lastPressTime = Time.fixedTime;
				break;
			default:
				break;
			}
		}

	}
}
