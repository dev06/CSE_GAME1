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
		MaxHealth = 500;
		Health = MaxHealth;
		_targetTransform = GameObject.FindWithTag("Player").transform;
		_fillImage = transform.FindChild("HealthBar").gameObject.transform.FindChild("FillImage").GetComponent<Image>();
		_stillImage = transform.FindChild("HealthBar").gameObject.transform.FindChild("StillImage").GetComponent<Image>();
		_HealthText = transform.FindChild("HealthBar").gameObject.transform.FindChild("Text").GetComponent<Text>();
	}

	void Update()
	{
		CheckIfIsDead();
		transform.LookAt(_targetTransform);
		transform.Translate(Vector3.forward * Time.deltaTime * 1.0f);
		UpdateHealthQuad();
	}

	private float _velocity;
	void UpdateHealthQuad()
	{
		//_healthQuad.transform.localScale = Vector3.Lerp(_healthQuad.transform.localScale, new Vector3((Health / MaxHealth) , 1, 1), Time.deltaTime * 2.0f);
		//_heathQuadMaterial.SetColor("_EmissionColor", Color.Lerp(Color.red, Color.green, (Health / MaxHealth)));

		_fillImage.fillAmount = Mathf.SmoothDamp(_fillImage.fillAmount, Health / MaxHealth, ref _velocity, Time.deltaTime * 2.0f);
		_fillImage.color = Color.Lerp(Color.red, Color.green, (Health / MaxHealth));
		_HealthText.color = _fillImage.color;
		_stillImage.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 50.0f));
		_HealthText.text = "" + (int)(_fillImage.fillAmount * Health);
	}





}
