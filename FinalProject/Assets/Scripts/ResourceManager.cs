using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

	public int hungerRate;
	public int thirstRate;
	public int sleepyRate;
	public int oxygenConsumedRate;
	public int powerConsumedRate;
	public int hydrogenGainedRate;

	private static readonly string[] resourceNames = {"Hydration", "Satiety", "Sleep", "Water", "Food", "Power", "Hydrogen", "Oxygen", "Carbon"};

	public GameObject resourceSlidersParent;
	private Slider[] resourceSliders;

	private Dictionary<string, int> resources;
	private Dictionary<string, Image> sliderFills;

	private int carbonWaste = 0;
	private int waterWaste = 0;

	private Color max = Color.green;
	private Color min = Color.red;

	void Start ()
	{
		initializeResources ();

		//call applies the over-time-rates to each resource that has one every 30 seconds
		InvokeRepeating ("resourceTick", 0.0f, 30.0f);
	}

	void Awake () 
	{
		resourceSliders = resourceSlidersParent.GetComponentsInChildren<Slider>();
	}

	//initializes both the resource and sliderFill tables
	void initializeResources ()
	{
		resources = new Dictionary<string, int> ();
		sliderFills = new Dictionary<string, Image> ();

		foreach (string resource in resourceNames)
		{
			resources.Add (resource, 100);
		}

		foreach (Slider slider in resourceSliders)
		{
			sliderFills.Add(slider.name, slider.GetComponentsInChildren<Image>()[1]);
		}
	}

	void updateSliders ()
	{
		//updates all sliders in resourceSliders so long as their names correspond to a resource
		foreach (Slider slider in resourceSliders)
		{
			slider.value = resources[slider.name];

			//changes slider color to reflect the current value
			sliderFills[slider.name].color = Color.Lerp(min, max, slider.value/slider.maxValue);
		}
	}
		
	//applies all rates
	void resourceTick ()
	{
		loseResource ("Hydration", thirstRate);
		loseResource ("Satiety", hungerRate);
		loseResource ("Sleep", sleepyRate);
		loseResource ("Oxygen", oxygenConsumedRate);
		loseResource ("Power", powerConsumedRate);
		addResource ("Hydrogen", hydrogenGainedRate);

		updateSliders();

	}

	bool checkResourcesForGameOver ()
	{
		if (resources ["Satiety"] <= 0 ||
		    resources ["Hydration"] <= 0 ||
		    resources ["Sleep"] <= 0 ||
		    resources ["Oxygen"] <= 0)
		{
			return true;
		} else
		{
			return false;
		}
	}

	//adds quantity to resource input
	public void addResource (string resourceGained, int quantity)
	{
		resources [resourceGained] += quantity;
		if (resources [resourceGained] > 100)
		{
			resources [resourceGained] = 100;
		}

		updateSliders();
	}

	//adds quantity to each resource input
	public void addResource (string[] resourcesGained, int quantity)
	{
		foreach (string resource in resourcesGained)
		{
			resources [resource] += quantity;

			if (resources [resource] > 100)
			{
			resources [resource] = 100;
			}
		}
		updateSliders();
	}

	//removes quantity to resource input
	public bool loseResource (string resourceLost, int quantity)
	{

		if (resources [resourceLost] < quantity)
		{
			return false;
		}

		resources [resourceLost] -= quantity;
		updateSliders();
		return true;
	}

	//removes quantity from each resource input
	public bool loseResource (string[] resourcesLost, int quantity)
	{
		//check to see if this amount of resources can even be lost
		foreach (string resource in resourcesLost)
		{
			if (resources [resource] < quantity)
			{
				return false;
			}
		}

		foreach (string resource in resourcesLost)
		{
			resources [resource] -= quantity;
		}

		updateSliders();
		return true;
	}

	public void addWaste (int solidWaste, int liquidWaste)
	{
		carbonWaste += solidWaste;
		waterWaste += liquidWaste;
	}

	public void goToSleep ()
	{
		Debug.Log ("BOTTOM TEXT");
		while (resources ["Sleep"] < 100)
		{
			//stop sleeping if too hungry, thirsty, or air reserves are too low
			if (resources ["Satiety"] < 10 || resources ["Hydration"] < 10 || resources ["Oxygen"] < 5)
			{
				return;
			}

			//one tick per 5 sleep needed to be recovered
			addResource ("Sleep", 6);
			resourceTick ();

		}


		addResource ("Water", waterWaste);
		addResource ("Carbon", carbonWaste);
		waterWaste = carbonWaste = 0;

		//TODO: fade to black and back


	}


	
}
