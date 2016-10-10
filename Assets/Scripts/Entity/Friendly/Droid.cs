using UnityEngine;
using System.Collections;

public class Droid : Mob {

	private  float _angularVelocity;
	private Vector3 _destionationRight;
	private Vector3 _destionationLeft;
	private Transform _target;
	private Transform _bulletLeft;
	private Transform _bulletRight;
	private float _horizontalDistance;
	private float _verticalDistance;
	private float _shootTimer;
	private bool _shot;
	private GameObject[] _enemies;
	private Transform _enemyTarget;
	void Start () {
		Init();
		MaxHealth = Constants.DroidMaxHealth;
		Health = MaxHealth;
		_angularVelocity = 5.0f;
		_speed = Constants.DroidMovementSpeed;
		_agent.speed = _speed;
		_bulletLeft = transform.FindChild("BulletLeft");
		_bulletRight = transform.FindChild("BulletRight");
		_target = _gameController.Player.gameObject.transform;
		_horizontalDistance = 5.0f;
		_verticalDistance = 0.5f;
		_shot = true;
		_enemies = GameObject.FindGameObjectsWithTag("Entity/Enemy");
	}

	void Update ()
	{
		_destionationRight = _target.position + ( _target.right * _horizontalDistance) + ( -_target.forward * _verticalDistance);
		_destionationLeft =  _target.position + ( -_target.right * _horizontalDistance) + ( -_target.forward * _verticalDistance);

		if (_enemyTarget != null)
		{
			_bulletLeft.LookAt(_enemyTarget.transform.position);
			_bulletRight.LookAt(_enemyTarget.transform.position);
			if (CanShoot())
			{
				Shoot();
				_shot = true;
			}

			transform.LookAt(_enemyTarget.transform.position);
		} else
		{
			if (_agent.remainingDistance < 2.0f)
			{
				transform.rotation = Quaternion.Lerp(transform.rotation,  _target.transform.rotation, Time.deltaTime * _angularVelocity);
			}
		}

		Debug.Log(_enemyTarget);
		_agent.SetDestination(GetClosestDestination());



	}

	void FixedUpdate()
	{
		for (int i = 0; i < _enemies.Length; i++)
		{
			if (_enemies[i] != null)
			{
				Mob _mob = _enemies[i].GetComponent<Mob>();
				Debug.Log(_mob.behaviour);
				if (_mob.behaviour == EntityBehaviour.Chase)
				{
					_enemyTarget = _mob.transform;
				} else {
					_enemyTarget = null;
				}
			}
		}
	}

	Vector3 GetClosestDestination()
	{
		if (Vector3.Distance(transform.position, _destionationRight) < Vector3.Distance(transform.position, _destionationLeft))
		{
			return _destionationRight;
		} else {
			return _destionationLeft;
		}
	}


	void Shoot()
	{
		_gameController.projectileManager.Shoot(Constants.Droid_Bullet, _bulletLeft.position, _bulletLeft.forward, Body.Droid);
		_gameController.projectileManager.Shoot(Constants.Droid_Bullet, _bulletRight.position, _bulletRight.forward, Body.Droid);
	}

	bool CanShoot()
	{
		if (_shot)
		{
			if (_shootTimer < 10)
			{
				_shootTimer += Time.deltaTime;
			} else {
				_shootTimer = 0;
			}

			if (_shootTimer > .3f)
			{
				_shootTimer = 0;
				_shot = false;
				return true;
			}
		}
		return false;
	}
}
