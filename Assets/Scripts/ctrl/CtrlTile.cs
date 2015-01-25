using UnityEngine;
using System.Collections;

public class CtrlTile : MonoBehaviour 
{
	enum Direction { up, down, left, right};

	public bool isPath = false;
	public bool isPlayerClose = false;
	//public bool isCountingDown = false;

	public float timeLeftOnTile = 0;

	private GameObject GoNext;
	private GameObject GoPossible;

	void Awake()
	{
		GoPossible = transform.Find("possiblenext").gameObject;
		GoNext = transform.Find("next").gameObject;
	}

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
		Debug.Log("onTileMarkNext");
		GoNext.SetActive (true);
	}

	public void onTileUnMarkNext()
	{
		GoNext.SetActive (false);
	}

	public void onTilePossibleNext()
	{
		GoPossible.SetActive (true);
	}

	public void onTileUnPossibleNext()
	{
		GoPossible.SetActive (false);
	}


	public void onTileDown()
	{
		timeLeftOnTile = 0;
		GetComponent<Animator>().SetBool("goUp", false);
		EvtManager.onTileDown(this.name);
	}
}
