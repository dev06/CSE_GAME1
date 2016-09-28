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
			_gameController.inventoryManager.AddItem(SwithGameItemToItemID(gameItem));
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
				                Resources.Load<Sprite>("Item/blueBall"), 1, ItemID.BlueBall, ItemType.Projectile);
			case GameItem.YELLOWBALL:
				return new Item("Yellow Ball",
				                "This ball allows you to teleport to a certain location. ",
				                Resources.Load<Sprite>("Item/yellowBall"), 1, ItemID.YellowBall, ItemType.Projectile);
			case GameItem.PURPLEBALL:
				return new Item("Purple Ball",
				                "A powerfull ball that is capable of destroying the enemies in 10 seconds. ",
				                Resources.Load<Sprite>("Item/purpleBall"), 1, ItemID.PurpleBall, ItemType.Projectile);
		}
		return null;
	}
}
