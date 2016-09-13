//Devan Patel
//Applications and Scripting
//Sep.12.2016using UnityEngine;
using UnityEngine;
using System.Collections;

public class LargeProjectile : Projectile {

	void Start ()
	{
		Init();
		_velocity = 30;
		_size = Random.Range(.2f, .4f);
		_color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
		_damage = Constants.LargeProjectileDamage;
		transform.localScale = new Vector3(_size, _size, _size);
		GetComponent<MeshRenderer>().materials[0].color = _color;
		GetComponent<Rigidbody>().velocity = forward * _velocity;
		transform.forward = forward;
		SetTrailColor();
	}

	void Update ()
	{
		Destroy(gameObject, _maxLife);
	}



}
