//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;

public class PlayerController : Mob
{

	#region-----PRIVATE MEMBERS-----
	private float _healthRepletionTimer;
	private float _healthRepletionTimerCounter;
	#endregion-----/PRIVATE MEMBERS-----

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

		//Repletes the player health.
		if (Health < MaxHealth)
		{
			if (_healthRepletionTimerCounter > _healthRepletionTimer)
			{
				Health += (Constants.HealthRepletionPoints * Time.deltaTime);
			}
		}

		_healthRepletionTimerCounter += Time.deltaTime;


		//Ends the game
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

	/// <summary>
	/// Returns a float value based on the parameter passed.
	/// </summary>
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
