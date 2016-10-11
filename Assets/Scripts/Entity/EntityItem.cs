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
				Debug.Log("Item exits and quantity should be updated" + index );
			}
			Destroy(gameObject);
		}
	}

	private Item SwithGameItemToItemID(GameItem item)
	{


		switch (item)
		{
			case GameItem.BLUEBALL:
				return new Item("Blue Ball",
				                "A great ball that will slow down the enemies for certain time period. ",
				                Resources.Load<Sprite>("Item/blueBall"), 50, ItemID.BlueBall, ItemType.Projectile);
			case GameItem.YELLOWBALL:
				return new Item("Yellow Ball",
				                "This ball allows you to teleport to a certain location. ",
				                Resources.Load<Sprite>("Item/yellowBall"), 50, ItemID.YellowBall, ItemType.Projectile);
			case GameItem.PURPLEBALL:
				return new Item("Purple Ball",
				                "A powerfull ball that is capable of destroying the enemies in 10 seconds. ",
				                Resources.Load<Sprite>("Item/purpleBall"), 50, ItemID.PurpleBall, ItemType.Projectile);
			case GameItem.BASICHEALTH:
				return new Item("Basic Health",
				                "A Simple Medkit that restores " +  Constants.BasicHealthRepletion + " health points" ,
				                Resources.Load<Sprite>("Item/greenHealth"), 3, ItemID.BasicHealth, ItemType.Collectible);
			case GameItem.INTERMEDHEALTH:
				return new Item("Intermediate Health",
				                "A little advanced Medkit that restores " + Constants.InterMedHealthRepletion + " health points" ,
				                Resources.Load<Sprite>("Item/redHealth"), 2, ItemID.InterMedHealth, ItemType.Collectible);

		}
		return null;
	}
}
