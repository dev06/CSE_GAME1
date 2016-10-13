//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;

public class PlayerController : Mob
{

	#region-----PRIVATE MEMBERS-----
	private float _healthRepletionTimer;
	private float _healthRepletionTimerCounter;
	private MeshRenderer _meshRenderer;
	private ParticleSystemRenderer _hoverEffect;
	private ParticleSystemRenderer _shootEffect;

	#endregion-----/PRIVATE MEMBERS-----

	public bool isImmortal;

	void OnEnable()
	{
		EventManager.OnQuickItemChange += ManageSkin;
	}

	void Start()
	{
		Init();
		_healthRepletionTimer = Constants.HealthRepletionTimer;
		_meshRenderer = GetComponent<MeshRenderer>();
		_hoverEffect = (ParticleSystemRenderer)transform.FindChild("HoverEffect").GetComponent<ParticleSystem>().GetComponent<Renderer>();
		_shootEffect = (ParticleSystemRenderer)((GameObject)Resources.Load("Prefabs/Particles/ShootEffect")).GetComponent<ParticleSystem>().GetComponent<Renderer>();

		MaxHealth = Constants.PlayerMaxHealth;
		Health = MaxHealth;

	}


	// Update is called once per frame
	void Update()
	{

		ManageRepletion();
		ManageHoverEffect();
		EndGame();


	}

	private void ManageSkin()
	{
		InventorySlot _currentQuickItemSlot = _gameController.inventoryManager.quickItemSelectedSlot;
		if (_currentQuickItemSlot != null)
		{
			if (_currentQuickItemSlot.item != null)
			{
				Item item = _currentQuickItemSlot.item;
				switch (item.itemID)
				{
					case ItemID.BlueBall:
					{
						_meshRenderer.material = Constants.Character_Blue_Mat;
						_hoverEffect.material = Constants.Character_Blue_Hover_Mat;
						_shootEffect.material = Constants.Character_Blue_Shoot_Mat;

						break;
					} case ItemID.YellowBall:
					{
						_meshRenderer.material = Constants.Character_Yellow_Mat;
						_hoverEffect.material = Constants.Character_Yellow_Hover_Mat;
						_shootEffect.material = Constants.Character_Yellow_Shoot_Mat;

						break;
					}
					case ItemID.PurpleBall:
					{
						_meshRenderer.material = Constants.Character_Purple_Mat;
						_hoverEffect.material = Constants.Character_Purple_Hover_Mat;
						_shootEffect.material = Constants.Character_Purple_Shoot_Mat;
						break;
					}
				}
			}
		}
	}

	private void EndGame()
	{
		if (_gameController.menuActive == MenuActive.GAME)
		{
			if (Health <= 0)
			{
				GameObject.Find("RetryCanvas").GetComponent<Animation>().Play(GameObject.Find("RetryCanvas").GetComponent<Animation>().clip.name);
				_gameController.EnableMenu(MenuActive.RETRY);
			}
		}
	}

	protected override void DoDamage(float damage)
	{
		if (isImmortal == false)
		{
			base.DoDamage(damage);
		}
	}

	private void ManageHoverEffect()
	{
		_hoverEffect.gameObject.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 150.0f));
	}

	private void ManageRepletion()
	{
		if (Health < MaxHealth)
		{
			if (_healthRepletionTimerCounter > _healthRepletionTimer)
			{
				Health += (Constants.HealthRepletionPoints * Time.deltaTime);
			}
		}

		_healthRepletionTimerCounter += Time.deltaTime;
	}

	void OnCollisionEnter(Collision col)
	{

		if (col.gameObject.tag == "Entity/Enemy")
		{
			if (Health > 0) {
				Health -= Constants.BotInitalDamage;
			}


			_healthRepletionTimerCounter = 0;
		}
	}

	/// <summary>
	/// Returns a float value based on the parameter passed.
	/// </summary>
	public float ReturnFloatValue(string value)
	{
		switch (value)
		{
			case "Timer": return _healthRepletionTimer;
			case "Counter": return _healthRepletionTimerCounter;
		}

		return 0;
	}

	void OnDisable()
	{
		EventManager.OnQuickItemChange -= ManageSkin;
	}

}
