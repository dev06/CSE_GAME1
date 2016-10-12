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
			int index;
			if (!_gameController.inventoryManager.DoesItemExits(_gameController.inventoryManager.inventorySlots, SwithGameItemToItemID(gameItem), out index))
			{
				_gameController.inventoryManager.AddItem(SwithGameItemToItemID(gameItem));
			} else
			{
				Item updatedItem  =  SwithGameItemToItemID(gameItem);
				_gameController.inventoryManager.inventorySlots[index].UpdateItem(updatedItem);
			}



			Item _item = SwithGameItemToItemID(gameItem);

			if (_item.itemType != ItemType.Buff)
			{
				Destroy(gameObject);
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
			case GameItem.SPEEDBUFF:
				return new Item("Speed Buff",
				                "Increases player speed for certain amount of time " + Constants.SuperHealthRepletion + " health points" ,
				                Resources.Load<Sprite>("Item/buff"), 1, ItemID.SpeedBuff, ItemType.Buff);

		}
		return null;
	}
}
