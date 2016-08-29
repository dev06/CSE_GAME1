using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Vector3 forward;
	protected GameController _gameController;
	protected float _maxLife;
	protected float _size;
	protected Color _color;
	protected float _velocity;
	protected float _damage;
	protected ParticleSystem _trail;


	void Start ()
	{
		Init();
	}

	public void Init()
	{
		_gameController = FindObjectOfType(typeof(GameController)) as GameController;
		_trail =  (transform.GetChild(0).GetComponent<ParticleSystem>()) ? transform.GetChild(0).GetComponent<ParticleSystem>() : null;
		_maxLife = 3;
		SetTrailColor();

	}

	public void SetTrailColor()
	{
		ParticleSystem.ColorOverLifetimeModule _colorOverLifeTime = _trail.colorOverLifetime;
		if (_trail != null)
		{

			Gradient grad = new Gradient();
			grad.SetKeys(new GradientColorKey[] { new GradientColorKey(_color, 0.0f), new GradientColorKey(_color, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) } );
			_colorOverLifeTime.color = new ParticleSystem.MinMaxGradient(grad);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag != "Player")
		{
			if (col.gameObject.GetComponent<Mob>() != null)
			{
				col.gameObject.GetComponent<Mob>().GetHealth -= _damage;
				Destroy(gameObject);
			}
		}
	}
}
