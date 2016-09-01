using UnityEngine;
using System.Collections;

public class SmallProjectile : Projectile {

	// Use this for initialization
	void Start () {
		Init();

		_velocity = 50;
		_size = Random.Range(.1f, .2f);
		_color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
		_damage = 10;
		transform.localScale = new Vector3(_size, _size, _size);
		GetComponent<MeshRenderer>().materials[0].color = _color;
		GetComponent<Rigidbody>().velocity = forward * _velocity;
		transform.forward = forward;
		SetTrailColor();

	}

	// Update is called once per frame
	void Update ()
	{
		Destroy(gameObject, _maxLife);
	}

}
