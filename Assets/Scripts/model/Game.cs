using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour 
{
	public GameObject[] goPlayerPrefabs;

	public List<string> playerIDs = new List<string>();
	public List<Point> spawnPoints = new List<Point>();

	public bool isStarted;
	public bool isFinished;

	void Awake () 
	{
		CtrlGrid grid = GameObject.Find ("Grid").GetComponent<CtrlGrid> ();
		spawnPoints.Add (new Point (0,0));
		spawnPoints.Add (new Point (grid.width-1,0));
		spawnPoints.Add (new Point (0,grid.height-1));
		spawnPoints.Add (new Point (grid.width-1,grid.height-1));
	}
	
	void Update () 
	{
		if (checkIfWon()) 
		{
			GameObject.Find("Main").GetComponent<NetworkManager>().gameUi.SetActive(false);
			GameObject.Find("Main").GetComponent<NetworkManager>().gameWin.SetActive(true);
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
				won=p.hasWon();
			}
		}

		return won;
	}

	public void addPlayer(int id)
	{
		Debug.Log ("Game - addPlayer - id" + id);
		GameObject player = (GameObject) Network.Instantiate(goPlayerPrefabs[id], Vector3.zero, Quaternion.identity, 0);;

		player.GetComponent<Player>().isMe = player.GetComponent<NetworkView>().isMine;

		player.GetComponent<Player> ().pos = new Point (spawnPoints [id].x, spawnPoints [id].y);
		player.GetComponent<Player> ().spawnPoint = spawnPoints [id];
		player.transform.localPosition = Vector3.zero;
		playerIDs.Add (id.ToString());

		if (id==1) 
		{
			//TODO: start da game
			isStarted = true;
			Debug.Log("Game Has freaking σtarted!");
		}

	}
}
