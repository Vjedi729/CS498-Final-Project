using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

	public ResourceManager resources;
	public FarmManager farm;
	public BotDispatch botDispatch;

	public GameObject[] resourceGroups;
	private int currentResourceGroup = 0;
	public Button increaseBtn, decreaseBtn, nextPlotBtn, prevPlotBtn, nextResourceGroupBtn, prevResourceGroupBtn, contextBtn;

	private delegate void ContextualFunction();
	ContextualFunction plantCullOrHarvest;

	public TextMeshProUGUI contextualAction;
	public TextMeshProUGUI quantity;
	public TextMeshProUGUI cropSize;

	private int currentPlotNum = 0;
	public TextMeshProUGUI currentPlot;

	public Slider maturity;
	private Image maturityFill;

	private Color green = Color.green;
	private Color red = Color.red;
	private bool currentlyChangingColor = false;

	public float autoResourceScreenChangeTime = 5.0f;
	public float resouceScreenStayTimeAfterPress = 10.0f;

	// Use this for initialization
	void Start ()
	{
		nextResourceGroupBtn.onClick.AddListener (delegate { changeResourceTab (1); } );
		prevResourceGroupBtn.onClick.AddListener (delegate { changeResourceTab (-1); } );

		increaseBtn.onClick.AddListener (delegate { changeQuantity (1); } );
		decreaseBtn.onClick.AddListener (delegate { changeQuantity (-1); } );

		plantCullOrHarvest = plant;
		contextBtn.onClick.AddListener (chooseAFunction);

		nextPlotBtn.onClick.AddListener (delegate { changePlot(1); } );
		prevPlotBtn.onClick.AddListener (delegate { changePlot(-1); } );

		maturityFill = maturity.GetComponentsInChildren<Image>()[1];

		//manually deactivate resource groups one and two so that
		//other scripts can find them
		autoChangeResourceTab();
		/*
		resourceGroups[1].SetActive(false);
		resourceGroups[2].SetActive(false);
		*/
	}

	void changeResourceTab (int change)
	{
		CancelInvoke ("autoChangeResourceTab");
		currentResourceGroup += change;

		//wraps around
		if (currentResourceGroup > 2)
		{
			currentResourceGroup = 0;
		} else if (currentResourceGroup < 0)
		{
			currentResourceGroup = 2;
		}

		//disable all but the proper resource group
		foreach (GameObject resourceGroup in resourceGroups)
		{
			if (resourceGroup != resourceGroups [currentResourceGroup])
			{
				resourceGroup.SetActive (false);
			} else
			{
				resourceGroup.SetActive (true);
			}
		}

		Invoke ("autoChangeResourceTab", resouceScreenStayTimeAfterPress);
	}

	void autoChangeResourceTab ()
	{
		currentResourceGroup += 1;

		//wraps around
		if (currentResourceGroup > 2)
		{
			currentResourceGroup = 0;
		} else if (currentResourceGroup < 0)
		{
			currentResourceGroup = 2;
		}

		//disable all but the proper resource group
		foreach (GameObject resourceGroup in resourceGroups)
		{
			if (resourceGroup != resourceGroups [currentResourceGroup])
			{
				resourceGroup.SetActive (false);
			} else
			{
				resourceGroup.SetActive (true);
			}
		}

		Invoke ("autoChangeResourceTab", autoResourceScreenChangeTime);
	}

	public void updatePlotInfo ()
	{
		maturity.value = farm.plots [currentPlotNum].maturity;
		maturityFill.color = Color.Lerp(red, green, maturity.value / maturity.maxValue);

		cropSize.text = farm.plots [currentPlotNum].cropSize.ToString ();
		updateContextualButton();
	}

	private void chooseAFunction() 
	{
		plantCullOrHarvest();
	}

	private void updateContextualButton () 
	{
		if (farm.plots [currentPlotNum].maturity == 100)
		{
			plantCullOrHarvest = harvest;
			contextualAction.text = "Harvest";
		} else if (farm.plots [currentPlotNum].inUse)
		{
			plantCullOrHarvest = cull;
			contextualAction.text = "Cull";
		} else
		{
			plantCullOrHarvest = plant;
			contextualAction.text = "Plant";
		}
	}


	void changeQuantity (int x)
	{
		int value = int.Parse (quantity.text);
		value += x;
		if (value < 0)
		{
			value = 0;
		} else if (value > 100)
		{
			value = 100;
		}

		quantity.text = value.ToString ();
	}



	void plant ()
	{
		if (int.Parse (quantity.text) > 0) {
			if (botDispatch.getBot (15.0f)) {
				farm.plots [currentPlotNum].plantCrop (int.Parse (quantity.text));
				quantity.text = 0.ToString ();
				updatePlotInfo ();
				return;
			}
		}

		if (!currentlyChangingColor)
		{
			StartCoroutine (requestDenied (contextBtn));
		}
	}

	void cull ()
	{

		if (int.Parse (quantity.text) > 0) {
			if (botDispatch.getBot (15.0f)) {
				farm.plots [currentPlotNum].cullCrop (int.Parse (quantity.text));
				quantity.text = 0.ToString ();
				updatePlotInfo ();
				return;
			}
		}

		if (!currentlyChangingColor)
		{
			StartCoroutine (requestDenied (contextBtn));
		}
	}

	void harvest ()
	{

		int harvestSize = farm.plots [currentPlotNum].harvestCrop ();
		if (harvestSize != 0)
		{
			if (botDispatch.getBot (15.0f))
			{
				quantity.text = 0.ToString ();
				resources.addResource ("Food", harvestSize);
				return;
			}
		}
		if (!currentlyChangingColor)
		{
			StartCoroutine (requestDenied (contextBtn));
		}
	}

	//change plot number and update the canvas
	void changePlot (int change)
	{

		currentPlotNum += change;
		if (currentPlotNum >= farm.noPlots)
		{
			currentPlotNum = 0;
		} else if (currentPlotNum < 0) 
		{
			currentPlotNum = farm.noPlots - 1;
		}

		currentPlot.text = currentPlotNum.ToString();
		updatePlotInfo ();
	}

	//flash button red when the action can't be performed
	IEnumerator requestDenied (Button btn) 
	{
		currentlyChangingColor = true;


		Image img = btn.gameObject.GetComponent<Image>();
		Color originalColor = img.color;
		float timeElapsed = 0f;

		//arbitrary value chosen after some testing
		//time it takes to change to red and back
		const float time = 0.1f;

		while (timeElapsed < time)
		{
			timeElapsed += Time.deltaTime;
			img.color = Color.Lerp(originalColor, red, timeElapsed/time);
			yield return null;
		}

		timeElapsed = 0f;

		while (timeElapsed < time)
		{
			timeElapsed += Time.deltaTime;
			img.color = Color.Lerp(red, originalColor, timeElapsed/time);
			yield return null;
		}

		currentlyChangingColor = false;
		yield return null;
	}


}
