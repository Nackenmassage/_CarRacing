using UnityEngine;

public class LinePath : MonoBehaviour
{
	public LineSegment[] LineSegments;
	public float Radius = 2f;

	void Start()
	{
		var _pathPoints = new Transform[transform.childCount];
		LineSegments = new LineSegment[transform.childCount];
		int idx = 0;
		foreach (Transform child in transform)
		{
			_pathPoints[idx] = child;
			idx++;
		}
		for (int i = 0, j = 0; i < _pathPoints.Length; i++)
		{
			j = (j + 1) % _pathPoints.Length;
			LineSegments[i] = new LineSegment(_pathPoints[i].position, _pathPoints[j].position, Radius);
		}
	}

	void Update()
	{
		foreach(LineSegment lineSegment in LineSegments)
		{
			Debug.DrawLine(lineSegment.Start, lineSegment.End, Color.green);
		}
	}
}
