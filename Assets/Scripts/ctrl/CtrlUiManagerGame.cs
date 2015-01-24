using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CtrlUiManagerGame : MonoBehaviour 
{
	private Game gameInstance;
	public float timeLeft = 300;

	public Text GoTimeLeft;

	// Use this for initialization
	void Awake () 
	{
		gameInstance = GameObject.Find("Main").GetComponent<Game>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameInstance.isStarted)
		{
			timeLeft -= Time.deltaTime;
			GoTimeLeft.text = "You all die in: " + System.Math.Round((double) timeLeft, 2).ToString();

			if (timeLeft < 0)
			{
				EvtManager.onGameFinish();
			}
		}
	}

	void onEnable()
	{
		//EvtManager.gameStart += onGameStart;6
	}

	void onDisable()
	{
		//EvtManager.gameStart -= onGameStart;
	}
}
