//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ToggleMouseButton : ToggleEventHandler
{

	public Image mouseUIImage;

	void Start () {
		Init();
		mouseUIImage = transform.FindChild("MouseImage").GetComponent<Image>();
	}

	/// <summary>
	/// Sets the children active to bool
	/// </summary>
	/// <param name="b"></param>
	public void SetActive(bool b)
	{
		_outLineImage.enabled = b;
		transform.GetChild(0).gameObject.SetActive(b);
		transform.GetChild(1).gameObject.SetActive(b);

	}

	/// <summary>
	/// overrides the on pointer click
	/// </summary>
	/// <param name="data"></param>
	public override void OnPointerClick(PointerEventData data)
	{
		if (_gameController.controllerProfile == ControllerProfile.CUSTOM) {
			base.OnPointerClick(data);
			mouseUIImage.enabled = isOn;
		}
	}



}
