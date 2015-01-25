using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour 
{
	public GameObject[] goPlayerPrefabs;

	public GameObject gameUi;
	public GameObject gameButtonScreen;

	public List<string> playerIDs = new List<string>();
	public List<Point> spawnPoints = new List<Point>();

	public bool isStarted;
	public bool isFinished;

	CtrlGrid grid;

	private bool initGrid = false;

	void Awake () 
	{
			grid = GameObject.Find ("Grid").GetComponent<CtrlGrid> ();
			spawnPoints.Add (new Point (0,0));
			spawnPoints.Add (new Point (grid.width-1,0));
			spawnPoints.Add (new Point (0,grid.height-1));
			spawnPoints.Add (new Point (grid.width-1,grid.height-1));
	}
	
	void Update () 
	{
		if (networkView.isMine)
		{
			if (checkIfWon()) 
			{
				GameObject.Find("Main").GetComponent<NetworkManager>().gameUi.SetActive(false);
				GameObject.Find("Main").GetComponent<NetworkManager>().gameWin.SetActive(true);
			}
		}

		if (isStarted && !initGrid) 
		{
			grid.transform.localScale = new Vector3 (1, 1, 1);
			initGrid = true;

			gameButtonScreen.SetActive(false);
			gameUi.SetActive(true);
		}
	}

	public bool checkIfWon()
	{
		bool won = false;
		Transform playersT = GameObject.Find ("Players").transform;
		for (int i=0;i<playersT.childCount;i++)
		{
			Player p=playersT.GetChild(i).gameObject.GetComponent<Player>();
			if (p!=null)
			{
				won = won & p.hasWon();
			}
		}

		return won;
	}


	public void addPlayer(int id)
	{
		// Debug.Log ("Game - addPlayer - id" + id);
		GameObject player = (GameObject) Network.Instantiate(goPlayerPrefabs[id], Vector3.zero, Quaternion.identity, 0);;

		player.GetComponent<Player>().isMe = player.GetComponent<NetworkView>().isMine;

		player.GetComponent<Player> ().pos = new Point (spawnPoints [id].x, spawnPoints [id].y);
		player.GetComponent<Player> ().spawnPoint = spawnPoints [id];
		player.transform.localPosition = Vector3.zero;
		playerIDs.Add (id.ToString());
	}


	public void startGame()
	{
		isStarted = true;
		// Debug.Log("Game Has freaking σtarted!");
	}
	
	void  OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.Serialize(ref this.isStarted);
		}
		else
		{
			bool ss=false;
			stream.Serialize(ref ss);
			this.isStarted=ss;
		}
	}
}
