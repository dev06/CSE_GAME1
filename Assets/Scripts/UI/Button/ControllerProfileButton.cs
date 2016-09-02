﻿using UnityEngine;
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
	void Update () {
		RotateHoverSprite();
	}

	void RotateHoverSprite()
	{
		if (HoverSprite != null)
		{
			transform.Rotate(new Vector3(0, 0, -Time.deltaTime * _speed));
			transform.GetChild(0).transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
		}
	}


	public override void OnPointerClick(PointerEventData data)
	{
		if (data.button ==  PointerEventData.InputButton.Left)
		{
			base.OnPointerClick(data);
			_gameController.controllerProfile = this.controllerProfile ;
		}
	}





}
