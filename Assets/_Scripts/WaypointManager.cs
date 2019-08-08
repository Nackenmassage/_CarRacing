using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
	public GameObject[] waypoints;
	public int n = 0;

	SeekBehaviour seekBehaviour;

	//void Start()
	//{
	//	seekBehaviour.Target = waypoints[n].transform;
	//}

	public void ChangeTarget()
	{
		n++;
		if(n > waypoints.Length) { n = 0; }
	}
}
