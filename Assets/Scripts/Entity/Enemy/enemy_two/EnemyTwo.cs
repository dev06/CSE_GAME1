using UnityEngine;
using System.Collections;

public class EnemyTwo : Mob {

	private GameObject _hover;
	private float _shootTimer;
	private bool _shot;
	private GameObject _bulletLeft;
	private GameObject _bulletRight;


	void Start ()
	{
		Init();
		MaxHealth = Constants.BotMaxHealth;
		Health = MaxHealth;
		_hover = transform.FindChild("HoverEffect").gameObject;
		_bulletRight = transform.FindChild("BulletRight").gameObject;
		_bulletLeft = transform.FindChild("BulletLeft").gameObject;
		_shot = true;
	}

	void Update ()
	{
		ManageHoverEffect();
		Move();
	}

	private void Move()
	{
		ManageInitPoints();
		if (Vector3.Distance(transform.position, _gameController.Player.transform.position) < 20)
		{
			if (Vector3.Distance(transform.position, _gameController.Player.transform.position) < 10)
			{
				if (CanShoot())
				{
					_gameController.projectileManager.Shoot(Constants.Enemy_Two_Bullet, _bulletRight.transform.position, _bulletRight.transform.forward, body);
					_gameController.projectileManager.Shoot(Constants.Enemy_Two_Bullet, _bulletLeft.transform.position, _bulletLeft.transform.forward, body);
					_shot = true;
				}
				_gameController.Player.gameObject.SendMessage("DoDamage", Time.deltaTime * Constants.PatrolEnemyDamage);
			}
			_agent.SetDestination(_gameController.Player.transform.position);
			RotateTowards(_gameController.Player.transform);
		}
	}

	private void ManageInitPoints()
	{
		_bulletRight.transform.forward = transform.forward;
		_bulletLeft.transform.forward =  transform.forward;

	}

	private bool CanShoot()
	{
		if (_shot)
		{
			_shootTimer += Time.deltaTime;
		}

		if (_shootTimer > Constants.GuardEnemyShootDelay)
		{
			_shootTimer = 0;
			_shot = false;
			return true;
		}

		return false;
	}



	private void ManageHoverEffect()
	{
		_hover.transform.Rotate(new Vector3(0, 0,  Time.deltaTime * 150.0f));
	}
}
