using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveBehaviour : SteeringBehaviour
{
	public float slowdownDistance;

	private Transform target;

	public override void Awake()
	{
		base.Awake();
		target = GameObject.Find("Target").transform;
	}

	// Steering but getting slower when getting nearer towards the target and stopping
	public override Vector3 Steer()
	{
		Vector3 _desired = target.position - transform.position;
		float _distanceToTarget = _desired.magnitude;
		if (_distanceToTarget < slowdownDistance)
		{
			float _t = _distanceToTarget / slowdownDistance;
			_desired = _desired.normalized * _t * vehicle.MaxSpeed;
		}
		else
		{
			_desired = _desired.normalized * vehicle.MaxSpeed;
		}
		Vector3 _steer = _desired - vehicle.Velocity;
		_steer = Vector3.ClampMagnitude(_steer, vehicle.MaxForce);
		return _steer;
	}
}
