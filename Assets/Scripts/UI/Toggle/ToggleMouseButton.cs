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

	// Update is called once per frame
	void Update ()
	{

	}

	public void SetActive(bool b)
	{
		_outLineImage.enabled = b;
		transform.GetChild(0).gameObject.SetActive(b);
		transform.GetChild(1).gameObject.SetActive(b);

	}


	public override void OnPointerClick(PointerEventData data)
	{
		if (_gameController.controllerProfile == ControllerProfile.CUSTOM) {
			base.OnPointerClick(data);
			mouseUIImage.enabled = isOn;
		}
	}



}
