using UnityEngine;
using System.Collections;

public class CtrlGrid : MonoBehaviour 
{
	public int width=16;
	public int height=16;

	public GameObject gridTile;

	// Use this for initialization
	void Start () 	
	{
		//generatePrimitives();
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
				GameObject go = (GameObject) GameObject.Instantiate(gridTile);
				go.transform.parent=this.transform;
				go.transform.localPosition=new Vector3(x,y,0);
				go.transform.localScale=new Vector3(0.9f,0.9f,0.9f);
				go.name = "x"+ x.ToString() + "y" + y.ToString();
			}
		}

		gameObject.transform.localScale = Vector3.zero;
	}
}
