using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveBehaviour : SteeringBehaviour
{
	public float DecelerationTweaker = 30f;
	public float slowdownDistance;

	private Transform target;
	private float slowDownStartSpeed;
	private bool isStartSpeedSet;

	public override void Awake()
	{
		base.Awake();
		target = GameObject.Find("Target").transform;
	}

	// Steering but getting slower when getting nearer towards the target and stopping
	public override Vector3 Steer()
	{
		Vector3 _desired = target.position - transform.position;
		Vector3 _steer = Vector3.zero;
		float _distanceToTarget = _desired.magnitude;
		if (_distanceToTarget < slowdownDistance)
		{
			if (!isStartSpeedSet)
			{
				slowDownStartSpeed = vehicle.Velocity.magnitude;
				isStartSpeedSet = true;
			}
			float _t = _distanceToTarget / slowdownDistance;
			float _mappedSpeed = Mathf.Lerp(0f, slowDownStartSpeed, _t);
			_mappedSpeed = _mappedSpeed <= 0.2f ? 0f : _mappedSpeed;
			_desired = _desired.normalized * _mappedSpeed;
			_steer = _desired - vehicle.Velocity;
			float _decel = (1 - _t) * DecelerationTweaker;
			_steer *= _decel;
		}
		else
		{
			isStartSpeedSet = false;
			_desired = _desired.normalized * vehicle.MaxSpeed;
			_steer = _desired - vehicle.Velocity;
			_steer = Vector3.ClampMagnitude(_steer, vehicle.MaxForce);
		}
		return _steer;
	}
}
