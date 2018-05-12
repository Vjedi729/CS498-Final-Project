using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour {

	public ResourceManager resourceManager;
	public UIManager UI;
	public farmPlot[] plots;
	public int noPlots;

	//hardcoded to 10 for now
	//public int ticksToMaturity

	private static readonly string[] FARM_UPKEEP_RESOURCES = {"Water", "Carbon", "Power"};

	public class farmPlot {

		public int cropSize = 0;
		public int maturity = 0;
		public bool inUse = false;

		public farmPlot ()
		{
			refreshPlot ();
		}

		//returns true if new crop successfully planted
		public bool plantCrop (int quantity)
		{
			if (!inUse && quantity > 0)
			{
				inUse = true;
				cropSize = quantity;
				maturity = 0;
				return true;
			} else
			{
				return false;
			}
		}

		public void cullCrop (int cullQuantity)
		{
			cropSize -= cullQuantity;
			if (cropSize < 0)
			{
				cropSize = 0;
			}

			if (cropSize == 0)
			{
				refreshPlot ();
			}
		}

		public int harvestCrop ()
		{
			if (maturity <= 10)
			{
				Debug.Log ("too soon to harvest plot");
				return 0;
			}

			int harvestSize = cropSize;
			refreshPlot ();
			return harvestSize;
		}

		private void refreshPlot ()
		{
			cropSize = maturity = 0;
			inUse = false;
		}

	}

	// Use this for initialization
	void Start ()
	{
		plots = new farmPlot[4];
		for (int i = 0; i < 4; i++)
		{
			plots [i] = new farmPlot ();
		}
		InvokeRepeating ("farmUpkeep", 0.0f, 5.0f);
	}

	void farmUpkeep ()
	{
		foreach (farmPlot plot in plots)
		{
			if (plot.inUse)
			{
				//TODO: Handle when the player can't meet the upkeep more elegantly/handle partial upkeeps at all
				if (resourceManager.loseResource (FARM_UPKEEP_RESOURCES, Mathf.CeilToInt (plot.cropSize / 20)))
				{
					plot.maturity += 5;
				} else
				{
					//lose 25% of crop if you can't meet the upkeep
					plot.cullCrop ((int)(plot.cropSize / 4));
				}
			}
		}

		UI.updatePlotInfo ();
	}
}
