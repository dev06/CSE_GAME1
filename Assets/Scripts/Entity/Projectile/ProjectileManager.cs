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
		_blueBullet = (GameObject)Resources.Load("Prefabs/Projectile/BlueBullet");
		_yellowBullet = (GameObject)Resources.Load("Prefabs/Projectile/YellowBullet");
		_purpleBullet = (GameObject)Resources.Load("Prefabs/Projectile/PurpleBullet");
		_shootEffectPrefab = (GameObject)Resources.Load("Prefabs/Particles/ShootEffect");
		_activeEntities = GameObject.Find("ActiveEntities");
		_bulletRight = GameObject.Find("BulletRight");
		_bulletLeft = GameObject.Find("BulletLeft");

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
				switch (_gameController.inventoryManager.quickItemSelectedSlot.item.itemID)
				{
					case ItemID.YellowBall:
					{
						Shoot(_yellowBullet);
						break;
					}
					case ItemID.BlueBall:
					{
						Shoot(_blueBullet);
						break;
					}
					case ItemID.PurpleBall:
					{
						Shoot(_purpleBullet);
						break;
					}
				}
			}
		}
	}

	private void Shoot(GameObject prefab)
	{
		ShootProjectile(prefab, _bulletRight.transform.position);
		ShootProjectile(prefab, _bulletLeft.transform.position);
	}

	private void ShootProjectile(GameObject _prefab, Vector3 _position)
	{

		GameObject _clone = Instantiate(_prefab, _position , Quaternion.identity) as GameObject;


		GameObject _effect = Instantiate(_shootEffectPrefab, _position, Quaternion.identity) as GameObject;

		_clone.GetComponent<Projectile>().forward = Camera.main.transform.forward;
		_clone.transform.parent = _activeEntities.transform;
		_effect.transform.parent = _activeEntities.transform;



	}

	void OnDisable()
	{
		EventManager.OnShoot -= EntityOnShoot;
	}
}
