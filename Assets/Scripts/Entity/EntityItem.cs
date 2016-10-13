using UnityEngine;
using System.Collections;


public class Entity: MonoBehaviour
{


	protected GameController _gameController;
	private Vector3 _rotation;
	private Vector3 _targetHover;
	private Vector3 _hoverPos;

	private float _rotationSpeedOffset;
	void Start()
	{
		Init();
	}

	protected void Init()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_rotation = Vector3.zero;
		_rotationSpeedOffset = Random.Range(1.0f, 2.0f);
	}

	void FixedUpdate()
	{
		HoverAndRotate();
	}

	private void HoverAndRotate()
	{
		_rotation.x = 0;
		_rotation.y = Time.deltaTime * Constants.EntityRotationSpeed * _rotationSpeedOffset;
		_rotation.z = 0;
		transform.Rotate(_rotation);

		_targetHover.y = Mathf.PingPong(Constants.EntityHoverFreq * Time.time, Constants.EntityHoverAmp);
		_targetHover.y -= (Constants.EntityHoverAmp / 2.0f);
		_hoverPos.y = Mathf.Lerp(_hoverPos.y, _targetHover.y, 2.5f * Time.deltaTime);
		transform.position += _hoverPos;
	}
}

public class EntityItem : Entity {

	public GameItem gameItem;

	void Start()
	{
		Init();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{




			Item _item = SwithGameItemToItemID(gameItem);

			if (_item.itemType != ItemType.Buff)
			{
				int index;
				if (!_gameController.inventoryManager.DoesItemExits(_gameController.inventoryManager.inventorySlots, SwithGameItemToItemID(gameItem), out index))
				{
					_gameController.inventoryManager.AddItem(SwithGameItemToItemID(gameItem));
				} else
				{
					Item updatedItem  =  SwithGameItemToItemID(gameItem);
					_gameController.inventoryManager.inventorySlots[index].UpdateItem(updatedItem);
				}
				Destroy(gameObject);
			} else
			{
				int index;
				if (!_gameController.inventoryManager.DoesItemExits(_gameController.inventoryManager.inventorySlots, SwithGameItemToItemID(gameItem), out index))
				{
					_gameController.inventoryManager.AddItem(SwithGameItemToItemID(gameItem));
				}
			}

		}
	}

	private Item SwithGameItemToItemID(GameItem item)
	{


		switch (item)
		{
		case GameItem.BLUEBALL:
			return new Item("Blue Ball",
			                "A projectile that does " + Constants.Character_BlueProjectileDamage + " points of damage." ,
			                Resources.Load<Sprite>("Item/blueBall"), 150, ItemID.BlueBall, ItemType.Projectile);
		case GameItem.YELLOWBALL:
			return new Item("Yellow Ball",
			                "A projectile that does " + Constants.Character_YellowProjectileDamage + " points of damage.",
			                Resources.Load<Sprite>("Item/yellowBall"), 150, ItemID.YellowBall, ItemType.Projectile);
		case GameItem.PURPLEBALL:
			return new Item("Purple Ball",
			                "A projectile that does  " + Constants.Character_PurpleProjectileDamage + " points of damage.",
			                Resources.Load<Sprite>("Item/purpleBall"), 100, ItemID.PurpleBall, ItemType.Projectile);
		case GameItem.BASICHEALTH:
			return new Item("Basic Health",
			                "A Simple Medkit that restores " +  Constants.BasicHealthRepletion + " health points" ,
			                Resources.Load<Sprite>("Item/greenHealth"), 4, ItemID.BasicHealth, ItemType.Collectible);
		case GameItem.INTERMEDHEALTH:
			return new Item("Intermediate Health",
			                "A little advanced Medkit that restores " + Constants.InterMedHealthRepletion + " health points" ,
			                Resources.Load<Sprite>("Item/redHealth"), 3, ItemID.InterMedHealth, ItemType.Collectible);
		case GameItem.ADVANCEDHEALTH:
			return new Item("Advanced Health",
			                "A advanced Medkit that restores " + Constants.AdvancedHealthRepletion + " health points" ,
			                Resources.Load<Sprite>("Item/orangeHealth"), 3, ItemID.AdvancedHealth, ItemType.Collectible);
		case GameItem.SUPERHEALTH:
			return new Item("Super Health",
			                "A Super Medkit that restores " + Constants.SuperHealthRepletion + " health points" ,
			                Resources.Load<Sprite>("Item/blueHealth"), 1, ItemID.SuperHealth, ItemType.Collectible);
		case GameItem.SPEED_BUFF:
			return new Item("Speed Buff",
			                "Increases player speed for certain amount of time" ,
			                Resources.Load<Sprite>("Item/buff"), 1, ItemID.SpeedBuff, ItemType.Buff);
		case GameItem.SLOWMOTION_BUFF:
			return new Item("Slow motion Buff",
			                "Slows down time for certain amount of period." ,
			                Resources.Load<Sprite>("Item/slowMotion_buff"), 1, ItemID.SlowMotionBuff, ItemType.Buff);
		case GameItem.TELEPORTATION_BUFF:
			return new Item("Teleporter",
			                "Teleports you to certain location" ,
			                Resources.Load<Sprite>("Item/teleport_buff"), 1, ItemID.TeleportationBuff, ItemType.Buff);
		case GameItem.IMMORTALITY_BUFF:
			return new Item("Immortality",
			                "Makes you immortal for certain period of time." ,
			                Resources.Load<Sprite>("Item/immortal_buff"), 1, ItemID.ImmortalityBuff, ItemType.Buff);
		case GameItem.BLAST_BUFF:
			return new Item("Blast",
			                "Creates a blast shock wave near the player and damages near by enemies by " + Constants.BlastDamage + " health points." ,
			                Resources.Load<Sprite>("Item/blast_buff"), 1, ItemID.BlastBuff, ItemType.Buff);
		case GameItem.PROJECTILE_PENETRATION_BUFF:
			return new Item("Projectile Penetration",
			                "Projectile 15% more damage" ,
			                Resources.Load<Sprite>("Item/blast_buff"), 1, ItemID.ProjectilePenetrationBuff, ItemType.Buff);
		}
		return null;
	}
}
