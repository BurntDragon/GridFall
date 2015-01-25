using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour 
{
	private const string typeName = "BurntDragonGame";
	public string gameName = "Grid MindFrack";

	private HostData[] hostList;
	
	public GameObject button;
	
	public GameObject menu;
	public GameObject gameUi;
	public GameObject gameWin;
	public GameObject gameButtonScreen;
	public GameObject gameButton;


	public int playerCount=0;

	private bool listNotShow = false;

	private Game daGame;

	public void StartServer()
	{
//		MasterServer.ipAddress = "127.0.0.1";
//		MasterServer.port = 23466;
//		Network.natFacilitatorIP = "127.0.0.1";
//		Network.natFacilitatorPort = 50005;

		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void Start()
	{
		daGame = GameObject.Find ("Main").GetComponent<Game> ();
	}

	void Update()
	{
		if (hostList != null && listNotShow)
		{
			for (int i = 0; i < hostList.Length; i++)
			{
				foreach (HostData d in hostList)
				{
					GameObject go = (GameObject)Instantiate(button);
					
					go.transform.SetParent(menu.transform);
					go.GetComponent<RectTransform>().anchoredPosition = new Vector2(200,(i*-100)-200);
					go.transform.localScale = new Vector3(1, 1, 1);
					Button b = go.GetComponent<Button>();
					b.onClick.AddListener(() => JoinServer(d));
					
					go.transform.Find("Text").GetComponent<Text>().text = d.gameName;
				}
			}

			listNotShow = false;
		}
	}

	void OnServerInitialized()
	{
		// Debug.Log("Server Initialized");
		init();
	}

	void OnPlayerConnected()
	{
		// Debug.Log("Client connected");

		if (Network.isServer) 
		{
			gameButton.GetComponent<Button>().interactable = true;
			gameButton.transform.Find("Text").GetComponent<Text>().text = "Start Game!";
		}
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
		{
			hostList = MasterServer.PollHostList();
		}
	}

	void OnConnectedToServer()
	{
		// Debug.Log("Joined Server ");
		init();
	}

	public void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
		listNotShow = true;
	}

	private void JoinServer(HostData hostData)
	{
		playerCount = hostData.connectedPlayers;
		Network.Connect(hostData);
	}

	private void SpawnPlayer()
	{
		// Debug.Log ("SpawnPlayer:" + playerCount);
		daGame.addPlayer(playerCount);
		playerCount++;
	}

	private void init()
	{
		menu.SetActive(false);
		gameButtonScreen.SetActive(true);

		gameUi.SetActive(true);

		GameObject.Find ("Grid").GetComponent<CtrlGrid> ().generatePrimitives();

		SpawnPlayer();
	}
}
