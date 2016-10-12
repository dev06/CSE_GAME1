using UnityEngine;
using System.Collections;

public class SpeedBuff : Buff {

	private bool _active;

	void Start ()
	{
		Init();
	}

	public SpeedBuff()
	{
		_duration = 5.0f;
	}



	public override void Tick()
	{
		if (_active)
		{
			_currentBuffTime += Time.deltaTime;
		}
		if (_currentBuffTime > _duration)
		{
			_active = false;
			_currentBuffTime = 0;
		}

		Constants.PlayerMovementSpeed = (_active) ? Constants.SpeedBuffAmount : Constants.DefaultPlayerMovementSpeed;

	}


	public override void UseBuff()
	{
		_active = true;
	}


}
