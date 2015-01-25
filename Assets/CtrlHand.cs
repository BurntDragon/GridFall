using UnityEngine;
using System.Collections;

public class CtrlHand : MonoBehaviour 
{
	private Game gameInstance;
	private bool initHand = false;

	void Awake()
	{
		gameInstance = GameObject.Find("Main").GetComponent<Game>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameInstance.isStarted && !initHand) 
		{
			GetComponent<Animator>().enabled = true;
		}
	}
}
