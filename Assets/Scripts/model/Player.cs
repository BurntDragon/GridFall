using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

	public float maxTimeLeftOnTile=10;
	public float timeLeftOnTile=0;

	public Point pos=new Point(0,0);
	Point[] path;
	int pathIndex=0;

	GameObject goPlayer;

	// Use this for initialization
	void Start () 
	{
		timeLeftOnTile = maxTimeLeftOnTile;
		path = new PathModeler ().generatePath (pos, new Point (8, 8), 10);

		goPlayer=GameObject.CreatePrimitive(PrimitiveType.Sphere);
		goPlayer.transform.parent=this.transform;
		goPlayer.transform.localPosition=new Vector3((float)pos.x,(float)pos.y,0);
		goPlayer.renderer.material.color = Color.red;

		for (int i=0;i<path.Length;i++)
		{
			GameObject go=GameObject.CreatePrimitive(PrimitiveType.Sphere);
			go.transform.parent=this.transform;
			go.transform.localPosition=new Vector3((float)path[i].x,(float)path[i].y,0);
			go.transform.localScale=new Vector3(0.5f,0.5f,0.5f);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeLeftOnTile -= Time.deltaTime;
		if (timeLeftOnTile < 0) 
		{
//			EvtManager.fell(this);
		}
	}


	bool checkIfRightMoveandIncrementIndex ()
	{
		if (pos.Equals (path [pathIndex + 1])) 
		{
			pathIndex++;
			return true;
		} 
		else 
		{
			pathIndex=0;
			pos=new Point(0,0);
			return false;
		}
	}

	public void move(int x)
	{
		CtrlGrid grid = GameObject.Find ("Grid").GetComponent<CtrlGrid> ();

		if (x == 0 && pos.y+1<grid.height) 
		{
				pos.y+=1;
				goPlayer.transform.localPosition=new Vector3((float)pos.x,(float)pos.y,0);
		}
		else
		if (x == 1 && pos.y>0) 
		{
			pos.y-=1;
			goPlayer.transform.localPosition=new Vector3((float)pos.x,(float)pos.y,0);
		}
		else
		if (x == 3 && pos.x+1<grid.width) 
		{
			pos.x+=1;
			goPlayer.transform.localPosition=new Vector3((float)pos.x,(float)pos.y,0);
		}
		else
		if (x == 2 && pos.x>0) 
		{
			pos.x-=1;
			goPlayer.transform.localPosition=new Vector3((float)pos.x,(float)pos.y,0);
		}

			checkIfRightMoveandIncrementIndex ();
	}
	
}
