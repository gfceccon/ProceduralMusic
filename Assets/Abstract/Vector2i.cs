using UnityEngine;
using System.Collections;

public class Vector2i{

	public static Vector2[] CDIRECTIONS = {Vector2.up ,Vector2.right, new Vector2(0.0f,-1.0f), new Vector2(-1.0f,0.0f)};
	public int x,y;

	public Vector2i()
	{
		x = y = 0;
	}

	public Vector2i(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public static implicit operator Vector2i(Vector2 v)
	{
		Vector2i v2i = new Vector2i();
		v2i.x = (int)v.x;
		v2i.y = (int)v.y;
		return v2i;
	}

	public static implicit operator Vector2i(Vector3 v)
	{
		Vector2i v2i = new Vector2i();
		v2i.x = (int)v.x;
		v2i.y = (int)v.y;
		return v2i;
	}

	public static implicit operator Vector2(Vector2i v)
	{
		Vector2 va = new Vector2(v.x,v.y);
		return va;
	}



	public static bool operator ==(Vector2i v1, Vector2i v2)
	{
		if(System.Object.ReferenceEquals(v1,v2)) return true;
		if((object)v1 == null || (object)v2 == null) return false;
		return v1.x == v2.x && v1.y == v2.y;
	}

	public static bool operator !=(Vector2i v1, Vector2i v2)
	{

		return !(v1 == v2);
	}

	public static Vector2i operator +(Vector2i v1, Vector2i v2)
	{
		return new Vector2i(v1.x+v2.x,v1.y+v2.y);
	}
	public override string ToString ()
	{
		return string.Format ("({0},{1})", x,y);
	}

	public override bool Equals (object obj)
	{
		if(obj == null)
			return false;

		Vector2i tmpobj = obj as Vector2i;
		if((System.Object)tmpobj == null)
			return false;

		return (tmpobj.x == this.x && tmpobj.y == this.y);
	}

	public override int GetHashCode ()
	{
		return y ^ x;
	}
}
