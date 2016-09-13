//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CustomKeyButton : ButtonEventHandler
{

	#region------PRIVATE MEMBERS--------
	private float _speed = 75.0f;
	private Text _text;
	private Image _imageIcon;
	#endregion------/PRIVATE MEMBERS--------


	void Start ()
	{
		Init();
		_text = transform.GetChild(0).GetComponent<Text>();
		_imageIcon = transform.GetChild(1).GetComponent<Image>();
		_imageIcon.color = RestColor;
		_text.color = RestColor;
	}

	// Update is called once per frame
	void Update () {

		if (_gameController.controllerProfile == ControllerProfile.CUSTOM)
		{
			if (hovering)
			{
				GameController.selectedButtonID = buttonID;
			}
		}

		if (HoverSprite != null && hovering)
		{
			transform.Rotate(new Vector3(0, 0, -Time.deltaTime * _speed));
			transform.GetChild(0).transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
			transform.GetChild(1).transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));

		}
		UpdateKeyText();
	}

	/// <summary>
	/// Updates the values for keys in the UI.
	/// </summary>
	private void UpdateKeyText()
	{
		if (_gameController != null)
		{
			if (_gameController.controllerProfile == ControllerProfile.WASD)
			{
				_imageIcon.enabled = false;
				_text.text = (buttonID == ButtonID.LEFT) ? "A" : (buttonID == ButtonID.DOWN) ? "S" : (buttonID == ButtonID.RIGHT) ? "D" : "W";
			} else if (_gameController.controllerProfile == ControllerProfile.TGFH)
			{
				_imageIcon.enabled = false;
				_text.text = (buttonID == ButtonID.LEFT) ? "F" : (buttonID == ButtonID.DOWN) ? "G" : (buttonID == ButtonID.RIGHT) ? "H" : "T";

			} else if (_gameController.controllerProfile == ControllerProfile.CUSTOM)
			{
				RegisterCustomKey(GameController.selectedButtonID);

				if (buttonID == ButtonID.LEFT)
				{
					RegisterArrowKey(0);
				} else if (buttonID == ButtonID.RIGHT) {
					RegisterArrowKey(2);
				} else if (buttonID == ButtonID.UP) {
					RegisterArrowKey(1);
				} else if (buttonID == ButtonID.DOWN) {
					RegisterArrowKey(3);
				}
			} else
			{
				_text.text = "";
			}
		}
	}


	/// <summary>
	/// Registers a custom key based on the selected button id.
	/// </summary>
	/// <param name="selectedButtonID"></param>
	private void RegisterCustomKey(ButtonID selectedButtonID)
	{
		foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
		{
			if (key != KeyCode.Mouse0 && key != KeyCode.Mouse1 && key != KeyCode.Mouse2 && key != KeyCode.Mouse3 && key != KeyCode.Mouse4 && key != KeyCode.Mouse5 && key != KeyCode.Mouse6 && key != KeyCode.Escape)
			{
				if (Input.GetKeyDown(key))
				{
					if (key == KeyCode.Backspace)
					{
						if (selectedButtonID == ButtonID.LEFT)
						{
							_gameController.customKey[0] = KeyCode.None;
						} else if (selectedButtonID == ButtonID.UP)
						{
							_gameController.customKey[1] = KeyCode.None;
						} else if (selectedButtonID == ButtonID.RIGHT)
						{
							_gameController.customKey[2] = KeyCode.None;
						} else if (selectedButtonID ==  ButtonID.DOWN) {
							_gameController.customKey[3] = KeyCode.None;
						}
					} else
					{
						if (_gameController.ToggleMouseControl)
						{
							for (int i = _gameController.customKey.Length / 2; i < _gameController.customKey.Length; i++)
							{
								if (key == _gameController.customKey[i])
								{
									MapKey(GameController.selectedButtonID, key);

									_gameController.customKey[i] = KeyCode.None;
								} else
								{
									if (DoesKeyExists(key) == false)
									{
										MapKey(GameController.selectedButtonID, key);
									}
								}
							}
						} else
						{
							if (DoesKeyExists(key) == false)
							{
								MapKey(GameController.selectedButtonID, key);
							}
						}
					}
				}
			}
		}
	}

	/// <summary>
	/// Checks to see if key is already mapped
	/// </summary>
	private bool DoesKeyExists(KeyCode key)
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

	/// <summary>
	/// Maps a key to selected button
	/// </summary>
	/// <param name="selectedButtonID"></param>
	/// <param name="key"></param>
	private void MapKey(ButtonID selectedButtonID, KeyCode key)
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
		} else if (selectedButtonID ==  ButtonID.DOWN)
		{
			_gameController.customKey[3] = key;
		}

	}

	/// <summary>
	/// Registers any arrow keys pressed.
	/// </summary>
	/// <param name="index"></param>
	private void RegisterArrowKey(int index)
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

	/// <summary>
	/// Resets the Arrow key to their default rotations.
	/// </summary>
	private void ResetArrowKey() {
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

	/// <summary>
	/// Overrides the pointer enter from base class.
	/// </summary>
	/// <param name="data"></param>
	public override void OnPointerEnter(PointerEventData data)
	{
		if (_gameController.controllerProfile == ControllerProfile.CUSTOM)
		{
			base.OnPointerEnter(data);

		}
	}
	/// <summary>
	/// Overrides the pointer exit from base class
	/// </summary>
	/// <param name="data"></param>
	public override void OnPointerExit(PointerEventData data)
	{
		if (_gameController.controllerProfile == ControllerProfile.CUSTOM)
		{
			base.OnPointerExit(data);
		}
	}

	/// <summary>
	/// Overrides the pointer click from base class
	/// </summary>
	/// <param name="data"></param>
	public override void OnPointerClick(PointerEventData data)
	{

		if (data.button == PointerEventData.InputButton.Left)
		{
			base.OnPointerClick(data);
			if (_gameController.controllerProfile == ControllerProfile.CUSTOM)
			{

				GameController.selectedButtonID = buttonID;
			}
		}
	}
}



