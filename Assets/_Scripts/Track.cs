using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
	[SerializeField] private Transform[] waypoints;

	private void OnDrawGizmos()
	{
		if(waypoints == null || waypoints.Length < 2) { return; }

		for (int i = 0; i < waypoints.Length; i++)
		{
			if(waypoints[i] == null) { return; }
			Gizmos.color = i == 0 ? Color.green : Color.white;
			Gizmos.DrawSphere(waypoints[i].position, 0.4f);
			if(!(i == waypoints.Length - 1))
			{
				Gizmos.DrawLine(waypoints[i].position, waypoints[(i + 1)].position);
			}
		}
	}
}
