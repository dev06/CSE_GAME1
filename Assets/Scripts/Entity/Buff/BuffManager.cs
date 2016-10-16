using UnityEngine;
using System.Collections;

public class BuffManager : MonoBehaviour {

	private GameController _gameController;
	private PlayerController _player;

	private Buff _speedBuff = new SpeedBuff();
	private Buff _slowMotionBuff = new SlowMotionBuff();
	private Buff _teleportBuff = new TeleportBuff();
	private Buff _immortalBuff = new ImmortalityBuff();
	private Buff _blastBuff = new BlastBuff();
	private Buff _projectilePenetrationBuff = new ProjectilePenetration();



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
		_projectilePenetrationBuff.Tick();
		_immortalBuff.Tick();
	}


	public void UseBuff(Item item)
	{
		if (item.itemID == ItemID.SpeedBuff)
		{
			if (_speedBuff._active == false)
			{
				_speedBuff.UseBuff();
				_gameController.inventoryManager.quickItemSelectedSlot.DepleteItem(item, 1);
			}

		} else if (item.itemID == ItemID.SlowMotionBuff)
		{
			if (_slowMotionBuff._active == false)
			{
				_slowMotionBuff.UseBuff();
				_gameController.inventoryManager.quickItemSelectedSlot.DepleteItem(item, 1);
			}
		} else if (item.itemID == ItemID.TeleportationBuff)
		{

			_teleportBuff.UseBuff(_gameController);
			_gameController.inventoryManager.quickItemSelectedSlot.DepleteItem(item, 1);

		} else if (item.itemID == ItemID.ImmortalityBuff)
		{
			if (_immortalBuff._active == false)
			{
				_immortalBuff.UseBuff(_gameController);
				_gameController.inventoryManager.quickItemSelectedSlot.DepleteItem(item, 1);
			}
		} else if (item.itemID == ItemID.BlastBuff)
		{
			_blastBuff.UseBuff(_gameController);
			_gameController.inventoryManager.quickItemSelectedSlot.DepleteItem(item, 1);
		} else if (item.itemID == ItemID.ProjectilePenetrationBuff)
		{
			if (_projectilePenetrationBuff._active == false)
			{
				_projectilePenetrationBuff.UseBuff(_gameController);
				_gameController.inventoryManager.quickItemSelectedSlot.DepleteItem(item, 1);
			}
		}
	}


}
