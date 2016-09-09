﻿using UnityEngine;
using System.Collections;

public class PlayerController : Mob
{

	private float _healthRepletionTimer;
	private float _healthRepletionTimerCounter;

	void Start()
	{
		Init();
		_healthRepletionTimer = Constants.HealthRepletionTimer;
		MaxHealth = Constants.PlayerMaxHealth;
		Health = MaxHealth;

	}

	// Update is called once per frame
	void Update()
	{
		if (Health < MaxHealth)
		{
			if (_healthRepletionTimerCounter > _healthRepletionTimer)
			{
				Health += (Constants.HealthRepletionPoints * Time.deltaTime);
			}
		}

		_healthRepletionTimerCounter += Time.deltaTime;


		if (_gameController.menuActive == MenuActive.GAME)
		{
			if (Health <= 0)
			{
				GameObject.Find("RetryCanvas").GetComponent<Animation>().Play(GameObject.Find("RetryCanvas").GetComponent<Animation>().clip.name);
				_gameController.EnableMenu(MenuActive.RETRY);
			}
		}

	}




	void OnCollisionEnter(Collision col)
	{

		if (col.gameObject.tag == "Entity/Enemy")
		{
			if (Health > 0) {
				Health -= Constants.BotInitalDamage;
			}


			_healthRepletionTimerCounter = 0;
		}
	}
	/*Summary
		Returns a float value base on a parameter
	*/
	public float ReturnFloatValue(string value)
	{
		switch (value)
		{
			case "Timer": return _healthRepletionTimer;
			case "Counter": return _healthRepletionTimerCounter;
		}

		return 0;
	}





}
