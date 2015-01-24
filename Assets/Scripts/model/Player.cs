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
	public bool isMoving;

	private GameObject GoUpButton;
	private GameObject GoDownButton;
	private GameObject GoLeftButton;
	private GameObject GoRightButton;
	private GameObject GoLocalTime;
	
	// Use this for initialization
	void Awake () 
	{
		gameInstance = GameObject.Find("Main").GetComponent<Game>();

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
		}
	}

	// Use this for initialization
	void Start () 
	{
		timeLeftOnTile = maxTimeLeftOnTile;
		CtrlGrid grid = GameObject.Find ("Grid").GetComponent<CtrlGrid> ();

		path = new PathModeler ().generatePath (pos, new Point (grid.width/2, grid.height/2), 10);

		goPlayer=GameObject.CreatePrimitive(PrimitiveType.Sphere);
		goPlayer.transform.parent=this.transform;
		goPlayer.transform.localPosition=new Vector3((float)pos.x,(float)pos.y,0);
		goPlayer.renderer.material.color = Color.red;

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
//				go.transform.parent=this.transform;
//				go.transform.localPosition=new Vector3((float)path[i].x,(float)path[i].y,z);
//				go.transform.localScale=new Vector3(0.2f,0.2f,0.2f);
//			}
//		}

		if (!isMe)
		{
			createNextStep();
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameInstance.isStarted) 
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
//		string name=this.gameObject.name+"NextStep Player: " + this.name;
//		GameObject go = GameObject.Find (name);
//		Destroy (go);

		if (pathIndex + 1 < path.Length) 
		{
//			go = GameObject.CreatePrimitive (PrimitiveType.Sphere);
//			switch (gameObject.name)
//				{
//				case "player1": go.renderer.material.color=Color.red; break;
//				case "player2": go.renderer.material.color=Color.blue;break;
//				case "player3": go.renderer.material.color=Color.green;break;
//				case "player4": go.renderer.material.color=Color.cyan;break;
//				}
//			go.name=name;
//			go.transform.parent = this.transform;
//			go.transform.localPosition = new Vector3 ((float)path [pathIndex + 1].x, (float)path [pathIndex + 1].y, 0);
//			go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			string name= "x"+path [pathIndex + 1].x+"y"+(float)path [pathIndex + 1].y;
			GameObject go = GameObject.Find (name);
			go.GetComponent<CtrlTile>().onTileMarkNext();
		}
	}

	void createPossibleSteps()
	{
		CtrlGrid grid = GameObject.Find ("Grid").GetComponent<CtrlGrid> ();
		Point [] neighbours=PathModeler.getPossibleNeighbours(pos,grid.width);
		for (int i=0; i<neighbours.Length; i++) 
		{
			string name= "x"+neighbours[i].x+"y"+neighbours[i].y;
			GameObject.Find(name).GetComponent<CtrlTile>().onTileUp();
		}
	}

	
	void checkIfRightMoveandIncrementIndex ()
	{
//		Debug.Log ("checkIfRightMoveandIncrementIndex: checen!!!");
		if (pathIndex+1>=path.Length)
		{
//				EvtManager.playerReachedEnd(player);
			goPlayer.transform.localPosition=new Vector3(path[path.Length-1].x,path[path.Length-1].y,0);
		}
		else if (pos.Equals (path[pathIndex + 1])) 
		{
			GameObject.Find(getTileName(path[pathIndex-1])).GetComponent<CtrlTile>().onTileDown();

			pathIndex++;
			goPlayer.transform.localPosition = new Vector3 ((float)pos.x, (float)pos.y, 0);
			timeLeftOnTile=maxTimeLeftOnTile;

			GameObject.Find(getTileName(pos)).GetComponent<CtrlTile>().onTileUp();

		} 
		else 
		{
			fell();
		}

		createNextStep();
		createPossibleSteps();
	}

	public void move(int x)
	{
		if (!isMoving)
		{
			isMoving=true;
			CtrlGrid grid = GameObject.Find ("Grid").GetComponent<CtrlGrid> ();

			if (x == 0 && pos.y+1<grid.height) 
			{
				pos.y+=1;
			}
			else
			if (x == 1 && pos.y>0) 
			{
				pos.y-=1;
			}
			else
			if (x == 3 && pos.x+1<grid.width) 
			{
				pos.x+=1;
			}
			else
			if (x == 2 && pos.x>0) 
			{
				pos.x-=1;
			}

			checkIfRightMoveandIncrementIndex ();
			isMoving=false;
		}
	}

	void fell()
	{
//		Debug.Log ("fell: fellen!!!");
		pathIndex = 0;
		pos = new Point(spawnPoint.x,spawnPoint.y);
		goPlayer.transform.localPosition = new Vector3 (spawnPoint.x, spawnPoint.y, 0);
		timeLeftOnTile = maxTimeLeftOnTile;

		///XXX
		//EvtManager.onPlayerFell(1);
	}

	void  OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting)
		{
			int cnt=path.Length;
			stream.Serialize(ref cnt);
			for (int i=0;i<cnt;i++)
			{
				Point p=path[i];
				stream.Serialize(ref p.x);
				stream.Serialize(ref p.y);
			}
		}
		else
		{
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

		}
	}

	public static string getTileName(Point p)
	{
		string name= "x"+p.x+"y"+p.y;
		return name;
	}
}
