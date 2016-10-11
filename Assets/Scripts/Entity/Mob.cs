//Devan Patel
//Applications and Scripting
//Sep.12.2016using UnityEngine;
using UnityEngine;
using System.Collections;
using  UnityEngine.UI;
public class Mob : MonoBehaviour {

	public Body body;
	public EntityBehaviour behaviour;

	#region---- PRIVATE MEMBERS----

	protected float Health;
	protected float MaxHealth;
	protected bool _isDead;
	protected GameController _gameController;
	protected NavMeshAgent _agent;
	protected float _speed;
	protected GameObject _healthBar;

	private float _velocity;
	protected Image _fillImage;
	protected Image _stillImage;
	protected Text _HealthText;
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

	protected void RepleteHealth(float amount)
	{
		if (Health > 0 && Health < MaxHealth)
		{
			Health += amount;
		}

		if (Health > MaxHealth)
		{
			Health = MaxHealth;
		}
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

	protected  void RotateTowards (Transform target) {
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 30);
	}

	/// <summary>
	/// Gets and Set Health
	/// </summary>
	public float GetHealth
	{
		get { return Health; }
		set { this.Health = value; }
	}

	public float GetMaxHealth
	{
		get {return MaxHealth; }
		set {this.MaxHealth = value; }
	}

	protected void UpdateHealthQuad()
	{
		_fillImage.transform.parent.transform.LookAt(Camera.main.transform.position);
		_fillImage.fillAmount = Mathf.SmoothDamp(_fillImage.fillAmount, Health / MaxHealth, ref _velocity, .3f);
		_fillImage.color = Color.Lerp(Color.red, Color.green, (Health / MaxHealth));
		_HealthText.color = _fillImage.color;
		_stillImage.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 50.0f));
		_HealthText.text = "" + (int)(_fillImage.fillAmount * MaxHealth);
	}



}
