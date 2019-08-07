using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapAround : MonoBehaviour
{
	private Camera cam;
	private Plane plane;
	private Ray ray;

	void Start()
	{
		cam = Camera.main;
	}

	void Update()
	{
		CheckViewportPosition();
	}

	// checks if the mesh is outside of the viewport
	// if so teleport it to the other side
	public void CheckViewportPosition()
	{
		Vector3 _viewPos = cam.WorldToViewportPoint(transform.position);
		if (_viewPos.x > 1f || _viewPos.x < 0f || _viewPos.y > 1f || _viewPos.y < 0f)
		{
			plane = new Plane(Vector3.up, transform.position);
			if (_viewPos.x > 1f)
			{
				Vector3 _newPos = new Vector3(0f, _viewPos.y, _viewPos.z);
				NewPosRayCast(_newPos);
			}
			else if (_viewPos.x < 0f)
			{
				Vector3 _newPos = new Vector3(1f, _viewPos.y, _viewPos.z);
				NewPosRayCast(_newPos);
			}
			else if (_viewPos.y > 1f)
			{
				Vector3 _newPos = new Vector3(_viewPos.x, 0f, _viewPos.z);
				NewPosRayCast(_newPos);
			}
			else if (_viewPos.y < 0f)
			{
				Vector3 _newPos = new Vector3(_viewPos.x, 1f, _viewPos.z);
				NewPosRayCast(_newPos);
			}
		}
	}

	// 
	public void NewPosRayCast(Vector3 _newPos)
	{
		ray = cam.ViewportPointToRay(_newPos);
		if (plane.Raycast(ray, out float _distance))
		{
			transform.position = ray.origin + ray.direction * _distance;
		}
	}
}
