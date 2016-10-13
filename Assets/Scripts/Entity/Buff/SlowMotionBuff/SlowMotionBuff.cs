using UnityEngine;
using System.Collections;

public class SlowMotionBuff : Buff {

	private bool _active;
	private GameObject _gameObject;

	void Start ()
	{
		Init();
		_gameObject = gameObject;

	}

	public SlowMotionBuff()
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

		Time.timeScale = (_active) ? 0.5f : 1.0f;

	}


	public override void UseBuff()
	{
		_active = true;
	}

}
