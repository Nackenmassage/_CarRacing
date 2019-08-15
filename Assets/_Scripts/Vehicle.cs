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
	public bool debugVelocity;
	public string debugString;
	public GUIStyle style;

	private Vector3 acceleration;
	private float magnitudeCheck;

	private void Awake()
	{
		steeringBehaviour = GetComponents<SteeringBehaviour>();
		magnitudeCheck = MaxForce;
	}

	public void ApplyForce(Vector3 _force, float _weight)
	{
		_force *= _weight;
		magnitudeCheck -= _force.magnitude;
		debugString += magnitudeCheck + "\n";
		_force = (Time.deltaTime / Mass) * _force;
		acceleration += _force;
		//_force = Vector3.ClampMagnitude(_force, MaxForce);
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

	private void OnGUI()
	{
		if (!debugVelocity) { return; }
		GUILayout.Label(debugString, style);
	}

	private void OnDrawGizmos()
	{
		if (!debugVelocity) { return; }
		Gizmos.DrawWireSphere(transform.position, 0.5f);
	}
}
