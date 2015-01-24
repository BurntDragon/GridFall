using UnityEngine;
using System.Collections;

public class CtrlTile : MonoBehaviour 
{
	enum Direction { up, down, left, right};

	public bool isPath = false;
	public bool isPlayerClose = false;
	//public bool isCountingDown = false;

	public float timeLeftOnTile = 0;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timeLeftOnTile > 0) 
		{
			timeLeftOnTile -= Time.deltaTime;

			if (timeLeftOnTile<0)
			{
				onTileDown();
			}
		}
	}

	public void onTileUp(float timeToFall)
	{
		timeLeftOnTile = timeToFall;

		if (!GetComponent<Animator> ().enabled) 
		{
				GetComponent<Animator> ().enabled = true;
		}else 
		{
				GetComponent<Animator> ().SetBool ("goUp", true);
		}

		EvtManager.onTileUp(this.name);
	}

	public void onTileWarn()
	{
		EvtManager.onTileWarn(this.name);
	}

	public void onTileMarkNext()
	{
		GetComponent<Animator>().SetBool("markForPossibleNext", true);
	}
	
	public void onTileDown()
	{
		GetComponent<Animator>().SetBool("goUp", false);
		EvtManager.onTileDown(this.name);
	}
}
