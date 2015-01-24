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
	public Point[] path;
	public int pathIndex=0;

	GameObject goPlayer;

	public bool isMe;

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

		if (!isMe)
		{
			for (int i=0;i<path.Length;i++)
			{
				GameObject go=GameObject.CreatePrimitive(PrimitiveType.Sphere);
				go.transform.parent=this.transform;
				go.transform.localPosition=new Vector3((float)path[i].x,(float)path[i].y,0);
				go.transform.localScale=new Vector3(0.5f,0.5f,0.5f);
			}
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


	void checkIfRightMoveandIncrementIndex ()
	{

		if (pathIndex+1>=path.Length)
		{
//				EvtManager.playerReachedEnd(player);
			goPlayer.transform.localPosition=new Vector3(path[path.Length-1].x,path[path.Length-1].y,0);
		}
		else
		if (pos.Equals (path [pathIndex + 1])) 
		{
			pathIndex++;
			goPlayer.transform.localPosition = new Vector3 ((float)pos.x, (float)pos.y, 0);
		} 
		else 
		{
			fell();
		}
	}

	public void move(int x)
	{
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
	}

	void fell()
	{
		pathIndex = 0;
		pos = new Point (0, 0);
		goPlayer.transform.localPosition = new Vector3 (0, 0, 0);
		timeLeftOnTile = maxTimeLeftOnTile;

		///XXX
		//EvtManager.onPlayerFell(1);
	}
}
