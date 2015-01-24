using UnityEngine;
using System.Collections;

[System.Serializable]
public class Point : System.Object 
{
	public int x;
	public int y;

	public Point(int x,int y)
	{
		this.x = x;
		this.y = y;
	}

	public Point()
	{

	}
}
