using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CustomKeyButton : ButtonEventHandler {

	// Use this for initialization
	private static ButtonID selectedButtonID;
	private float _speed = 75.0f;
	private Text _text;
	public ButtonID buttonID;
	void Start () {
		Init();
		_text = transform.GetChild(0).GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {

		if (_gameController.controllerProfile == ControllerProfile.CUSTOM)
		{
			if (hovering)
			{

				selectedButtonID = buttonID;
			}
		}

		if (HoverSprite != null && hovering)
		{
			transform.Rotate(new Vector3(0, 0, -Time.deltaTime * _speed));
			transform.GetChild(0).transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
		}
		UpdateKeyText();
	}

	void UpdateKeyText()
	{
		if (_gameController != null)
		{
			if (_gameController.controllerProfile == ControllerProfile.WASD)
			{
				_text.text = (buttonID == ButtonID.LEFT) ? "A" : (buttonID == ButtonID.DOWN) ? "S" : (buttonID == ButtonID.RIGHT) ? "D" : "W";
			} else if (_gameController.controllerProfile == ControllerProfile.TGFH)
			{
				_text.text = (buttonID == ButtonID.LEFT) ? "F" : (buttonID == ButtonID.DOWN) ? "G" : (buttonID == ButtonID.RIGHT) ? "H" : "T";
			} else if (_gameController.controllerProfile == ControllerProfile.CUSTOM)
			{
				RegisterCustomKey();
				_text.text = (buttonID == ButtonID.LEFT) ? "" + _gameController.customKey[0] : (buttonID == ButtonID.DOWN) ? "" + _gameController.customKey[3] : (buttonID == ButtonID.RIGHT) ? "" + _gameController.customKey[2] : "" + _gameController.customKey[1];
			} else
			{
				_text.text = "";
			}
		}
	}



	void RegisterCustomKey()
	{
		foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
		{
			if (key != KeyCode.Mouse0 && key != KeyCode.Mouse1 && key != KeyCode.Mouse2 && key != KeyCode.Mouse3 && key != KeyCode.Mouse4 && key != KeyCode.Mouse5 && key != KeyCode.Mouse6 && key != KeyCode.Escape)
			{
				if (Input.GetKeyDown(key))
				{

					if (DoesKeyExists(key) == false)
					{
						if (selectedButtonID == ButtonID.LEFT)
						{
							_gameController.customKey[0] = key;
						} else if (selectedButtonID == ButtonID.UP)
						{
							_gameController.customKey[1] = key;
						} else if (selectedButtonID == ButtonID.RIGHT)
						{
							_gameController.customKey[2] = key;
						} else if (selectedButtonID ==  ButtonID.DOWN) {
							_gameController.customKey[3] = key;
						}
					}
				}
			}
		}
	}

	bool DoesKeyExists(KeyCode key)
	{
		for (int i = 0; i < _gameController.customKey.Length; i++)
		{
			if (_gameController.customKey[i] == key)
			{
				return true;
			}
		}
		return false;
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


	public override void OnPointerClick(PointerEventData data)
	{
		base.OnPointerClick(data);
		if (_gameController.controllerProfile == ControllerProfile.CUSTOM)
		{
			selectedButtonID = buttonID;
		}


	}
}


public enum ButtonID
{
	LEFT,
	RIGHT,
	UP,
	DOWN,

	ROT_LEFT,
	ROT_RIGHT,
	ROT_UP,
	ROT_DOWN,
}
