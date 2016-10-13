using UnityEngine;
using System.Collections;

public class GuardEnemyProjectile : Projectile {


	void Start ()
	{
		Init();
		GetComponent<Rigidbody>().velocity = forward * 50;
		_damage = Constants.GuardEnemy_ProjectileDamage;
	}

	void Update()
	{
		Destroy(gameObject, _maxLife);
	}

	void FixedUpdate()
	{
		float speed = Random.Range(40.0f, 50.0f);
		transform.Rotate(new Vector3(Time.deltaTime * Time.time * speed,  Time.deltaTime * Time.time * speed, Time.deltaTime * Time.time * speed));
	}


	void OnTriggerEnter(Collider col)
	{

		if (col.gameObject.GetComponent<Mob>() != null)
		{

			if (col.gameObject.GetComponent<Mob>().body != owner)
			{
				col.gameObject.SendMessage("DoDamage", _damage);
				Destroy(gameObject);
			}

		} else {
			GameObject effect_clone = Instantiate(_effect, transform.position, Quaternion.identity) as GameObject;
			effect_clone.transform.parent = _gameController.activeEntities.transform;
		}
	}
}
