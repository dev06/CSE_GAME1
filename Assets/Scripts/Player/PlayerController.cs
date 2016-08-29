using UnityEngine;
using System.Collections;

public class PlayerController : Mob
{

	private float _healthRepletionTimer;
	private float _healthRepletionTimerCounter;
	void Start()
	{
		_healthRepletionTimer = 5;
		MaxHealth = 100;
		Health = MaxHealth;

	}

	// Update is called once per frame
	void Update()
	{
		if (Health < MaxHealth)
		{
			if (_healthRepletionTimerCounter > _healthRepletionTimer)
			{

				Health += 2 * Time.deltaTime;
			}
		}

		_healthRepletionTimerCounter += Time.deltaTime;

	}

	void OnCollisionEnter(Collision col)
	{

		if (col.gameObject.name == "Bot")
		{
			Health -= 5;
			_healthRepletionTimerCounter = 0;
		}
	}





}
