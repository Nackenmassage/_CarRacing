using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
	public SOSteeringData data;

	protected Vehicle vehicle;

	public abstract Vector3 Steer();

	public virtual void Awake()
	{
		vehicle = GetComponent<Vehicle>();
	}
}
