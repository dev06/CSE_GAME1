using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
public class ImmortalityBuff : Buff {


	private bool _active;
	private GameObject _gameObject;
	private PlayerController _player;

	void Start ()
	{
		Init();
		_gameObject = gameObject;

	}

	public ImmortalityBuff()
	{
		_duration = 20.0f;

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

		if (_player != null)
		{
			_player.isImmortal = _active;
			Camera.main.transform.gameObject.GetComponent<SepiaTone>().enabled = _active;
		}
	}


	public override void UseBuff(GameController _gameController)
	{
		_active = true;
		_player = _gameController.Player.GetComponent<PlayerController>();
		_player.isImmortal = true;
	}

}
