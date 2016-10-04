using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavMeshController : MonoBehaviour {

	public List<GameObject> navMesh_wayPoints = new List<GameObject>();
	int point = 0;
	void Start()
	{
		GameObject[] _pillars = GameObject.FindGameObjectsWithTag("Entity/Pillar");

		for (int i = 0; i < _pillars.Length; i++)
		{
			navMesh_wayPoints.Add(_pillars[i]);
		}
	}

	public int GetNextWayPoint()
	{
		point += GetDirection();
		if (point < 0) point = 0;
		if (point > navMesh_wayPoints.Count - 1) point = navMesh_wayPoints.Count - 1;
		return point;
	}

	private int GetDirection()
	{
		return (Random.Range(0, 2) == 0) ? 1 : -1;
	}

}
