using UnityEngine;
using System.Collections;


public class Bot : Mob
{

	// Use this for initialization


	private Transform _targetTransform;
	void Start()
	{
		Health = 150;

		_targetTransform = GameObject.FindWithTag("Player").transform;

	}

	void Update()
	{
		CheckIfIsDead();
		transform.LookAt(_targetTransform);
		transform.Translate(Vector3.forward * Time.deltaTime * 1.0f);
	}





}
