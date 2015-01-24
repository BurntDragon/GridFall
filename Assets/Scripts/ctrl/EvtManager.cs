using UnityEngine;
using System.Collections;

public class EvtManager : MonoBehaviour
{
	public static Game gameInstance;

	public delegate void playerAction(int player);
	public static event playerAction playerFell;
	public static event playerAction playerMove;
	public static event playerAction playerReachedEnd;

	public delegate void tileAction(string tile);
	public static event tileAction tileUp;
	public static event tileAction tileWarn;
	public static event tileAction tileDown;

	//public delegate void junctionAction(string nextPathToFollow, int nodeToFollow);
	//public static event junctionAction nextPathToFollow;

	void Awake()
	{
		gameInstance = GameObject.Find("Main").GetComponent<Game>();
	}

	public void onGameStart()
	{
		// Debug.Log ("EvtManager - onGameStart");
		gameInstance.isStarted = true;
	}
	
	public static void onGameFinish()
	{
		// Debug.Log ("EvtManager - onGameFinish");
		gameInstance.isFinished = true;
	}
	
	public static void onPlayerFell(int player)
	{
		// Debug.Log ("EvtManager - onPlayerFell" + player.ToString());

		if(playerFell != null)
		{	
			playerFell(player);
		}
	}

	public static void onPlayerReachedEnd(int player)
	{
		// Debug.Log ("EvtManager - onPlayerReachedEnd" + player.ToString());
		if(playerReachedEnd != null)
		{	
			playerReachedEnd(player);
		}
	}

	public static void onTileDown(string tile)
	{
		// Debug.Log ("EvtManager - onTileDown" + tile);
		
		if(tileDown != null)
		{	
			tileDown(tile);
		}
	}
	
	public static void onTileWarn(string tile)
	{
		// Debug.Log ("EvtManager - onTileWarn" + tile);
		if(tileWarn != null)
		{	
			tileWarn(tile);
		}
	}

	public static void onTileUp(string tile)
	{
		// Debug.Log ("EvtManager - onTileUp" + tile);
		
		if(tileUp != null)
		{	
			tileUp(tile);
		}
	}
}