using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleVehicle : MonoBehaviour
{
	public Transform Target;
	public Vector3 Velocity;
	public float Mass = 1f;
	public float MaxForce = 1f;
	public float MaxSpeed = 1f;
	public float FleeRange = 10f;

	private Transform bodyMeshTransform;

	void Awake()
	{
		bodyMeshTransform = transform.GetChild(0);
	}

	void Update()
	{
		//Seek(Target.transform.position);
		Flee(Target.transform.position);
		transform.rotation *= GetRotation();
	}

	// Seeks the target
	public void Seek(Vector3 _target)
	{
		Vector3 _desired = _target - transform.position;   // calculates a vector by subtracting the current position from the targetposition
		_desired.Normalize();                              // normalize the vector
		_desired *= MaxSpeed;                              // multiplies with the speed
		Vector3 _steer = _desired - Velocity;              // calculates the actual vector, where the object has to move
		_steer = Vector3.ClampMagnitude(_steer, MaxForce); // clamps the force
		UpdateVehicle(_steer);
	}

	// Update and the vehicle movement
	public void UpdateVehicle(Vector3 _force)
	{
		Vector3 _acceleration = _force / Mass;             // acceleration of actual force 
		Velocity += _acceleration * Time.deltaTime;        // Velocity adds up from accelerations over time
		transform.position += Velocity * Time.deltaTime;   // position change based on time
	}

	// Flees from the target
	public void Flee(Vector3 _target)
	{
		Vector3 _desired = _target - transform.position;   // calculates a vector by subtracting the current position from the targetposition
		if (_desired.sqrMagnitude < FleeRange * FleeRange)
		{
			_desired.Normalize();                              // normalize the vector
			_desired *= MaxSpeed;                              // multiplies with the speed
			Vector3 _steer = _desired - Velocity;              // calculates the actual vector, where the object has to move
			_steer = Vector3.ClampMagnitude(_steer, MaxForce); // clamps the force
			UpdateVehicle(-_steer);
		}
		else
		{
			transform.position += Velocity * Time.deltaTime;
		}
	}

	public Quaternion GetRotation()
	{
		return Quaternion.FromToRotation(bodyMeshTransform.transform.forward, Velocity.normalized);
	}

}
