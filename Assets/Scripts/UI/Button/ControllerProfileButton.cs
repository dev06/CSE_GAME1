//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ControllerProfileButton : ButtonEventHandler {

	// Use this for initialization
	public ControllerProfile controllerProfile;
	private float _speed = 75.0f;

	void Start () {
		Init();
		transform.GetChild(0).GetComponent<Text>().color = RestColor;
	}

	// Update is called once per frame
	void Update ()
	{
		RotateHoverSprite();
	}

	/// <summary>
	/// Rotates the Hover sprite
	/// </summary>
	private void RotateHoverSprite()
	{
		if (HoverSprite != null)
		{
			transform.Rotate(new Vector3(0, 0, -Time.deltaTime * _speed));
			transform.GetChild(0).transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
		}
	}

	/// <summary>
	/// Overrides the on pointer click from the base class
	/// </summary>
	/// <param name="data"></param>
	public override void OnPointerClick(PointerEventData data)
	{
		if (data.button ==  PointerEventData.InputButton.Left)
		{
			base.OnPointerClick(data);
			_gameController.controllerProfile = this.controllerProfile ;
		}
	}
}
