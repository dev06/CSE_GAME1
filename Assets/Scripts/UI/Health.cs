using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Health : MonoBehaviour {


	private GameController _gameSceneManager;
	private PlayerController _target;
	private float _value;
	private Image _fill;
	private Image _still;
	private Text _text;
	private float _velocity;

	void Start ()
	{
		Init();
	}

	void Init()
	{
		_gameSceneManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		_target = _gameSceneManager.Player.GetComponent<PlayerController>();
		_fill = transform.FindChild("FillImage").GetComponent<Image>();
		_still = transform.GetChild(0).FindChild("StillImage").GetComponent<Image>();
		_text = transform.GetChild(0).FindChild("Text").GetComponent<Text>();

	}

	void Update ()
	{
		_value = _target.GetHealth;
		_text.text = "" + _value;
		_fill.fillAmount = Mathf.SmoothDamp(_fill.fillAmount, _value / 100.0f, ref _velocity, .3f);

	}
}
