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

	private void Awake()
	{
		steeringBehaviour = GetComponents<SteeringBehaviour>();
	}

	public void ApplyForce(Vector3 _force, float _weight)
	{
		acceleration += (_force * _weight) / Mass;
		acceleration = Vector3.ClampMagnitude(acceleration, MaxForce);
	}

	public void UpdateVehicle()
	{
		Velocity += acceleration * Time.deltaTime;
		Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);
		transform.position += Velocity * Time.deltaTime;
		acceleration = Vector3.zero;
	}
}
