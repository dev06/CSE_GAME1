using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	protected GameController _gameController;
	protected float _maxLife;
	protected float _size;
	protected Color _color;
	protected float _velocity;
	protected float _damage;

	void Start ()
	{
		Init();
	}

	public void Init()
	{
		_gameController = FindObjectOfType(typeof(GameController)) as GameController;
		_maxLife = 3;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag != "Player")
		{
			if (col.gameObject.GetComponent<Mob>() != null)
			{
				col.gameObject.GetComponent<Mob>().GetHealth -= _damage;
			}
		}
	}
}
