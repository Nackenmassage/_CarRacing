using UnityEngine;

//[System.Serializable]
public class LineSegment
{
	public Vector3 Start;
	public Vector3 End;
	public float Radius;

	public LineSegment() { }

	public LineSegment(Vector3 _start, Vector3 _end, float _radius)
	{
		Start = _start;
		End = _end;
		Radius = _radius;
	}
}
