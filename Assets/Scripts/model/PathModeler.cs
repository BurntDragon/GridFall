using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathModeler : System.Object 
{
	public int bounds=13;
//	public int numberOfSteps=10;

	int [,] tempGrid;

	public void init()
	{
		tempGrid = new int[bounds, bounds];
	}


	public Point[] generatePath(Point from, Point to, int numberOfSteps)
	{
		List<Point> steps=new List<Point>();
		steps.Add(from);

		int xstep=to.x-from.x;
		int ystep=to.y-from.y;

		int x=from.x;
		int y=from.y;
		while(x!=to.x)
		{
			if (xstep<0) x--; else x++;
			Point v=new Point(x,y);
			Debug.Log (x+","+y);
			steps.Add(v);
		}

		while(y!=to.y)
		{
			if (ystep<0) y--; else y++;
			Point v=new Point(x,y);
			Debug.Log (x+","+y);
			steps.Add(v);
		}

		return steps.ToArray();
	}

	public Point getRandomNeighbour(Point v)
	{
		List<Point> l = new List<Point>();

		if (v.x > 0) 
		{
			l.Add(new Point(v.x-1,v.y));
		}

		if (v.y > 0) 
		{
			l.Add(new Point(v.x,v.y-1));
		}

		if (v.x <bounds ) 
		{
			l.Add(new Point(v.x+1,v.y));
		}

		if (v.y <bounds ) 
		{
			l.Add(new Point(v.x,v.y+1));
		}
		return l[Random.Range(0,l.Count)];
	}
}
