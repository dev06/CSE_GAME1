using UnityEngine;
using System.Collections;

public class LargeProjectile : Projectile {

	void Start ()
	{
		Init();

		_velocity = 30;
		_size = Random.Range(.5f, .8f);
		_color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
		_damage = 20;
		transform.localScale = new Vector3(_size, _size, _size);
		GetComponent<MeshRenderer>().materials[0].color = _color;
		GetComponent<Rigidbody>().velocity = _gameController.Player.transform.GetChild(0).transform.forward * _velocity;
		transform.forward = _gameController.Player.transform.GetChild(0).transform.forward;
	}

	void Update ()
	{
		Destroy(gameObject, _maxLife);
	}
}
