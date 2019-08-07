using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : SteeringBehaviour
{
	public Transform Target;
	public float changeTargetDistance;

	WaypointManager wm;

	public override void Awake()
	{
		base.Awake();
		//target = GameObject.Find("Target").transform;
	}

	////Steering at full speed at all times
	//public override Vector3 Steer()
	//{
	//	Vector3 _desired = Target.position - transform.position;
	//	_desired.Normalize();
	//	_desired *= vehicle.MaxSpeed;
	//	Vector3 _steer = _desired - vehicle.Velocity;
	//	_steer = Vector3.ClampMagnitude(_steer, vehicle.MaxForce);
	//	return _steer;
	//}

	//Steering at full speed at all times
	public override Vector3 Steer()
	{
		Vector3 _desired = Target.position - transform.position;
		float _distanceToTarget = _desired.magnitude;
		if (_distanceToTarget < changeTargetDistance)
		{
			wm.ChangeTarget();
		}
		_desired.Normalize();
		_desired *= vehicle.MaxSpeed;
		Vector3 _steer = _desired - vehicle.Velocity;
		_steer = Vector3.ClampMagnitude(_steer, vehicle.MaxForce);
		return _steer;
	}


	//// Steering but getting slower when getting nearer towards the target and stopping
	//public override Vector3 Steer()
	//{
	//	Vector3 _desired = target.position - transform.position;
	//	float _d = _desired.magnitude;
	//	_desired.Normalize();
	//	if (_d < vehicle.slowRange)
	//	{
	//		_desired *= vehicle.slowSpeed * Time.deltaTime;
	//	}
	//	else
	//	{
	//		_desired *= vehicle.MaxSpeed;
	//	}
	//	Vector3 _steer = _desired - vehicle.Velocity;
	//	_steer = Vector3.ClampMagnitude(_steer, vehicle.MaxForce);
	//	return _steer;
	//}
}
