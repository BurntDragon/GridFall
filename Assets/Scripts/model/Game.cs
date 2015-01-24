using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour 
{
	public GameObject playerPrefab;

	public List<string> playerIDs = new List<string>();
	public List<Point> spawnPoints = new List<Point>();

	public bool isStarted;
	public bool isFinished;

	void Start () 
	{
		CtrlGrid grid = GameObject.Find ("Grid").GetComponent<CtrlGrid> ();
		spawnPoints.Add (new Point (0,0));
		spawnPoints.Add (new Point (grid.width-1,0));
		spawnPoints.Add (new Point (0,grid.height-1));
		spawnPoints.Add (new Point (grid.width-1,grid.height-1));

		addPlayer ("1");
		addPlayer ("2");
		addPlayer ("3");
		addPlayer ("4");
	}
	
	void Update () 
	{

	}

	public void addPlayer(string id)
	{
		GameObject player = (GameObject) GameObject.Instantiate (playerPrefab);

		if ("1".Equals(id)) player.GetComponent<Player> ().isMe=true;

		player.name = "player" + id;
		player.transform.parent = GameObject.Find ("World").transform;
		player.GetComponent<Player> ().pos = new Point (spawnPoints [playerIDs.Count].x, spawnPoints [playerIDs.Count].y);
		player.GetComponent<Player> ().spawnPoint = spawnPoints [playerIDs.Count];
//		player.transform = new Vector3 (spawnPoints [playerIDs.Count].x, spawnPoints [playerIDs.Count].y);
		player.transform.localPosition = Vector3.zero;
		playerIDs.Add (id);

		if (playerIDs.Count == spawnPoints.Count) 
		{
			//TODO: start da game
			//isStarted = true;
		}
	}
}
