using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollowBehaviour : SteeringBehaviour
{
	public Transform[] Waypoints;
	public float arrivalThreshold = 0.5f;

	public Transform CurTarget;
	private int curIdx = 0;

	public override Vector3 Steer()
	{
		Vector3 _desired = CurTarget.position - transform.position;
		if (_desired.sqrMagnitude < arrivalThreshold * arrivalThreshold)
		{
			curIdx = (curIdx + 1) % Waypoints.Length;
			CurTarget = Waypoints[curIdx];
		}
		_desired.Normalize();
		_desired *= vehicle.MaxSpeed;
		Vector3 _steer = _desired - vehicle.Velocity;
		_steer = Vector3.ClampMagnitude(_steer, vehicle.MaxForce);
		return _steer;
	}
}
