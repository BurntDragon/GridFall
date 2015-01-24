using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathModeler : System.Object 
{
	public int bounds=9;
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


//		// Debug.Log ("step "+xstep + "," + ystep);

		Point milestone1 = new Point (Random.Range (1, bounds - 2), Random.Range (1, bounds - 2));
		Point milestone2 = new Point (Random.Range (1, bounds - 2), Random.Range (1, bounds - 2));
		Point center = new Point (bounds / 2 , bounds / 2 );

		while (milestone1.Equals(center)) 
		{
			milestone1 = new Point (Random.Range (1, bounds - 2), Random.Range (1, bounds - 2));
		}

		while (milestone2.Equals(center)) 
		{
			milestone2 = new Point (Random.Range (1, bounds - 2), Random.Range (1, bounds - 2));
		}

		int x=from.x;
		int y=from.y;
		int xstep=milestone1.x-x;
		int ystep=milestone1.y-y;

		//milestone 1
		while(x!=milestone1.x)
		{
			if (xstep<0) x--; else x++;
			Point v=new Point(x,y);
			//			// Debug.Log ("χ:"+x+","+y);
			steps.Add(v);
		}
		
		while(y!=milestone1.y)
		{
			if (ystep<0) y--; else y++;
			Point v=new Point(x,y);
			//			// Debug.Log ("ψ:"+x+","+y);
			steps.Add(v);
		}

		xstep=milestone2.x-x;
		ystep=milestone2.y-y;

		
		while(x!=milestone2.x)
		{
			if (xstep<0) x--; else x++;
			Point v=new Point(x,y);
			//			// Debug.Log ("χ:"+x+","+y);
			steps.Add(v);
		}
		
		while(y!=milestone2.y)
		{
			if (ystep<0) y--; else y++;
			Point v=new Point(x,y);
			//			// Debug.Log ("ψ:"+x+","+y);
			steps.Add(v);
		}

		xstep=to.x-x;
		ystep=to.y-y;

		
		while(x!=to.x)
		{
			if (xstep<0) x--; else x++;
			Point v=new Point(x,y);
			//			// Debug.Log ("χ:"+x+","+y);
			steps.Add(v);
		}


		while(y!=to.y)
		{
			if (ystep<0) y--; else y++;
			Point v=new Point(x,y);
			//			// Debug.Log ("ψ:"+x+","+y);
			steps.Add(v);
		}


		return steps.ToArray();
	}

	public  Point getRandomNeighbour(Point v )
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

	public static Point [] getPossibleNeighbours(Point v,int bounds)
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
		return l.ToArray();
	}

}
