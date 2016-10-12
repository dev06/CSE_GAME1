using UnityEngine;
using System.Collections;

public class BuffManager : MonoBehaviour {

	private GameController _gameController;
	private PlayerController _player;

	private Buff _speedBuff = new SpeedBuff();

	void Start ()
	{
		Init();
	}

	private void Init()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_player = _gameController.Player.GetComponent<PlayerController>();
	}
	void Update ()
	{
		_speedBuff.Tick();
	}


	public void UseBuff(Item item)
	{
		if (item.itemID == ItemID.SpeedBuff)
		{
			_speedBuff.UseBuff();
		}
	}


}
