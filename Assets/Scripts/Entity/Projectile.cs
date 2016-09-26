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
		//_trail =  (transform.GetChild(0).GetComponent<ParticleSystem>()) ? transform.GetChild(0).GetComponent<ParticleSystem>() : null;
		_maxLife = 3;
		SetTrailColor();
		GetComponent<Rigidbody>().velocity = forward * 50;
		transform.GetChild(0).transform.forward = forward;

	}

	void Update()
	{
		Destroy(gameObject, _maxLife);
	}


	/// <summary>
	/// Sets the trail color for particles
	/// </summary>
	public void SetTrailColor()
	{
		// if (_trail != null)
		// {

		// 	ParticleSystem.ColorOverLifetimeModule _colorOverLifeTime = _trail.colorOverLifetime;
		// 	Gradient grad = new Gradient();
		// 	grad.SetKeys(new GradientColorKey[] { new GradientColorKey(_color, 0.0f), new GradientColorKey(_color, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) } );
		// 	_colorOverLifeTime.color = new ParticleSystem.MinMaxGradient(grad);
		// }
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
