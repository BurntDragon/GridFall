using UnityEngine;
using System.Collections;

public class EvtManager : MonoBehaviour 
{
	public delegate void gameAction();
	public static event gameAction gameStart;
	public static event gameAction gameFinish;
	
	public delegate void playerAction(int player);
	public static event playerAction playerFell;
	public static event playerAction playerReachedEnd;

	//public delegate void junctionAction(string nextPathToFollow, int nodeToFollow);
	//public static event junctionAction nextPathToFollow;

	
	public static void onGameStart()
	{
		if(gameStart != null)
		{	
			gameStart();
		}
	}
	
	public static void onGameFinish()
	{
		if(gameFinish != null)
		{
			gameFinish();
		}
	}
	
	public static void onPlayerFell(int player)
	{
		if(playerFell != null)
		{	
			playerFell(player);
		}
	}

	public static void onPlayerReachedEnd(int player)
	{
		if(playerReachedEnd != null)
		{	
			playerReachedEnd(player);
		}
	}
}
