//Devan Patel
//Applications and Scripting
//Sep.12.2016using UnityEngine;
using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	[HideInInspector]
	public Body owner;
	[HideInInspector]
	public Vector3 forward;
	public Transform target;
	#region----PRIVATE MEMBERS-----
	protected GameController _gameController;
	protected float _maxLife;
	protected float _size;
	protected Color _color;
	protected float _velocity;
	protected ParticleSystem _trail;
	protected GameObject _effect;
	protected float _damage;
	#endregion----PRIVATE MEMBERS-----



	void Start ()
	{
		Init();
	}

	/// <summary>
	/// Init all the components
	/// </summary>
	public void Init()
	{
		_effect = (GameObject)Resources.Load("Prefabs/Particles/Effect");
		_gameController = FindObjectOfType(typeof(GameController)) as GameController;
		_maxLife = 3;

		if (transform.childCount > 0)
		{
			transform.GetChild(0).transform.forward = forward;
		}
	}

	protected void MoveTo(Vector3 destination, float velocity)
	{
		Vector3.MoveTowards(transform.position, destination, velocity);
	}

}
