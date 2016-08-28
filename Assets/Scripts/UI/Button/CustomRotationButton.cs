using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CustomRotationButton : ButtonEventHandler {

	// Use this for initialization
	//public ButtonID buttonID;
	private float _speed = 50f;
	private Text _text;
	private Image _imageIcon;
	private ToggleMouseButton _toggleMouseButton;
	private KeyCode keyMapped;
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

		if (_gameController.controllerProfile == ControllerProfile.CUSTOM)
		{
			if (hovering)
			{

				GameController.selectedButtonID = buttonID;
			}
		}


		SetActive(!_toggleMouseButton.isOn);

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



		if (_gameController != null && _toggleMouseButton.isOn == false)
		{
			if (_gameController.controllerProfile == ControllerProfile.TGFH)
			{
				_imageIcon.enabled = true;
				_text.enabled = false;
				ResetArrowKey();
				_toggleMouseButton.mouseUIImage.enabled = false;
			} else if (_gameController.controllerProfile == ControllerProfile.WASD) {
				SetActive(false);
				_imageIcon.enabled = false;
				_toggleMouseButton.mouseUIImage.enabled = true;
			} else {
				RegisterCustomKey(GameController.selectedButtonID);



				_toggleMouseButton.mouseUIImage.enabled = _toggleMouseButton.isOn;
			}
		}


		if (_gameController != null)
		{
			if (_gameController.controllerProfile == ControllerProfile.CUSTOM)
			{
				if (_toggleMouseButton.isOn == false)
				{
					if (buttonID == ButtonID.ROT_LEFT)
					{
						RegisterArrowKey(4);
					} else if (buttonID == ButtonID.ROT_RIGHT) {
						RegisterArrowKey(6);
					} else if (buttonID == ButtonID.ROT_UP) {
						RegisterArrowKey(5);
					} else if (buttonID == ButtonID.ROT_DOWN) {
						RegisterArrowKey(7);
					}
				} else
				{
					_imageIcon.enabled = false;
				}
			}


			_toggleMouseButton.SetActive(_gameController.controllerProfile == ControllerProfile.CUSTOM);
		}
	}

	void RegisterCustomKey(ButtonID selectedButtonID)
	{
		foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
		{
			if (key != KeyCode.Mouse0 && key != KeyCode.Mouse1 && key != KeyCode.Mouse2 && key != KeyCode.Mouse3 && key != KeyCode.Mouse4 && key != KeyCode.Mouse5 && key != KeyCode.Mouse6 && key != KeyCode.Escape)
			{
				if (Input.GetKeyDown(key))
				{

					if (key == KeyCode.Backspace)
					{
						if (selectedButtonID == ButtonID.ROT_LEFT)
						{
							_gameController.customKey[4] = KeyCode.None;
						} else if (selectedButtonID == ButtonID.ROT_UP)
						{
							_gameController.customKey[5] = KeyCode.None;
						} else if (selectedButtonID == ButtonID.ROT_RIGHT)
						{
							_gameController.customKey[6] = KeyCode.None;
						} else if (selectedButtonID ==  ButtonID.ROT_DOWN) {
							_gameController.customKey[7] = KeyCode.None;
						}
					} else
					{

						if (DoesKeyExists(key) == false)
						{

							if (selectedButtonID == ButtonID.ROT_LEFT)
							{
								_gameController.customKey[4] = key;
							}  if (selectedButtonID == ButtonID.ROT_UP)
							{

								_gameController.customKey[5] = key;
							}  if (selectedButtonID == ButtonID.ROT_RIGHT)
							{
								_gameController.customKey[6] = key;
							}  if (selectedButtonID ==  ButtonID.ROT_DOWN)
							{

								_gameController.customKey[7] = key;
							}
						}
					}
				}
			}
		}
	}

	bool DoesKeyExists(KeyCode key)
	{
		bool keyExits = false;
		for (int i = 0; i < _gameController.customKey.Length; i++)
		{
			if (_gameController.customKey[i] == key)
			{
				keyExits =  true;
			}
		}

		return keyExits;

	}

	void RegisterArrowKey(int index)
	{
		if (_gameController.customKey[index] == KeyCode.RightArrow)
		{
			_imageIcon.enabled = true;
			_imageIcon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _imageIcon.transform.rotation.z - 90));
			_text.text = "";
		} else if (_gameController.customKey[index] == KeyCode.LeftArrow) {
			_imageIcon.enabled = true;
			_imageIcon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _imageIcon.transform.rotation.z + 90));
			_text.text = "";
		} else if (_gameController.customKey[index] == KeyCode.UpArrow) {
			_imageIcon.enabled = true;
			_imageIcon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _imageIcon.transform.rotation.z));
			_text.text = "";
		} else if (_gameController.customKey[index] == KeyCode.DownArrow) {
			_imageIcon.enabled = true;
			_imageIcon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _imageIcon.transform.rotation.z + 180));
			_text.text = "";
		} else {
			_imageIcon.enabled = false;
			_text.text = "" + _gameController.customKey[index];
		}
	}

	void ResetArrowKey() {
		if (buttonID == ButtonID.ROT_UP)
		{
			_imageIcon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _imageIcon.transform.rotation.z));
		} else if (buttonID == ButtonID.ROT_DOWN) {
			_imageIcon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _imageIcon.transform.rotation.z + 180));
		} else if (buttonID == ButtonID.ROT_LEFT)
		{
			_imageIcon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _imageIcon.transform.rotation.z + 90));
		} else if (buttonID == ButtonID.ROT_RIGHT) {
			_imageIcon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _imageIcon.transform.rotation.z - 90));
		}
	}


	void SetActive(bool b)
	{

		_text.enabled = _image.enabled = b;


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
			//selectedButtonID = buttonID;
		}
	}





}
