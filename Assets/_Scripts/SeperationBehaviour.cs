using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeperationBehaviour : SteeringBehaviour
{
	public Vehicle[] vehicles;
	public float seperationDistance;

	public override void Awake()
	{
		base.Awake();
	}

	public override Vector3 Steer()
	{
		Vector3 _avgDir = Vector3.zero;
		int _numberOfNearVehicles = 0;
		for (int i = 0; i < vehicles.Length; i++)
		{
			if(vehicles[i] == vehicle)
			{
				continue;
			}
			Vector3 _dir = vehicles[i].transform.position - transform.position;
			float _sqrDist = _dir.sqrMagnitude;
			if(_sqrDist < seperationDistance * seperationDistance)
			{
				_avgDir += _dir.normalized;
				_numberOfNearVehicles++;
			}
		}

		if(_numberOfNearVehicles == 0)
		{
			return Vector3.zero;
		}

		_avgDir /= _numberOfNearVehicles;
		_avgDir = new Vector3(_avgDir.x, 0f, _avgDir.z);
		Vector3 _desired = _avgDir * -1 * vehicle.MaxSpeed;
		Vector3 _steer = _desired - vehicle.Velocity;
		_steer = Vector3.ClampMagnitude(_steer, vehicle.MaxForce);

		return _steer;
	}
}
