using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
public class ImmortalityBuff : Buff {


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
			_buffIndicator.targetValue = _duration - _currentBuffTime;
			_buffIndicator.targetMaxValue = _duration;
		}
		if (_currentBuffTime > _duration)
		{
			_active = false;
			_currentBuffTime = 0;
			_buffIndicator.alive = _active;
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
		_buffIcon = Instantiate((GameObject)Resources.Load("Prefabs/UIPrefabs/BuffContainer/BuffIcon"));
		_buffIcon.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/immortal_buff");
		_buffIndicator = _buffIcon.GetComponent<BuffIndicator>();
		GameObject _container = GameObject.Find("BuffContainer");
		_buffIcon.transform.SetParent(_container.transform);
		_container.GetComponent<BuffContainer>().currentBuffs.Add(_buffIcon);

	}

}
