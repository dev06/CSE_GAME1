using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {


	protected Image _image;
	private RectTransform _rectTransform;
	protected GameController _gameController;
	protected bool hovering;
	public Sprite HoverSprite;
	public Sprite RestSprite;
	public Color HoverColor;
	public Color RestColor;
	public float HoverSize;
	public float RestSize;

	public ButtonID buttonID;

	void Start ()
	{
		Init();
	}

	protected void Init()
	{
		_image = (GetComponent<Image>() != null) ? GetComponent<Image>() : null;
		_rectTransform = (GetComponent<RectTransform>() != null) ? GetComponent<RectTransform>() : null;
		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		if (_image != null)
		{
			_image.color = RestColor;
		}
	}


	void Update () {

	}


	public virtual void OnPointerEnter(PointerEventData data)
	{
		hovering = true;
		if (_image != null)
		{
			_image.color = HoverColor;
			_image.sprite = HoverSprite;
		}

		if (_rectTransform != null)
		{
			_rectTransform.localScale = new Vector3(HoverSize, HoverSize, 1);
		}
	}

	public virtual void OnPointerExit(PointerEventData data)
	{
		hovering = false;
		if (_image != null)
		{
			_image.color = RestColor;
			_image.sprite = RestSprite;
		}

		if (_rectTransform != null)
		{
			_rectTransform.localScale = new Vector3(RestSize, RestSize, 1);
		}

		GameController.selectedButtonID = ButtonID.NONE;

	}

	public virtual void OnPointerClick(PointerEventData data)
	{

	}
}
