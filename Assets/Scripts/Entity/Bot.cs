using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bot : Mob
{

	// Use this for initialization


	private Transform _targetTransform;
	private GameObject _healthQuad;
	private Transform _healthQuadTransform;
	private Material _heathQuadMaterial;


	private Image _fillImage;
	private Image _stillImage;
	private Text _HealthText;


	void Start()
	{
		Init();
		MaxHealth = Constants.BotMaxHealth;
		Health = MaxHealth;
		_targetTransform = GameObject.FindWithTag("Player").transform;
		_fillImage = transform.FindChild("HealthBar").gameObject.transform.FindChild("FillImage").GetComponent<Image>();
		_stillImage = transform.FindChild("HealthBar").gameObject.transform.FindChild("StillImage").GetComponent<Image>();
		_HealthText = transform.FindChild("HealthBar").gameObject.transform.FindChild("Text").GetComponent<Text>();
	}

	void Update()
	{
		if (_gameController.menuActive == MenuActive.GAME)
		{
			CheckIfIsDead();
			transform.LookAt(_targetTransform);
			transform.Translate(Vector3.forward * Time.deltaTime * Constants.BotMovementSpeed);
			UpdateHealthQuad();
		}
	}

	private float _velocity;
	void UpdateHealthQuad()
	{

		_fillImage.fillAmount = Mathf.SmoothDamp(_fillImage.fillAmount, Health / MaxHealth, ref _velocity, .3f);
		_fillImage.color = Color.Lerp(Color.red, Color.green, (Health / MaxHealth));
		_HealthText.color = _fillImage.color;
		_stillImage.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 50.0f));
		_HealthText.text = "" + (int)(_fillImage.fillAmount * MaxHealth);
	}



	public void CallThis()
	{
		Debug.Log("Called this");
	}





}
