using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	float maxTimeLeftOnTile=10;
	float timeLeftOnTile=0;

	// Use this for initialization
	void Start () 
	{
		timeLeftOnTile = maxTimeLeftOnTile;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeLeftOnTile -= Time.deltaTime;
		if (timeLeftOnTile < 0) 
		{
//			EvtManager.fell(this);
		}
	}
}
