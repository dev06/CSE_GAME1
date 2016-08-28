using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ToggleEventHandler : MonoBehaviour, IPointerClickHandler {

	public bool isOn;
	protected Image _image;
	protected Image _outLineImage;
	protected GameController _gameController;
	public Sprite ActiveSprite;
	public Sprite RestSprite;
	public Color ActiveColor;
	public Color RestColor;

	void OnEnable()
	{
		Init();
		_image.sprite = (isOn) ? ActiveSprite : RestSprite;
		_image.color = (isOn) ? ActiveColor : RestColor;
	}

	void Start () {
		Init();
	}

	protected void Init()
	{
		_image = transform.GetChild(1).GetComponent<Image>();
		_outLineImage = GetComponent<Image>();
		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	// Update is called once per frame
	void Update () {

	}

	public virtual void OnPointerClick(PointerEventData data)
	{
		isOn = !isOn;
		_gameController.ToggleMouseControl = isOn;
		_image.sprite = (isOn) ? ActiveSprite : RestSprite;
		_image.color = (isOn) ? ActiveColor : RestColor;
	}
}
