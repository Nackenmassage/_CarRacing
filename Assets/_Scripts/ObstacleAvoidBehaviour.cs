using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidBehaviour : SteeringBehaviour
{
	public Obstacle[] Obstacles;
	public float maxAhead = 5f;

	private Vector3 nearestPointToObstacle = Vector3.zero;

	public override Vector3 Steer()
	{
		Vector3 _antenna = vehicle.Velocity * maxAhead;
		Debug.DrawRay(transform.position, _antenna, Color.blue);
		Obstacle _threat = null;

		for (int i = 0; i < Obstacles.Length; i++)
		{
			var _o = Obstacles[i];
			var _pos = transform.position;
			var _toObstacle = _o.Position - _pos;
			float _percOnAntenna = Vector3.Dot(_antenna, _toObstacle) / _antenna.sqrMagnitude;
			if (_percOnAntenna < 0f || _percOnAntenna > 1f)
			{
				continue;
			}
			var _nearestOnAntenna = _pos + _antenna * _percOnAntenna;
			nearestPointToObstacle = _o.Position - _nearestOnAntenna;
			var _sqrRadius = _o.Radius * _o.Radius;
			if (_sqrRadius > nearestPointToObstacle.sqrMagnitude)
			{
				if (_threat == null || _toObstacle.sqrMagnitude < (_threat.Position - _pos).sqrMagnitude)
				{
					_threat = _o;
				}
			}
		}

		if (_threat != null)
		{
			//var _desired = _antenna - _threat.Position;
			//_desired = _desired.normalized * vehicle.MaxSpeed;
			//var _steer = Vector3.ClampMagnitude(_desired - vehicle.Velocity, vehicle.MaxForce) * _threat.Radius;
			var _desired = -nearestPointToObstacle * (nearestPointToObstacle.magnitude + _threat.Radius * 2f);
			var _steer = _desired - vehicle.Velocity;
			Debug.DrawRay(transform.position, _steer, Color.red, 0.5f);
			Debug.DrawRay(transform.position, _desired, Color.green, 0.5f);
			return _steer;
		}
		else
		{
			return Vector3.zero;
		}
	}
}
