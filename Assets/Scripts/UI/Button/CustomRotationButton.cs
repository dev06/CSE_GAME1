using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CustomRotationButton : ButtonEventHandler {

	// Use this for initialization
	public ButtonID buttonID;
	private float _speed = 50f;
	private Text _text;
	private Image _imageIcon;
	private ToggleMouseButton _toggleMouseButton;
	void Start ()
	{
		Init();
		_text = transform.GetChild(0).GetComponent<Text>();
		_imageIcon = transform.GetChild(1).GetComponent<Image>();
		_toggleMouseButton = transform.parent.transform.parent.transform.FindChild("ToggleMouseControl").GetComponent<ToggleMouseButton>();
	}

	// Update is called once per frame
	void Update ()
	{

		gameObject.SetActive(!_toggleMouseButton.isOn);

		if (HoverSprite != null)
		{
			transform.Rotate(new Vector3(0, 0, -Time.deltaTime * _speed));
			transform.GetChild(0).transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
			transform.GetChild(1).transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
		}

		UpdateRotationInput();
	}

	void UpdateRotationInput()
	{
		if (_gameController != null)
		{
			if (_gameController.controllerProfile == ControllerProfile.TGFH)
			{
				_imageIcon.enabled = true;
			} else {
				_imageIcon.enabled = false;
			}
		}
	}

	public override void OnPointerEnter(PointerEventData data)
	{
		if (_gameController.controllerProfile == ControllerProfile.CUSTOM)
		{
			base.OnPointerEnter(data);
		}
	}

	public override void OnPointerExit(PointerEventData data)
	{
		if (_gameController.controllerProfile == ControllerProfile.CUSTOM)
		{
			base.OnPointerExit(data);
		}
	}




}
