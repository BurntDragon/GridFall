using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkView))]
public class Player : MonoBehaviour 
{
	private Game gameInstance;

	public float maxTimeLeftOnTile=10;
	public float timeLeftOnTile=0;

	public Point pos=new Point(0,0);
	public Point spawnPoint=new Point(0,0);
	public Point[] path;
	public int pathIndex=0;
	
	GameObject goPlayer;

	public bool isMe;
	public bool isMoving=true;

	public GameObject gameUi;

	private GameObject GoUpButton;
	private GameObject GoDownButton;
	private GameObject GoLeftButton;
	private GameObject GoRightButton;
	private GameObject GoLocalTime;

	private CtrlGrid grid;

	// Use this for initialization
	void Awake () 
	{
		grid = GameObject.Find ("Grid").GetComponent<CtrlGrid>();
		transform.parent = GameObject.Find("Players").transform;
		name = "player" + transform.parent.childCount;

		Debug.Log ("Awake from player:" + name);

		gameInstance = GameObject.Find("Main").GetComponent<Game>();

		gameUi = GameObject.Find ("GameUi");
		GoUpButton = GameObject.Find ("UpButton");
		GoDownButton = GameObject.Find ("DownButton");
		GoLeftButton = GameObject.Find ("LeftButton");
		GoRightButton = GameObject.Find ("RightButton");
		GoLocalTime = GameObject.Find ("LocalTime");

		if (networkView.isMine)
		{
			GoUpButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
			GoUpButton.GetComponent<Button> ().onClick.AddListener (() => move (0));

			GoDownButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
			GoDownButton.GetComponent<Button> ().onClick.AddListener (() => move (1));

			GoLeftButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
			GoLeftButton.GetComponent<Button> ().onClick.AddListener (() => move (2));

			GoRightButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
			GoRightButton.GetComponent<Button> ().onClick.AddListener (() => move (3));

			gameUi.SetActive(false);
		}
	}

	// Use this for initialization
	void Start () 
	{
		timeLeftOnTile = maxTimeLeftOnTile;
		CtrlGrid grid = GameObject.Find ("Grid").GetComponent<CtrlGrid> ();

		if (isMe) 
		{
			path = new PathModeler ().generatePath (pos, new Point (grid.width / 2, grid.height / 2), 10);
		}

		goPlayer = this.gameObject;
		goPlayer.transform.localPosition = new Vector3 (spawnPoint.x, spawnPoint.y, 0);
//		GameObject goPlayerSphere=GameObject.CreatePrimitive(PrimitiveType.Sphere);
//		goPlayerSphere.transform.parent=this.transform;
//		goPlayerSphere.transform.localPosition = Vector3.zero;
//		goPlayerSphere.renderer.material.color = Color.red;

//		if (!isMe)
//		{
//			for (int i=0;i<path.Length;i++)
//			{
//				GameObject go=GameObject.CreatePrimitive(PrimitiveType.Sphere);
//				float z=0;
//				switch (gameObject.name)
//				{
//					case "player1": go.renderer.material.color=Color.red; z=-0.2f; break;
//					case "player2": go.renderer.material.color=Color.blue;z=-0.1f; break;
//					case "player3": go.renderer.material.color=Color.green;z=0.0f; break;
//					case "player4": go.renderer.material.color=Color.cyan;z=0.1f; break;
//				}
//
//				go.transform.parent=this.transform.parent;
//				go.transform.localPosition=new Vector3((float)path[i].x,(float)path[i].y,z);
//				go.transform.localScale=new Vector3(0.2f,0.2f,0.2f);
//			}
//		}

		if (isMe)
		{
			createPossibleSteps(true,pos);
			GameObject.Find(getTileName(pos)).GetComponent<CtrlTile>().onTileUp(timeLeftOnTile);
		}
		else
		{
			if (pathIndex<path.Length)
			{
				Debug.Log(getTileName(path [pathIndex + 1]));
				GameObject.Find(getTileName(path [pathIndex + 1])).GetComponent<CtrlTile>().onTileMarkNext();
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameInstance.isStarted && networkView.isMine) 
		{
			timeLeftOnTile -= Time.deltaTime;
			if (timeLeftOnTile < 0) 
			{
				fell ();
			}

			GoLocalTime.GetComponent<Text> ().text = "You die in: " + System.Math.Round ((double)timeLeftOnTile, 2).ToString ();
		}
	}

	void createNextStep()
	{
////		string name=this.gameObject.name+"NextStep Player: " + this.name;
////		GameObject go = GameObject.Find (name);
////		Destroy (go);
//
//		if (pathIndex + 1 < path.Length) 
//		{
////			go = GameObject.CreatePrimitive (PrimitiveType.Sphere);
////			switch (gameObject.name)
////				{
////				case "player1": go.renderer.material.color=Color.red; break;
////				case "player2": go.renderer.material.color=Color.blue;break;
////				case "player3": go.renderer.material.color=Color.green;break;
////				case "player4": go.renderer.material.color=Color.cyan;break;
////				}
////			go.name=name;
////			go.transform.parent = this.transform;
////			go.transform.localPosition = new Vector3 ((float)path [pathIndex + 1].x, (float)path [pathIndex + 1].y, 0);
////			go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
//			string name= "x"+path [pathIndex + 1].x+"y"+(float)path [pathIndex + 1].y;
//			GameObject go = GameObject.Find (name);
//			go.GetComponent<CtrlTile>().onTileMarkNext();
//		}
	}

	void createPossibleSteps(bool enableSteps, Point p)
	{
				Point [] neighbours = PathModeler.getPossibleNeighbours (p, grid.width);

				for (int i=0; i<neighbours.Length; i++) {
						string neghborName = "x" + neighbours [i].x + "y" + neighbours [i].y;
						//// Debug.Log("createPossibleSteps - nameOfNeighbor:" + neghborName);
						GameObject possibleNeghbor = GameObject.Find (neghborName);

						if (possibleNeghbor) {
								CtrlTile ctrlTile = possibleNeghbor.GetComponent<CtrlTile> ();
								if (enableSteps) {
										ctrlTile.onTilePossibleNext ();
								} else {
										ctrlTile.onTileUnPossibleNext ();
								}
						}
				}
		}

	
	void checkIfRightMoveandIncrementIndex ()
	{
//		// Debug.Log ("checkIfRightMoveandIncrementIndex: checen!!!");

		if (pathIndex+1>=path.Length) //end of path
		{
//				EvtManager.playerReachedEnd(player);
			goPlayer.transform.localPosition=new Vector3(path[path.Length-1].x,path[path.Length-1].y,0);
			isMoving=false;
		}
		else if (pos.Equals (path[pathIndex + 1])) //next step
		{
			if (pathIndex > 0)
			{
				GameObject.Find(getTileName(path[pathIndex])).GetComponent<CtrlTile>().onTileDown();
			}

			pathIndex++;
			goPlayer.transform.localPosition = new Vector3 ((float)pos.x, (float)pos.y, 0);
			timeLeftOnTile=maxTimeLeftOnTile;

			//// Debug.Log("checkIfRightMoveandIncrementIndex - tileup: " + getTileName(pos));
			GameObject.Find(getTileName(pos)).GetComponent<CtrlTile>().onTileUp(timeLeftOnTile);
		} 
		else 
		{
			fell();
		}
	}

	public void move(int x)
	{
		createPossibleSteps(false,pos);
//		if (isMoving)
//		{
			CtrlGrid grid = GameObject.Find ("Grid").GetComponent<CtrlGrid> ();

			if (x == 0 && pos.y+1<grid.height) //N
			{
				pos.y+=1;
				transform.Find("Robot").transform.localEulerAngles=Vector3.zero;		
			}
			else if (x == 1 && pos.y>0) //S
			{
				pos.y-=1;
				transform.Find("Robot").transform.localEulerAngles=new Vector3(0,0,180);		
			}
			else if (x == 3 && pos.x+1<grid.width) //E
			{
				pos.x+=1;
				transform.Find("Robot").transform.localEulerAngles=new Vector3(0,0,90);		
			}
			else if (x == 2 && pos.x>0) //W
			{
				pos.x-=1;
				transform.Find("Robot").transform.localEulerAngles=new Vector3(0,0,-90);		
			}

			checkIfRightMoveandIncrementIndex ();
			createPossibleSteps(true,pos);
//		}
	}

	void fell()
	{
		createPossibleSteps(false,path[pathIndex]);
		GameObject.Find(getTileName(path[pathIndex])).GetComponent<CtrlTile>().onTileDown();

		//		// Debug.Log ("fell: fellen!!!");
		pathIndex = 0;
		pos = new Point(spawnPoint.x,spawnPoint.y);
		createPossibleSteps(true,pos);
		goPlayer.transform.localPosition = new Vector3 (spawnPoint.x, spawnPoint.y, 0);
		timeLeftOnTile = maxTimeLeftOnTile;

		///XXX
		//EvtManager.onPlayerFell(1);
	}

	void  OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
//		Debug.Log ("OnSerializeNetworkView :" + gameObject.name);
		if (stream.isWriting)
		{

			stream.Serialize(ref this.pos.x);
			stream.Serialize(ref this.pos.y);
			
			stream.Serialize(ref spawnPoint.x);
			stream.Serialize(ref spawnPoint.y);


			Vector3 pos=transform.localPosition;
			stream.Serialize(ref pos);

			int cnt=path.Length;
			stream.Serialize(ref cnt);
			for (int i=0;i<cnt;i++)
			{
				Point p=path[i];
				stream.Serialize(ref p.x);
				stream.Serialize(ref p.y);
			}

			stream.Serialize(ref this.pathIndex);

		}
		else
		{
			int px=0;
			int py=0;
			stream.Serialize(ref px);
			stream.Serialize(ref py);
			Point newPos=new Point(px,py);
			if (this.pos!=newPos)
			{
				GameObject.Find(getTileName(newPos)).GetComponent<CtrlTile>().onTileUp(timeLeftOnTile);
				GameObject.Find(getTileName(this.pos)).GetComponent<CtrlTile>().onTileDown();
			}
			this.pos=newPos;

			int sx=0;
			int sy=0;
			stream.Serialize(ref sx);
			stream.Serialize(ref sy);
			this.spawnPoint=new Point(sx,sy);

			Vector3 pos=Vector3.zero;
			stream.Serialize(ref pos);
			transform.localPosition=pos;

			int cnt=0;
			stream.Serialize(ref cnt);
			Point[] newPath=new Point[cnt];
			for (int i=0;i<cnt;i++)
			{
				int x=0;
				int y=0;
				stream.Serialize(ref x);
				stream.Serialize(ref y);
				newPath[i]=new Point(x,y);
			}
			path=newPath;

			int newPathIndex=0;
			stream.Serialize(ref newPathIndex);
			if (pathIndex!=newPathIndex)
			{
				GameObject.Find (getTileName(path [pathIndex + 1])).GetComponent<CtrlTile>().onTileUnMarkNext();
			}
			GameObject.Find (getTileName(path [newPathIndex + 1])).GetComponent<CtrlTile>().onTileMarkNext();

			pathIndex=newPathIndex;
		}
	}

	public static string getTileName(Point p)
	{
		return "x"+p.x+"y"+p.y;
	}

	public bool hasWon()
	{
		return pathIndex == path.Length - 1;
	}
}
