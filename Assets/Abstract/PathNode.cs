using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PathNode : IComparable
{
	Vector2i pos;
	int g, h, f;
	PathNode parent;
	
	
	public PathNode(Vector2i pos)
	{
		this.pos = pos;
		this.g = this.h = this.f = -1;
	}
	
	public Vector2i GetPosition()
	{
		return pos;
	}
	
	public void UpdateScore(Vector2i Initial, Vector2i target)
	{
		if(parent != null)
		{
			g = parent.GetG() + 1;
		}
		else
		{
			g = ManhattanDistance(Initial,pos);
		}
		h = ManhattanDistance(pos, target);
		f = h + g;
	}
	
	public static int ManhattanDistance(Vector2i point1, Vector2i point2)
	{
		int xdif = (int)Mathf.Abs(Mathf.Floor(point1.x) - Mathf.Floor(point2.x));
		int ydif = (int)Mathf.Abs(Mathf.Floor(point1.y) - Mathf.Floor(point2.y));
		return xdif + ydif;
	}
	
	public int GetScore()
	{
		return f;
	}
	
	public int GetG()
	{
		return g;
	}
	
	public void SetParent(PathNode parent)
	{
		this.parent = parent;
	}
	
	public PathNode GetParent()
	{
		return this.parent;
	}
	
	public int CompareTo(System.Object other)
	{
		if (other == null) return 1;
		PathNode asd = other as PathNode;
		int oscore = asd.f;
		if(f > oscore)
			return 1;
		if(f < oscore)
			return -1;
		return 0;
	}
	
	public override bool Equals (object obj)
	{
		if(obj == null)
			return false;
		
		PathNode tmpobj = obj as PathNode;
		if((System.Object)tmpobj == null)
			return false;
		
		return (tmpobj.GetPosition() == pos);
	}
	
	public override int GetHashCode ()
	{
		return pos.x ^ pos.y;
	}
}