using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
	public Vehicle[] vehicles;

	void Start()
	{
		foreach (Vehicle vehicle in vehicles)
		{
			SeperationBehaviour sb = vehicle.GetComponent<SeperationBehaviour>();
			if (sb != null)
			{
				sb.vehicles = this.vehicles;
			}
		}
	}

	void Update()
	{
		for (int v = 0; v < vehicles.Length; v++)
		{
			Vehicle vehicle = vehicles[v];
			SteeringBehaviour[] steeringBehaviours = vehicle.steeringBehaviour;
			for (int sbIdx = 0; sbIdx < steeringBehaviours.Length; sbIdx++)
			{
				var sb = steeringBehaviours[sbIdx];
				Vector3 _steer = sb.Steer();
				vehicle.ApplyForce(_steer, sb.data.Weight);
				vehicle.UpdateVehicle();
			}
		}
	}
}
