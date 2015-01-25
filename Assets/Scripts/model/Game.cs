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

		if (playerIDs.Count == spawnPoints.Count) 
		{
			//TODO: start da game
			isStarted = true;
			Debug.Log("Game Has freaking σtarted!");
		}

	}
}
