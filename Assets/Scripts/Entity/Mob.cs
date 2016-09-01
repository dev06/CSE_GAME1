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
	/// <summary>
	/// Inits the components
	/// </summary>
	public void Init()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

	/// <summary>
	/// Checks to see if the gameobject is dead
	/// </summary>
	protected void CheckIfIsDead()
	{
		if (_isDead)
		{
			Destroy(gameObject);
		}
		_isDead = GetHealth <= 0;

	}
	/// <summary>
	/// Gets and Set Health
	/// </summary>
	public float GetHealth
	{
		get { return Health; }
		set { this.Health = value; }
	}



}
