using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {


	private Image _image;
	private RectTransform _rectTransform;
	protected GameController _gameController;

	public Color HoverColor;
	public Color RestColor;
	public float HoverSize;
	public float RestSize;

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
		if (_image != null)
		{
			_image.color = HoverColor;
		}

		if (_rectTransform != null)
		{
			_rectTransform.localScale = new Vector3(HoverSize, HoverSize, 1);
		}
	}

	public virtual void OnPointerExit(PointerEventData data)
	{
		if (_image != null)
		{
			_image.color = RestColor;
		}

		if (_rectTransform != null)
		{
			_rectTransform.localScale = new Vector3(RestSize, RestSize, 1);
		}

	}

	public virtual void OnPointerClick(PointerEventData data)
	{
		Debug.Log("Clicked");
	}
}
