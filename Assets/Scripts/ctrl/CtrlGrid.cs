using UnityEngine;
using System.Collections;

public class CtrlGrid : MonoBehaviour 
{
	public int width=16;
	public int height=16;



	// Use this for initialization
	void Start () 	
	{
		generatePrimitives();

		new PathModeler ().generatePath (new Point(0,0),new Point(8,8),12 );
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void generatePrimitives()
	{
		for (int x=0;x<width;x++)
		{
			for (int y=0;y<width;y++)
			{
				GameObject go=GameObject.CreatePrimitive(PrimitiveType.Quad);
				go.transform.parent=this.transform;
				go.transform.localPosition=new Vector3(x,0,y);
				go.transform.eulerAngles=new Vector3(90,0,0);


			}
		}
	}
}
