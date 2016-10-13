using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpeedBuff : Buff {

	private GameObject _gameObject;

	void Start ()
	{
		Init();
		_gameObject = gameObject;

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
			_buffIndicator.targetValue = _duration - _currentBuffTime;
			_buffIndicator.targetMaxValue = _duration;
		}
		if (_currentBuffTime > _duration)
		{
			_active = false;
			_currentBuffTime = 0;
			_buffIndicator.alive = _active;
		}


		Constants.PlayerMovementSpeed = (_active) ? Constants.SpeedBuffAmount : Constants.DefaultPlayerMovementSpeed;
	}


	public override void UseBuff()
	{
		_active = true;
		_buffIcon = Instantiate((GameObject)Resources.Load("Prefabs/UIPrefabs/BuffContainer/BuffIcon"));
		_buffIcon.transform.GetComponent<Image>().sprite =  Resources.Load<Sprite>("Item/buff");
		_buffIndicator = _buffIcon.GetComponent<BuffIndicator>();
		GameObject _container = GameObject.Find("BuffContainer");
		_buffIcon.transform.SetParent(_container.transform);
		_container.GetComponent<BuffContainer>().currentBuffs.Add(_buffIcon);

	}


}
