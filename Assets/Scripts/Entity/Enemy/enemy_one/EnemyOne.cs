using UnityEngine;
using System.Collections;

public class EnemyOne : Mob {

	private GameObject _hover;
	public GameObject Target;
	void Start ()
	{
		Init();
		MaxHealth = Constants.BotMaxHealth;
		Health = MaxHealth;
		_hover = transform.FindChild("HoverEffect").gameObject;
	}

	// Update is called once per frame
	void Update ()
	{
		ManageHoverEffect();
		if (_gameController.navMeshController.navMesh_wayPoints.Count > 0)
		{
			if (_agent.remainingDistance < 10)
			{
				_agent.SetDestination(_gameController.navMeshController.navMesh_wayPoints[_gameController.navMeshController.GetNextWayPoint()].transform.position);
			}
		}
	}

	private void ManageHoverEffect()
	{
		_hover.transform.Rotate(new Vector3(0, 0,  Time.deltaTime * 150.0f));
	}
}
