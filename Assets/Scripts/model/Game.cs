using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	float timeLeft=300;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		if (timeLeft<0)
		{
//			EvtManager.gameOver();
		}
	}
}
