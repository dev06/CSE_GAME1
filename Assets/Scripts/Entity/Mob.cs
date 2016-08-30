using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

	protected float Health;
	protected float MaxHealth;
	protected bool _isDead;
	void Start () {

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
