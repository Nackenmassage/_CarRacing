using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowBehaviour : SteeringBehaviour
{
	public LineSegment[] LineSegments;
	public float predictionDistance = 2f;

	private LineSegment curLineSegment;
	private int curlsIdx = 0;

	public override Vector3 Steer()
	{
		Vector3 _start = curLineSegment.Start;
		Vector3 _end = curLineSegment.End;
		float _radius = curLineSegment.Radius;
		Vector3 _futurePos = transform.position + (vehicle.Velocity.normalized * predictionDistance);
		Vector3 _startToFuturePos = _futurePos - _start;
		Debug.DrawLine(_start, _end, Color.magenta);
		Vector3 _startToEnd = _end - _start;
		Vector3 _startToEndDir = _startToEnd.normalized;
		float _dot = Vector3.Dot(_startToFuturePos, _startToEndDir);
		_dot = Mathf.Abs(_dot);
		Vector3 _normalPoint = _start + (_startToEndDir * _dot);

		if((_startToEndDir * _dot).sqrMagnitude > _startToEnd.sqrMagnitude)
		{
			curlsIdx = (curlsIdx + 1) % LineSegments.Length;
			SetData(curlsIdx);
		}

		if ((_futurePos - _normalPoint).sqrMagnitude > _radius)
		{
			Vector3 _desired = _normalPoint - vehicle.transform.position;
			_desired.Normalize();
			_desired *= vehicle.MaxSpeed;
			Vector3 _steer = _desired - vehicle.Velocity;
			_steer = Vector3.ClampMagnitude(_steer, vehicle.MaxForce);
			return _steer;
		}
		else
		{
			return Vector3.zero;
		}
	}

	public void SetData(int _lineIdx)
	{
		curLineSegment = LineSegments[_lineIdx];
	}
}