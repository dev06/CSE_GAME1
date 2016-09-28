//Devan Patel
//Applications and Scripting
//Sep.12.2016using UnityEngine;
using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	#region----PRIVATE MEMBERS-----
	public Vector3 forward;
	protected GameController _gameController;
	protected float _maxLife;
	protected float _size;
	protected Color _color;
	protected float _velocity;
	protected float _damage;
	protected ParticleSystem _trail;
	protected GameObject _effect;
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
		GetComponent<Rigidbody>().velocity = forward * 50;
		transform.GetChild(0).transform.forward = forward;

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
		if (col.gameObject.tag != "Player")
		{
			if (col.gameObject.GetComponent<Mob>() != null)
			{
				col.gameObject.SendMessage("DoDamage", _damage);

			} else {
				GameObject effect_clone = Instantiate(_effect, transform.position, Quaternion.identity) as GameObject;
				effect_clone.transform.parent = _gameController.activeEntities.transform;

			}
			Destroy(gameObject);
		}
	}
}
