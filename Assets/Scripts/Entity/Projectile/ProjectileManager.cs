using UnityEngine;
using System.Collections;

public class ProjectileManager : MonoBehaviour {

	private GameController _gameController;
	private GameObject _blueBullet;
	private GameObject _yellowBullet;
	private GameObject _purpleBullet;
	private GameObject _shootEffectPrefab;
	private GameObject _activeEntities;

	private GameObject _bulletLeft;
	private GameObject _bulletRight;
	void Start()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		EventManager.OnShoot += EntityOnShoot;
		_blueBullet = Constants.Blue_Bullet;
		_yellowBullet = Constants.Yellow_Bullet;
		_purpleBullet = Constants.Purple_Bullet;
		_shootEffectPrefab = (GameObject)Resources.Load("Prefabs/Particles/ShootEffect");
		_activeEntities = GameObject.Find("ActiveEntities");
		_bulletRight = _gameController.Player.transform.FindChild("BulletRight").gameObject;
		_bulletLeft = _gameController.Player.transform.FindChild("BulletLeft").gameObject;

	}

	void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (EventManager.OnShoot != null)
			{
				EventManager.OnShoot();
			}
		}
	}


	private void EntityOnShoot()
	{
		if (_gameController.inventoryManager.quickItemSelectedSlot != null)
		{
			if (_gameController.inventoryManager.quickItemSelectedSlot.item != null)
			{
				Vector3 _forward = Camera.main.transform.forward;
				switch (_gameController.inventoryManager.quickItemSelectedSlot.item.itemID)
				{
					case ItemID.YellowBall:
					{
						Shoot(_yellowBullet, _bulletLeft.transform.position, _forward, Body.Player);
						Shoot(_yellowBullet, _bulletRight.transform.position, _forward, Body.Player);
						break;
					}
					case ItemID.BlueBall:
					{
						Shoot(_blueBullet, _bulletLeft.transform.position, _forward, Body.Player);
						Shoot(_blueBullet, _bulletRight.transform.position, _forward, Body.Player);
						break;
					}
					case ItemID.PurpleBall:
					{
						Shoot(_purpleBullet, _bulletLeft.transform.position, _forward, Body.Player);
						Shoot(_purpleBullet, _bulletRight.transform.position, _forward, Body.Player);
						break;
					}
				}
			}
		}
	}

	public void Shoot(GameObject prefab, Vector3 _position, Vector3 _forward, Body _owner)
	{
		ShootProjectile(prefab, _position, _forward, _owner);
	}

	private void ShootProjectile(GameObject _prefab, Vector3 _position, Vector3 _forward, Body _owner)
	{
		GameObject _clone = Instantiate(_prefab, _position , Quaternion.identity) as GameObject;
		GameObject _effect = Instantiate(_shootEffectPrefab, _position, Quaternion.identity) as GameObject;
		_clone.GetComponent<Projectile>().forward = _forward;
		_clone.GetComponent<Projectile>().owner = _owner;
		_clone.transform.parent = _activeEntities.transform;
		_effect.transform.parent = _activeEntities.transform;
	}

	void OnDisable()
	{
		EventManager.OnShoot -= EntityOnShoot;
	}
}
