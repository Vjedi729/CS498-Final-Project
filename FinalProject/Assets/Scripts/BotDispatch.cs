using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotDispatch : MonoBehaviour {

	public GameObject[] botStatuses;

	private Bot[] bots;
	private Image[] BusyIcons;
	private Image[] BrokeIcons;

	void Start ()
	{
		bots = new Bot[4];
		BusyIcons = new Image[4];
		BrokeIcons = new Image[4];
		for (int i = 0; i < botStatuses.Length; i++)
		{
			bots[i] = new Bot();
			BusyIcons[i] = botStatuses[i].transform.Find("Busy Icon").GetComponent<Image>();
			BrokeIcons[i] = botStatuses[i].transform.Find("Broken Icon").GetComponent<Image>();

			BusyIcons[i].enabled = false;
			BrokeIcons[i].enabled = false;
		}
	}

	public class Bot
	{
		public bool busy;
		public bool broke;

		public Bot() 
		{
			busy = broke = false;
		}
	}

	//returns true if a bot is available for a task
	//it will return to being free after the time alloted
	public bool getBot (float timeNeeded)
	{
		for(int i = 0; i < bots.Length; i++)
		{
			if (!bots[i].busy & !bots[i].broke)
			{
				bots[i].busy = true;
				StartCoroutine(returnBotInSeconds(i, timeNeeded));
				UpdateBotIcons();
				return true;
			}
		}

		//all bots busy or broken
		return false;
	}

	//restores a bot after being broken
	void returnBot (int botNumber)
	{
		bots[botNumber].busy = bots[botNumber].broke = false;
	}

	IEnumerator returnBotInSeconds (int botNumber, float time)
	{
		yield return new WaitForSeconds(time);
		returnBot(botNumber);
		UpdateBotIcons();
	}


	void UpdateBotIcons ()
	{
		for (int i = 0; i < bots.Length; i++)
		{
			if (bots [i].busy)
			{
				BusyIcons [i].enabled = true;
			} else
			{
				BusyIcons [i].enabled = false;
			}

			if (bots [i].broke)
			{
				BrokeIcons [i].enabled = true;
			} else
			{
				BrokeIcons [i].enabled = false;
			}
		}
	}
}
