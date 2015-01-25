using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class SoundManager : MonoBehaviour 
{
	public AudioClip tileUp;
	public AudioClip tileWarn;
	public AudioClip tileDown;

	public AudioClip playerFell;
	public AudioClip playerMove;
	public AudioClip playerReachedEnd;
	
	//public AudioClip[] deposit;
	
	void OnEnable()
	{
		EvtManager.playerFell += playPlayerFell;
		EvtManager.playerMove += playPlayerMove;
		EvtManager.playerReachedEnd += playPlayerReachedEnd;
		EvtManager.tileUp += playTileUp;
		EvtManager.tileWarn += playTileWarn;
		EvtManager.tileDown += playTileDown;
	}
	
	void OnDisable()
	{
		EvtManager.playerFell -= playPlayerFell;
		EvtManager.playerMove -= playPlayerMove;
		EvtManager.playerReachedEnd -= playPlayerReachedEnd;
		EvtManager.tileUp -= playTileUp;
		EvtManager.tileWarn -= playTileWarn;
		EvtManager.tileDown -= playTileDown;
	}

	void playTileUp(string tile)
	{
		// // Debug.Log ("playTileUp" + tile);
		GetComponent<AudioSource>().PlayOneShot(tileUp);
	}
	
	void playTileWarn(string tile)
	{
		// // Debug.Log ("playTileWarn" + tile);
		GetComponent<AudioSource>().PlayOneShot(tileWarn);
	}
	
	void playTileDown(string tile)
	{
		// // Debug.Log ("playTileDown" + tile);
		GetComponent<AudioSource>().PlayOneShot(tileDown);
	}

	void playPlayerFell(int player)
	{
		GetComponent<AudioSource>().PlayOneShot(playerFell);
	}
	
	void playPlayerMove(int player)
	{
		GetComponent<AudioSource>().PlayOneShot(playerMove);
	}
	
	void playPlayerReachedEnd(int player)
	{
		GetComponent<AudioSource>().PlayOneShot(playerReachedEnd);
	}
	
//	
//	public void playDeposit(int sound)
//	{
//		sound--;
//		
//		if (sound>5)
//		{
//			sound=5;
//		}
//		
//		GetComponent<AudioSource>().PlayOneShot(deposit[sound]);
//	}
	
}
