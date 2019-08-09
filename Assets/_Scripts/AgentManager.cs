using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
	public Vehicle[] Vehicles;
	public Obstacle[] Obstacles;
	public Transform[] Waypoints;

	void Start()
	{
		foreach (Vehicle vehicle in Vehicles)
		{
			var Sbs = new List<SteeringBehaviour>(vehicle.steeringBehaviour);
			var sortedSbS = Sbs.OrderBy(p => p.data.Priority);

			vehicle.steeringBehaviour = sortedSbS.ToArray();

			SeperationBehaviour sb = vehicle.GetComponent<SeperationBehaviour>();
			if (sb != null)
			{
				sb.vehicles = Vehicles;
			}
			ObstacleAvoidBehaviour oab = vehicle.GetComponent<ObstacleAvoidBehaviour>();
			if (oab != null)
			{
				oab.Obstacles = Obstacles;
			}
			WaypointFollowBehaviour wfb = vehicle.GetComponent<WaypointFollowBehaviour>();
			if (wfb != null)
			{
				wfb.Waypoints = Waypoints;
				wfb.CurTarget = Waypoints[0];
			}
		}
	}

	void Update()
	{
		for (int v = 0; v < Vehicles.Length; v++)
		{
			Vehicle vehicle = Vehicles[v];
			SteeringBehaviour[] steeringBehaviours = vehicle.steeringBehaviour;
			for (int sbIdx = 0; sbIdx < steeringBehaviours.Length; sbIdx++)
			{
				var sb = steeringBehaviours[sbIdx];
				Vector3 _steer = sb.Steer();
				vehicle.ApplyForce(_steer, sb.data.Weight);
			}
			vehicle.UpdateVehicle();
		}
	}
}
