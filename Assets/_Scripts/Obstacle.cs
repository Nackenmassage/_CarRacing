using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public float Radius;
	public Vector3 Position;

	void Awake()
	{
		Position = transform.position;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(transform.position, Radius);
	}
}
