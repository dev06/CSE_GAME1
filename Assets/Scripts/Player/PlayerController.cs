using UnityEngine;
using System.Collections;

public class PlayerController : Mob
{

	private float _healthRepletionTimer;
	private float _healthRepletionTimerCounter;
	void Start()
	{
		_healthRepletionTimer = 10;
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
				Health += (2.0f * Time.deltaTime);
			}
		}

		_healthRepletionTimerCounter += Time.deltaTime;

	}

	void OnCollisionEnter(Collision col)
	{

		if (col.gameObject.tag == "Entity/Enemy")
		{
			if (Health > 0) {
				Health -= 5;
			}
			Debug.Log(col.gameObject.name);
			_healthRepletionTimerCounter = 0;
		}
	}




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
