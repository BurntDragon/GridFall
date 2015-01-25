using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CtrlUiManagerGame : MonoBehaviour 
{
	private Game gameInstance;
	public float timeLeft = 300;

	public Text GoTimeLeft;

	private int a = 0;
	private int b = 0;

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

			a = (int) Mathf.Round(timeLeft / 60);
			b = (int) Mathf.Round(timeLeft % 60);

			GoTimeLeft.text = a.ToString() + ":" + b.ToString();

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
