using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

	protected float Health;
	protected float MaxHealth;
	protected bool _isDead;
	protected GameController _gameController;
	void Start ()
	{
		Init();
	}

	public void Init()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}


	void Update ()
	{

	}

	protected void CheckIfIsDead()
	{
		if (_isDead)
		{
			Destroy(gameObject);
		}
		_isDead = GetHealth <= 0;

	}

	public float GetHealth
	{
		get { return Health; }
		set { this.Health = value; }
	}



}
