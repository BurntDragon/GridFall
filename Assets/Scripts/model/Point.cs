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

	public override bool Equals(System.Object obj)
	{
		// If parameter is null return false.
		if (obj == null)
		{
			return false;
		}
		
		// If parameter cannot be cast to Point return false.
		Point p = obj as Point;
		if ((System.Object)p == null)
		{
			return false;
		}
		
		// Return true if the fields match:
		return (x == p.x) && (y == p.y);
	}

}
