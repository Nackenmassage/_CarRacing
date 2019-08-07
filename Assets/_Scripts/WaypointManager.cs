using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
	public GameObject[] waypoints;
	private int i = 0;

	SeekBehaviour seekBehaviour;

	void Awake()
	{
		seekBehaviour.Target = waypoints[i].transform;
	}

	public void ChangeTarget()
	{
		i++;
		if(i > waypoints.Length) { i = 0; }
		seekBehaviour.Target = waypoints[i].transform;
	}
}
