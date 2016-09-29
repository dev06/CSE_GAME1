﻿//Devan Patel
//Applications and Scripting
//Sep.12.2016using UnityEngine;
using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

	#region---- PRIVATE MEMBERS----
	protected float Health;
	protected float MaxHealth;
	protected bool _isDead;
	protected GameController _gameController;
	protected NavMeshAgent _agent;
	#endregion----/PRIVATE MEMBERS----


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
		_agent = (GetComponent<NavMeshAgent>() != null) ? transform.GetComponent<NavMeshAgent>() : null;
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
	/// Does damage to the entity based on the damage
	/// </summary>
	/// <param name="damage"></param>
	protected void DoDamage(float damage)
	{
		if (Health > 0)
		{
			Health -= damage;
		}
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
