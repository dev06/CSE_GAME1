using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ControllerProfileButton : ButtonEventHandler {

	// Use this for initialization
	public ControllerProfile controllerProfile;
	void Start () {
		Init();
	}

	// Update is called once per frame
	void Update () {

	}

	public override void OnPointerClick(PointerEventData data)
	{
		base.OnPointerClick(data);
		_gameController.controllerProfile = this.controllerProfile;
	}





}
