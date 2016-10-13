using UnityEngine;
using System.Collections;

public class BuffManager : MonoBehaviour {

	private GameController _gameController;
	private PlayerController _player;

	private Buff _speedBuff = new SpeedBuff();
	private Buff _slowMotionBuff = new SlowMotionBuff();
	private Buff _teleportBuff = new TeleportBuff();
	private Buff _immortalBuff = new ImmortalityBuff();



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
		_slowMotionBuff.Tick();
		_teleportBuff.Tick();
		_immortalBuff.Tick();
	}


	public void UseBuff(Item item)
	{
		if (item.itemID == ItemID.SpeedBuff)
		{
			_speedBuff.UseBuff();
		} else if (item.itemID == ItemID.SlowMotionBuff)
		{
			_slowMotionBuff.UseBuff();
		} else if (item.itemID == ItemID.TeleportationBuff)
		{
			_teleportBuff.UseBuff(_gameController);
		} else if (item.itemID == ItemID.ImmortalityBuff)
		{
			_immortalBuff.UseBuff(_gameController);
		}
	}


}
