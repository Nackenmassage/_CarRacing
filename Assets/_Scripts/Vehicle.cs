using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
	public SteeringBehaviour[] steeringBehaviour;
	public Vector3 Velocity;
	public float Mass = 1f;
	public float MaxForce = 1f;
	public float MaxSpeed = 1f;

	private Vector3 acceleration;
	private float magnitudeCheck;

	private void Awake()
	{
		steeringBehaviour = GetComponents<SteeringBehaviour>();
		magnitudeCheck = MaxForce;
	}

	public void ApplyForce(Vector3 _force, float _weight)
	{
		_force = (Time.deltaTime / Mass) * _force;
		_force *= _weight;
		acceleration += _force;
		//_force = Vector3.ClampMagnitude(_force, MaxForce);
		magnitudeCheck -= _force.magnitude;
		if(magnitudeCheck > 0f)
		{
			// wenn die ausgerechnete magnitude kleiner ist als die max force, einfach zur gesamtmenge dazurechnen
			acceleration += _force;
		}
		else
		{
			// ansonsten bringe den vektor auf die länge der verbleibenden magnitude
			acceleration += Vector3.ClampMagnitude(acceleration, (_force.magnitude - magnitudeCheck));
			//acceleration = acceleration.normalized * MaxForce;
			//acceleration = Vector3.ClampMagnitude(acceleration, MaxForce);
		}
	}

	public void UpdateVehicle()
	{
		Velocity += acceleration;
		Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);
		transform.position += Velocity * Time.deltaTime;
		acceleration = Vector3.zero;
		magnitudeCheck = MaxForce;
	}
}
