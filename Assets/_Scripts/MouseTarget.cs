using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{
	private Camera cam;
	private Plane plane;

	void Awake()
	{
		cam = Camera.main;
		plane = new Plane(Vector3.up, Vector3.zero);
	}

	void Update()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if(plane.Raycast(ray, out float _distance))
		{
			Vector3 _pointAlongPlane = ray.origin + ray.direction * _distance;
			transform.position = new Vector3(_pointAlongPlane.x, 0f, _pointAlongPlane.z);
		}
	}
}
