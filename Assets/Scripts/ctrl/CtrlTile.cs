using UnityEngine;
using System.Collections;

public class CtrlTile : MonoBehaviour 
{
	enum Direction { up, down, left, right};

	public bool isPath = false;
	public bool isPlayerClose = false;
	public bool isCountingDown = false;

	public float timeLeftOnTile = 0;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isCountingDown) 
		{
			timeLeftOnTile -= Time.deltaTime;

			if (timeLeftOnTile<0)
			{
				EvtManager.onTileDown(this.name);
			}
		}
	}

	void onTileUp()
	{
		GetComponent<Animator>().SetBool("goUp", true);
	}

	void onTileWarn()
	{

	}

	void onTileDown()
	{
		GetComponent<Animator>().SetBool("goUp", false);
	}
}
